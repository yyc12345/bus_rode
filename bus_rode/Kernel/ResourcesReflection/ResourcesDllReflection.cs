using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using bus_rode.Kernel.Tools;
using bus_rode.Kernel.FileIO.FileBlockStruct;

namespace bus_rode.Kernel.ResourcesReflection {
    public class ResourcesDllReflection {

        public ResourcesDllReflection() {

            DllState = enumDllReflectionState.Unknow;
            DllDependBusRodeVersion = 0;
            DllRegion = "";
            DllVersion = "";
            DllWriter = "";

        }

        /// <summary>
        /// 完成初始化的反馈
        /// </summary>
        public Action FinishInitialization;

        ///// <summary>
        ///// 有新信息输入的反馈-line
        ///// </summary>
        //public Action<LineFileBlockStruct> NewDataWithLine;
        ///// <summary>
        ///// 有新信息输入的反馈-stop
        ///// </summary>
        //public Action<StopFileBlockStruct> NewDataWithStop;
        ///// <summary>
        ///// 有新信息输入的反馈-line list
        ///// </summary>
        //public Action<List<HaveLineFileBlockStruct>> NewDataWithLineList;
        ///// <summary>
        ///// 有新信息输入的反馈-stop list
        ///// </summary>
        //public Action<List<string>> NewDataWithStopList;
        ///// <summary>
        ///// 有新信息输入的反馈-subway
        ///// </summary>
        //public Action<SubwayFileBlockStruct> NewDataWithSubway;

        /// <summary>
        /// 往dll里面传入数据的命令行，存储指令在其中让dll处理之
        /// </summary>
        public FieldInfo Command;
        /// <summary>
        /// 反射所需要的获取得到的类
        /// </summary>
        public object ReflectionClass;
        /// <summary>
        /// dll中初始化的方法
        /// </summary>
        public MethodInfo MethodInitialize;
        /// <summary>
        /// dll中获取数据的方法
        /// </summary>
        public MethodInfo MethodGetData;

        /// <summary>
        /// dll state
        /// </summary>
        public enumDllReflectionState DllState;
        /// <summary>
        /// 依赖版本
        /// </summary>
        public int DllDependBusRodeVersion;
        /// <summary>
        /// 地址
        /// </summary>
        public string DllRegion;
        /// <summary>
        /// 作者
        /// </summary>
        public string DllWriter;
        /// <summary>
        /// 版本
        /// </summary>
        public string DllVersion;


        /// <summary>
        /// 检查文件过程，如果出错，返回错误描述
        /// </summary>
        /// <param name="installedRegion">要检查的地区名</param>
        /// <returns></returns>
        public string CheckFile(string installedRegion) {
            //check file
            if (!System.IO.File.Exists(Kernel.Tools.SystemInformation.WorkingPath + "ResourcesDll.dll")) {
                DllState = enumDllReflectionState.Illegal;
                return Language.GetItem("langCodeReflectionNoFile");
            }

            //check basic function
            var ass = System.Reflection.Assembly.LoadFile(Kernel.Tools.SystemInformation.WorkingPath + "ResourcesDll.dll");
            Type tp;
            FieldInfo dependVersion, region, writer, version, command;
            MethodInfo initialize, getData;
            try {
                tp = ass.GetType("BusRodeDll.ResourcesDll", true);

                dependVersion = tp.GetField("DllDependBusRodeVersion");
                region = tp.GetField("DllRegion");
                writer = tp.GetField("DllWriter");
                version = tp.GetField("DllVersion");

                command = tp.GetField("DllCommand");

                initialize = tp.GetMethod("Initialize", BindingFlags.Instance | BindingFlags.Public);
                getData = tp.GetMethod("GetData", BindingFlags.Instance | BindingFlags.Public);

            } catch (Exception) {
                DllState = enumDllReflectionState.Illegal;
                return Language.GetItem("langCodeReflectionNoFunction");
            }

            //set instance
            ReflectionClass = Activator.CreateInstance(tp);

            //check detail
            if (Kernel.Tools.ApplicationInformation.AppBuildNumber != System.Convert.ToInt32(dependVersion.GetValue(ReflectionClass))) {
                DllState = enumDllReflectionState.Illegal;
                return Language.GetItem("langCodeReflectionMistakenDependVersion");
            }
            if (installedRegion != region.GetValue(ReflectionClass).ToString()) {
                DllState = enumDllReflectionState.Illegal;
                return Language.GetItem("langCodeReflectionMistakenLocation");
            }

            //write message
            DllDependBusRodeVersion = System.Convert.ToInt32(dependVersion.GetValue(ReflectionClass));
            DllRegion = region.GetValue(ReflectionClass).ToString();
            DllWriter = writer.GetValue(ReflectionClass).ToString();
            DllVersion = version.GetValue(ReflectionClass).ToString();

            Command = command;
            MethodInitialize = initialize;
            MethodGetData = getData;

            DllState = enumDllReflectionState.Legal;
            return "";
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public Task Initialize() {
            if (DllState != enumDllReflectionState.Legal) throw new InvalidOperationException();

            DllState = enumDllReflectionState.Initiatlizing;
            return Task.Run(() => {
                var result = System.Convert.ToBoolean(MethodInitialize.Invoke(ReflectionClass, null));

                if (result) DllState = enumDllReflectionState.Ready;
                else DllState = enumDllReflectionState.Disabled;

                FinishInitialization();
            });
        }

        /// <summary>
        /// 返回仅分割后的结果
        /// </summary>
        /// <returns></returns>
        public Task Read() {
            return Task.Run(() => {

                string result = "";
                try {
                    result = MethodGetData.Invoke(ReflectionClass, null).ToString();
                } catch (Exception) {
                    return new List<string>();
                }

                if (result == "") return new List<string>();

                //set list
                return new StringGroup(result,"@").ToList();
            });
        }

    }

}
