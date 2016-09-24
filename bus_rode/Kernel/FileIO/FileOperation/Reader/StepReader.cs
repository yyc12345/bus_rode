using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using bus_rode.Kernel.Tools;
using System.Collections;

namespace bus_rode.Kernel.FileIO.FileOperation.Reader {
    public class StepReader<T> where T : FileIO.IFileBlockStruct, new() {

        /// <summary>
        /// 顺序读取的读取器
        /// </summary>
        protected StreamReader stepReader;
        /// <summary>
        /// 顺序读取状态
        /// </summary>
        protected enumFileStatus stepReaderStatus;

        /// <summary>
        /// 要读取文件的地址
        /// </summary>
        protected string filePath;
        /// <summary>
        /// 读取下一行的行数
        /// </summary>
        protected int stepReadLine;

        public StepReader() {
            var cache = new T();
            if (cache.SupportStepReader == false) { throw new NotImplementedException("This file resources is not support this function."); }
            stepReaderStatus = enumFileStatus.Empty;
            stepReadLine = cache.stepReadLine;
            filePath = cache.mainFilePath;
        }

        /// <summary>
        /// 开始读取
        /// </summary>
        public void BeginToStepRead() {
            if (stepReaderStatus != enumFileStatus.Ready) {
                stepReader = new StreamReader(Environment.CurrentDirectory + filePath, Assistance.utf8WithoutBOM);
                stepReaderStatus = enumFileStatus.Ready;
            } else {
                throw new InvalidOperationException("Initialize failed!");
            }
        }

        /// <summary>
        /// 读取下一个
        /// </summary>
        public T ReadNext() {
            if (stepReaderStatus == enumFileStatus.Empty) { throw new NullReferenceException("Isn't open file"); }

            //读取

            if (stepReadLine < 1) { throw new OverflowException("read line's number so short"); }

            string word = stepReader.ReadLine();
            if (word == "") {
                //读完了，给空
                return new T();
            }

            //读取，刚刚读的是内容写进去
            ArrayList savedWord = new ArrayList();
            savedWord.Add(word);

            //剩余的看看要不要读
            for (int a = 0; a < (stepReadLine - 1); a++) {
                savedWord.Add(stepReader.ReadLine());
            }

            //返回
            var cacheRes = new T();
            cacheRes.ConvertToResources(new Tools.StringGroup(savedWord, Environment.NewLine));
            return cacheRes;
        }

        /// <summary>
        /// 停止读取
        /// </summary>
        public void StopStepReading() {
            if (stepReaderStatus == enumFileStatus.Ready) {
                stepReader.Dispose();
                stepReaderStatus = enumFileStatus.Empty;

            } else {
                throw new NullReferenceException("Reader isn't initialize!");
            }
        }

    }
}
