using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bus_rode.Kernel.Tools;
using bus_rode.Kernel.FileIO.FileOperation;
using System.Collections;

namespace bus_rode.Kernel.FileIO.FileBlockStruct {

    /// <summary>
    /// 线路种类的枚举
    /// </summary>
    public enum enumLineType {
        /// <summary>
        /// 公交车
        /// </summary>
        Bus = 0,
        /// <summary>
        /// 地铁
        /// </summary>
        Subway = 1
    }

    /// <summary>
    /// 线路等待的枚举
    /// </summary>
    public enum enumLineWait {
        /// <summary>
        /// 极难等(30min以上1班)
        /// </summary>
        SoHard = 0,
        /// <summary>
        /// 难等(16-30min 1班)
        /// </summary>
        Hard = 1,
        /// <summary>
        /// 适中(6-15min 1班)
        /// </summary>
        JustSoSo = 2,
        /// <summary>
        /// 易等(5-2min 1班)
        /// </summary>
        Easy = 3,
        /// <summary>
        /// 极易等(2min 1班或更多班次)
        /// </summary>
        SoEasy = 4
    }

    /// <summary>
    /// bus文件结构：支持step/seek读，seek写
    /// </summary>
    public class LineFileBlockStruct : FileIO.IFileBlockStruct {

        public LineFileBlockStruct() {
            lineName = "";
            lineMessage = new string[5];
            lineType = enumLineType.Bus;
            lineWait = enumLineWait.JustSoSo;
            lineRuntime = new int[4];
            lineUpStop = new StringGroup("", "");
            lineDownStop = new StringGroup("", "");
        }

        public bool SupportStepReader { get { return true; } }
        public bool SupportSeekReader { get { return true; } }
        public bool SupportBlockReader { get { return false; } }
        public bool SupportCompletelyReader { get { return false; } }

        public bool SupportStepWriter { get { return false; } }
        public bool SupportSeekWriter { get { return true; } }
        public bool SupportCompletelyWriter { get { return false; } }

        public string mainFilePath { get { return "\\library\\Bus.dat"; } }
        public string seekFilePath { get { return "\\library\\BusSeek.dat"; } }
        public int stepReadLine { get { return 14; } }
        public int blockReadCount { get { return 1; } }

        /// <summary>
        /// 线路名
        /// </summary>
        public string lineName;
        /// <summary>
        /// 线路信息 0-4 共5条
        /// </summary>
        public string[] lineMessage;
        /// <summary>
        /// 线路种类
        /// </summary>
        public enumLineType lineType;
        /// <summary>
        /// 车辆的可等待性
        /// </summary>
        public enumLineWait lineWait;
        /// <summary>
        /// 线路运行时间[0]=始发小时 [1]=始发分钟 [2]=末班小时 [3]=末班分钟
        /// </summary>
        public int[] lineRuntime;
        /// <summary>
        /// 线路上行站台
        /// </summary>
        public Kernel.Tools.StringGroup lineUpStop;
        /// <summary>
        /// 线路下行站台
        /// </summary>
        public Kernel.Tools.StringGroup lineDownStop;

        public Tools.StringGroup ConvertToStringGroup() {
            ArrayList outContant = new ArrayList();

            outContant.Add(lineName);
            for (int a = 0; a < 5; a++) {
                outContant.Add(lineMessage[a]);
            }
            if (lineType == enumLineType.Bus) {
                outContant.Add("0");
            } else {
                outContant.Add("1");
            }
            outContant.Add((int)lineWait);
            for (int a = 0; a < 4; a++) {
                outContant.Add(lineRuntime[a].ToString());
            }
            outContant.Add(lineUpStop.ToNewSplitWord(","));
            outContant.Add(lineDownStop.ToNewSplitWord(","));

            return new StringGroup(outContant, Environment.NewLine);
        }

        public void ConvertToResources(Tools.StringGroup value) {
            if (value.sourceString == "") { return; }

            //处理文件数据
            string[] result = value.ToStringGroup();
            lineName = result[0];
            for (int a = 0; a < 5; a++) {
                lineMessage[a] = result[a + 1];
            }
            if (result[6] == "0") {
                lineType = enumLineType.Bus;
            } else {
                lineType = enumLineType.Subway;
            }
            lineWait = (enumLineWait)(int.Parse(result[7]));
            for (int a = 0; a < 4; a++) {
                lineRuntime[a] = System.Convert.ToInt32(result[a + 8]);
            }
            lineUpStop = new Tools.StringGroup(result[12], ",");
            lineDownStop = new Tools.StringGroup(result[13], ",");

        }


    }
}
