using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bus_rode.Kernel.Tools;
using bus_rode.Kernel.FileIO.FileBlockStruct;
using bus_rode.Kernel.FileIO.FileOperation;

namespace bus_rode.Kernel.FileIO.Conversion {

    public class BusConvertToSubway {

        /// <summary>
        /// 旧的地铁站台列表
        /// </summary>
        private BigArrayList oldSubway;
        /// <summary>
        /// 旧的地铁站台出口列表
        /// </summary>
        private BigArrayList oldSubwayExit;

        /// <summary>
        /// 所有地铁站台
        /// </summary>
        private BigArrayList subwayStop;

        /// <summary>
        /// 文件读取
        /// </summary>
        private FileOperation.Reader.StepReader<LineFileBlockStruct> fr;

        /// <summary>
        /// 旧地铁读取
        /// </summary>
        private FileOperation.Reader.StepReader<SubwayFileBlockStruct> fr2;

        /// <summary>
        /// 新地铁写入
        /// </summary>
        private FileOperation.Writer.SeekWriter<SubwayFileBlockStruct> fw;

        public BusConvertToSubway() {
            oldSubway = new BigArrayList();
            oldSubwayExit = new BigArrayList();
            subwayStop = new BigArrayList();
            fr = new FileOperation.Reader.StepReader<LineFileBlockStruct>();
            fr2 = new FileOperation.Reader.StepReader<SubwayFileBlockStruct>();
            fw = new FileOperation.Writer.SeekWriter<SubwayFileBlockStruct>();
        }

        /// <summary>
        /// 立即转换
        /// </summary>
        public void Convert() {

            //============================读取旧的
            fr2.BeginToStepRead();

            while (true) {

                var cache = fr2.ReadNext();

                if (cache.stopName == "") { break; }

                oldSubway.Add(cache.stopName);
                oldSubwayExit.Add(cache.stopExit);
            }

            fr2.StopStepReading();

            //============================获取新的
            fr.BeginToStepRead();

            while (true) {

                var cache2 = fr.ReadNext();

                if (cache2.lineName == "") { break; }
                if (cache2.lineType == enumLineType.Bus) { continue; }

                foreach (string item in cache2.lineUpStop.ToStringGroup()) {
                    var index = subwayStop.IndexOf(item);
                    if (index == -1) {
                        //没写过
                        subwayStop.Add(item);
                    } else {
                        //写了
                        //不管
                    }
                }
                foreach (string item in cache2.lineDownStop.ToStringGroup()) {
                    var index = subwayStop.IndexOf(item);
                    if (index == -1) {
                        //没写过
                        subwayStop.Add(item);
                    } else {
                        //写了
                        //不管
                    }
                }


            }

            fr.StopStepReading();

            //============================写入

            fw.BeginToWrite();

            for (int a = 0; a < subwayStop.Count; a++) {

                int index = oldSubway.IndexOf(subwayStop.Item(a));
                var cache3 = new SubwayFileBlockStruct();
                cache3.stopName = subwayStop.Item(a).ToString();

                if (index == -1) {
                    //没有以前的数据
                    var cache4 = new List<StringGroup>();
                    cache4.Add(new StringGroup("示例出口#通向遥远的地方", "#"));
                    cache3.stopExit = cache4;
                } else {
                    //有数据，直接写
                    cache3.stopExit = (List<StringGroup>)oldSubwayExit.Item(index);
                }

                fw.WriteNext(cache3);
            }

            fw.StopWriting();

        }

    }
}
