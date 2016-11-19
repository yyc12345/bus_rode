using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using bus_rode.Kernel.FileIO.FileBlockStruct;

namespace bus_rode.Kernel.ResourcesReflection {
    public class ResourcesDllReflection {

        public ResourcesDllReflection() {

            DllState = enumDllReflectionState.Unknow;
            DllDependBusRodeVersion = "";
            DllGetTick = "";
            DllRegion = "";
            DllVersion = "";
            DllWriter = "";

        }

        /// <summary>
        /// 完成初始化的反馈
        /// </summary>
        public Action FinishInitialization;

        /// <summary>
        /// 有新信息输入的反馈-line
        /// </summary>
        public Action<LineFileBlockStruct> NewDataWithLine;
        /// <summary>
        /// 有新信息输入的反馈-stop
        /// </summary>
        public Action<StopFileBlockStruct> NewDataWithStop;
        /// <summary>
        /// 有新信息输入的反馈-line list
        /// </summary>
        public Action<List<HaveLineFileBlockStruct>> NewDataWithLineList;
        /// <summary>
        /// 有新信息输入的反馈-stop list
        /// </summary>
        public Action<List<string>> NewDataWithStopList;
        /// <summary>
        /// 有新信息输入的反馈-subway
        /// </summary>
        public Action<SubwayFileBlockStruct> NewDataWithSubway;

        /// <summary>
        /// 往dll里面传入数据的命令行，存储指令在其中让dll处理之
        /// </summary>
        public FieldInfo Command;
        /// <summary>
        /// 反射所需要的获取得到的类
        /// </summary>
        public object ReflectionClass;


        /// <summary>
        /// null is unknow, true is disable, false is able
        /// </summary>
        public enumDllReflectionState DllState;
        /// <summary>
        /// 依赖版本
        /// </summary>
        public string DllDependBusRodeVersion;
        /// <summary>
        /// 地址
        /// </summary>
        public string DllRegion;
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

        public Task Read() {
            return Task.Run(() => {

            });
        }

        public void Stop() {
            throw new NotImplementedException();
        }
    }

}
