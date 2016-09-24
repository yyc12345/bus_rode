using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bus_rode.Kernel.Tools;
using System.Collections;
using System.IO;

namespace bus_rode.Kernel.FileIO.FileOperation.Reader {
    public class CompletelyReader<T> where T : FileIO.IFileBlockStruct, new() {

        /// <summary>
        /// 要读取文件的地址
        /// </summary>
        protected string filePath;

        public CompletelyReader() {
            var cache = new T();
            if (cache.SupportCompletelyReader==false) { throw new NotImplementedException("This file resources is not support this function."); }
            filePath = cache.mainFilePath;
        }

        /// <summary>
        /// 读取全部
        /// </summary>
        /// <returns></returns>
        public T ReadAll() {

            ArrayList getWords = new ArrayList();
            StreamReader fr = new StreamReader(Environment.CurrentDirectory + filePath, Assistance.utf8WithoutBOM);
            string word = "";

            //read while
            while (true) {
                word = fr.ReadLine();

                if (word == "") { break; }

                getWords.Add(word);
            }

            fr.Dispose();
            var cacheRes = new T();
            cacheRes.ConvertToResources(new StringGroup(getWords, Environment.NewLine));
            return cacheRes;

        }

    }
}
