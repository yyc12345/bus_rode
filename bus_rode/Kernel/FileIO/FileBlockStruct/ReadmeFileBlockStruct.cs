using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using bus_rode.Kernel.Tools;

namespace bus_rode.Kernel.FileIO.FileBlockStruct {

    /// <summary>
    /// readme结构，支持completely读，completely写
    /// </summary>
    public class ReadmeFileBlockStruct : FileIO.IFileBlockStruct {

        public ReadmeFileBlockStruct() {
            addressGlobal = "";
            addressLocal = "";
            developer = "";
            releaseDate = "";
        }

        public bool SupportStepReader { get { return false; } }
        public bool SupportSeekReader { get { return false; } }
        public bool SupportBlockReader { get { return false; } }
        public bool SupportCompletelyReader { get { return true; } }

        public bool SupportStepWriter { get { return false; } }
        public bool SupportSeekWriter { get { return false; } }
        public bool SupportCompletelyWriter { get { return true; } }

        public string mainFilePath { get { return "\\library\\Readme.dat"; } }
        public string seekFilePath { get { return ""; } }
        public int stepReadLine { get { return 0; } }
        public int blockReadCount { get { return 1; } }

        /// <summary>
        /// 全球适应地址
        /// </summary>
        public string addressGlobal;
        /// <summary>
        /// 本地适应地址
        /// </summary>
        public string addressLocal;
        /// <summary>
        /// 开发者
        /// </summary>
        public string developer;
        /// <summary>
        /// 释放日期
        /// </summary>
        public string releaseDate;

        public Tools.StringGroup ConvertToStringGroup() {

            //处理文件数据
            ArrayList outContant = new ArrayList();

            outContant.Add(addressGlobal);
            outContant.Add(addressLocal);
            outContant.Add(developer);
            outContant.Add(releaseDate);

            return new StringGroup(outContant, Environment.NewLine);
        }

        public void ConvertToResources(Tools.StringGroup value) {
            if (value.sourceString == "") { return; }

            //处理文件数据
            string[] result = value.ToStringGroup();
            addressGlobal = result[0];
            addressLocal = result[1];
            developer = result[2];
            releaseDate = result[3];

        }

    }

}
