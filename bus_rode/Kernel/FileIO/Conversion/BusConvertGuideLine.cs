using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bus_rode.Kernel.Tools;
using bus_rode.Kernel.FileIO.FileBlockStruct;
using bus_rode.Kernel.FileIO.FileOperation;
using System.Collections;

namespace bus_rode.Kernel.FileIO.Conversion {
    public class BusConvertGuideLine {

        /// <summary>
        /// 写入
        /// </summary>
        private FileOperation.Writer.SeekWriter<GuideLineBlockStruct> fw;
        /// <summary>
        /// 读取
        /// </summary>
        private FileOperation.Reader.StepReader<LineFileBlockStruct> fr;
        /// <summary>
        /// 检测某站经过车次
        /// </summary>
        private FileOperation.Reader.SeekReader<StopFileBlockStruct> fr2;

        /// <summary>
        /// 当前车次可换乘车次
        /// </summary>
        private ArrayList thisBusCanTranslate;

        public BusConvertGuideLine() {
            fr = new FileOperation.Reader.StepReader<LineFileBlockStruct>();
            fw = new FileOperation.Writer.SeekWriter<GuideLineBlockStruct>();
            fr2 = new FileOperation.Reader.SeekReader<StopFileBlockStruct>();
            thisBusCanTranslate = new ArrayList();
        }

        /// <summary>
        /// 立即转换
        /// </summary>
        public void Convert() {

            //依次准备写
            fw.BeginToWrite();
            //依次读
            fr.BeginToStepRead();

            while (true) {

                var cache = fr.ReadNext();

                if (cache.lineName == "") { break; }

                var cache2 = new ArrayList();
                var cache4 = new List<FileIO.FileOperation.seekIndexStruct>();

                foreach (string item in cache.lineUpStop.ToStringGroup()) {
                    if (cache2.Contains(item) == false) {
                        //没写过
                        cache2.Add(item);
                    } else {
                        //写了
                    }
                }
                foreach (string item in cache.lineDownStop.ToStringGroup()) {
                    if (cache2.Contains(item) == false) {
                        //没写过
                        cache2.Add(item);
                    } else {
                        //写了
                    }
                }

                //准备搜索
                foreach (object item in cache2) {
                    var cache3 = new FileIO.FileOperation.seekIndexStruct();
                    cache3.Head = item.ToString();
                    cache4.Add(cache3);
                }

                //立即搜索
                var seekResult = fr2.SeekRead(cache4);

                //找出结果
                foreach (StopFileBlockStruct item in seekResult) {
                    foreach (string item2 in item.stopCrossLine.ToStringGroup()) {
                        if (thisBusCanTranslate.Contains(item2) == false) {
                            //添加
                            thisBusCanTranslate.Add(item2);
                        }
                    }
                }

                //准备数据
                var cache5 = new GuideLineBlockStruct();
                cache5.lineName = cache.lineName;
                cache5.lineCrossLine = new StringGroup(thisBusCanTranslate, ",");

                //写入
                fw.WriteNext(cache5);

                //回归数据
                thisBusCanTranslate.Clear();

            }


            fr.StopStepReading();
            fw.StopWriting();

        }

    }
}
