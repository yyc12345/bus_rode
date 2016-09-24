using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using bus_rode.Kernel.Tools;

namespace bus_rode.Kernel.FileIO.FileOperation.Writer {
    public class StepWriter<T> where T : FileIO.IFileBlockStruct, new() {

        /// <summary>
        /// 顺序写入的读取器
        /// </summary>
        protected StreamWriter stepWriter;
        /// <summary>
        /// 顺序写入状态
        /// </summary>
        protected enumFileStatus stepWriterStatus;

        /// <summary>
        /// 要写入主文件的地址
        /// </summary>
        protected string filePath;

        public StepWriter() {
            var cache = new T();
            if (cache.SupportStepWriter == false) { throw new NotImplementedException("This file resources is not support this function."); }
            stepWriterStatus = enumFileStatus.Empty;
            filePath = cache.mainFilePath;
        }

        /// <summary>
        /// 开始写入
        /// </summary>
        public void BeginToWrite() {
            if (stepWriterStatus == enumFileStatus.Empty) {

                stepWriter = new StreamWriter(Environment.CurrentDirectory + filePath, false, Assistance.utf8WithoutBOM);
                stepWriterStatus = enumFileStatus.Ready;

            } else {
                throw new InvalidOperationException("Initialize failed!");
            }
        }

        /// <summary>
        /// 写入项
        /// </summary>
        /// <param name="data">要写入的内容</param>
        public void WriteNext(T data) {
            if (stepWriterStatus == enumFileStatus.Empty) { throw new NullReferenceException("Isn't open file"); }

            //转换
            var intoFile = data.ConvertToStringGroup();

            //写入
            if (intoFile.sourceString == "") { throw new NullReferenceException("Nothing need write!"); }

            foreach (string item in intoFile.ToStringGroup()) {
                //写入
                stepWriter.WriteLine(item);
            }

        }

        /// <summary>
        /// 结束写入
        /// </summary>
        public void StopWriting() {
            if (stepWriterStatus == enumFileStatus.Ready) {

                stepWriter.Dispose();
                stepWriterStatus = enumFileStatus.Empty;

            } else {
                throw new NullReferenceException("Reader isn't initialize!");
            }
        }

    }
}
