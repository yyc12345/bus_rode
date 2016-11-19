using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using bus_rode.Kernel.Tools;


namespace bus_rode.Kernel.FileIO.FileOperation.Reader {
    public class SeekReader<T> where T : FileIO.IFileBlockStruct, new() {

        /// <summary>
        /// 快速访问管理
        /// </summary>
        protected MemorizePreviousItem memorizeManagement;

        /// <summary>
        /// 要读取主文件的地址
        /// </summary>
        protected string mainFilePath;
        /// <summary>
        /// 要读取索引文件的地址
        /// </summary>
        protected string seekFilePath;

        public SeekReader() {
            var cache = new T();
            if (cache.SupportSeekReader==false) { throw new NotImplementedException("This file resources is not support this function."); }
            mainFilePath = cache.mainFilePath;
            seekFilePath = cache.seekFilePath;
            memorizeManagement = new MemorizePreviousItem();
        }

        /// <summary>
        /// 开始随机读取
        /// </summary>
        /// <param name="searchHead">要搜寻的头</param>
        public List<T> SeekRead(List<seekIndexStruct> seekHead) {

            if (seekHead.Count != 0) { throw new NullReferenceException("head is empty!"); }

            //确认结果
            var result = new List<T>(seekHead.Count);

            //=======================================================从内存暂存区读
            //读取循环
            int cache = -1;
            foreach (seekIndexStruct item in seekHead) {
                cache += 1;
                var cacheGet = memorizeManagement.TryGetItem(item.Head);

                //有了
                if (cacheGet.sourceString != "") {
                    //写入
                    var cacheRes = new T();
                    cacheRes.ConvertToResources(cacheGet);
                    result[cache] = cacheRes;
                    //取消这部分的文件读取
                    //设头为空，取消读取
                    seekHead[cache].Head = "";
                }
            }


            //=======================================================读取序号
            StreamReader indexReader = new StreamReader(Kernel.Tools.SystemInformation.WorkingPath + seekFilePath, Assistance.utf8WithoutBOM);
            string word = "";

            //读取循环
            while (true) {
                word = indexReader.ReadLine();

                if (word == "") { throw new IOException("Out of file!"); }

                int searchIndex = CheckHeadInSeekHead(seekHead,word);
                if (searchIndex != -1) {
                    //对的
                    Kernel.Tools.StringGroup index = new Kernel.Tools.StringGroup(indexReader.ReadLine(), ",");
                    string[] indexString = index.ToStringGroup();

                    //0表示seek坐标，1表示seek读取长度
                    seekHead[searchIndex].Index = System.Convert.ToInt64(indexString[0]);
                    seekHead[searchIndex].Length = System.Convert.ToInt32(indexString[1]);

                    //检测写满
                    if (CheckSeekHeadIsFull(seekHead) == true) { break; }

                } else {
                    //不是的
                    indexReader.ReadLine();
                }
            }

            indexReader.Dispose();

            //====================================================================================读取
            var seekReader = new FileStream(Kernel.Tools.SystemInformation.WorkingPath + mainFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            //读取循环
            cache = -1;
            foreach (seekIndexStruct item in seekHead) {

                cache += 1;
                //检测空头，空头放过去
                if (item.Head == "") { continue; }

                //seek
                seekReader.Seek(item.Index, SeekOrigin.Begin);

                //get
                byte[] getByte = new byte[item.Length];
                seekReader.Read(getByte, 0, item.Length);

                //translate
                Kernel.Tools.StringGroup outContant = new Kernel.Tools.StringGroup(System.Text.Encoding.UTF8.GetString(getByte), Environment.NewLine);

                //削去最后一行的空行
                ArrayList cacheOutContant = outContant.ToArrayList();
                cacheOutContant.RemoveAt(cacheOutContant.Count - 1);
                outContant = new Tools.StringGroup(cacheOutContant, Environment.NewLine);

                var cacheRes = new T();
                cacheRes.ConvertToResources(outContant);
                result[cache] = cacheRes;

            }

            return result;

        }


        /// <summary>
        /// 检查SeekHead是否有指定文本，有返回索引，没有则返回-1
        /// </summary>
        /// <param name="seekHead">要检查的数组</param>
        /// <param name="head">寻找的头</param>
        /// <returns></returns>
        protected int CheckHeadInSeekHead(List<seekIndexStruct> seekHead,string head) {
            int index = -1;
            foreach (seekIndexStruct item in seekHead) {
                index += 1;
                if (item.Head == head) { return index; }
            }

            return -1;
        }

        /// <summary>
        /// 检测索引是否检测满了
        /// </summary>
        /// <param name="seekHead">要检查的数组</param>
        /// <returns></returns>
        protected bool CheckSeekHeadIsFull(List<seekIndexStruct> seekHead) {
            foreach (seekIndexStruct item in seekHead) {
                if (item.Length == 0) {
                    if (item.Head == "") {
                        //头空，不要读的，可以算满
                    } else {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}
