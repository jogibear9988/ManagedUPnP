//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_WindowsFirewall

using System;
using System.Windows.Forms;

namespace ManagedUPnP
{
    /// <summary>
    /// Encapsulates a static class to check the Windows firewall
    /// to ensure that it is set to allow UPnP and SSDP network
    /// traffic through. Note that on Windows Vista and above this
    /// method of getting the state will work, but setting will fail,
    /// see the CheckUPnPFirewallRules method.
    /// </summary>
    public static class WindowsFirewall
    {
        #region Private Enumerations

        /// <summary>
        /// Encapsulates the Scope property of the NetFwTypeLib type library.
        /// </summary>
        private enum Scope
        {
            /// <summary>
            /// All.
            /// </summary>
            All = 0,

            /// <summary>
            /// Local Subnet.
            /// </summary>
            LocalSubnet = 1,

            /// <summary>
            /// Custom.
            /// </summary>
            Custom = 2,

            /// <summary>
            /// Max.
            /// </summary>
            Max = 3,
        }

        /// <summary>
        /// Encapsulates the ServiceType property of the NetFwTypeLib type library.
        /// </summary>
        private enum ServiceType
        {
            /// <summary>
            /// File and print.
            /// </summary>
            FileAndPrint = 0,

            /// <summary>
            /// UPnP.
            /// </summary>
            UPnP = 1,

            /// <summary>
            /// Remote Desktop.
            /// </summary>
            RemoteDesktop = 2,

            /// <summary>
            /// None.
            /// </summary>
            None = 3,

            /// <summary>
            /// Max.
            /// </summary>
            Max = 4
        }

        #endregion

        #region Public Enumerations

        /// <summary>
        /// Encapsulates the status of the Windows Firewall and
        /// UPnP Framework exception.
        /// </summary>
        public enum Status
        {
            /// <summary>
            /// Status Uninitialised.
            /// </summary>
            None,

            /// <summary>
            /// The windows firewall is not enabled.
            /// </summary>
            FirewallNotEnabled,

            /// <summary>
            /// Exceptions are not allowed.
            /// </summary>
            ExceptionsNotAllowed,

            /// <summary>
            /// The firewall is open to UPnP traffic.
            /// </summary>
            Open,

            /// <summary>
            /// The firewall is closed to UPnP traffic.
            /// </summary>
            Closed,

            /// <summary>
            /// The firewall is in an unknown state.
            /// </summary>
            Unknown
        }

        /// <summary>
        /// Encapsulates the Operating System Version
        /// </summary>
        [Flags]
        public enum OperatingSystem
        {
            /// <summary>
            /// Unknown or invalid.
            /// </summary>
            Unknown = 0x0,


            /// <summary>
            /// 32-Bit version of windows.
            /// </summary>
            Windows_32Bit_x86 = 0x1,

            /// <summary>
            /// 64-Bit version of windows.
            /// </summary>
            Windows_64Bit_x64 = 0x2,


            /// <summary>
            /// Win32s.
            /// </summary>
            WindowsWin32s = 0x10000,

            /// <summary>
            /// Windows 95.
            /// </summary>
            Windows95 = 0x20000,

            /// <summary>
            /// Windows 98.
            /// </summary>
            Windows98 = 0x40000,

            /// <summary>
            /// Windows ME.
            /// </summary>
            WindowsME = 0x80000,

            /// <summary>
            /// Windows NT.
            /// </summary>
            WindowsNT = 0x100000,

            /// <summary>
            /// Windows 2000.
            /// </summary>
            Windows2000 = 0x200000,

            /// <summary>
            /// Windows XP.
            /// </summary>
            WindowsXP = 0x400000,

            /// <summary>
            /// Windows CE.
            /// </summary>
            WindowsCE = 0x800000,

            /// <summary>
            /// Windows 2003.
            /// </summary>
            Windows2003 = 0x1000000,

            /// <summary>
            /// Windows Vista.
            /// </summary>
            WindowsVista = 0x2000000,

            /// <summary>
            /// Windows 7.
            /// </summary>
            Windows7 = 0x4000000,


            /// <summary>
            /// Windows 98 SE.
            /// </summary>
            Windows98_SE = 0x4,

            /// <summary>
            /// Windows NT 3.5.1.
            /// </summary>
            WindowsNT_351 = 0x8,

            /// <summary>
            /// Windows NT 4.0.
            /// </summary>
            WindowsNT_40 = 0x10,

            /// <summary>
            /// Windows 2000 Service Pack 1.
            /// </summary>
            Windows2000_SP1 = 0x20,

            /// <summary>
            /// Windows 2000 Service Pack 2.
            /// </summary>
            Windows2000_SP2 = 0x40,

            /// <summary>
            /// Windows 2000 Service Pack 3.
            /// </summary>
            Windows2000_SP3 = 0x80,

            /// <summary>
            /// Windows 2000 Service Pack 4.
            /// </summary>
            Windows2000_SP4 = 0x100,

            /// <summary>
            /// Windows XP Service Pack 1.
            /// </summary>
            WindowsXP_SP1 = 0x200,

            /// <summary>
            /// Windows XP Service Pack 2.
            /// </summary>
            WindowsXP_SP2 = 0x400,

            /// <summary>
            /// Windows XP Service Pack 3.
            /// </summary>
            WindowsXP_SP3 = 0x800,

            /// <summary>
            /// Windows Vista Service Pack 1.
            /// </summary>
            WindowsVista_SP1 = 0x1000,

            /// <summary>
            /// Windows Vista Service Pack 2.
            /// </summary>
            WindowsVista_SP2 = 0x2000,

            /// <summary>
            /// Windows 7 Service Pack 1.
            /// </summary>
            Windows7_SP1 = 0x4000,

            /// <summary>
            /// All architecture flags.
            /// </summary>
            Architecture = 
                Windows_32Bit_x86 | 
                Windows_64Bit_x64,

            /// <summary>
            /// All Windows Release flags.
            /// </summary>
            Release =
                WindowsWin32s | Windows95 | Windows98 | WindowsME | WindowsNT |
                Windows2000 | WindowsXP | WindowsCE | Windows2003 | WindowsVista |
                Windows7,

            /// <summary>
            /// All windows Edition and Service Pack flags.
            /// </summary>
            EditionServicePack =
                Windows98_SE | WindowsNT_351 | WindowsNT_40 |
                Windows2000_SP1 | Windows2000_SP2 | Windows2000_SP3 | Windows2000_SP4 |
                WindowsXP_SP1 | WindowsXP_SP2 | WindowsXP_SP3 |
                WindowsVista_SP1 | WindowsVista_SP2 |
                Windows7_SP1,

            /// <summary>
            /// A later version of windows then this enumeration supports.
            /// </summary>
            LaterVersion = 0x10000000
        }

        #endregion

        #region Private Static Locals

        /// <summary>
        /// Holds the operating system information.
        /// </summary>
        private static OperatingSystem? mosOperatingSystem = null;

        #endregion

        #region Private Static Methods

        /// <summary>
        /// Gets the OS version as a single flags enumeration.
        /// </summary>
        /// <returns>The OS version os Unknown if undetermined.</returns>
        private static OperatingSystem GetOSVersion()
        {
            // Get OperatingSystem information from the system namespace.
            System.OperatingSystem losInfo = System.Environment.OSVersion;

            OperatingSystem losReturn = OperatingSystem.Unknown;

            // Determine Windows Architecture
            losReturn |=
                (
                     System.Environment.Is64BitOperatingSystem
                     ? OperatingSystem.Windows_64Bit_x64
                     : OperatingSystem.Windows_32Bit_x86
                 );

            // Determine the platform.
            switch (losInfo.Platform)
            {
                case System.PlatformID.Win32S:
                    losReturn |= OperatingSystem.WindowsWin32s;
                    break;

                case System.PlatformID.WinCE:
                    losReturn |= OperatingSystem.WindowsCE;
                    break;

                // Platform is Windows 95, Windows 98, 
                // Windows 98 Second Edition, or Windows Me.
                case System.PlatformID.Win32Windows:

                    switch (losInfo.Version.Minor)
                    {
                        case 0:
                            losReturn |= OperatingSystem.Windows95;
                            break;
                        case 10:
                            if (losInfo.Version.Revision.ToString() == "2222A")
                                losReturn |= OperatingSystem.Windows98 | OperatingSystem.Windows98_SE;
                            else
                                losReturn |= OperatingSystem.Windows98;
                            break;
                        case 90:
                            losReturn |= OperatingSystem.WindowsME;
                            break;
                    }

                    break;

                // Platform is Windows NT 3.51, Windows NT 4.0, Windows 2000,
                // or Windows XP.
                case System.PlatformID.Win32NT:

                    switch (losInfo.Version.Major)
                    {
                        default:
                            if (losInfo.Version.Major > 6)
                                losReturn |= OperatingSystem.LaterVersion;
                            break;

                        case 3:
                            losReturn |= OperatingSystem.WindowsNT | OperatingSystem.WindowsNT_351;
                            break;
                        case 4:
                            losReturn |= OperatingSystem.WindowsNT | OperatingSystem.WindowsNT_40;
                            break;
                        case 5:
                            if (losInfo.Version.Minor == 0)
                                losReturn |=
                                    OperatingSystem.Windows2000 |
                                    AddServicePacks(
                                        losReturn,
                                        losInfo.ServicePack,
                                        new string[] { "1", "2", "3", "4" },
                                        new OperatingSystem[] 
                                        { 
                                            OperatingSystem.Windows2000_SP1, 
                                            OperatingSystem.Windows2000_SP2, 
                                            OperatingSystem.Windows2000_SP3,
                                            OperatingSystem.Windows2000_SP4
                                        });
                            else
                            {
                                losReturn |=
                                    OperatingSystem.WindowsXP |
                                    AddServicePacks(
                                        losReturn,
                                        losInfo.ServicePack,
                                        new string[] { "1", "2", "3" },
                                        new OperatingSystem[] 
                                        { 
                                            OperatingSystem.WindowsXP_SP1, 
                                            OperatingSystem.WindowsXP_SP2, 
                                            OperatingSystem.WindowsXP_SP3 
                                        });
                            }
                            break;
                        case 6:
                            if (losInfo.Version.Minor == 0)
                                losReturn |=
                                    OperatingSystem.WindowsVista |
                                    AddServicePacks(
                                        losReturn,
                                        losInfo.ServicePack,
                                        new string[] { "1", "2" },
                                        new OperatingSystem[] 
                                        { 
                                            OperatingSystem.WindowsVista_SP1, 
                                            OperatingSystem.WindowsVista_SP2 
                                        });
                            else
                                losReturn |=
                                    OperatingSystem.Windows7 |
                                    AddServicePacks(
                                        losReturn,
                                        losInfo.ServicePack,
                                        new string[] { "1" },
                                        new OperatingSystem[] { OperatingSystem.Windows7_SP1 });
                            break;
                    }

                    break;
            }

            // Log results
            if (Logging.Enabled)
                Logging.Log(
                    null,
                    String.Format("Operating System: {0}", losReturn.ToString()));

            return losReturn;
        }

        /// <summary>
        /// Adds service pack values to an OperatingSystem enumeration based
        /// on the Service Pack string.
        /// </summary>
        /// <param name="value">The value to add the service pack enumeration flags to.</param>
        /// <param name="servicePack">The service pack string.</param>
        /// <param name="searches">The string searches to use.</param>
        /// <param name="results">The results for each string search to use binary or with.</param>
        /// <returns>The new operating system enumeration value.</returns>
        private static OperatingSystem AddServicePacks(
            OperatingSystem value,
            string servicePack,
            string[] searches,
            OperatingSystem[] results)
        {
            for (int liCounter = 0; liCounter < Math.Min(searches.Length, results.Length); liCounter++)
                if (servicePack.Contains(searches[liCounter]))
                    value |= results[liCounter];

            return value;
        }

        /// <summary>
        /// Creates a COM Object by name.
        /// </summary>
        /// <param name="comName">The Application name of the COM Object to create.</param>
        /// <returns>The created COM object or null if not available.</returns>
        private static dynamic CreateCOMObject(string comName)
        {
            // Get the type
            System.Type ltCOMType = System.Type.GetTypeFromProgID(comName);

            try
            {
                // If type found
                if (ltCOMType != null)
                    // Create the instance
                    return System.Activator.CreateInstance(ltCOMType);
                else
                    // Otherwise return null
                    return null;
            }
            catch
            {
                // Any errors and we return null.
                return null;
            }
        }

        /// <summary>
        /// Gets the windows firwall service exception for UPnP.
        /// </summary>
        /// <param name="profile">On return, contains the profile information.</param>
        /// <returns>The service object (http://msdn.microsoft.com/en-us/library/windows/desktop/aa366421(v=VS.85)</returns>
        private static dynamic UPnPService(out dynamic profile)
        {
            try
            {
                dynamic loFwMgr = CreateCOMObject("HNetCfg.FwMgr");
                profile = loFwMgr.LocalPolicy.CurrentProfile;
                dynamic loService = profile.Services.Item(ServiceType.UPnP);
                return loService;
            }
            catch
            {
                profile = null;
                return null;
            }
        }

        /// <summary>
        /// Gets the status of the Windows Firewall and UPnP exception.
        /// </summary>
        /// <returns>The status found.</returns>
        private static Status GetWindowsUPnPFirewallStatus()
        {
            if (Logging.Enabled)
                Logging.Log(
                    null,
                    "Determining status of Windows Firewall UPnP Ports.", 1);

            try
            {
                try
                {
                    Status lsStatus = Status.None;
                    dynamic loProfile;
                    dynamic lsService = UPnPService(out loProfile);

                    if (loProfile != null)
                    {
                        if (!loProfile.FirewallEnabled)
                            lsStatus = Status.FirewallNotEnabled;
                        else
                            if (loProfile.ExceptionsNotAllowed) lsStatus = Status.ExceptionsNotAllowed;
                    }

                    if (lsStatus == Status.None)
                    {
                        if (lsService != null)
                            lsStatus = (lsService.Enabled ? Status.Open : Status.Closed);
                        else
                        {
                            if (Logging.Enabled)
                                Logging.Log(
                                    null,
                                    "Windows UPnP Firewall Service could not be located");

                            lsStatus = Status.Unknown;
                        }
                    }

                    if (Logging.Enabled)
                        Logging.Log(
                            null,
                            String.Format("Status: {0}", lsStatus.ToString()));

                    return lsStatus;
                }
                catch (Exception loE)
                {
                    if (Logging.Enabled)
                        Logging.Log(
                            null,
                            String.Format("Getting UPnP Firewall Port status failed with error: {0}", loE.ToString())
                    );

                    return Status.Unknown;
                }
            }
            finally
            {
                if (Logging.Enabled)
                    Logging.Log(
                        null,
                        "Finished status of Windows Firewall UPnP Ports.", -1);
            }
        }

        /// <summary>
        /// Sets the status of the Windows Firewall and UPnP exception.
        /// </summary>
        /// <param name="value">The status to set, only Open and Closed are valid.</param>
        private static void SetWindowsUPnPFirewallStatus(Status value)
        {
            if (Logging.Enabled)
                Logging.Log(
                    null,
                    String.Format("Setting status of Windows Firewall UPnP Ports to: {0}", value.ToString()),
                    1);

            try
            {
                try
                {
                    dynamic loProfile;
                    dynamic lsService = UPnPService(out loProfile);

                    if (loProfile != null)
                    {
                        if (!loProfile.FirewallEnabled)
                        {
                            if (Logging.Enabled)
                                Logging.Log(
                                    null,
                                    String.Format("Setting UPnP Firewall Port status failed because firewall is not enabled.")
                                );
                            return;
                        }

                        if (loProfile.ExceptionsNotAllowed)
                        {
                            if (Logging.Enabled)
                                Logging.Log(
                                    null,
                                    String.Format("Setting UPnP Firewall Port status failed because firewall exceptions are not allowed.")
                                );
                            return;
                        }
                    }

                    if (lsService != null)
                    {
                        if (value == Status.Closed || value == Status.Open)
                        {
                            if (Logging.Enabled)
                                Logging.Log(
                                    null,
                                    String.Format("Old Status: {0}", (lsService.Enabled ? Status.Open : Status.Closed)));

                            lsService.Enabled = (value == Status.Open);
                            lsService.Scope = Scope.All;
                        }
                    }
                    else
                        if (Logging.Enabled)
                            Logging.Log(
                                null,
                                String.Format("Windows UPnP Firewall Service could not be located")
                            );
                }
                catch (Exception loE)
                {
                    if (Logging.Enabled)
                        Logging.Log(
                            null,
                            String.Format("Setting UPnP Firewall Port status failed with error: {0}", loE.ToString())
                        );
                }
            }
            finally
            {
                if (Logging.Enabled)
                    Logging.Log(
                        null,
                        String.Format("Finished setting status of Windows Firewall UPnP Ports: {0}", value.ToString()),
                        -1);
            }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Checks to ensure that the Widnows Firewall allows UPnP traffic and
        /// asks the user if it is OK to enable the exception if it is possible
        /// to do so, other tells the user they must do it manually.
        /// </summary>
        /// <param name="dialogOwner">The owner of any message box, or null for none.</param>
        /// <returns>True if the firewall is now Open to UPnP traffic, false otherwise.</returns>
        public static bool CheckUPnPFirewallRules(IWin32Window dialogOwner)
        {
            OperatingSystem losVersion = OSVersion;
            MessageBoxButtons lbButtons = MessageBoxButtons.YesNo;
            bool lbReturn = true;

            switch (UPnPPortsOpen)
            {
                case Status.Closed:
                    if ((losVersion | OperatingSystem.WindowsXP) == OperatingSystem.WindowsXP)
                    {
                        while (UPnPPortsOpen == Status.Closed)
                        {
                            DialogResult ldrResult =
                                MessageBox.Show(
                                    dialogOwner,
                                    "The UPnP Windows Firewall rules are not enabled, " +
                                    "if you do not enable them this program will not work, " +
                                    "do you wish to enable them now?",
                                    Application.ProductName,
                                    lbButtons, MessageBoxIcon.Warning);

                            if (ldrResult == DialogResult.Yes ||
                                ldrResult == DialogResult.Retry)
                                ManagedUPnP.WindowsFirewall.UPnPPortsOpen = ManagedUPnP.WindowsFirewall.Status.Open;
                            else
                            {
                                lbReturn = false;
                                break;
                            }

                            lbButtons = MessageBoxButtons.RetryCancel;
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                            dialogOwner,
                            "The UPnP Windows Firewall rules are not enabled, " +
                            "please open your firewall settings and enable UPnP " +
                            "access to your network (TCP:2869 and UDP:1900).\r\n\r\n" +
                            "This program may not function properly until this is done.",
                            Application.ProductName,
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        lbReturn = false;
                    }
                    break;

                case Status.ExceptionsNotAllowed:
                    MessageBox.Show(
                        dialogOwner,
                        "Please note, this program may not work, as you have your Windows " +
                        "Firewall enabled, but have not allowed exceptions. Please check your firewall settings.",
                        Application.ProductName,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lbReturn = false;
                    break;
            }

            return lbReturn;
        }

        #endregion

        #region Public Static Properties

        /// <summary>
        /// Gets the Windows OS Version.
        /// </summary>
        public static OperatingSystem OSVersion
        {
            get
            {
                if (!mosOperatingSystem.HasValue)
                    mosOperatingSystem = GetOSVersion();

                return mosOperatingSystem.Value;
            }
        }

        /// <summary>
        /// Gets or sets the status of the Windows Firewall UPnP network traffic exception. 
        /// Only Open and Closed are valid for setting.
        /// </summary>
        public static Status UPnPPortsOpen
        {
            get
            {
                return GetWindowsUPnPFirewallStatus();
            }
            set
            {
                SetWindowsUPnPFirewallStatus(value);
            }
        }

        #endregion
    }
}

#endif