# bus\_rode MOD Develop Book (ZH-CN)

*Language:����,Chinese*

---
#### ����
Bus\_rode��һ���ܹ�չʾһ��������ӵ�й�������״����һ�����򣬵����ָ����ǵĳ��������һ���µ�Ҫ���ܹ��Ӳ�ʵʱ��Դ���������ܹ�ʹ���ǵĳ��и��ӷ��㡣�ʣ����ǿ�����һ���µĹ���ģ�飬���Կ��ٷ���ʵʱ��Դ��������bus\_rode֮�С�  
bus\_rode��һ���ƽ̨������Ϊ�˱�֤ÿ��ƽ̨�Ŀ����ԣ���������Ҫʹ�ò�ͬ��������д����ͬһ���ܵ�ģ�飬���½��г�����ƽ̨���ڱ�дģ������Ժͻ�ȡ�ĵط�����δ֧�ֵ�OS���ڴ��б��С���Ҳ������[bus\_rode\_all](https://github.com/yyc12345/bus_rode_all)��������ÿ�����̵�ģ��ģ�壩��  

|OS|Language|IDE|Link|Out file type|
|:---:|:---:|:---:|:---:|:---:|
|Windows|C# \[��C Sharp\] �� Visual Basic .NET|Visual Studio 2015|[bus\_rode\_windows](https://github.com/yyc12345/bus_rode_windows)|\*\.dll|

---
#### Ŀ¼
1. ��������Ҫ��
1. ������Ŀ����
1. �������
1. �ı�����
1. �ύ�뷢��
1. ���

---
#### ��������Ҫ��
1. Visual Studio 2015����ߵİ汾���汾��������桢רҵ�桭�������Ľ����汾��𣩲���
1. Windows 7����ߵĲ���ϵͳ���Ƽ�Windows 10

---
#### ������Ŀ����
bus\_rode��һ����ƽ̨����������ڲ�ͬ����£�������Ŀ�Ĺ�����������ͬ�����ǿ�����һϵ��ģ�幩ʹ�ã��⽫�������ٹ������̡�  
�����ֱ����һЩOS�Ļ�����Ŀ��������  

##### Windows
����ص�ַ����Ŀ��ȡ�󣬴���Ŀ���������ڽ��������Դ�������п���bus\_rode\_dll\_smaple��bus\_rode\_dll\_smaple\_csharp��2����Ŀ�����Ƕ����������ɶ�Ӧ��ģ�飬����Ч��һ����ֻ����ʹ�õ����Բ�ͬ��ѡ��һ������Ϥ�����ԣ���д��Ĵ��롣  
��������д��Ϻ�ϣ������dllʱ���뽫��ѡ���������������Ӧ��Ŀ������ģʽ��ΪRelease�����ɼ��ɣ���������ҵ����ɵ��ļ�������Ϊƾ���ľ��鲻���Ҳ����ġ�  
�ļ��ı����������������Ѿ���ģ������������ˣ�**���������޸�**��  
�������ơ��������ռ䡢Ӧ�ó������͵ȵȶ������úõģ������޸ģ��޸ĺ�һ���޷����ز����Ŀ��������bus\_rode�Ŀ�ܱ���һ�µģ����bus\_rode���õĿ��Ϊ.NET 4.5��Ҳ��Ҫ�޸ġ�����Ժ�������.NET�汾�ı��ˣ�ģ�����£���ʹ�����µ�ģ�崴��ģ�顣  
���ǣ�������Ϣ��һ���ǿ����޸ĵģ����еı��⡢˵���ȶ����Ը���������Ҫ�޸ģ��ò������ݲ��ᱻbus\_rode��ȡ��ֻ�ǻ���ʾ�� �ļ���Դ������ ֮�С�  

---
#### �������
���ǻ��ڽ�������ƪ���н���bus\_rode��ÿ��OS�ϵ�ģ��Ĵ���������Լ�һЩ��Ҫ��ע����� 
���������û���������룬���������������һ���Ķ�ע������ͽ�����

* Windows

**ͷ��**  
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
�˲��ִ�����������������ռ����ȣ���Щ�ǲ��������ĵģ�C#�����е�using������Ҫ���������Ӷ�Ӧ��usingָ���ֻ��using .NET�е��ڽ���������ռ䣬�����������ⲿ��⣨��������⣩��Visual Basic .NET ͬ����  

**���캯��**  
*C#*
```
//���캯��������һЩ���Խ��б�Ҫ������
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
'���캯��������һЩ���Խ��б�Ҫ������
Public Sub New()

    DllDependBusRodeVersion = 8000
    DllWriter = "Smaple Name"
    DllRegoin = "China-Unknow-Unknow"
    DllVersion = "1.0.0.0"
    DllGetTick = 5000

End Sub

```
�˲��ִ�������ڳ�ʼ��ģ��ʱ����Ҫ��д����Ϣ����Щ��Ϣ������ʾ��ģ����Ϣ�С�  
* DllDependBusRodeVersion��ʾ֧�ֵ�bus\_rode��build���֣�������Ҫ��֤ģ���ܹ���ȷ�ر���������ȷ��bus\_rode�ϣ����Ը�����������ģ����֧�ֵ�bus\_rode�汾����ͬ���ڰ汾�Ų�ͬʱ��ģ�鲻�ᱻ���أ�ֻ�ܿ���ģ����Ϣ��  
* DllWriter��ʾ��д�ߵ�������  
* DllRegoin��ʾ��ģ���ȡ�����Ǹ��ط���ʵʱ��Ϣ�����ı�Ӧ������Դ�ļ�readme.txt�еĵ�һ�У�ע�⣬���ǵڶ��У�������Դ���ڵ�ַ���ı�������ͬ��������ͬ��ģ��Ҳ���ᱻ���ء����ڸ������д���������Դ�����ֲ�readme.txt�йز��֡�  
* DllVersion��ʾģ��İ汾�š�  
* DllGetTick��ʾģ��ÿ��ȡһ����Դ�����ʱ�䣬�� ���� ��Ϊ��λ��1000����=1�룩������Ϊ500�ı���������Ϊ500�ı�����ʱ�򣬻���Int( DllGetTick / 500 )ȡ����������СֵΪ5000��5�룩��  
���������������������������ݣ�������Ҫ�ڳ�ʼ��ʱд��һЩֵ����������������Ҫ���Ӹ�����䣬��������������5����ֵ����Ǳز����ٵģ�����ɾȥ��

**��������**  
*C#*
```
//dll��������bus_rode�İ汾�ţ�ֻ�е��ڵ��õ�bus_rode�İ汾�ţ���dll�Żᱻ����
public int DllDependBusRodeVersion;
//dll������
public string DllWriter;
//dll������������ʽ������-ʡ/��-��/����
public string DllRegoin;
//dll�İ汾��
public string DllVersion;
//dllÿ�λ�ȡʵʱ��Դ����Ҫ�����ʱ�䣬�ú�������λ����������Ϊ5000(5��)
public int DllGetTick;

//dll��ʵ������ʱ����Ҫ��ȡ����·���ݵ���·��
public string DllUseBusLineName;

```
*VB*
```
'dll��������bus_rode�İ汾�ţ�ֻ�е��ڵ��õ�bus_rode�İ汾�ţ���dll�Żᱻ����
Public DllDependBusRodeVersion As Integer
'dll������
Public DllWriter As String
'dll������������ʽ������-ʡ/��-��/����
Public DllRegoin As String
'dll�İ汾��
Public DllVersion As String
'dllÿ�λ�ȡʵʱ��Դ����Ҫ�����ʱ�䣬�ú�������λ����������Ϊ5000(5��)
Public DllGetTick As Integer

'dll��ʵ������ʱ����Ҫ��ȡ����·���ݵ���·��
Public DllUseBusLineName As String

```
DllDependBusRodeVersion��DllWriter��DllRegoin��DllVersion��DllGetTick��ǰ���ѽ��ܹ���Ӧ���ã���������ֻ�����������ģ��������޸ġ�  
��������עһ��ʣ���2�  
DllUseBusLineName��ʾ��ǰ��Ҫ����ʵʱ��Ϣ����·���ƣ������Ƶ���Դ����Դ�ļ�have\_bus.txt�е���·�������ھ�������Ƹ�ʽ�����Բ�����Դ�����ֲ�have\_bus.txtһ�ڡ��ñ�����bus\_rodeÿһ�ε���ʱʵʱ�޸ģ��������������������ģ��У���Ҫʵʱ��ע�ñ�����ʹ�øñ�����ָ������·������ȡʵʱ��Դ���ñ����������κ�����·Ƿ��޸ģ�ֻ����bus\_rode�ĵ�������bus\_rode�����޸ġ�
��ȡ��·ʱ����Ҫע�������Ҫͬʱ��ȡ���еĺ����еġ�Ȼ��һ�����ء�  
���������Ҫ����һЩ����ȫ�ֱ��������Բ��������䣬���������䶼�Ǳ���ģ������޸ġ�  

**������ GetResources\(\)**  
*C#*
```
//dll�л�ȡ����ʵʱλ�õĵ�����ȡ���������л�ȡ������Ҫд������
public string GetResources()
{


    //���������Ҫʹ��һ��string�������滻 "" ���з���
    return "";

}

```
*VB*
```
'dll�л�ȡ����ʵʱλ�õĵ�����ȡ���������л�ȡ������Ҫд������
Public Function GetResources() As String


'���������Ҫʹ��һ��String�������滻 "" ���з���
        Return ""

End Function

```
�ú�����ÿ���DllGetTick��bus\_rode����һ�Σ��������Ҳ�ǻ�ȡ�ĺ��Ĳ��֣��ú�����bus\_rode�ڲ������˶��̴߳��������뵣�Ĺ���Ļ�ȡ�Ῠ��bus\_rode��
Return�����ص���һ����һ��������֯���ַ������ں������ǻ������������������Return֮��� ����Ӧ���滻Ϊһ����Ч��String�������ñ����洢�˰�����д�õ��ַ��������Դ��ؿ��ַ�������ʾ��ȡ������Դ��

---
#### �ı�����
��������Ҫ���ܷ����ı��ĸ�ʽ�����ص��ı��������ظø�ʽ�����򲻻ᱻʶ�𡣸ò��������а汾��bus\_rode��ͨ�ã������ϵͳ�޹ء�  
�����ı��ĸ�ʽָ����������Դ��ȡ���������ϲ�������������ص�String�����ı��ĸ�ʽ��  

##### �����﷨
>**��ģ��1@��ģ��2@��ģ��3@��ģ��4@......@��ģ��n**  

����@Ϊ**�ָ���**���ָ�ÿ��ģ�顣ÿһ�������ĳ�ģ���ʾһ������λ�ã���ģ���ڲ����и�ʽ���ں��Ľ��ܡ���ģ�������û�����ơ�  

##### ��ģ���﷨
>**LineName#Toward#PassStation#Message1#Message2#Message3**

����#Ϊ�ָ������ָ�ÿ�����ݣ����ָ��ÿ�����ֱ�ʾһ�����ݿ飬Ӧ����һ��ָ��������ʾ��  
���У�LineName��ʾҪ��������·������������Դ�ļ�have_bus.txt���е���·����Toward�ĺϷ��ֶ�Ϊ **0 �� 1** ��0��ʾΪ**����**��1��ʾΪ**����**������ʾ����������·�����еķ������ھ���������У������bus.txt�ж��������еĶ��塣  
PassStation�����ǵ�ǰ��������������һ��վ̨����������һվ�������Ҫע�⣬������ֵ�����**����**�����������������վ̨����PassStation����ֵ���ᱻ��д��mid\_bus�����в���ʾ�ڽ����У���ȷ����������ǻ���0��ʼ�ģ����Ҳ�Ҫ����200��������ֶ�Ӧ����·������վ̨�б���item��ţ����������������PassStation�����Բ鿴bus\_rode��Դ�롣
Message1��2��3�ֱ��Ǵ���ʱ��Ҫ���ӵ�˵��������Ϊ�գ�������Ҫ������Ϣʱ��ʽΪ��  
>LineName#Toward#StationName###

---
#### �ύ�뷢��
�������ȶ�֮�������Խ�������Ŀ�������CHMOSGroup�������ǲ鿴�����Ƿ�ϸ�����ںϸ���������Ȩ�������ģ�飬������ʹ�á�  
��ģ�鲻�ð����κζ�����룬Ҳ�������κι�����ɣ��������ǲ�����dll��Ϊģ����صķ�ʽ��������׳�Ϊ�����ߵ�Ŀ�꣬��������Ҳ���dll�ϼӼ�飬��ֹ�κβ����Ϲ淶��dll�����û������С�  
����bus\_rode�ǿ�ƽ̨�����������ֻΪĳһOS��д��ģ�飬��ô����ģ�齫ֻ֧����һOS����Ȼ���ǽ��龡���ܶ��֧�ָ���OS�������������ֲ��㣬���ǲ�����ֻ����ĳһOS��ģ�顣�����ύʱ����һ���ύ��OS��ģ�飬���û�����ʱ�����ǻ��֪�û���һģ������������ЩOS�ġ�

---
#### ���
��д��Ա��Tad Wiliam  
ʱ�䣺2016-4-22 23:40



