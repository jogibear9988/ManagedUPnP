using System;
using System.Windows.Forms;
using ManagedUPnP;

namespace ComponentsTest
{
    /// <summary>
    /// Encapsulates a form to demonstrate how to use the 
    /// UPnPDiscovery component.
    /// </summary>
    public partial class frmUPnPDiscoveryTest : Form
    {
        #region Private Locals

        /// <summary>
        /// Stores the last found WANIPConnection service.
        /// </summary>
        private Service msWANIPConnectionService = null;

        #endregion

        #region Public Initialisation

        /// <summary>
        /// Creates a new components test form.
        /// </summary>
        public frmUPnPDiscoveryTest()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the enabled property of the forms
        /// controls, based on the current state.
        /// </summary>
        private void UpdateEnabled()
        {
            cmdStart.Enabled = !UPnP.Active;
            cmdStop.Enabled = UPnP.Active;
            cmdShowIPAddress.Enabled = msWANIPConnectionService != null;
        }

        /// <summary>
        /// Writes a line to the text box.
        /// </summary>
        /// <param name="line">The line to write.</param>
        private void WriteLine(string line)
        {
            tbLog.AppendText(String.Format("{0}\r\n", line));
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Occurs when the form laods.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void frmComponentsTest_Load(object sender, EventArgs e)
        {
            // Update buttons enabled properties
            UpdateEnabled();

            // Deselect any text
            tbLog.SelectionStart = 0;
        }

        /// <summary>
        /// Occurs when the UPnPDiscovery component starts
        /// a search.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void UPnP_SearchStarted(object sender, EventArgs e)
        {
            UpdateEnabled();
            WriteLine("Started...");
        }

        /// <summary>
        /// Occurs when the UPnPDiscovery component fails to
        /// start its search. Note that the SearchEnded
        /// event still fires after this.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void UPnP_SearchFailed(object sender, EventArgs e)
        {
            // Notify user
            WriteLine("Failed...");
        }

        /// <summary>
        /// Occurs when the UPnPDiscovery component finds a new device.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="a">The event arguments.</param>
        private void UPnP_DeviceAdded(object sender, DeviceAddedEventArgs a)
        {
            // Notify user
            WriteLine(String.Format("Device Found '{0}' - {1} - {2}", a.Device.FriendlyName, a.Device.UniqueDeviceName, a.Device.Type));
        }

        /// <summary>
        /// Occurs when the UPnPDiscovery component finds a new service.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="a">The event arguments.</param>
        private void UPnP_ServiceAdded(object sender, ServiceAddedEventArgs a)
        {
            // Notify user
            WriteLine(String.Format("Service Found: '{0}' - {1} - {2}", a.Service.Id, a.Service.Name, a.Service.ServiceTypeIdentifier));

            // If the service is a WANIPConnection service (any version)
            if (a.Service.ServiceTypeIdentifier.Contains(":WANIPConnection:"))
            {
                // Set the service
                msWANIPConnectionService = a.Service;

                // Update buttons enabled properties
                UpdateEnabled();

                // Notify user we have found the service
                WriteLine("[Service Located]");
            }
        }

        /// <summary>
        /// Occurs when the UPnPDiscovery component completes
        /// its initial search. Note that this does not mean
        /// the search has ended, any new UPnP devices which
        /// become available will still trigger events to fire.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="a">The event arguments.</param>
        private void UPnP_SearchComplete(object sender, SearchCompleteEventArgs a)
        {
            // Notify user
            WriteLine("Completed Initial Stage...");
        }


        /// <summary>
        /// Occurs when the UPnPDiscovery component ends 
        /// its search.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void UPnP_SearchEnded(object sender, EventArgs e)
        {
            // Update the controls enabled properties
            UpdateEnabled();

            // Notify user
            WriteLine("Ended...");
        }

        /// <summary>
        /// Occurs when the user clicks the Start button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void cmdStart_Click(object sender, EventArgs e)
        {
            // Start the UPnP discovery
            UPnP.Active = true;
        }

        /// <summary>
        /// Occurs when the user clicks the Stop button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void cmdStop_Click(object sender, EventArgs e)
        {
            // Deactivate the UPnP discovery search
            // Note that all Devices and Services are still
            // accessible when the search is stopped.
            UPnP.Active = false;
        }

        /// <summary>
        /// Occurs when the user clicks the Show IP Address button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void cmdTest_Click(object sender, EventArgs e)
        {
            // Check to ensure that we have found the service
            if (msWANIPConnectionService != null)
            {
                string lsIP;
                try
                {
                    // Invoke the GetExternalIPAddress action
                    msWANIPConnectionService.InvokeAction<string>("GetExternalIPAddress", out lsIP);

                    // Show the IP Address
                    MessageBox.Show(String.Format("Your IP Address is: {0}", lsIP), Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception loE)
                {
                    // On error show the error in a message
                    MessageBox.Show(
                        String.Format("Error occurred getting IP Address: {0}", loE.ToString()),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information 
                    );
                }
            }
            else
                // Just in case user manages to click disabled button - who knows what can happen
                MessageBox.Show("WANIPConnection service not located yet. CLick the start button to search for compatible services.");
        }

        /// <summary>
        /// Occurs when new ManagedUPnP logging occurs.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="a">The event arguments.</param>
        private void LogIntercept_LogLines(object sender, LogLinesEventArgs a)
        {
            WriteLine("\tADVANCED LOG] " + a.Lines.Replace("\r\n", "\r\n\tADVANCEDLOG] "));
        }

        #endregion
    }
}
