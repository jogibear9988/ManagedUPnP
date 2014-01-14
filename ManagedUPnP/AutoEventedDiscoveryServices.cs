//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;

namespace ManagedUPnP
{
    /// <summary>
    /// Encapsulates an class which maintaines a list of services
    /// for a service type and provides events for servicing.
    /// </summary>
    /// <remarks>
    /// The services are automatically removed and added as they become
    /// available or become dead. This class is thread safe.
    /// </remarks>
    /// <typeparam name="T">The type of the auto discovery service</typeparam>
    public class AutoEventedDiscoveryServices<T> : AutoDiscoveryServices<T> where T : IAutoDiscoveryService, IDisposable 
    {
        #region Event Arguments

        /// <summary>
        /// Encapsulates the arguments for the StatusNotifyAction event.
        /// </summary>
        public class StatusNotifyActionEventArgs : EventArgs
        {
            #region Private Locals

            /// <summary>
            /// The action being notified.
            /// </summary>
            private NotifyAction mnaAction;

            /// <summary>
            /// The data for the action.
            /// </summary>
            private object moData;

            #endregion

            #region Initialisation

            /// <summary>
            /// Creates a nwe status notify action event args.
            /// </summary>
            /// <param name="notifyAction">The action being notified.</param>
            /// <param name="data">The data for the action.</param>
            public StatusNotifyActionEventArgs(NotifyAction notifyAction, object data)
            {
                mnaAction = notifyAction;
                moData = data;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets the notify action raised.
            /// </summary>
            public NotifyAction NotifyAction
            {
                get
                {
                    return mnaAction;
                }
            }

            /// <summary>
            /// Gets the data for the notify action. 
            /// (see <see cref="ManagedUPnP.AutoDiscoveryServices{T}.NotifyAction" /> )
            /// enum for more information).
            /// </summary>
            public object Data
            {
                get
                {
                    return moData;
                }
            }

            #endregion
        }

        /// <summary>
        /// Encapsulates the arguments for the CanCreateServiceFor event.
        /// </summary>
        public class CanCreateServiceForEventArgs : EventArgs
        {
            #region Private Locals

            /// <summary>
            /// Set to true if a service can be created from the found service.
            /// </summary>
            private bool mbCanCreate = true;

            /// <summary>
            /// The service for which is being enquired.
            /// </summary>
            private Service msService;

            #endregion

            #region Initialisation

            /// <summary>
            /// Creates a new can create service for event arguments.
            /// </summary>
            /// <param name="service">The service for the equiry.</param>
            public CanCreateServiceForEventArgs(Service service)
            {
                msService = service;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets the service for which to create the service for.
            /// </summary>
            public Service Service
            {
                get
                {
                    return msService;
                }
            }

            /// <summary>
            /// Gets or sets whether a service of this can be created for this service.
            /// </summary>
            public bool CanCreate
            {
                get
                {
                    return mbCanCreate;
                }
                set
                {
                    mbCanCreate = value;
                }
            }

            #endregion
        }

        /// <summary>
        /// Encapsulates the arguments for the create service event arguments.
        /// </summary>
        public class CreateServiceForEventArgs : CanCreateServiceForEventArgs
        {
            #region Private Locals

            /// <summary>
            /// The created auto service for the service.
            /// </summary>
            private T masCreatedAutoService;

            #endregion

            #region Public Initialisation

            /// <summary>
            /// Creates a new create auto service event arguments.
            /// </summary>
            /// <param name="service">The service to create the auto service for.</param>
            public CreateServiceForEventArgs(Service service)
                : base(service)
            {
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets or sets the created auto service.
            /// </summary>
            public T CreatedAutoService
            {
                get
                {
                    return masCreatedAutoService;
                }
                set
                {
                    masCreatedAutoService = value;
                    CanCreate = masCreatedAutoService != null;
                }
            }

            #endregion
        }

        #endregion

        #region Event Handler Delegates

        /// <summary>
        /// Event handler for whether a service can have an auto service created for it.
        /// </summary>
        /// <param name="sender">The sender of the events.</param>
        /// <param name="e">The event arguments.</param>
        public delegate void CanCreateServiceForEventHandler(object sender, CanCreateServiceForEventArgs e);

        /// <summary>
        /// Event handler for whether a service needs an auto service can be created for it.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        public delegate void CreateServiceForEventHandler(object sender, CreateServiceForEventArgs e);

        /// <summary>
        /// Event handler for when a status notification occurs.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments</param>
        public delegate void StatusNotifyActionEventHandler(object sender, StatusNotifyActionEventArgs e);

        #endregion

        #region Events

        /// <summary>
        /// Handles when when a status notification occurs.
        /// </summary>
        public event StatusNotifyActionEventHandler StatusNotifyAction; 

        /// <summary>
        /// Handles when whether a service needs an auto service can be created for it.
        /// </summary>
        public event CanCreateServiceForEventHandler CanCreateServiceFor;

        /// <summary>
        /// Handles when a service needs an auto service created for it.
        /// </summary>
        public event CreateServiceForEventHandler CreateServiceFor;

        #endregion

        #region Public Initialisation

        /// <summary>
        /// Creates a new AutoDiscoveryServices object.
        /// </summary>
        /// <param name="discoveryServiceType">The service type to discover or String.Empty for all services.</param>
        public AutoEventedDiscoveryServices(string discoveryServiceType)
            : base(discoveryServiceType)
        {
        }

        #endregion

        #region Event Callers

        /// <summary>
        /// Calls the status notify action event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnStatusNotifyAction(StatusNotifyActionEventArgs e)
        {
            if (StatusNotifyAction != null)
                StatusNotifyAction(this, e);
        }

        /// <summary>
        /// Calls the can create service for event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnCanCreateServiceFor(CanCreateServiceForEventArgs e)
        {
            if (CanCreateServiceFor != null)
                CanCreateServiceFor(this, e);
            else
                e.CanCreate = false;
        }

        /// <summary>
        /// Calls the create service for event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnCreateServiceFor(CreateServiceForEventArgs e)
        {
            if (CreateServiceFor != null)
                CreateServiceFor(this, e);
            else
            {
                e.CreatedAutoService = default(T);
                e.CanCreate = false;
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Notifies of status change with data.
        /// </summary>
        /// <param name="action">The action for the notification.</param>
        /// <param name="data">The data for the action (see <see cref="ManagedUPnP.AutoDiscoveryServices{T}.NotifyAction" /> enumeration).</param>
        protected override void StatusNotify(AutoDiscoveryServices<T>.NotifyAction action, object data)
        {
            OnStatusNotifyAction(new StatusNotifyActionEventArgs(action, data));
            base.StatusNotify(action, data);
        }

        /// <summary>
        /// Determines whether a service is compatible with T.
        /// </summary>
        /// <param name="service">The service to determine compatibility for.</param>
        /// <returns>True if the service is compatible false otherwise.</returns>
        protected override bool CanCreateAutoServiceFor(Service service)
        {
            CanCreateServiceForEventArgs laArgs = new CanCreateServiceForEventArgs(service);
            OnCanCreateServiceFor(laArgs);
            return laArgs.CanCreate;
        }

        /// <summary>
        /// Creates a T object for the specified service.
        /// </summary>
        /// <param name="service">The service to create T for.</param>
        /// <returns>The service created or null if unable to create.</returns>
        protected override T CreateAutoServiceFor(Service service)
        {
            CreateServiceForEventArgs laArgs = new CreateServiceForEventArgs(service);
            OnCreateServiceFor(laArgs);

            if (laArgs.CanCreate)
                return laArgs.CreatedAutoService;
            else
                return default(T);
        }

        #endregion
    }
}
