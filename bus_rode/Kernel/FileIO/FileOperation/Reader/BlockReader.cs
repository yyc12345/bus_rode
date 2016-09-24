using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using bus_rode.Kernel.Tools;
using System.Collections;

namespace bus_rode.Kernel.FileIO.FileOperation.Reader {
    public class BlockReader<T> where T : FileIO.IFileBlockStruct, new() {

        /*栈管理器原则：
         * 初始填写中栈为0
         * 读取下一块：直接按每一次读取完操作
         * 复读：中栈清空，左栈弹栈写入中栈，按中栈读取
         * 读取上一块：中栈清空，左栈连续弹2次栈，第二次弹栈的值写入中栈，按中栈读取
         * 
         * 每一次读取完后：以中栈+读的数据length的值写入中栈，原中栈压入左栈
        */
        /// <summary>
        /// 左栈管理器
        /// </summary>
        protected Stack<long> leftResumeBrokenManagement;
        /// <summary>
        /// 中间正在使用的position-中栈
        /// </summary>
        protected long middleResumeBrokenManagement;

        /// <summary>
        /// 要读取文件的地址
        /// </summary>
        protected string filePath;
        /// <summary>
        /// 读取下一行的行数
        /// </summary>
        protected int stepReadLine;

        /// <summary>
        /// 一个块要读取的数据个数
        /// </summary>
        protected int blockReadCount;

        public BlockReader() {
            var cache = new T();
            if (cache.SupportBlockReader == false) { throw new NotImplementedException("This file resources is not support this function."); }
            blockReadCount = cache.blockReadCount;
            stepReadLine = cache.stepReadLine;
            filePath = cache.mainFilePath;
            middleResumeBrokenManagement = 0;
            leftResumeBrokenManagement = new Stack<long>();
        }

        /// <summary>
        /// 重读，若首次读取，请执行读取下一个
        /// </summary>
        /// <param name="lineCount">读取遍数，既获取多少个数据</param>
        /// <returns></returns>
        public List<T> ReadBlockAgain() {
            if (blockReadCount < 1) { throw new NullReferenceException("lineCount isn't shorter than 1"); }

            //=======================================索引
            //获取
            if (leftResumeBrokenManagement.Count == 0) { return new List<T>(); } //左栈为空，挂掉
            middleResumeBrokenManagement = leftResumeBrokenManagement.Pop();
            //设置
            var fr = new FileStream(Environment.CurrentDirectory + filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            fr.Seek(middleResumeBrokenManagement, SeekOrigin.Begin);
            var blockReader = new StreamReader(fr, Assistance.utf8WithoutBOM);

            //=======================================读取
            var result = new List<T>();
            long cacheLength = 0;
            for (int a = 0; a < blockReadCount; a++) {

                //读取
                if (stepReadLine < 1) { throw new OverflowException("read line's number so short"); }

                string word = blockReader.ReadLine();
                if (word == "") {
                    //读完了，返回
                    goto write_stack;
                }

                //读取，刚刚读的是内容写进去
                ArrayList savedWord = new ArrayList();
                savedWord.Add(word);

                //剩余的看看要不要读
                for (int b = 0; b < (stepReadLine - 1); b++) {
                    savedWord.Add(blockReader.ReadLine());
                }

                //写入
                var cacheRes = new T();
                cacheRes.ConvertToResources(new Tools.StringGroup(savedWord, Environment.NewLine));
                result.Add(cacheRes);
                //加回车写入长度
                byte[] addWords = System.Text.Encoding.UTF8.GetBytes(new Tools.StringGroup(savedWord, Environment.NewLine).ToString() + Environment.NewLine);
                cacheLength += addWords.LongLength;
            }

write_stack:;
            //=======================================写栈
            leftResumeBrokenManagement.Push(middleResumeBrokenManagement);
            middleResumeBrokenManagement += cacheLength;

            blockReader.Dispose();
            fr.Dispose();

            return result;
        }

        /// <summary>
        /// 读上一个
        /// </summary>
        /// <param name="lineCount">读取遍数，既获取多少个数据</param>
        /// <returns></returns>
        public List<T> ReadPreviousBlock() {
            if (blockReadCount < 1) { throw new NullReferenceException("lineCount isn't shorter than 0"); }

            //=======================================索引
            //获取
            if (leftResumeBrokenManagement.Count < 2) { return new List<T>(); } //左栈个数小于2，挂掉
            leftResumeBrokenManagement.Pop();
            middleResumeBrokenManagement = leftResumeBrokenManagement.Pop();
            //设置
            var fr = new FileStream(Environment.CurrentDirectory + filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            fr.Seek(middleResumeBrokenManagement, SeekOrigin.Begin);
            var blockReader = new StreamReader(fr, Assistance.utf8WithoutBOM);

            //=======================================读取
            var result = new List<T>();
            long cacheLength = 0;
            for (int a = 0; a < blockReadCount; a++) {

                //读取
                if (stepReadLine < 1) { throw new OverflowException("read line's number so short"); }

                string word = blockReader.ReadLine();
                if (word == "") {
                    //读完了，返回
                    goto write_stack;
                }

                //读取，刚刚读的是内容写进去
                ArrayList savedWord = new ArrayList();
                savedWord.Add(word);

                //剩余的看看要不要读
                for (int b = 0; b < (stepReadLine - 1); b++) {
                    savedWord.Add(blockReader.ReadLine());
                }

                //写入
                var cacheRes = new T();
                cacheRes.ConvertToResources(new Tools.StringGroup(savedWord, Environment.NewLine));
                result.Add(cacheRes);
                //加回车写入长度
                byte[] addWords = System.Text.Encoding.UTF8.GetBytes(new Tools.StringGroup(savedWord, Environment.NewLine).ToString() + Environment.NewLine);
                cacheLength += addWords.LongLength;
            }

write_stack:;
            //=======================================写栈
            leftResumeBrokenManagement.Push(middleResumeBrokenManagement);
            middleResumeBrokenManagement += cacheLength;

            blockReader.Dispose();
            fr.Dispose();

            return result;
        }

        /// <summary>
        /// 读下一个
        /// </summary>
        /// <param name="lineCount">读取遍数，既获取多少个数据</param>
        /// <returns></returns>
        public List<T> ReadNextBlock() {
            if (blockReadCount < 1) { throw new NullReferenceException("lineCount isn't shorter than 0"); }

            //=======================================索引
            //获取
            //直接按中栈设置
            //设置
            var fr = new FileStream(Environment.CurrentDirectory + filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            fr.Seek(middleResumeBrokenManagement, SeekOrigin.Begin);
            var blockReader = new StreamReader(fr, Assistance.utf8WithoutBOM);

            //=======================================读取
            var result = new List<T>();
            long cacheLength = 0;
            for (int a = 0; a < blockReadCount; a++) {

                //读取
                if (stepReadLine < 1) { throw new OverflowException("read line's number so short"); }

                string word = blockReader.ReadLine();
                if (word == "") {
                    //读完了，返回
                    goto write_stack;
                }

                //读取，刚刚读的是内容写进去
                ArrayList savedWord = new ArrayList();
                savedWord.Add(word);

                //剩余的看看要不要读
                for (int b = 0; b < (stepReadLine - 1); b++) {
                    savedWord.Add(blockReader.ReadLine());
                }

                //写入
                var cacheRes = new T();
                cacheRes.ConvertToResources(new Tools.StringGroup(savedWord, Environment.NewLine));
                result.Add(cacheRes);
                //加回车写入长度
                byte[] addWords = System.Text.Encoding.UTF8.GetBytes(new Tools.StringGroup(savedWord, Environment.NewLine).ToString() + Environment.NewLine);
                cacheLength += addWords.LongLength;
            }

write_stack:;
            //=======================================写栈
            //如果为0，就不要写栈了
            if (cacheLength != 0) {
                leftResumeBrokenManagement.Push(middleResumeBrokenManagement);
                middleResumeBrokenManagement += cacheLength;
            }

            blockReader.Dispose();
            fr.Dispose();

            return result;
        }

    }
}
