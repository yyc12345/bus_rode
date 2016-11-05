using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bus_rode.Kernel.Tools;

namespace bus_rode.Kernel.FileIO.FileBlockStruct {

    /// <summary>
    /// have line结构，支持block/step读，step写
    /// </summary>
    public class HaveLineFileBlockStruct : FileIO.IFileBlockStruct {

        public HaveLineFileBlockStruct() {
            lineName = "";
        }

        public bool SupportStepReader { get { return true; } }
        public bool SupportSeekReader { get { return false; } }
        public bool SupportBlockReader { get { return true; } }
        public bool SupportCompletelyReader { get { return false; } }

        public bool SupportStepWriter { get { return true; } }
        public bool SupportSeekWriter { get { return false; } }
        public bool SupportCompletelyWriter { get { return false; } }

        public string mainFilePath { get { return "\\library\\HaveBus.dat"; } }
        public string seekFilePath { get { return ""; } }
        public int stepReadLine { get { return 1; } }
        public int blockReadCount { get { return 200; } }

        /// <summary>
        /// 线路名
        /// </summary>
        public string lineName;


        public Tools.StringGroup ConvertToStringGroup() {
            return new StringGroup(lineName, Environment.NewLine);
        }

        public void ConvertToResources(Tools.StringGroup value) {
            if (value.sourceString == "") { return; }

            lineName = value.ToString();
        }


    }
}
