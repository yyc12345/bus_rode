# Bus\_rode Resources Package File Format (ZH-CN)

*Language:����,Chinese*

---
#### ����
��ʱ�����ǲ�ϣ���ظ����ر��ع�����ͨ��Ϣ����������������������֮��ѡ������Ϊ��Դѹ���ļ�Ȼ�������ں����ĸ���ʹ�ã���Ҫʱֻ�轫����ؼ���  
���ĵ�ּ��˵��Bus\_rode����Դѹ���ļ�������ν�����ļ��ϲ���һ���ļ��в����Էַ��ġ����ļ���ʽ������Bus\_rode����ƽ̨  

---
#### �ļ���ʽ

| ���Ƿ� | ֵ���� | ���� |  
| :---: | :---: | :---: |  
| HEAD | Char(4) | ��**4**�ַ��ֶα�ʾ����һ�����õ���Դ�ļ�����ֵ����Ϊ**BRSP** ������Bus\_rode������Ϊ����Դ���� |  
| VER | Integer 64 | ���ֶα�ʾ����Դ��������һ��Bus\_rode�汾������ֵ�볢�Լ�����Դ��Bus\_rode�汾�Ų����ϵ�ʱ����Դ���ᱻ���� |  
|-|-|-|  
|SIZE\_BUS|Integer 64|��ֵָʾ���ĵ�Bus.txt�ļ���ռ����Ĵ�С��**����**��SIZEֵ��**��������˳��**���룬�����ȡ�ᷢ������|  
|SIZE\_BUSSEEK|Integer 64|��ֵָʾ���ĵ�BusSeek.txt�ļ���ռ����Ĵ�С|  
|SIZE\_HAVEBUS|Integer 64|��ֵָʾ���ĵ�HaveBus.txt�ļ���ռ����Ĵ�С|  
|SIZE\_README|Integer 64|��ֵָʾ���ĵ�Readme.txt�ļ���ռ����Ĵ�С|  
|SIZE\_STOP|Integer 64|��ֵָʾ���ĵ�Stop.txt�ļ���ռ����Ĵ�С|  
|SIZE\_STOPSEEK|Integer 64|��ֵָʾ���ĵ�StopSeek.txt�ļ���ռ����Ĵ�С|  
|SIZE\_SUBWAY|Integer 64|��ֵָʾ���ĵ�Subway.txt�ļ���ռ����Ĵ�С|  
|SIZE\_SUBWAYSEEK|Integer 64|��ֵָʾ���ĵ�SubwaySeek.txt�ļ���ռ����Ĵ�С|  
|SIZE\_GUIDELINE|Integer 64|��ֵָʾ���ĵ�GuideLine.txt�ļ���ռ����Ĵ�С|  
|SIZE\_GUIDELINESEEK|Integer 64|��ֵָʾ���ĵ�GuideLineSeek.txt�ļ���ռ����Ĵ�С|  
|-|-|-|  
|DATA\_BUS| Char() ���������Ķ�Ӧ��SIZE\_BUS����|��Դ�ļ�Bus.txt�����ݴ洢���򣬸������С�����ĵ�SIZE�����ȡ����ֵ��������Դ�ļ��Ĵ洢˳��**�������մ�˳��**�������޷���ȡ|  
|DATA\_BUSSEEK| Char() ���������Ķ�Ӧ��SIZE\_BUSSEEK����|��Դ�ļ�BusSeek.txt�����ݴ洢����|  
|DATA\_HAVEBUS| Char() ���������Ķ�Ӧ��SIZE\_HAVEBUS����|��Դ�ļ�HaveBus.txt�����ݴ洢����|  
|DATA\_README| Char() ���������Ķ�Ӧ��SIZE\_README����|��Դ�ļ�Readme.txt�����ݴ洢����|  
|DATA\_STOP| Char() ���������Ķ�Ӧ��SIZE\_STOP����|��Դ�ļ�Stop.txt�����ݴ洢����|  
|DATA\_STOPSEEK| Char() ���������Ķ�Ӧ��SIZE\_STOPSEEK����|��Դ�ļ�StopSeek.txt�����ݴ洢����|  
|DATA\_SUBWAY| Char() ���������Ķ�Ӧ��SIZE\_SUBWAY����|��Դ�ļ�Subway.txt�����ݴ洢����|  
|DATA\_SUBWAYSEEK| Char() ���������Ķ�Ӧ��SIZE\_SUBWAY����|��Դ�ļ�SubwaySeek.txt�����ݴ洢����|  
|DATA\_GUIDELINE| Char() ���������Ķ�Ӧ��SIZE\_GUIDELINE����|��Դ�ļ�GuideLine.txt�����ݴ洢����|  
|DATA\_GUIDELINESEEK| Char() ���������Ķ�Ӧ��SIZE\_GUIDELINESEEK����|��Դ�ļ�GuideLineSeek.txt�����ݴ洢����|  

TIPS:  
* ������е�ֵ������ \.NET Framework�е�����������ռ�õĴ�СΪ׼��������Ϊ�������ԣ�����Ҫ������ؼ���Ͳ�ѯ��ʹ����ȷ����������
* ������е�һЩ��ʽ������ĳЩ���ĺ�ı䣬��ע�⼰ʱ�鿴�������°汾�Ĵ��ĵ��������Է�ֹ��Դ�޷�����
* ��Դ�ļ�Ӧ����UTF8��BOM��ʽ���б���Ͷ�ȡ

---
#### ��ȡԭ��
�����ֽ����½��ܴ��������̣�������Ҫ��ϸ���ݣ���鿴[���Դ����](https://github.com/yyc12345/bus_rode/blob/master/bus_rode/Kernel/Tools/ResourcesFileCompression.cs)  

����ʹ��System.IO�µ�BinaryReader��BinaryWriter��������ض�д��������Ҫʹ�õķ�����
* [BinaryWriter](https://msdn.microsoft.com/zh-cn/library/system.io.binarywriter(v=vs.110).aspx)
    * [Write\(Int64\)](https://msdn.microsoft.com/zh-cn/library/y8886cw7(v=vs.110).aspx)
    * [Write\(Char\(\)\)](https://msdn.microsoft.com/zh-cn/library/k13xbf58(v=vs.110).aspx)
* [BinaryReader](https://msdn.microsoft.com/zh-cn/library/system.io.binaryreader(v=vs.110).aspx)
    * [ReadChars\(Int32\)](https://msdn.microsoft.com/zh-cn/library/system.io.binaryreader.readchars(v=vs.110).aspx)
    * [ReadInt64\(\)](https://msdn.microsoft.com/zh-cn/library/system.io.binaryreader.readint64(v=vs.110).aspx)
* [String.ToCharArray\(\)](https://msdn.microsoft.com/zh-cn/library/ezftk57x(v=vs.110).aspx)
* New String\(Char\(\)\)

###### ���
д���ļ�ͷ\(2��ֵ\)����SIZE��˳���ȡ�ļ�ȫ������Ȼ��ת��Ϊһ��Char\(\)�������䳤�ȣ�д��SIZE���飬���ζ�ȡ��ϣ�Ȼ���ٰ���������д��DATA����  
###### ���
��ȡ�ļ�ͷ��ȷ����Դ�Ƿ���Լ��ء���SIZE��˳���ȡ����SIZE�����ֵ��Ȼ��ֵ���ζ�ȡDATA�����ݲ�����̶����ļ�����

---
#### ������־

###### 2016/4/2 12:09
�����ĵ�

###### 2016/6/7 9:58
�����ļ��ṹ����Ϊbus\_rode��Ҫ���ļ��仯��

###### 2016/9/24 17:44
����������ӵ�ַ������˵������Ϊbus\_rode����Դ��ȡ��ʽ��������

---
#### ���
��д��Ա��Tad Wiliam  
ʱ�䣺2016/6/7 9:58
