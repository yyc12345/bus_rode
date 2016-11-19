using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using bus_rode.Kernel.Tools;

namespace bus_rode.Kernel.FileIO.FileOperation.Writer {
    public class SeekWriter<T> where T : FileIO.IFileBlockStruct, new() {

        /// <summary>
        /// seek文件写入需要的写入器
        /// </summary>
        protected StreamWriter seekWriter;
        /// <summary>
        /// seek文件写入状态
        /// </summary>
        protected enumFileStatus seekWriterStatus;
        /// <summary>
        /// 顺序写入的读取器
        /// </summary>
        protected FileStream stepWriter;
        /// <summary>
        /// 顺序写入状态
        /// </summary>
        protected enumFileStatus stepWriterStatus;

        /// <summary>
        /// 要写入主文件的地址
        /// </summary>
        protected string mainFilePath;
        /// <summary>
        /// 要写入索引文件的地址
        /// </summary>
        protected string seekFilePath;

        public SeekWriter() {
            var cache = new T();
            if (cache.SupportSeekWriter == false) { throw new NotImplementedException("This file resources is not support this function."); }
            stepWriterStatus = enumFileStatus.Empty;
            seekWriterStatus = enumFileStatus.Empty;
            mainFilePath = cache.mainFilePath;
            seekFilePath = cache.seekFilePath;
        }

        /// <summary>
        /// 开始写入
        /// </summary>
        public void BeginToWrite() {
            if ((seekWriterStatus == enumFileStatus.Empty) && (stepWriterStatus == enumFileStatus.Empty)) {

                stepWriter = new FileStream(Kernel.Tools.SystemInformation.WorkingPath + mainFilePath, FileMode.Create, FileAccess.Write, FileShare.Read);
                stepWriterStatus = enumFileStatus.Ready;

                seekWriter = new StreamWriter(Kernel.Tools.SystemInformation.WorkingPath + seekFilePath, false, Assistance.utf8WithoutBOM);
                seekWriterStatus = enumFileStatus.Ready;

            } else {
                throw new InvalidOperationException("Initialize failed!");
            }
        }

        /// <summary>
        /// 写入项
        /// </summary>
        /// <param name="data">要写入的内容</param>
        public void WriteNext(T data) {
            if ((seekWriterStatus == enumFileStatus.Empty) && (stepWriterStatus == enumFileStatus.Empty)) { throw new NullReferenceException("Isn't open file"); }

            //转换
            var intoFile = data.ConvertToStringGroup();

            //写入
            if (intoFile.sourceString == "") { throw new NullReferenceException("Nothing need write!"); }

            //预备数据
            //先增加一个空行，必要的，因为写入时要写入回车
            string mixBeforeWords = intoFile.ToNewSplitWord(Environment.NewLine) + Environment.NewLine;

            //转换
            byte[] mixWords = System.Text.Encoding.UTF8.GetBytes(mixBeforeWords);
            int nowWordsLength = mixWords.Length;
            long nowPosition = stepWriter.Position;

            //写入
            stepWriter.Write(mixWords, 0, nowWordsLength);

            //写入索引
            //write name
            seekWriter.WriteLine(intoFile.ToStringGroup()[0]);
            //write address position,length
            seekWriter.WriteLine(nowPosition + "," + nowWordsLength);

        }

        /// <summary>
        /// 结束写入
        /// </summary>
        public void StopWriting() {
            if ((seekWriterStatus == enumFileStatus.Ready) && (stepWriterStatus == enumFileStatus.Ready)) {

                stepWriter.Dispose();
                stepWriterStatus = enumFileStatus.Empty;

                seekWriter.Dispose();
                seekWriterStatus = enumFileStatus.Empty;

            } else {
                throw new NullReferenceException("Reader isn't initialize!");
            }
        }

    }
}
