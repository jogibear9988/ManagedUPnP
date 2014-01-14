//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//  Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//  A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

History:
=========================
2.0
* Added an extra overload for Discovery.FindServices(ipAddressFamily, resolveNetworkInterfaces), this will find all services in a synchronous manner.
* Better support for new Adapters, AdapterUnicastIPAddressInformations, AdapterIPAddresses, RootHostAddresses, and RootHostName Device properties with IPv6 (removed address type checking).
* Added more parameter checking with Find Services and raises exceptions.

1.9
* Added Adapters, AdapterUnicastIPAddressInformations, AdapterIPAddresses, RootHostAddresses, and RootHostName properties to Device class
* Added NIC Device IP Addresses to device info in ManagedUPnPTest application
* Added Description support for all UPnP datatypes: bin.base64, number, uuid, bin.hex
* All new data types also support to and from string conversions
* ManagedUPnPTest application now supports the reading and entering of byte types (bin.Base64)
* New property added to data type information attribute - AllowEmpty, is true if array or string datatypes can be empty
* Added AddressFamily and ResolveNetworkInterfaces properties to AutoDiscoverServices class
* Updated ManagedUPnP test to display IP addresses of conencted interfaces utilising the InterfaceGuid where available (Windows Vista and above)
* Other minor bug fixes

1.8
* Code generation now supports VB.NET generation
* ManagedUPnPTest updated to support new VB.NET code generation
* New class added to ManagedUPnP.Components - LogInterceptor - a GUI thread safe ManagedUPnP logging component
* Added LogInterceptor example to ComponentsTest project
* More examples added via XML comments
* Other minor bug fixes

1.7
* XML Comments added to code generation for Devices and Services including Service Action parameter properties
* New WindowsFirewall static class added - This class can check the Windows firewall settings and fix them under Windows XP
* More comphensive XML comments in the code
* Brings code inline with new CHM help file located in Documentation section (more documentation to come)
* Many little bug fixes

1.6
* NOTE: None of these changes should break any of your code, I have done my very best to keep it backward compatible
* Code generation for Devices and Services is now smarter - it will not create empty Regions
* Code generation for Services now adds generic typed events for each "Evented" state variable - saves you doing the work
* New classes added in ManagedUPnP.Components namespace, the UPnPDiscovery component allows extremely easy access to discovering Devices and Services from a windows forms application asynchronously, simply drop the component on your form, add the events and set the Active property to true. This component will automatically take into account for GUI Thread related issues. See the example project.
* New demo Project added to solution - ComponentsTest - demonstrating the UPnPDiscovery component
* ManagedUPnPTest project now defers code generation of Services and Devices until after you click the tab - this reduces lag
* New conditional defines added to allow you to compile ManagedUPnP.DLL without certain namespaces, see ConditionalDefines.CS in the root of the ManagedUPnP project for more information.
* Added more XML comments to Discovery class to make it easier to understand
* Many little bug fixes
* PLANNED FOR FUTURE VERSION: XML Comments in all generated class code. including extra information about parameters and state variables
* PLANNED FOR FUTURE VERSION: Proper documentation - thanks Chloh for the review! :)

1.5
* Fixed problem with generating service classes with default Services namespace (ManagedUPnP.Services was being confused with local namespace)

1.4
* Added Logging static class to ManagedUPnP namespace, simply use Logging.Enabled = true, and Logging.LogLines += to enable a verbose UPnP log for runtime debugging.
* Added Log box to ManagedUPnPTest project
* Added Verbose Log option to ExternalIPAdress project (/V)
* Added extra static functions to Service class code generation to get all services for the type and return array of class objects
* Other minor enhancements (nothing that should break your code).

1.1
* Fixed problem with ServiceDescriptionCache where it wasnt using the RootDevice for the caching resulting in issues when using the cache (thanks to tbau from Whirlpool.net.au for his help with testing).
* Other minor enhancements (nothing that should break your code).

1.0
* Now out of beta
* ArgumentsDescription list used to use a dictionary for its items, however, dictionaries are not garunteed to preserve order, the arguments must stay in order so they are accessible in the order that they need to be provided in the action calls, this has now been changed and this dictionary uses a OrderedIndexedDictionary instead.
* Other minor changes (nothinn to break dependent code).

0.9b
* Initial release contains ability to discover services / devices
* Easily get service and device descriptions to determine capabilities (as a structured class set)
* Demo project included to provided user ability to navigate all UPnP devices and execute actions or query state variables

0.91b
* Contains the ability to fully generate classes for Devices and Services, whether it be generating them as standalone classes, or generating an entire tree of a device, the demonstration application has been updated as well to provide access to these features from a user interface.
* Minor bug fixes with some devices which have malformed XML descriptions
* Other minor bug fixes in the library and testing application
* More functions added to Devices, and Device for finding devices by Type and / ModelName
* Other enhancements (nothing that should break your code).

0.92b
* More static functions added to Discovery class to enable Async searching using delegate callback with timeout / number of devices (searching for Devices or Services is provided) see Discovery.FindDevicesAsync and Discovery.FindServicesAsync, of course the Discovery class can still be instantiated to use events.
* Included with this download is the ExternalIPAddress console application, it demonstrates how to use ManagedUPnP to write a console application to show the user their external IP address on a UPnP enabled modem / router.

0.93b
* ManagedUPnP namespace no longer needs the ManagedUPnP.Descriptions namespace to compile, this means you can include ManagedUPnP into your project but exclude the entire Descriptions (and CodeGEn) folder if you dont need it (Code generation still needs the descriptions namespace however). All functions are still provided as extension methods within the Descriptions namespace, so no other code should need to be changed, just your using clause.
* All description related functions have been moved to ManagedUPnP.Descriptions as Extension Methods (so you may need to add using ManagedUPnP.Descriptions to some classes that you have written)
* AutoDiscoveryServices status notifications have changed - COMDeviceNetworkInterfaceGuid and FullRootDeviceDescription have been replaced with DeviceFound - Use ((Device)data).InterfaceGuidAvailable and ((Device)data).InterfaceGuid to get the network interface guid if available, and use ((Device)data).RootDescriptionCache.Cache[ldDevice].ToStringWithFullServices() to get the root description. This is to prevent the description from being loaded unless necessary.
* All description objects now have access to their Parent (Parent proeprty) description object as a polymorphic reference of type Description, a new protected method has been added to the description base class to get a certain generation of parent (see GetParentFrom). All constructors for the descriptions now require a parent to be passed, this can be null if no parent is valid. This change should not require any changes to dependent code unless you have created your own description objects (which should be very rare).
* The ArgumentDescription object now has a new property: RelatedStateVariableDescription, which will get the actual related state variable description object if the state variable information is available.
* The Discovery.FindServicesAsync incorrectly passed false for the resolveNetworkInterfaces parameter, this has been fixed by adding the parameter in and passing it to the FindDevicesAsync function call.
* Demonstration apps updated where applicable for any changes.


Compilation Requirements:
=========================
* Visual Studio 2010 Express for C# or better
* .NET framework 4
* Windows XP SP3 or above 
	
	!!!!!NOTE: If compiling the ManagedUPnP.DLL under Windows XP be sure the 
		"Development_WindowsXP" conditional define is defined. It does not matter
		what OS you compile it under, it will still run on all OS's from Windows XP SP3 
		and above. However when compiling under Windows XP you must have the define set.

		Right Click on ManagedUPnP Project - Properties - Build - Conditional compilation symbols =

			Development_WindowsXP

		For Windows XP Compilation or =

			Development_NotWindowsXP

		For window vista or above compilation

Demo Run Requirements:
======================

* .NET framework 4
* Windows XP SP3 or above 
* A UPnP enabled device or software

Please go here to post bugs or suggestions:
	http://managedupnp.codeplex.com/discussions/258346


Thankyou.