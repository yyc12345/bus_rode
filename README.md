# ![Icon](https://github.com/yyc12345/bus_rode/blob/master/icon.png) bus\_rode
This is a convenient application which can monitor bus and show city's public traffic.  

---
#### User enviroment  

**OS:** Windows 7 or over this\(NT kernel 6.1 or over this\)  
**Runtime library:** .NET Framework 4.5  

*TIPS:*
* Before you start using this application, you must download a dll which can be loaded by bus\_rode and provide local public traffic information.  
* If the monitor dll of the city where you stay has been released. You can download it and load it to monitor bus. It is can be more convenient that you use this monitor dll.  

---
#### Build enviroment  

**IDE:** Visual Studio 2015 Update 2  
**Language:** C\#\(C Sharp\)  
**Frame:** WPF  
**Nuget packages:**  
* MaterialDesignColors.1.1.3  
* MaterialDesignThemes.2.0.1-ci644  

*TIPS:*  
* You must have a local bus resource or a dll that can get the bus and stop information to test this application.  
* The kernel is separated from the user interface. So you can use this kernel to develop some application that are ran in the different platform, but I never promise the thing that all of this kernel's sentences can be compiled normally.\(I never have some encourge to develop a cross-platform application, but you can do it.\) Here are some points which avoid the cross-platform development.  
	* ResourcesFileCompression is depend on the format of windows file path  
	* All of reflection class is depend on the System.Reflection. This namespace is not exist in .Net Core perhaps  
* You must observe the LICENSE in the application which you fork this solution and continue developing, and the application's soure code and release file must include the LICENSE.  
* If you have some problem on reading code. Don't worried. Because my solution is messy. Please calm down and completely read.  

---
#### Recent plan  

* Finish ResourcesReflection class.  
* Create a project which can show the unsloved issues on that main application crashed. The invoke which in main application, need use \#IF DEBUG, because take consideration into debug.  
* Update the file which describe the command and output data's structure in MOD development.
* Create 4 smaple of MOD development. 2 is Monitor dll. 2 is Resources dll. And the one is mini version. The other is full version which include the method of processing command.

---
#### License

[MIT License](https://github.com/yyc12345/bus_rode/blob/master/LICENSE "MIT License")
