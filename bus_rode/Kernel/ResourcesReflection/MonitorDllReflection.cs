using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace bus_rode.Kernel.ResourcesReflection {
    public class MonitorDllReflection {

        public MonitorDllReflection() {
            MainThread = new System.Threading.Thread(ReadLoop);

            AvoidUpdate = false;
            ForceToUpdate = false;

            DllDisable = null;
            DllDependBusRodeVersion = "";
            DllGetTick = "";
            DllRegoin = "";
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
        /// 主循环线程
        /// </summary>
        public System.Threading.Thread MainThread;

        /// <summary>
        /// 设为true停止刷新
        /// </summary>
        public bool AvoidUpdate;
        /// <summary>
        /// 强制一次刷新，优先级高于Avoid，且刷新一次之后自动变为false
        /// </summary>
        public bool ForceToUpdate;

        /// <summary>
        /// null is unknow, true is disable, false is able
        /// </summary>
        public bool? DllDisable;
        /// <summary>
        /// 依赖版本
        /// </summary>
        public string DllDependBusRodeVersion;
        /// <summary>
        /// 地址
        /// </summary>
        public string DllRegoin;
        /// <summary>
        /// 获取间隔
        /// </summary>
        public string DllGetTick;
        /// <summary>
        /// 作者
        /// </summary>
        public string DllWriter;
        /// <summary>
        /// 版本
        /// </summary>
        public string DllVersion;

        public bool CheckFile() {
            throw new NotImplementedException();
        }

        public void Initialize() {
            throw new NotImplementedException();
        }

        public void ReadLoop() {
            throw new NotImplementedException();
        }

        public void Stop() {
            throw new NotImplementedException();
        }

        
    }

    public class MonitorBlockStruct {
        public string Id;
        public string Message1;
        public string Message2;
        public string Message3;
        public int BelongToStopIndex;
    }

}
