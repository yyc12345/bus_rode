using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bus_rode.Kernel.Tools;

namespace bus_rode.Kernel.FileIO.FileBlockStruct {

    /// <summary>
    /// stop结构，支持seek/step/block读，seek写
    /// </summary>
    public class StopFileBlockStruct : FileIO.IFileBlockStruct {

        public StopFileBlockStruct() {
            stopName = "";
            stopCrossLine = new StringGroup("", "");
        }

        public bool SupportStepReader { get { return true; } }
        public bool SupportSeekReader { get { return true; } }
        public bool SupportBlockReader { get { return true; } }
        public bool SupportCompletelyReader { get { return false; } }

        public bool SupportStepWriter { get { return false; } }
        public bool SupportSeekWriter { get { return true; } }
        public bool SupportCompletelyWriter { get { return false; } }

        public string mainFilePath { get { return "\\library\\Stop.dat"; } }
        public string seekFilePath { get { return "\\library\\StopSeek.dat"; } }
        public int stepReadLine { get { return 2; } }
        public int blockReadCount { get { return 100; } }

        /// <summary>
        /// 站台名
        /// </summary>
        public string stopName;
        /// <summary>
        /// 站台经过车次
        /// </summary>
        public StringGroup stopCrossLine;

        public Tools.StringGroup ConvertToStringGroup() {
            return new StringGroup(stopName + Environment.NewLine + stopCrossLine, Environment.NewLine);
        }

        public void ConvertToResources(Tools.StringGroup value) {
            if (value.sourceString == "") { return; }

            //处理文件数据
            string[] result = value.ToStringGroup();
            stopName = result[0];
            stopCrossLine = new StringGroup(result[1], ",");

        }

    }

}
