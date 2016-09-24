using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bus_rode.Kernel.FileIO.FileBlockStruct;
using bus_rode.Kernel.FileIO.FileOperation.Reader;
using bus_rode.Kernel.FileIO;
using bus_rode.Kernel.Management.Assistance;
using System.Threading.Tasks;
using bus_rode.Kernel.Tools;

namespace bus_rode.Kernel.Management {

    public class StopManagement {
        /// <summary>
        /// 当前选定站台
        /// </summary>
        private StopFileBlockStruct nowStop;
        /// <summary>
        /// 当前选定站台
        /// </summary>
        public StopFileBlockStruct NowStop { get { return nowStop; } }
        /// <summary>
        /// 当前支持的站台
        /// </summary>
        private HashSet<string> supportingStops;
        /// <summary>
        /// 当前支持的站台
        /// </summary>
        public HashSet<string> SupportingStops { get { return supportingStops; } }
        /// <summary>
        /// 附近站台列表
        /// </summary>
        private HashSet<NearbyStopStructure> nearbyStops;
        /// <summary>
        /// 附近站台列表
        /// </summary>
        public HashSet<NearbyStopStructure> NearbyStops { get { return nearbyStops; } }

        /// <summary>
        /// 站台读取器
        /// </summary>
        private SeekReader<StopFileBlockStruct> stopReader;
        /// <summary>
        /// 支持站台列表读取器
        /// </summary>
        private BlockReader<StopFileBlockStruct> haveStopReader;

        public StopManagement() {
            nowStop = new StopFileBlockStruct();
            nearbyStops = new HashSet<NearbyStopStructure>();
            supportingStops = new HashSet<string>();
            haveStopReader = new BlockReader<StopFileBlockStruct>();
            stopReader = new SeekReader<StopFileBlockStruct>();
        }


        /// <summary>
        /// 以选择站台刷新
        /// </summary>
        /// <param name="stopName">选择的站台名</param>
        public async void Update(string stopName) {
            await UpdateStop(stopName);
            await UpdateNearbyStop();
        }

        /// <summary>
        /// 以选择站台刷新
        /// </summary>
        /// <param name="stopName">选择的站台名</param>
        /// <returns></returns>
        private Task UpdateStop(string stopName) {
            return Task.Run(() => {
                nowStop = stopReader.SeekRead(new List<FileIO.FileOperation.seekIndexStruct> { new FileIO.FileOperation.seekIndexStruct { Head = stopName } })[0];
            });
        }

        /// <summary>
        /// 以当前车次更新附近的站台
        /// </summary>
        /// <returns></returns>
        private Task UpdateNearbyStop() {
            return Task.Run(() => {

                //=====================================获取数据
                var crossLinesList = nowStop.stopCrossLine.ToStringGroup();
                var cache = new List<bus_rode.Kernel.FileIO.FileOperation.seekIndexStruct>();
                //列表转换
                foreach (var item in crossLinesList) { cache.Add(new FileIO.FileOperation.seekIndexStruct { Head = item }); }
                //获取
                var reader = new SeekReader<LineFileBlockStruct>();
                var crossLinesData = reader.SeekRead(cache);

                //======================================循环所有车次查找
                foreach (var item in crossLinesData) {

                    //需要写入的站台列表
                    HashSet<string> neededWriteStops = new HashSet<string>();

                    //获取上下站台
                    var up = item.lineUpStop.ToList();
                    var down = item.lineDownStop.ToList();

                    //查找上下站
                    var upIndex = up.IndexOf(nowStop.stopName);
                    var downIndex = down.IndexOf(nowStop.stopName);

                    //分析首尾，直接加列表，因为重复的是加不进去的
                    if (upIndex < 0) { } //没有项目，不写
                    else if (upIndex == 0) { neededWriteStops.Add(up[upIndex + 1]); } //首项，只写下一项
                    else if (upIndex == up.Count - 1) { neededWriteStops.Add(up[upIndex - 1]); } //末项，只写前一项
                    else { neededWriteStops.Add(up[upIndex + 1]); neededWriteStops.Add(up[upIndex - 1]); } //前后项都写

                    if (downIndex < 0) { } //没有项目，不写
                    else if (downIndex == 0) { neededWriteStops.Add(down[downIndex + 1]); } //首项，只写下一项
                    else if (downIndex == down.Count - 1) { neededWriteStops.Add(down[downIndex - 1]); } //末项，只写前一项
                    else { neededWriteStops.Add(down[downIndex + 1]); neededWriteStops.Add(down[downIndex - 1]); } //前后项都写

                    //==========================写入
                    foreach (var item2 in neededWriteStops) {
                        //查找
                        var temp = from item3 in nearbyStops
                                   where item3.StopName == item2
                                   select item3;

                        if (temp.Any() == true) {
                            //有元素，写入
                            foreach (var item4 in temp) { item4.crossLines.Add(item.lineName); }
                        } else {
                            //无元素，添加项
                            var temp2 = new NearbyStopStructure();
                            temp2.StopName = item2;
                            temp2.crossLines.Add(item.lineName);

                            nearbyStops.Add(temp2);
                        }

                    }

                }


            });
        }

        /// <summary>
        /// 刷新支持的站台列表，以指定指令。返回一个值，指示读取是否成功
        /// </summary>
        /// <param name="order"></param>
        public bool UpdateSupportingList(enumListPageOrder order) {
            List<StopFileBlockStruct> result;

            if (order == enumListPageOrder.Previous) {
                result = haveStopReader.ReadPreviousBlock();
            } else {
                result = haveStopReader.ReadNextBlock();
            }

            if (result.Count != 0) {
                //可以，输入
                supportingStops.Clear();

                foreach (var item in result) {
                    supportingStops.Add(item.stopName);
                }

                return true;
            } else {
                //没有数据，到头了，不能再读了
                return false;
            }
        }


    }

    /// <summary>
    /// 附近站台结构
    /// </summary>
    public class NearbyStopStructure {

        public NearbyStopStructure() {
            StopName = "";
            crossLines = new List<string>();
        }

        /// <summary>
        /// 站台名
        /// </summary>
        public string StopName;
        /// <summary>
        /// 经过的线路列表
        /// </summary>
        private string CrossLines { get { return new StringGroup(crossLines, " ").ToString(); } }
        /// <summary>
        /// 经过的线路列表
        /// </summary>
        public List<string> crossLines;
    }

}
