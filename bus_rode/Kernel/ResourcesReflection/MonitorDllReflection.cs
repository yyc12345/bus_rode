using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using bus_rode.Kernel.Tools;

namespace bus_rode.Kernel.ResourcesReflection {
    public class MonitorDllReflection {

        public MonitorDllReflection() {
            MainThread = new System.Threading.Thread(ReadLoop);

            DllState = enumDllReflectionState.Unknow;
            DllDependBusRodeVersion = 0;
            DllGetTick = 5000;
            DllRegion = "";
            DllVersion = "";
            DllWriter = "";

        }

        /// <summary>
        /// 完成初始化的反馈
        /// </summary>
        public Action FinishInitialization;
        /// <summary>
        /// 有新信息输入的反馈
        /// </summary>
        public Action<List<Kernel.Management.UserInterfaceBlockStruct.LinePageRuntimeBlockStruct>> NewData;

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
        /// 主循环线程
        /// </summary>
        public System.Threading.Thread MainThread;

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
        /// 获取间隔
        /// </summary>
        public int DllGetTick;
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
            if (DllState != enumDllReflectionState.Unknow) throw new InvalidOperationException();

            //check file
            if (!System.IO.File.Exists(Kernel.Tools.SystemInformation.WorkingPath + "MonitorDll.dll")) {
                DllState = enumDllReflectionState.Illegal;
                return Language.GetItem("langCodeReflectionNoFile");
            }

            //check basic function
            var ass = System.Reflection.Assembly.LoadFile(Kernel.Tools.SystemInformation.WorkingPath + "MonitorDll.dll");
            Type tp;
            FieldInfo dependVersion, region, tick, writer, version, command;
            MethodInfo initialize, getData;
            try {
                tp = ass.GetType("BusRodeDll.MonitorDll", true);

                dependVersion = tp.GetField("DllDependBusRodeVersion");
                region = tp.GetField("DllRegion");
                tick = tp.GetField("DllGetTick");
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
            DllGetTick = System.Convert.ToInt32(tick.GetValue(ReflectionClass));
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
        /// 开始阅读
        /// </summary>
        public void StartReading() {
            if (DllState != enumDllReflectionState.Ready) throw new InvalidOperationException();
            DllState = enumDllReflectionState.Running;
            MainThread.Start();
        }

        /// <summary>
        /// 阅读循环
        /// </summary>
        private void ReadLoop() {

            string result = "";
            while (true) {

                //尝试获取，获取失败返回
                try {
                    result = MethodGetData.Invoke(ReflectionClass, null).ToString();
                } catch (Exception) {
                    continue;
                }

                if (result == "") continue;

                //set list
                var list = new List<Kernel.Management.UserInterfaceBlockStruct.LinePageRuntimeBlockStruct>();
                foreach (var item in new StringGroup(result, "@").ToStringGroup()) {
                    var temp = new StringGroup(item, "#").ToStringGroup();
                    list.Add(new Management.UserInterfaceBlockStruct.LinePageRuntimeBlockStruct() {
                        ID = temp[0],
                        IsUpLine = temp[1] == "0" ? true : false,
                        BelongToStopIndex = System.Convert.ToInt32(temp[2]),
                        Message1 = temp[3],
                        Message2 = temp[4],
                        Message3 = temp[5]
                    });
                }

                NewData(list);

            }
        }

        /// <summary>
        /// 强制一次获取，必须在停止主循环的前提下调用，且通过此函数返回数据而不使用委托
        /// </summary>
        /// <returns></returns>
        public List<Kernel.Management.UserInterfaceBlockStruct.LinePageRuntimeBlockStruct> ForceToGet() {
            if (DllState != enumDllReflectionState.Ready) throw new InvalidOperationException();

            string result = "";
            try {
                result = MethodGetData.Invoke(ReflectionClass, null).ToString();
            } catch (Exception) {
                return new List<Management.UserInterfaceBlockStruct.LinePageRuntimeBlockStruct>();
            }

            if (result == "") return new List<Management.UserInterfaceBlockStruct.LinePageRuntimeBlockStruct>();

            //set list
            var list = new List<Kernel.Management.UserInterfaceBlockStruct.LinePageRuntimeBlockStruct>();
            foreach (var item in new StringGroup(result, "@").ToStringGroup()) {
                var temp = new StringGroup(item, "#").ToStringGroup();
                list.Add(new Management.UserInterfaceBlockStruct.LinePageRuntimeBlockStruct() {
                    ID = temp[0],
                    IsUpLine = temp[1] == "0" ? true : false,
                    BelongToStopIndex = System.Convert.ToInt32(temp[2]),
                    Message1 = temp[3],
                    Message2 = temp[4],
                    Message3 = temp[5]
                });
            }

            return list;
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop() {
            if (DllState != enumDllReflectionState.Running) {
                throw new InvalidOperationException();
            } else {
                try {
                    MainThread.Abort();
                } catch (Exception) {

                }

                DllState = enumDllReflectionState.Ready;
            }
        }


    }


}
