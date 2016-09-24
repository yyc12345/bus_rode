using System;
using System.Collections;
using System.Collections.Generic;
using bus_rode.Kernel.Tools;

namespace bus_rode.Kernel.FileIO.FileOperation {

    /// <summary>
    /// 文件状态
    /// </summary>
    public enum enumFileStatus {
        /// <summary>
        /// 空闲
        /// </summary>
        Empty = 0,
        /// <summary>
        /// 正在使用
        /// </summary>
        Ready = 1
    }

    /// <summary>
    /// seek方式中的结构
    /// </summary>
    public class seekIndexStruct {

        public seekIndexStruct() {
            Head = "";
            Index = 0;
            Length = 0;
        }

        /// <summary>
        /// 头
        /// </summary>
        public string Head;
        /// <summary>
        /// 坐标
        /// </summary>
        public long Index;
        /// <summary>
        /// 长度
        /// </summary>
        public int Length;
    }

    /// <summary>
    /// 文件操作辅助类
    /// </summary>
    public class Assistance {

        /// <summary>
        /// utf8不带bom格式
        /// </summary>
        public static System.Text.UTF8Encoding utf8WithoutBOM { get { return new System.Text.UTF8Encoding(false); } }

    }

    /// <summary>
    /// 内存快读访问暂存区管理
    /// </summary>
    public class MemorizePreviousItem {

        /// <summary>
        /// 存储核心，最多10项
        /// </summary>
        private List<Tools.StringGroup> memorizeCore;
        /// <summary>
        /// 头记录
        /// </summary>
        private List<string> memorizeHead;

        public MemorizePreviousItem() {
            memorizeCore = new List<Tools.StringGroup>();
            memorizeHead = new List<string>();
        }

        /// <summary>
        /// 添加到队列
        /// </summary>
        /// <param name="data">添加内容</param>
        /// <param name="head">头标识</param>
        public void Add(StringGroup data, string head) {
            //确定删除
            if (memorizeCore.Count == 10) {
                memorizeCore.RemoveAt(0);
                memorizeHead.RemoveAt(0);
            }

            //添加
            memorizeCore.Add(data);
            memorizeHead.Add(head);
        }

        /// <summary>
        /// 尝试返回与head匹配的结果，没有返回空
        /// </summary>
        /// <param name="head">头标识</param>
        public StringGroup TryGetItem(string head) {
            int getIndex = memorizeHead.IndexOf(head);
            if (getIndex == -1) { return new StringGroup("", Environment.NewLine); }

            return memorizeCore[getIndex];
        }
    }

}