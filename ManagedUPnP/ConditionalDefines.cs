// Conditional Defines for ManagedUPnP:
//
//  * Development_WindowsXP - Define this if compiling ManagedUPnP under Windows XP, exclude if compiling under Windows Vista or above
//      NOTE: As long as you have the correct define set when compiling, the ManagedUPnP library will still load and run under any
//      OS from Windows XP above.
//
//  * Exclude_CodeGen - Define to exclude ManagedUPnP.CodeGen namespace from compilation (Code Generation)
//      NOTE: This namespace is only required for actually generating the code for classes, it is not required
//      for you to use pre-generated classes in your project.
//
//  * Exclude_Components - Define to exclude ManagedUPnP.Components namespace from compilation (Windows Forms Components)
//      NOTE: This namespace is only required if you are running under a Windows.Forms environment and wish to use
//      the components in your program.
//
//  * Exclude_Descriptions - Define to exclude ManagedUPnP.Descriptions namespace from compilation (Descriptions)
//      NOTE: Descriptions are required if CodeGen is being compiled, the Descriptions namespace will automatically be
//      included in the compile unless the Exclude_CodeGen compilation define is defined as well.
//
//  * !Exclude_WindowsFirewall - Define to exclude Managed.WindowsFirewall class from compilation.
