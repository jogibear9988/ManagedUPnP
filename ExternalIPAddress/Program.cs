using System;
using System.Collections.Generic;
using ManagedUPnP;
using ManagedUPnP.Descriptions;

namespace UPnPConsoleTest
{
    /// <summary>
    /// Runs a console program to find the external IP address.
    /// </summary>
    class Program
    {
        #region Enumerations

        /// <summary>
        /// The type of an argument switch.
        /// </summary>
        enum SwitchType
        {
            /// <summary>
            /// The switch is on.
            /// </summary>
            On,

            /// <summary>
            /// The switch is off.
            /// </summary>
            Off,

            /// <summary>
            /// Not a switch argument.
            /// </summary>
            NotASwitch,

            /// <summary>
            /// The argument was invalid.
            /// </summary>
            NA
        }

        #endregion

        #region Locals

        /// <summary>
        /// State variable for whether to check for GeTExternalIPAddress action or not.
        /// </summary>
        static bool mbCheckForAction = false;

        /// <summary>
        /// State variable to determine to log UPnP information.
        /// </summary>
        static bool mbVerboseUPnPLog = false;

        /// <summary>
        /// State variable for length of search timeout in milliseconds.
        /// </summary>
        static int miTimeout = 2000;

        #endregion

        #region Static Methods

        /// <summary>
        /// Writes the header to the console.
        /// </summary>
        static void DisplayHeader()
        {
            Console.WriteLine(
                "UPnP External IP Address Resolver\n" +
                "  Web: http://managedupnp.codeplex.com/\n");
        }

        /// <summary>
        /// Writes the help to the console.
        /// </summary>
        static void DisplayHelp()
        {
            Console.WriteLine(
                "ExternalIPAddress [/T:timeout][/C][/V]\n" +
                "\n" +
                "  /T:timeout    Timeout in milliseconds (-1 for indefinite).\n" +
                "  /C            Checks for action before executing.\n" +
                "  /V            Outputs verbose UPnP log.\n");
        }

        /// <summary>
        /// Performs the search of the services.
        /// </summary>
        /// <param name="timeout">The timeout in milliseconds for the search.</param>
        /// <param name="checkForAction">True to check for action before execution.</param>
        /// <param name="verboseLog">True to write verbose UPnP log information.</param>
        static void Search(int timeout, bool checkForAction, bool verboseLog)
        {
            // Write the settings
            Console.WriteLine("Searching for UPnP services...");
            Console.WriteLine(string.Format("  Timeout: {0}", (timeout == -1 ? "Indefinite" : string.Format("{0} ms", timeout))));
            Console.WriteLine(string.Format("  Check service for action: {0}", checkForAction));
            Console.WriteLine(string.Format("  Verbose UPnP Log: {0}", verboseLog));
            Console.WriteLine();

            if (verboseLog)
            {
                Console.WriteLine("Enabling verbose log...");
                ManagedUPnP.Logging.Enabled = true;
                ManagedUPnP.Logging.LogLines += new LogLinesEventHandler(Logging_LogLines);
                Console.WriteLine();
            }

            const string csGetExternalIPAddressAction = "GetExternalIPAddress";

            // Used in cache two services on a device return the same IP information
            HashSet<string> lhsDone = new HashSet<string>();

            // Find the services
            bool lbCompleted;
            Services lsServices = Discovery.FindServices(
               null,
               timeout, 0,
               out lbCompleted,
               AddressFamilyFlags.IPvBoth);

            Console.WriteLine(string.Format("Found {0} services, Search Completed: {1}", lsServices.Count, lbCompleted));
            Console.WriteLine();

            // Scan the services and write the IP address
            foreach (Service lsService in lsServices)
            {
                try
                {
                    // Check for action if required
                    if (!checkForAction || lsService.Description().Actions.ContainsKey(csGetExternalIPAddressAction))
                    {
                        // Invoke the action
                        string lsIP;
                        lsService.InvokeAction<string>(csGetExternalIPAddressAction, out lsIP);

                        // Build the info string
                        string lsInfo =
                            String.Format(
                                "{0} => {1} - IP Address: {2}",
                                lsService.Device.RootDevice.FriendlyName,
                                lsService.Device.FriendlyName,
                                lsIP);

                        // If we havnt alread reported this info
                        if(!lhsDone.Contains(lsInfo))
                        {
                            // Report and add it
                            Console.WriteLine(lsInfo);
                            lhsDone.Add(lsInfo);
                        }
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Occurs when a Managed UPnP log entry is raised.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="a">The event arguments.</param>
        static void Logging_LogLines(object sender, LogLinesEventArgs a)
        {
            string lsLineStart = "[UPnP] " + new String(' ', a.Indent * 4);
            Console.WriteLine(lsLineStart + a.Lines.Replace("\r\n", "\r\n" + lsLineStart));
        }

        /// <summary>
        /// Decodes an argument.
        /// </summary>
        /// <param name="arg">The argument string to decode.</param>
        /// <param name="name">Contains the name of the switch on return.</param>
        /// <param name="value">Contains the value of the argument on return.</param>
        /// <param name="switchType">Contains the type of switch on return.</param>
        /// <returns>True if the argument was valid false otherwise.</returns>
        static bool DecodeArgument(string arg, out string name, out string value, out SwitchType switchType)
        {
            bool lbValid = false;

            name = string.Empty;
            value = string.Empty;
            switchType = SwitchType.NA;

            // Check for switch
            if (arg.StartsWith("/"))
            {
                // If the switch is valid
                if (arg.Length > 1)
                {
                    // Determine switch type
                    switch(arg[1])
                    {
                        default: switchType = SwitchType.On; break;
                        case '-': { arg = "/" + arg.Substring(2); switchType = SwitchType.Off; break; }
                        case '+': { arg = "/" + arg.Substring(2); switchType = SwitchType.On; break; }
                    }

                    // Check for value
                    int liPos = arg.IndexOf(":");
                    value = string.Empty;

                    // Get name if no value
                    if (liPos == -1)
                        name = arg.Substring(1).ToUpper();
                    else
                    {
                        // Get name and value
                        name = arg.Substring(1, liPos - 1).ToUpper();
                        value = arg.Substring(liPos + 1);
                    }

                    lbValid = true;
                }
            }
            else
            {
                // Use argument as value only
                value = arg;
                lbValid = true;
            }

            return lbValid;
        }

        /// <summary>
        /// Processes a command-line argument.
        /// </summary>
        /// <param name="argsProcessed">The switch argument names already processed.</param>
        /// <param name="arg">The argument string to process.</param>
        /// <returns>True if argument was valid, false otherwise.</returns>
        static bool ProcessArg(HashSet<string> argsProcessed, string arg)
        {
            bool lbValid = false;

            string lsName, lsValue;
            SwitchType lstType;

            if (!DecodeArgument(arg, out lsName, out lsValue, out lstType))
                lbValid = false;
            else
            {
                if (lstType == SwitchType.On)
                {
                    if (argsProcessed.Contains(lsName)) lbValid = false;
                    argsProcessed.Add(lsName);

                    switch (lsName)
                    {
                        case "T":
                            lbValid = Int32.TryParse(lsValue, out miTimeout);
                            break;

                        case "C":
                            mbCheckForAction = true;
                            lbValid = true;
                            break;

                        case "V":
                            mbVerboseUPnPLog = true;
                            lbValid = true;
                            break;
                    }
                }
                else
                    lbValid = false;
            }

            return lbValid;
        }

        /// <summary>
        /// Main execution method.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        static void Main(string[] args)
        {
            try
            {
                DisplayHeader();

                // Process the arguments
                HashSet<string> lhsArgs = new HashSet<string>();
                foreach(string lsArg in args)
                    if (!ProcessArg(lhsArgs, lsArg))
                    {
                        // If any arguments are invalid, then display the help
                        DisplayHelp();
                        return;
                    }

                // Perform the search
                Search(miTimeout, mbCheckForAction, mbVerboseUPnPLog);
            }
            finally
            {
                // Wait for key (this is for debugging only really)
                Console.WriteLine("\nPress any key to continue.\n");
                Console.ReadKey();
            }
        }

        #endregion
    }
}
