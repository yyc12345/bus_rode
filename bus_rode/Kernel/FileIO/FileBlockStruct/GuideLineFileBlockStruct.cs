using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bus_rode.Kernel.Tools;

namespace bus_rode.Kernel.FileIO.FileBlockStruct {

    /// <summary>
    /// guideline结构，支持seek/step读，seek写
    /// </summary>
    public class GuideLineBlockStruct : FileIO.IFileBlockStruct {

        public GuideLineBlockStruct() {
            lineName = "";
            lineCrossLine = new StringGroup("", "");
        }

        public bool SupportStepReader { get { return true; } }
        public bool SupportSeekReader { get { return true; } }
        public bool SupportBlockReader { get { return false; } }
        public bool SupportCompletelyReader { get { return false; } }

        public bool SupportStepWriter { get { return false; } }
        public bool SupportSeekWriter { get { return true; } }
        public bool SupportCompletelyWriter { get { return false; } }

        public string mainFilePath { get { return "\\library\\GuideLine.dat"; } }
        public string seekFilePath { get { return "\\library\\GuideLineSeek.dat"; } }
        public int stepReadLine { get { return 2; } }
        public int blockReadCount { get { return 1; } }

        /// <summary>
        /// 站台名
        /// </summary>
        public string lineName;
        /// <summary>
        /// 站台经过车次
        /// </summary>
        public StringGroup lineCrossLine;

        public Tools.StringGroup ConvertToStringGroup() {
            return new StringGroup(lineName + Environment.NewLine + lineCrossLine, Environment.NewLine);
        }

        public void ConvertToResources(Tools.StringGroup value) {
            if (value.sourceString == "") { return; }

            //处理文件数据
            string[] result = value.ToStringGroup();
            lineName = result[0];
            lineCrossLine = new StringGroup(result[1], ",");

        }

    }


}
