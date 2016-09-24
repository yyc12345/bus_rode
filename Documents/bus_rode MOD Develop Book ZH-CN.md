# bus\_rode MOD Develop Book (ZH-CN)

*Language:中文,Chinese*

---
#### 概述
Bus\_rode是一个能够展示一个城市所拥有公交车的状况的一个程序，但这又给我们的程序提出了一个新的要求，能够接驳实时资源，这样才能够使人们的出行更加方便。故，我们开发了一个新的功能模块，用以快速访问实时资源并呈现在bus\_rode之中。  
bus\_rode是一款跨平台软件，为了保证每个平台的可用性，您可能需要使用不同语言来编写具有同一功能的模块，以下将列出各个平台用于编写模块的语言和获取的地方（暂未支持的OS不在此列表中。您也可以在[bus\_rode\_all](https://github.com/yyc12345/bus_rode_all)里分类浏览每个工程的模块模板）。  

|OS|Language|IDE|Link|Out file type|
|:---:|:---:|:---:|:---:|:---:|
|Windows|C# \[即C Sharp\] 或 Visual Basic .NET|Visual Studio 2015|[bus\_rode\_windows](https://github.com/yyc12345/bus_rode_windows)|\*\.dll|

---
#### 目录
1. 开发环境要求
1. 基本项目构建
1. 代码解析
1. 文本规则
1. 提交与发布
1. 后记

---
#### 开发环境要求
1. Visual Studio 2015或更高的版本，版本类别（社区版、专业版…诸如此类的叫做版本类别）不限
1. Windows 7或更高的操作系统，推荐Windows 10

---
#### 基本项目构建
bus\_rode是一个跨平台软件，因此在不同情况下，基本项目的构建会有所不同，我们开发了一系列模板供使用，这将帮助快速构建工程。  
下面会分别介绍一些OS的基本项目构建方法  

##### Windows
从相关地址将项目获取后，打开项目，您可以在解决方案资源管理器中看到bus\_rode\_dll\_smaple和bus\_rode\_dll\_smaple\_csharp这2个项目，他们都可用于生成对应的模块，而且效果一样，只不过使用的语言不同，选择一个你熟悉的语言，编写你的代码。  
当您在书写完毕后，希望生成dll时，请将你选择的那门语言所对应项目的生成模式调为Release，生成即可，至于如何找到生成的文件，我认为凭您的经验不会找不到的。  
文件的编译的相关配置我们已经在模板中配置完毕了，**请勿擅自修改**。  
程序集名称、根命名空间、应用程序类型等等都是配置好的，不能修改，修改后一定无法加载插件。目标框架是与bus\_rode的框架保持一致的，这里，bus\_rode采用的框架为.NET 4.5，也不要修改。如果以后依赖的.NET版本改变了，模板会更新，请使用最新的模板创建模块。  
但是，程序集信息这一项是可以修改的，其中的标题、说明等都可以根据您的需要修改，该部分内容不会被bus\_rode读取，只是会显示在 文件资源管理器 之中。  

---
#### 代码解析
我们会在接下来的篇幅中介绍bus\_rode在每个OS上的模板的代码解析，以及一些重要的注意事项。 
代码解析里没有完整代码，建议跟随完整代码一起阅读注意事项和解析。

* Windows

**头部**  
*C#*
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_rode_dll
{
    public class main_dll
    {
```
*VB*
```
Public Class main_dll
```
此部分代码包含类名、命名空间名等，这些是不容许更改的，C#代码中的using若有需要，可以添加对应的using指令，但只限using .NET中的内建类或命名空间，不容许引用外部类库（第三方类库）（Visual Basic .NET 同样）  

**构造函数**  
*C#*
```
//构造函数，对于一些属性进行必要的设置
public main_dll()
{

    DllDependBusRodeVersion = 8000;
    DllWriter = "Smaple Name";
    DllRegoin = "China-Unknow-Unknow";
    DllVersion = "1.0.0.0";
    DllGetTick = 5000;

}
```
*VB*
```
'构造函数，对于一些属性进行必要的设置
Public Sub New()

    DllDependBusRodeVersion = 8000
    DllWriter = "Smaple Name"
    DllRegoin = "China-Unknow-Unknow"
    DllVersion = "1.0.0.0"
    DllGetTick = 5000

End Sub

```
此部分代码包含在初始化模块时所需要填写的信息，这些信息将会显示在模块信息中。  
* DllDependBusRodeVersion表示支持的bus\_rode的build数字，我们需要保证模块能够正确地被加载在正确的bus\_rode上，所以该数字务必与该模块所支持的bus\_rode版本号相同，在版本号不同时，模块不会被加载，只能看到模块信息。  
* DllWriter表示编写者的姓名。  
* DllRegoin表示该模块获取的是那个地方的实时信息，该文本应当与资源文件readme.txt中的第一行（注意，不是第二行），即资源所在地址的文本必须相同，若不相同，模块也不会被加载。至于该如何填写，请参阅资源开发手册readme.txt有关部分。  
* DllVersion表示模块的版本号。  
* DllGetTick表示模块每获取一次资源间隔的时间，以 毫秒 作为单位（1000毫秒=1秒）。尽量为500的倍数，当不为500的倍数的时候，会以Int( DllGetTick / 500 )取整。建议最小值为5000（5秒）。  
如果您在这个类中添加了其余内容，而且需要在初始化时写入一些值，可以依据您的需要增加更多语句，但以上所叙述的5个赋值语句是必不可少的，不得删去。

**变量定义**  
*C#*
```
//dll所依赖的bus_rode的版本号，只有等于调用的bus_rode的版本号，该dll才会被加载
public int DllDependBusRodeVersion;
//dll的作者
public string DllWriter;
//dll所属地区，格式：国家-省/州-县/城市
public string DllRegoin;
//dll的版本号
public string DllVersion;
//dll每次获取实时资源所需要间隔的时间，用毫秒做单位，建议最少为5000(5秒)
public int DllGetTick;

//dll在实际运行时所需要获取的线路数据的线路名
public string DllUseBusLineName;

```
*VB*
```
'dll所依赖的bus_rode的版本号，只有等于调用的bus_rode的版本号，该dll才会被加载
Public DllDependBusRodeVersion As Integer
'dll的作者
Public DllWriter As String
'dll所属地区，格式：国家-省/州-县/城市
Public DllRegoin As String
'dll的版本号
Public DllVersion As String
'dll每次获取实时资源所需要间隔的时间，用毫秒做单位，建议最少为5000(5秒)
Public DllGetTick As Integer

'dll在实际运行时所需要获取的线路数据的线路名
Public DllUseBusLineName As String

```
DllDependBusRodeVersion、DllWriter、DllRegoin、DllVersion和DllGetTick在前面已介绍过对应作用，这里的语句只是用来声明的，不允许修改。  
我们来关注一下剩余的2项。  
DllUseBusLineName表示当前需要返回实时信息的线路名称，该名称的来源是资源文件have\_bus.txt中的线路名，至于具体的名称格式，可以参阅资源开发手册have\_bus.txt一节。该变量由bus\_rode每一次调用时实时修改，所以在主函数（见后文）中，需要实时关注该变量，使用该变量中指定的线路名来获取实时资源。该变量不得在任何情况下非法修改，只能在bus\_rode的调用下由bus\_rode自身修改。
获取线路时还需要注意的是需要同时获取上行的和下行的。然后一并返回。  
如果您还需要定义一些其余全局变量，可以补充更多语句，但上面的语句都是必须的，不可修改。  

**主函数 GetResources\(\)**  
*C#*
```
//dll中获取车辆实时位置的的主获取函数，所有获取操作需要写在这里
public string GetResources()
{


    //根据你的需要使用一个string变量来替换 "" 进行返回
    return "";

}

```
*VB*
```
'dll中获取车辆实时位置的的主获取函数，所有获取操作需要写在这里
Public Function GetResources() As String


'根据你的需要使用一个String变量来替换 "" 进行返回
        Return ""

End Function

```
该函数是每间隔DllGetTick由bus\_rode调用一次，这个函数也是获取的核心部分，该函数在bus\_rode内部已作了多线程处理，无须担心过多的获取会卡死bus\_rode。
Return所返回的是一个按一定规则组织的字符串，在后文我们会介绍如何遵守这个规则，Return之后的 ””应当替换为一个有效的String变量，该变量存储了按规则写好的字符串，可以传回空字符串，表示获取不到资源。

---
#### 文本规则
本部分主要介绍返回文本的格式，返回的文本必须遵守该格式，否则不会被识别。该部分在所有版本的bus\_rode中通用，与操作系统无关。  
这里文本的格式指的是由主资源获取函数（见上部代码解析）返回的String类型文本的格式。  

##### 基本语法
>**车模块1@车模块2@车模块3@车模块4@......@车模块n**  

其中@为**分隔符**，分隔每个模块。每一个单独的车模块表示一辆车的位置，车模块内部还有格式，在后文介绍。车模块的数量没有限制。  

##### 车模块语法
>**LineName#Toward#PassStation#Message1#Message2#Message3**

其中#为分隔符，分隔每个数据，被分割的每个部分表示一个数据块，应当由一个指定的量表示。  
其中，LineName表示要描述的线路名，必须是资源文件have_bus.txt中有的线路名。Toward的合法字段为 **0 或 1** ，0表示为**上行**，1表示为**下行**。他表示这辆车在线路中运行的方向，至于具体的上下行，须符合bus.txt中对于上下行的定义。  
PassStation意义是当前车到达过的最近的一个站台，而不是下一站，这点需要注意，这个部分的文字**必须**是填入这个车经过的站台数。PassStation的数值将会被填写入mid\_bus数组中并显示在界面中，请确保这个数字是基于0起始的，而且不要大于200，这个数字对应在线路界面中站台列表的item序号，如果您还不能理解PassStation，可以查看bus\_rode的源码。
Message1、2、3分别是传输时需要附加的说明，可以为空，即不需要传输信息时格式为：  
>LineName#Toward#StationName###

---
#### 提交与发布
当测试稳定之后，您可以将整个项目打包交付CHMOSGroup，由我们查看代码是否合格，最后在合格的情况下授权发布这个模块，给公众使用。  
该模块不得包含任何恶意代码，也不得有任何广告嫌疑，由于我们采用了dll作为模块加载的方式，这很容易成为攻击者的目标，所以我们也会对dll严加检查，防止任何不符合规范的dll流入用户的手中。  
由于bus\_rode是跨平台软件，如果您只为某一OS编写了模块，那么您的模块将只支持这一OS，虽然我们建议尽可能多地支持更多OS，但假若您人手不足，我们不介意只开发某一OS的模块。您在提交时可以一次提交多OS的模块，在用户下载时，我们会告知用户这一模块是适用于哪些OS的。

---
#### 后记
编写人员：Tad Wiliam  
时间：2016-4-22 23:40




