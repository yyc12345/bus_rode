using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bus_rode.Kernel.FileIO.FileBlockStruct;
using bus_rode.Kernel.FileIO.FileOperation.Reader;
using bus_rode.Kernel.FileIO;
using bus_rode.Kernel.Management.Assistance;

namespace bus_rode.Kernel.Management {

    public class LineManagement {

        /// <summary>
        /// 当前选定的车
        /// </summary>
        private LineFileBlockStruct nowLine;
        /// <summary>
        /// 当前选定的车
        /// </summary>
        public LineFileBlockStruct NowLine { get { return nowLine; } }
        /// <summary>
        /// 当前的地铁数据
        /// </summary>
        private HashSet<SubwayFileBlockStruct> nowSubwayStop;
        /// <summary>
        /// 当前的地铁数据
        /// </summary>
        public HashSet<SubwayFileBlockStruct> NowSubwayStop { get { return nowSubwayStop; } }
        /// <summary>
        /// 支持的线路
        /// </summary>
        private HashSet<string> supportingLines;
        /// <summary>
        /// 支持的线路
        /// </summary>
        public HashSet<string> SupportingLines { get { return supportingLines; } }

        /// <summary>
        /// 用于读取线路数据的读取器
        /// </summary>
        private SeekReader<LineFileBlockStruct> lineReader;
        /// <summary>
        /// 用于读取地铁数据的读取器
        /// </summary>
        private SeekReader<SubwayFileBlockStruct> subwayReader;
        /// <summary>
        /// 用于读取支持的线路数据的读取器
        /// </summary>
        private BlockReader<HaveBusFileBlockStruct> haveLineReader;

        public LineManagement() {
            nowLine = new LineFileBlockStruct();
            supportingLines = new HashSet<string>();
            subwayReader = new SeekReader<SubwayFileBlockStruct>();
            haveLineReader = new BlockReader<HaveBusFileBlockStruct>();
            lineReader = new SeekReader<LineFileBlockStruct>();
            nowSubwayStop = new HashSet<SubwayFileBlockStruct>();
        }

        /// <summary>
        /// 以选择车次刷新线路和地铁模块
        /// </summary>
        /// <param name="lineName">选择的车次</param>
        public async void Update(string lineName) {
            await UpdateLine(lineName);
            await UpdateSubway();
        }

        /// <summary>
        /// 以选择车次刷新
        /// </summary>
        /// <param name="lineName">选择的车次</param>
        /// <returns></returns>
        private Task UpdateLine(string lineName) {
            return Task.Run(() => {

                nowLine = lineReader.SeekRead(new List<FileIO.FileOperation.seekIndexStruct> { new FileIO.FileOperation.seekIndexStruct { Head = lineName } })[0];

            });
        }

        /// <summary>
        /// 以当前加载的线路为模板，如果可以，就加载地铁信息
        /// </summary>
        /// <returns></returns>
        private Task UpdateSubway() {

            return Task.Run(() => {

                if (nowLine.lineName != "" && nowLine.lineType == enumLineType.Subway) {

                    var inputSeek = new List<FileIO.FileOperation.seekIndexStruct>();
                    var cache = new List<string>();

                    //输入上下行站台
                    foreach (string item in nowLine.lineUpStop.ToStringGroup()) {
                        if (cache.Contains(item) == false) { cache.Add(item); }
                    }
                    foreach (string item in nowLine.lineDownStop.ToStringGroup()) {
                        if (cache.Contains(item) == false) { cache.Add(item); }
                    }

                    //输入查询列表
                    foreach (string item in cache) {
                        inputSeek.Add(new FileIO.FileOperation.seekIndexStruct { Head = item });
                    }

                    //查找
                    var temp  = subwayReader.SeekRead(inputSeek);
                    foreach(var item in temp) { nowSubwayStop.Add(item); }

                } else nowSubwayStop = new HashSet<SubwayFileBlockStruct>(); return;

            });

        }


        /// <summary>
        /// 刷新支持的线路列表，以指定指令。返回一个值，指示读取是否成功
        /// </summary>
        /// <param name="order"></param>
        public bool UpdateSupportingList(enumListPageOrder order) {
            List<HaveBusFileBlockStruct> result;

            if (order== enumListPageOrder.Previous) {
                result = haveLineReader.ReadPreviousBlock();
            }else {
                result = haveLineReader.ReadNextBlock();
            }
            
            if (result .Count != 0) {
                //可以，输入
                supportingLines.Clear();

                foreach (var item in result) {
                    supportingLines.Add(item.lineName);
                }

                return true;
            }else {
                //没有数据，到头了，不能再读了
                return false;
            }
        }

    }

  
}
