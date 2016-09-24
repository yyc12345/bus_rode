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
    public class BusConvertToStop {

        /// <summary>
        /// 站点
        /// </summary>
        private BigArrayList busStop;
        /// <summary>
        /// 经过线路
        /// </summary>
        private BigArrayList busLine;

        /// <summary>
        /// 文件读取
        /// </summary>
        private FileOperation.Reader.StepReader<LineFileBlockStruct> fr;

        /// <summary>
        /// 文件写入
        /// </summary>
        private FileOperation.Writer.SeekWriter<StopFileBlockStruct> fw;


        public BusConvertToStop() {
            busStop = new BigArrayList();
            busLine = new BigArrayList();
            fr = new FileOperation.Reader.StepReader<LineFileBlockStruct>();
            fw = new FileOperation.Writer.SeekWriter<StopFileBlockStruct>();
        }

        /// <summary>
        /// 立即转换
        /// </summary>
        public void Convert() {


            //===========================读取每条线路所有站台内容

            fr.BeginToStepRead();

            while (true) {

                var cache = fr.ReadNext();
                if (cache.lineName == "") { break; }    //没有了

                foreach (string item in cache.lineUpStop.ToStringGroup()) {
                    var index = busStop.IndexOf(item);
                    if (index == -1) {
                        //没写过
                        busStop.Add(item);
                        busLine.Add(cache.lineName + ",");
                    } else {
                        //写了
                        if (CheckWordHaveBus(busLine.Item(index).ToString(), cache.lineName) == false) {
                            busLine.Replace(index, busLine.Item(index).ToString() + cache.lineName + ",");
                        }
                    }
                }
                foreach (string item in cache.lineDownStop.ToStringGroup()) {
                    var index = busStop.IndexOf(item);
                    if (index == -1) {
                        //没写过
                        busStop.Add(item);
                        busLine.Add(cache.lineName + ",");
                    } else {
                        //写了
                        if (CheckWordHaveBus(busLine.Item(index).ToString(), cache.lineName) == false) {
                            busLine.Replace(index, busLine.Item(index).ToString() + cache.lineName + ",");
                        }
                    }
                }

            }
            fr.StopStepReading();

            //===========================写入
            fw.BeginToWrite();
            for (int a = 0; a < busStop.Count; a++) {
                var cache = new StopFileBlockStruct();
                cache.stopName = busStop.Item(a).ToString();
                string cache2 = busLine.Item(a).ToString();
                cache.stopCrossLine = new StringGroup(cache2.Substring(0, cache2.Length - 1), ",");

                fw.WriteNext(cache);
            }

            fw.StopWriting();

        }

        /// <summary>
        /// 检测字符串中是否包含指定内容
        /// </summary>
        /// <param name="checkedWord"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        private bool CheckWordHaveBus(string checkedWord, string word) {
            var cache = new StringGroup(checkedWord, ",");
            return cache.ToArrayList().Contains(word);
        }

    }
}
