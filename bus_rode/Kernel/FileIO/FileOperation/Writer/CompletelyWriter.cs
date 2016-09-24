using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bus_rode.Kernel.Tools;
using System.Collections;
using System.IO;

namespace bus_rode.Kernel.FileIO.FileOperation.Writer {
    public class CompletelyWriter<T> where T : FileIO.IFileBlockStruct, new() {

        /// <summary>
        /// 要读取文件的地址
        /// </summary>
        protected string filePath;

        public CompletelyWriter() {
            var cache = new T();
            if (cache.SupportCompletelyWriter == false) { throw new NotImplementedException("This file resources is not support this function."); }
            filePath = cache.mainFilePath;
        }

        public void WriteAll(T into) {

            //转换
            var intoContant = into.ConvertToStringGroup();

            if (intoContant.sourceString == "") { throw new NullReferenceException("Nothing need write!"); }
            StreamWriter fw = new StreamWriter(Environment.CurrentDirectory + filePath, false, Assistance.utf8WithoutBOM);

            //写入
            foreach (string item in intoContant.ToStringGroup()) {
                fw.WriteLine(item);
            }

            fw.Dispose();

        }

    }
}
