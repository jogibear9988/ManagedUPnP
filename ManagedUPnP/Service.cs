//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Runtime.InteropServices;
using UPNPLib;

namespace ManagedUPnP
{
    /// <summary>
    /// Encapsulates a UPnP service.
    /// </summary>
    public class Service : IDisposable, IAutoDiscoveryService  
    {
        #region Internal Classes

        /// <summary>
        /// Encapsulates the IUPnPServiceCallback class.
        /// </summary>
        internal class ServiceCallback : IUPnPServiceCallback
        {
            #region Private Locals

            /// <summary>
            /// The service for which this callback links to.
            /// </summary>
            private Service msService;

            #endregion

            #region Public Initialisation

            /// <summary>
            /// Creates a new service callback object.
            /// </summary>
            /// <param name="service">The service for which to raise the events on.</param>
            public ServiceCallback(Service service)
            {
                msService = service;
                msService.COMService.AddCallback(this);
            }

            #endregion

            #region Public Methods

            /// <summary>
            /// Sets this callback object to ignore all subsequent events.
            /// </summary>
            public void Ignore()
            {
                msService = null;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets the service for which this callback is linked to.
            /// </summary>
            public Service Service
            {
                get
                {
                    return msService;
                }
            }

            #endregion

            #region IUPnPServiceCallback Implementation

            /// <summary>
            /// Occurs when an event enabled state variable changes.
            /// </summary>
            /// <param name="service">The service for which the state variable changed.</param>
            /// <param name="stateVarName">The state variable name.</param>
            /// <param name="stateVarValue">The new value of the state variable.</param>
            void IUPnPServiceCallback.StateVariableChanged(UPnPService service, string stateVarName, object stateVarValue)
            {
                if(Service != null)
                    Service.OnStateVariableChanged(new StateVariableChangedEventArgs(stateVarName, stateVarValue));
            }

            /// <summary>
            /// Occurs when a service instance is no longer available.
            /// </summary>
            /// <param name="service">The service instance which is no longer available.</param>
            void IUPnPServiceCallback.ServiceInstanceDied(UPnPService service)
            {
                if(Service != null)
                    Service.OnServiceInstanceDied(new ServiceInstanceDiedEventArgs());
            }

            #endregion
        }

        #endregion

        #region Private Locals

        /// <summary>
        /// The underlying native service this service controls.
        /// </summary>
        private IUPnPService msCOMService;

        /// <summary>
        /// The callback object in use for this service.
        /// </summary>
        private ServiceCallback mscCallback = null;

        #endregion

        #region Protected Locals

        /// <summary>
        /// True if this service is no longer available.
        /// </summary>
        protected bool mbDead;

        /// <summary>
        /// The immediate device for this service.
        /// </summary>
        protected Device mdDevice;

        #endregion

        #region Events

        /// <summary>
        /// Raised when an event enabled state variable changes.
        /// </summary>
        public event StateVariableChangedEventHandler StateVariableChanged;

        /// <summary>
        /// Raised when the service is no longer available.
        /// </summary>
        /// <remarks>
        /// Dead is set to true after this event is received.
        /// </remarks>
        public event ServiceInstanceDiedEventHandler ServiceInstanceDied;

        #endregion

        #region Internal Initialisation

        /// <summary>
        /// Creates a new service object.
        /// </summary>
        /// <param name="device">The immediate device for the service.</param>
        /// <param name="comService">The native service for the service.</param>
        internal Service(Device device, IUPnPService comService)
        {
            msCOMService = comService;
            mscCallback = new ServiceCallback(this);

            mdDevice = device;
            mbDead = false;
        }

        /// <summary>
        /// Creates a new service object.
        /// </summary>
        /// <param name="comDevice">The immediate native device for the object.</param>
        /// <param name="interfaceGuid">The network interface Guid or Guid.Empty for none.</param>
        /// <param name="comService">The native service for the service.</param>
        internal Service(IUPnPDevice comDevice, Guid interfaceGuid, IUPnPService comService)
            : this(new Device(comDevice, interfaceGuid), comService)
        {
        }

        #endregion

        #region Public Initialisation

        /// <summary>
        /// Creates a new service object.
        /// </summary>
        /// <param name="service">The service to use for this service.</param>
        public Service(Service service)
            : this(service.Device, service.COMService) 
        {
        }

        #endregion

        #region Event Callers

        /// <summary>
        /// Raises the StateVariableChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnStateVariableChanged(StateVariableChangedEventArgs e)
        {
            if (StateVariableChanged != null)
                StateVariableChanged(this, e);
        }

        /// <summary>
        /// Raises the ServiceInstanceDied event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnServiceInstanceDied(ServiceInstanceDiedEventArgs e)
        {
            mbDead = true;

            if (ServiceInstanceDied != null)
                ServiceInstanceDied(this, e);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Checks the out parameters from the result of an action 
        /// to ensure the enough parameters were returned.
        /// </summary>
        /// <param name="outParams">The out params returned by the action.</param>
        /// <param name="numberRequired">The minimum number of required out parameters.</param>
        protected void CheckOutParams(object[] outParams, UInt32 numberRequired)
        {
            if (outParams == null && numberRequired != 0)
                throw new ArgumentNullException(
                    String.Format("The action returned no arguments, {0} arguments were required.", numberRequired)
                );

            if (outParams != null && outParams.Length < numberRequired)
                String.Format(
                    "The action did not return enough arguments, " + 
                    "{0} were returned but {1} arguments were required.", 
                    outParams.Length, numberRequired);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Queries a state variable on the service.
        /// </summary>
        /// <typeparam name="T">The expected return type for the state variable.</typeparam>
        /// <param name="name">The name of the state variable.</param>
        /// <returns>The return value of the state variable.</returns>
        /// <exception cref="UPnPException"></exception>
        /// <exception cref="NotSupportedException">Underlying com service is null.</exception>
        public T QueryStateVariable<T>(string name)
        {
            return (T)QueryStateVariable(name);
        }

        /// <summary>
        /// Queries a state variable on the service.
        /// </summary>
        /// <param name="name">The name of the state variable.</param>
        /// <returns>The value of the state variable.</returns>
        /// <exception cref="UPnPException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public object QueryStateVariable(string name)
        {
            try
            {
                if (msCOMService != null)
                    return msCOMService.QueryStateVariable(name);
                else
                    throw new NotSupportedException();
            }
            catch (COMException loE)
            {
                throw new UPnPException(loE);
            }
        }

        /// <summary>
        /// Invokes an action with has one out parameter.
        /// </summary>
        /// <typeparam name="T1">The type of the out parameter.</typeparam>
        /// <param name="name">The name of the action.</param>
        /// <param name="out1">The variable to receive the out parameter.</param>
        /// <param name="inParams">The in parameters for the action.</param>
        /// <exception cref="UPnPException"></exception>
        /// <exception cref="MissingMethodException">Action not found or failed.</exception>
        /// <exception cref="NotSupportedException">Underlying native service is null.</exception>
        public void InvokeAction<T1>(string name, out T1 out1, params object[] inParams)
        {
            object[] loOut = InvokeAction(name, inParams);
            CheckOutParams(loOut, 1);
            out1 = (T1)loOut[0];
        }

        /// <summary>
        /// Invokes an action with has two out parameters.
        /// </summary>
        /// <typeparam name="T1">The type of the first out parameter.</typeparam>
        /// <typeparam name="T2">The type of the second out parameter.</typeparam>
        /// <param name="name">The name of the action.</param>
        /// <param name="out1">The variable to receive the first out parameter.</param>
        /// <param name="out2">The variable to receive the second out parameter.</param>
        /// <param name="inParams">The in parameters for the action.</param>
        /// <exception cref="UPnPException"></exception>
        /// <exception cref="MissingMethodException">Action not found or failed.</exception>
        /// <exception cref="NotSupportedException">Underlying native service is null.</exception>
        public void InvokeAction<T1, T2>(string name, out T1 out1, out T2 out2, params object[] inParams)
        {
            object[] loOut = InvokeAction(name, inParams);
            CheckOutParams(loOut, 2);
            out1 = (T1)loOut[0];
            out2 = (T2)loOut[1];
        }

        /// <summary>
        /// Invokes an action with has three out parameters.
        /// </summary>
        /// <typeparam name="T1">The type of the first out parameter.</typeparam>
        /// <typeparam name="T2">The type of the second out parameter.</typeparam>
        /// <typeparam name="T3">The type of the third out parameter.</typeparam>
        /// <param name="name">The name of the action.</param>
        /// <param name="out1">The variable to receive the first out parameter.</param>
        /// <param name="out2">The variable to receive the second out parameter.</param>
        /// <param name="out3">The variable to receive the third out parameter.</param>
        /// <param name="inParams">The in parameters for the action.</param>
        /// <exception cref="UPnPException"></exception>
        /// <exception cref="MissingMethodException">Action not found or failed.</exception>
        /// <exception cref="NotSupportedException">Underlying native service is null.</exception>
        public void InvokeAction<T1, T2, T3>(string name, out T1 out1, out T2 out2, out T3 out3, params object[] inParams)
        {
            object[] loOut = InvokeAction(name, inParams);
            CheckOutParams(loOut, 3);
            out1 = (T1)loOut[0];
            out2 = (T2)loOut[1];
            out3 = (T3)loOut[2];
        }

        /// <summary>
        /// Invokes an action with has four out parameters.
        /// </summary>
        /// <typeparam name="T1">The type of the first out parameter.</typeparam>
        /// <typeparam name="T2">The type of the second out parameter.</typeparam>
        /// <typeparam name="T3">The type of the third out parameter.</typeparam>
        /// <typeparam name="T4">The type of the forth out parameter.</typeparam>
        /// <param name="name">The name of the action.</param>
        /// <param name="out1">The variable to receive the first out parameter.</param>
        /// <param name="out2">The variable to receive the second out parameter.</param>
        /// <param name="out3">The variable to receive the third out parameter.</param>
        /// <param name="out4">The variable to receive the forth out parameter.</param>
        /// <param name="inParams">The in parameters for the action.</param>
        /// <exception cref="UPnPException"></exception>
        /// <exception cref="MissingMethodException">Action not found or failed.</exception>
        /// <exception cref="NotSupportedException">Underlying native service is null.</exception>
        public void InvokeAction<T1, T2, T3, T4>(string name, out T1 out1, out T2 out2, out T3 out3, out T4 out4, params object[] inParams)
        {
            object[] loOut = InvokeAction(name, inParams);
            CheckOutParams(loOut, 4);
            out1 = (T1)loOut[0];
            out2 = (T2)loOut[1];
            out3 = (T3)loOut[2];
            out4 = (T4)loOut[3];
        }

        /// <summary>
        /// Invokes an action with has five out parameters.
        /// </summary>
        /// <typeparam name="T1">The type of the first out parameter.</typeparam>
        /// <typeparam name="T2">The type of the second out parameter.</typeparam>
        /// <typeparam name="T3">The type of the third out parameter.</typeparam>
        /// <typeparam name="T4">The type of the forth out parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth out parameter.</typeparam>
        /// <param name="name">The name of the action.</param>
        /// <param name="out1">The variable to receive the first out parameter.</param>
        /// <param name="out2">The variable to receive the second out parameter.</param>
        /// <param name="out3">The variable to receive the third out parameter.</param>
        /// <param name="out4">The variable to receive the forth out parameter.</param>
        /// <param name="out5">The variable to receive the fifth out parameter.</param>
        /// <param name="inParams">The in parameters for the action.</param>
        /// <exception cref="UPnPException"></exception>
        /// <exception cref="MissingMethodException">Action not found or failed.</exception>
        /// <exception cref="NotSupportedException">Underlying native service is null.</exception>
        public void InvokeAction<T1, T2, T3, T4, T5>(string name, out T1 out1, out T2 out2, out T3 out3, out T4 out4, out T5 out5, params object[] inParams)
        {
            object[] loOut = InvokeAction(name, inParams);
            CheckOutParams(loOut, 5);
            out1 = (T1)loOut[0];
            out2 = (T2)loOut[1];
            out3 = (T3)loOut[2];
            out4 = (T4)loOut[3];
            out5 = (T5)loOut[4];
        }

        /// <summary>
        /// Invokes an action with has six out parameters.
        /// </summary>
        /// <typeparam name="T1">The type of the first out parameter.</typeparam>
        /// <typeparam name="T2">The type of the second out parameter.</typeparam>
        /// <typeparam name="T3">The type of the third out parameter.</typeparam>
        /// <typeparam name="T4">The type of the forth out parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth out parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth out parameter.</typeparam>
        /// <param name="name">The name of the action.</param>
        /// <param name="out1">The variable to receive the first out parameter.</param>
        /// <param name="out2">The variable to receive the second out parameter.</param>
        /// <param name="out3">The variable to receive the third out parameter.</param>
        /// <param name="out4">The variable to receive the forth out parameter.</param>
        /// <param name="out5">The variable to receive the fifth out parameter.</param>
        /// <param name="out6">The variable to receive the sixth out parameter.</param>
        /// <param name="inParams">The in parameters for the action.</param>
        /// <exception cref="UPnPException"></exception>
        /// <exception cref="MissingMethodException">Action not found or failed.</exception>
        /// <exception cref="NotSupportedException">Underlying native service is null.</exception>
        public void InvokeAction<T1, T2, T3, T4, T5, T6>(
            string name, out T1 out1, out T2 out2, out T3 out3, 
            out T4 out4, out T5 out5, out T6 out6, 
            params object[] inParams)
        {
            object[] loOut = InvokeAction(name, inParams);
            CheckOutParams(loOut, 6);
            out1 = (T1)loOut[0];
            out2 = (T2)loOut[1];
            out3 = (T3)loOut[2];
            out4 = (T4)loOut[3];
            out5 = (T5)loOut[4];
            out6 = (T6)loOut[5];
        }

        /// <summary>
        /// Invokes an action with has seven out parameters.
        /// </summary>
        /// <typeparam name="T1">The type of the first out parameter.</typeparam>
        /// <typeparam name="T2">The type of the second out parameter.</typeparam>
        /// <typeparam name="T3">The type of the third out parameter.</typeparam>
        /// <typeparam name="T4">The type of the forth out parameter.</typeparam>
        /// <typeparam name="T5">The type of the fifth out parameter.</typeparam>
        /// <typeparam name="T6">The type of the sixth out parameter.</typeparam>
        /// <typeparam name="T7">The type of the seventh out parameter.</typeparam>
        /// <param name="name">The name of the action.</param>
        /// <param name="out1">The variable to receive the first out parameter.</param>
        /// <param name="out2">The variable to receive the second out parameter.</param>
        /// <param name="out3">The variable to receive the third out parameter.</param>
        /// <param name="out4">The variable to receive the forth out parameter.</param>
        /// <param name="out5">The variable to receive the fifth out parameter.</param>
        /// <param name="out6">The variable to receive the sixth out parameter.</param>
        /// <param name="out7">The variable to receive the seventh out parameter.</param>
        /// <param name="inParams">The in parameters for the action.</param>
        /// <exception cref="UPnPException"></exception>
        /// <exception cref="MissingMethodException">Action not found or failed.</exception>
        /// <exception cref="NotSupportedException">Underlying native service is null.</exception>
        public void InvokeAction<T1, T2, T3, T4, T5, T6, T7>(
            string name, out T1 out1, out T2 out2, out T3 out3,
            out T4 out4, out T5 out5, out T6 out6, out T7 out7,
            params object[] inParams)
        {
            object[] loOut = InvokeAction(name, inParams);
            CheckOutParams(loOut, 7);
            out1 = (T1)loOut[0];
            out2 = (T2)loOut[1];
            out3 = (T3)loOut[2];
            out4 = (T4)loOut[3];
            out5 = (T5)loOut[4];
            out6 = (T6)loOut[5];
            out7 = (T7)loOut[6];
        }
        
        /// <summary>
        /// Invokes an action.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <param name="inParams">The in parameters for the action.</param>
        /// <returns>An array of objects representing the out parameters for the action.</returns>
        /// <exception cref="UPnPException"></exception>
        /// <exception cref="MissingMethodException">Action not found or failed.</exception>
        /// <exception cref="NotSupportedException">Underlying native service is null.</exception>
        public object[] InvokeAction(string name, params object[] inParams)
        {
            try
            {
                if (msCOMService != null)
                {
                    object[] laIn = inParams;
                    if (laIn == null) laIn = new object[] { };

                    object loOut = null;

                    object loResult = msCOMService.InvokeAction(name, laIn, ref loOut);

                    if (loResult != null)
                        throw new MissingMethodException(
                            string.Format(
                            "({0}) => {1} failed with {2} result",
                            msCOMService.ServiceTypeIdentifier, name, loResult));

                    return (object[])loOut;
                }
                else
                    throw new NotSupportedException();
            }
            catch (COMException loE)
            {
                throw new UPnPException(loE);
            }
        }

        /// <summary>
        /// Gets whether this service has the ability to access another service.
        /// </summary>
        /// <param name="service">The service to test to accessibility.</param>
        /// <returns>True.</returns>
        public virtual bool CanAccess(Service service)
        {
            return true;
        }

        /// <summary>
        /// Gets this service as readable text.
        /// </summary>
        /// <returns>Gets the readable info as a string.</returns>
        public override string ToString()
        {
            return msCOMService.ReadableInfo();
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the underlying native com device.
        /// </summary>
        internal IUPnPDevice COMDevice
        {
            get
            {
                return mdDevice.COMDevice;
            }
        }

        /// <summary>
        /// Gets the underlying native com service.
        /// </summary>
        internal IUPnPService COMService
        {
            get
            {
                return msCOMService;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets whether this service instance has died.
        /// </summary>
        public bool Dead
        {
            get
            {
                return mbDead;
            }
        }

        /// <summary>
        /// Gets the device for this service.
        /// </summary>
        public Device Device
        {
            get
            {
                return mdDevice;
            }
        }

        /// <summary>
        /// Gerts the service type identifier for this service.
        /// </summary>
        public string ServiceTypeIdentifier
        {
            get
            {
                return COMService.ServiceTypeIdentifier;
            }
        }

        /// <summary>
        /// Gets the Id for this service.
        /// </summary>
        public string Id
        {
            get
            {
                return COMService.Id;
            }
        }

        /// <summary>
        /// Gets the result of the last invoke action or state variable query.
        /// </summary>
        public HTTPStatus LastTransportStatus
        {
            get
            {
                return (HTTPStatus)COMService.LastTransportStatus;
            }
        }

        /// <summary>
        /// Gets the globally unique key for this service.
        /// </summary>
        public string Key
        {
            get
            {
                return String.Format("{0}=>{1}", COMDevice.UniqueDeviceName, COMService.Id);
            }
        }

        /// <summary>
        /// Gets the friendly service type identifier name
        /// </summary>
        public string FriendlyServiceTypeIdentifier
        {
            get
            {
                return COMService.GetFriendlyServiceTypeIdentifier();
            }
        }

        /// <summary>
        /// Gets the readable name of this service.
        /// </summary>
        public string Name
        {
            get
            {
                return
                    String.Format(
                        "{0} => {1} on {2}",
                        COMDevice.RootDevice.FriendlyName,
                        FriendlyServiceTypeIdentifier,
                        COMDevice.FriendlyName);
            }
        }

        #endregion

        #region Finalisation

        /// <summary>
        /// Finalisation.
        /// </summary>
        ~Service()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes the service.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the service.
        /// </summary>
        /// <param name="disposeManaged">True to dispose managed objects.</param>
        protected virtual void Dispose(bool disposeManaged)
        {
            if (disposeManaged)
            {
                if (mscCallback != null)
                {
                    mscCallback.Ignore();
                    mscCallback = null;
                }
            }
        }

        #endregion    
    
        #region IAutoDiscoverService Implementation

        /// <summary>
        /// Gets this service
        /// </summary>
        Service IAutoDiscoveryService.Service
        {
            get 
            {
                return this;
            }
        }

        #endregion
    }
}
