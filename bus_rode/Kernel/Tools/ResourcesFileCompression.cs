using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using bus_rode.Kernel.Tools;

namespace bus_rode.Kernel.Tools {
    /// <summary>
    /// 解压缩文件类
    /// </summary>
    public class ResourcesFileCompression {

        /// <summary>
        /// 资源文件名称列表
        /// </summary>
        private const string ResourcesFiles = "Bus.dat,BusSeek.dat,HaveBus.dat,Readme.dat,Stop.dat,StopSeek.dat,Subway.dat,SubwaySeek.dat,GuideLine.dat,GuideLineSeek.dat";

        /// <summary>
        /// 压缩文件，返回true表示成功，false表示失败
        /// </summary>
        /// <param name="outFilePath">输出文件的绝对目录</param>
        /// <param name="fileFolder">输入文件所在的文件夹，需要加\</param>
        /// <param name="fileVersion">需要输入的build版本号</param>
        /// <returns></returns>
        public bool Compress(object outFilePath, object fileFolder, long fileVersion) {
            //转换
            string _outFilePath, _fileFolder;
            _outFilePath = (string)outFilePath;
            _fileFolder = (string)fileFolder;

            //检查
            if (CheckFolder(_fileFolder) == false) { return false; }
            if (File.Exists(_outFilePath) == true) { File.Delete(_outFilePath); }

            //开始保存
            FileStream outFile = new FileStream(_outFilePath, FileMode.Create, FileAccess.Write, FileShare.Write);
            BinaryReader br;
            BinaryWriter bw = new BinaryWriter(outFile, System.Text.Encoding.UTF8);

            StringGroup sg = new StringGroup(ResourcesFiles, ",");
            string[] fileArray = sg.ToStringGroup();

            FileStream intoFile;

            //计量数据数字的变量
            //char以1000为基数读取，读不完就写
            Int64 charCount = 0;
            ArrayList fileCountList = new ArrayList();

            //先乱写文件头
            bw.Write("BRSP".ToCharArray());
            for (int a = 0; a < fileArray.Length; a++) { bw.Write((Int64)0); }

            //读取文件写入
            for (int a = 0; a < fileArray.Length; a++) {
                intoFile = new FileStream(_fileFolder + fileArray[a], FileMode.Open, FileAccess.Read, FileShare.Read);
                br = new BinaryReader(intoFile, System.Text.Encoding.UTF8);

                //读取循环
                while (true) {

                    //先读取那么多
                    char[] cacheChar = br.ReadChars(1000);

                    //如果为0，没有内容，不结束读取
                    if (cacheChar.Length == 0) { break; }

                    //读取数量不足，说明也到文件尾了
                    if (cacheChar.Length < 1000) {
                        charCount += cacheChar.Length;
                        bw.Write(cacheChar);
                        break;
                    } else {
                        //继续读取
                        charCount += 1000;
                        bw.Write(cacheChar);
                    }
                }

                //善后处理
                br.Dispose();
                intoFile.Dispose();

                fileCountList.Add(charCount);
                charCount = 0;

            }

            //==============================结束各个变量，准备覆写
            bw.Dispose();
            outFile.Dispose();

            //覆写
            FileStream outFileProcess = new FileStream(_outFilePath, FileMode.Open, FileAccess.Write, FileShare.Write);
            BinaryWriter bwProcess = new BinaryWriter(outFileProcess, System.Text.Encoding.UTF8);

            //写文件头
            bwProcess.Write("BRSP".ToCharArray());
            bwProcess.Write(fileVersion);
            for (int a = 0; a < fileArray.Length; a++) { bwProcess.Write((Int64)fileCountList[a]); }

            bwProcess.Dispose();
            outFileProcess.Dispose();

            return true;
        }

        /// <summary>
        /// 解压缩文件，返回true表示成功，false表示失败
        /// </summary>
        /// <param name="filePath">包文件的绝对目录</param>
        /// <param name="outFileFolder">输出文件所在的文件夹，需要加\</param>
        /// <param name="fileVersion">需要验证的build版本号</param>
        /// <returns></returns>
        public bool Decompress(object filePath, object outFileFolder, long fileVersion) {
            //转换
            string _filePath, _outFileFolder;
            _filePath = (string)filePath;
            _outFileFolder = (string)outFileFolder;

            //检测
            if (File.Exists(_filePath) == false) { return false; }

            //开始读取
            FileStream intoFile = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryReader br = new BinaryReader(intoFile, System.Text.Encoding.UTF8);
            FileStream outFile;
            BinaryWriter bw;

            StringGroup sg = new StringGroup(ResourcesFiles, ",");
            string[] fileArray = sg.ToStringGroup();

            //计量数据数字的变量
            //char以1000为基数写入
            ArrayList fileCountList = new ArrayList();

            //======================先读文件头，判断
            string fileHead = new string(br.ReadChars(4));
            if (fileHead != "BRSP") {
                //不合格，不行，退出
                br.Dispose();
                intoFile.Dispose();
                return false;
            }
            if (br.ReadInt64() != fileVersion) {
                //不合格，不行，退出
                br.Dispose();
                intoFile.Dispose();
                return false;
            }

            //============确认无误后可以写入===============
            SetFolder(_outFileFolder);

            //读入区块
            for (int a = 0; a < fileArray.Length; a++) { fileCountList.Add(br.ReadInt64()); }

            //读数据
            for (int a= 0; a < fileArray.Length; a++) {

                //如果没有可以读的，取消之
                if ((Int64)fileCountList[a] == 0) { continue; }

                //打开文件
                outFile = new FileStream(_outFileFolder + fileArray[a], FileMode.Create, FileAccess.Write, FileShare.Write);
                bw = new BinaryWriter(outFile, System.Text.Encoding.UTF8);

                //=================================计算读取次数
                //需要的for循环次数
                int forCount = (int)((Int64)fileCountList[a] / 1000);
                //余下的多余的次数
                int lostCount = (int)((Int64)fileCountList[a] % 1000);

                //====================================读取循环
                //没有达到1000，直接在余下处理里处理
                if (forCount > 0) { for (int b = 0; b < forCount; b++) { bw.Write(br.ReadChars(1000)); } }

                //其余读取
                //是否需要读取
                if (lostCount == 0) { //不需要
                }else {
                    //需要
                    bw.Write(br.ReadChars(lostCount));
                }

                //===================善后处理
                bw.Dispose();
                outFile.Dispose();

            }

            //善后处理
            br.Dispose();
            intoFile.Dispose();

            return true;

        }

        /// <summary>
        /// 设置输出的目录
        /// </summary>
        /// <param name="folderPath"></param>
        public void SetFolder(string folderPath) {
            StringGroup sg = new StringGroup(ResourcesFiles, ",");
            string[] fileArray = sg.ToStringGroup();
            foreach (string item in fileArray) { File.Delete(folderPath + item); }
        }

        /// <summary>
        /// 检测文件夹下是否存在需要打包的文件
        /// </summary>
        /// <param name="folderPath">文件夹，需要加\</param>
        /// <returns></returns>
        public bool CheckFolder(string folderPath) {
            StringGroup sg = new StringGroup(ResourcesFiles, ",");
            string[] fileArray = sg.ToStringGroup();
            //判断
            foreach (string item in fileArray) { if (File.Exists(folderPath + item) == false) { return false; } }
            return true;
        }

    }
}
