# Bus\_rode Resources Package File Format (ZH-CN)

*Language:中文,Chinese*

---
#### 概述
有时候，我们不希望重复下载本地公共交通信息，所以您可以在其下载完之后选择将其打包为资源压缩文件然后以用于后续的各种使用，需要时只需将其加载即可  
本文档旨在说明Bus\_rode在资源压缩文件中是如何将多个文件合并到一个文件中并加以分发的。本文件格式适用于Bus\_rode各个平台  

---
#### 文件格式

| 助记符 | 值类型 | 描述 |  
| :---: | :---: | :---: |  
| HEAD | Char(4) | 该**4**字符字段表示这是一个可用的资源文件，该值必须为**BRSP** ，否则Bus\_rode不会认为该资源可用 |  
| VER | Integer 64 | 该字段表示该资源适用于哪一个Bus\_rode版本，当此值与尝试加载资源的Bus\_rode版本号不符合的时候，资源不会被加载 |  
|-|-|-|  
|SIZE\_BUS|Integer 64|该值指示后文的Bus.txt文件所占区块的大小，**所有**的SIZE值都**必须依此顺序**填入，否则读取会发生错误|  
|SIZE\_BUSSEEK|Integer 64|该值指示后文的BusSeek.txt文件所占区块的大小|  
|SIZE\_HAVEBUS|Integer 64|该值指示后文的HaveBus.txt文件所占区块的大小|  
|SIZE\_README|Integer 64|该值指示后文的Readme.txt文件所占区块的大小|  
|SIZE\_STOP|Integer 64|该值指示后文的Stop.txt文件所占区块的大小|  
|SIZE\_STOPSEEK|Integer 64|该值指示后文的StopSeek.txt文件所占区块的大小|  
|SIZE\_SUBWAY|Integer 64|该值指示后文的Subway.txt文件所占区块的大小|  
|SIZE\_SUBWAYSEEK|Integer 64|该值指示后文的SubwaySeek.txt文件所占区块的大小|  
|SIZE\_GUIDELINE|Integer 64|该值指示后文的GuideLine.txt文件所占区块的大小|  
|SIZE\_GUIDELINESEEK|Integer 64|该值指示后文的GuideLineSeek.txt文件所占区块的大小|  
|-|-|-|  
|DATA\_BUS| Char() 长度由上文对应的SIZE\_BUS决定|资源文件Bus.txt的数据存储区域，该区域大小由上文的SIZE区块读取的数值决定，资源文件的存储顺序**必须依照此顺序**，否则无法读取|  
|DATA\_BUSSEEK| Char() 长度由上文对应的SIZE\_BUSSEEK决定|资源文件BusSeek.txt的数据存储区域|  
|DATA\_HAVEBUS| Char() 长度由上文对应的SIZE\_HAVEBUS决定|资源文件HaveBus.txt的数据存储区域|  
|DATA\_README| Char() 长度由上文对应的SIZE\_README决定|资源文件Readme.txt的数据存储区域|  
|DATA\_STOP| Char() 长度由上文对应的SIZE\_STOP决定|资源文件Stop.txt的数据存储区域|  
|DATA\_STOPSEEK| Char() 长度由上文对应的SIZE\_STOPSEEK决定|资源文件StopSeek.txt的数据存储区域|  
|DATA\_SUBWAY| Char() 长度由上文对应的SIZE\_SUBWAY决定|资源文件Subway.txt的数据存储区域|  
|DATA\_SUBWAYSEEK| Char() 长度由上文对应的SIZE\_SUBWAY决定|资源文件SubwaySeek.txt的数据存储区域|  
|DATA\_GUIDELINE| Char() 长度由上文对应的SIZE\_GUIDELINE决定|资源文件GuideLine.txt的数据存储区域|  
|DATA\_GUIDELINESEEK| Char() 长度由上文对应的SIZE\_GUIDELINESEEK决定|资源文件GuideLineSeek.txt的数据存储区域|  

TIPS:  
* 本表格中的值类型以 \.NET Framework中的数据类型所占用的大小为准，如若换为其余语言，则需要进行相关计算和查询以使用正确的数据类型
* 本表格中的一些格式可能在某些更改后改变，请注意及时查看有无最新版本的此文档发布，以防止资源无法加载
* 资源文件应当以UTF8有BOM格式进行保存和读取

---
#### 读取原理
本部分将大致介绍打包解包过程，如若需要详细内容，请查看[相关源代码](https://github.com/yyc12345/bus_rode/blob/master/bus_rode/Kernel/Tools/ResourcesFileCompression.cs)  

我们使用System.IO下的BinaryReader和BinaryWriter来进行相关读写操作，主要使用的方法：
* [BinaryWriter](https://msdn.microsoft.com/zh-cn/library/system.io.binarywriter(v=vs.110).aspx)
    * [Write\(Int64\)](https://msdn.microsoft.com/zh-cn/library/y8886cw7(v=vs.110).aspx)
    * [Write\(Char\(\)\)](https://msdn.microsoft.com/zh-cn/library/k13xbf58(v=vs.110).aspx)
* [BinaryReader](https://msdn.microsoft.com/zh-cn/library/system.io.binaryreader(v=vs.110).aspx)
    * [ReadChars\(Int32\)](https://msdn.microsoft.com/zh-cn/library/system.io.binaryreader.readchars(v=vs.110).aspx)
    * [ReadInt64\(\)](https://msdn.microsoft.com/zh-cn/library/system.io.binaryreader.readint64(v=vs.110).aspx)
* [String.ToCharArray\(\)](https://msdn.microsoft.com/zh-cn/library/ezftk57x(v=vs.110).aspx)
* New String\(Char\(\)\)

###### 打包
写入文件头\(2个值\)，按SIZE的顺序读取文件全部内容然后转换为一个Char\(\)并测量其长度，写入SIZE区块，依次读取完毕，然后再把数据依次写入DATA区块  
###### 解包
读取文件头，确认资源是否可以加载。按SIZE的顺序读取各个SIZE区块的值，然后按值依次读取DATA的内容并赋予固定的文件名。

---
#### 更新日志

###### 2016/4/2 12:09
创建文档

###### 2016/6/7 9:58
更改文件结构，因为bus\_rode需要的文件变化了

###### 2016/9/24 17:44
补充代码链接地址，修正说法。因为bus\_rode的资源获取方式有所更改

---
#### 后记
编写人员：Tad Wiliam  
时间：2016/6/7 9:58
