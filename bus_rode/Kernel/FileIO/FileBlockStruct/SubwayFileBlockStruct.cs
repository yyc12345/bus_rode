using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bus_rode.Kernel.Tools;
using System.Threading.Tasks;

namespace bus_rode.Kernel.FileIO.FileBlockStruct {

    /// <summary>
    /// subway结构，支持step/seek读，seek写
    /// </summary>
    public class SubwayFileBlockStruct : FileIO.IFileBlockStruct {

        public SubwayFileBlockStruct() {
            stopName = "";
            stopExit = new List<StringGroup>();
        }

        public bool SupportStepReader { get { return true; } }
        public bool SupportSeekReader { get { return true; } }
        public bool SupportBlockReader { get { return false; } }
        public bool SupportCompletelyReader { get { return false; } }

        public bool SupportStepWriter { get { return false; } }
        public bool SupportSeekWriter { get { return true; } }
        public bool SupportCompletelyWriter { get { return false; } }

        public string mainFilePath { get { return "\\library\\Subway.dat"; } }
        public string seekFilePath { get { return "\\library\\SubwaySeek.dat"; } }
        public int stepReadLine { get { return 2; } }
        public int blockReadCount { get { return 1; } }

        /// <summary>
        /// 站台名
        /// </summary>
        public string stopName;
        /// <summary>
        /// 站台拥有的出口，次级分隔符 #
        /// </summary>
        public List<StringGroup> stopExit;

        public Tools.StringGroup ConvertToStringGroup() {

            string exitCache = "";

            foreach (StringGroup item in stopExit) {
                exitCache += Environment.NewLine + item.ToString();
            }

            //不加回车是因为上面的输出会自带回车
            return new StringGroup(stopName + exitCache, Environment.NewLine);
        }

        public void ConvertToResources(Tools.StringGroup value) {
            if (value.sourceString == "") { return; }

            //处理文件数据
            string[] result = value.ToStringGroup();
            stopName = result[0];

            var cache = new StringGroup(result[1], ",");
            foreach (string item in cache.ToStringGroup()) {
                stopExit.Add(new StringGroup(item, "#"));
            }

        }

    }

}
