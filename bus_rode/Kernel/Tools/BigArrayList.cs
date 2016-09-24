using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_rode.Kernel.Tools
{
    /// <summary>
    /// 加大容量的ArrayList
    /// </summary>
    public class BigArrayList
    {

        /// <summary>
        /// 主列表
        /// 每个分列表最多500项(0-499)
        /// 主列表最多100项(0-99)
        /// </summary>
        private List<ArrayList> MainCollection;

        /// <summary>
        /// 分支最大数据量
        /// </summary>
        private int BranchMaxCount;
        /// <summary>
        /// 主链上最大项数
        /// </summary>
        private int MainMaxCount;

        /// <summary>
        /// 列表中项的个数
        /// </summary>
        private int _count;
        /// <summary>
        /// 列表中项的个数
        /// </summary>
        public int Count { get { return _count; } }

        //构造，初始化
        public BigArrayList()
        {
            MainCollection = new List<ArrayList>();
            _count = 0;
            BranchMaxCount = 500;
            MainMaxCount = 100;
        }

        /// <summary>
        /// 向集合末尾添加一个数据
        /// </summary>
        /// <param name="value">数据</param>
        public void Add(object value)
        {
            //没有项
            if (MainCollection.Count == 0)
            {
                ArrayList into = new ArrayList();
                into.Add(value);
                MainCollection.Add(into);
            }
            else
            {
                //项数满了
                if (_count == BranchMaxCount * MainMaxCount) { throw new OutOfMemoryException("Out of the collection max count"); }
                else
                {
                    //到达一个项的末尾
                    if (_count % BranchMaxCount == 0)
                    {
                        ArrayList into = new ArrayList();
                        into.Add(value);
                        MainCollection.Add(into);
                    }
                    else
                    {
                        //常规添加
                        MainCollection[MainCollection.Count - 1].Add(value);
                    }
                }
            }

            _count += 1;
        }

        /// <summary>
        /// 向集合末尾添加一系列数据
        /// </summary>
        /// <param name="value"></param>
        public void Add(ArrayList value)
        {
            foreach (object item in value)
            {
                Add(item);
            }

        }

        /// <summary>
        /// 将集合清空
        /// </summary>
        public void Clear()
        {
            MainCollection.Clear();
        }

        /// <summary>
        /// 将集合指定序列处的数据代替
        /// </summary>
        /// <param name="address">索引</param>
        /// <param name="value">数据</param>
        public void Replace(int address, object value)
        {
            if (address > _count - 1) { throw new IndexOutOfRangeException("out of index"); }
            else
            {
                int mainIndex = (int)(address / BranchMaxCount);
                int branchIndex = (int)(address % BranchMaxCount);

                MainCollection[mainIndex][branchIndex] = value;
            }

        }

        /// <summary>
        /// 获取指定索引处的项
        /// </summary>
        /// <param name="address">索引</param>
        /// <returns></returns>
        public object Item(int address)
        {
            if (address > _count - 1)
            {
                throw new IndexOutOfRangeException("out of index");
                //return null;
            }
            else
            {
                int mainIndex = (int)(address / BranchMaxCount);
                int branchIndex = (int)(address % BranchMaxCount);

                return MainCollection[mainIndex][branchIndex];
            }
        }

        /// <summary>
        /// 移出集合指定索引处的数据-数据量很大时建议用取代
        /// </summary>
        /// <param name="address">索引</param>
        public void RemoveAt(int address)
        {
            if (address > _count - 1)
            {
                throw new IndexOutOfRangeException("out of index");
            }
            else
            {
                int mainIndex = (int)(address / BranchMaxCount);
                int branchIndex = (int)(address % BranchMaxCount);

                //remove
                MainCollection[mainIndex].RemoveAt(branchIndex);
                //change list
                for (int nowMainIndex = mainIndex; nowMainIndex < MainCollection.Count - 1; nowMainIndex++)
                {
                    //复制到上一行末
                    MainCollection[nowMainIndex].Add(MainCollection[nowMainIndex + 1][0]);
                    //删除下一行第一
                    MainCollection[nowMainIndex + 1].RemoveAt(0);
                }

                //检测最后一个是不是空项
                if (MainCollection[MainCollection.Count - 1].Count == 0) { MainCollection.RemoveAt(MainCollection.Count - 1); }


                _count -= 1;
            }
        }

        /// <summary>
        /// 返回给定数据在集合中的索引，没有返回-1
        /// </summary>
        /// <param name="value">数据</param>
        /// <returns></returns>
        public int IndexOf(object value)
        {
            int index = -1;

            for (int a = 0; a < MainCollection.Count; a++)
            {
                int branchIndex = MainCollection[a].IndexOf(value);
                if (branchIndex != -1)
                {
                    //可以
                    index = (a * BranchMaxCount) + branchIndex;
                    break;
                }
            }

            return index;
        }

        /// <summary>
        /// 将数据插入指定索引处-数据量很大时建议用取代
        /// </summary>
        /// <param name="address">索引</param>
        /// <param name="value">数据</param>
        public void Insert(int address, object value)
        {
            if ((address > _count - 1) || (_count == BranchMaxCount * MainMaxCount))
            {
                throw new IndexOutOfRangeException("out of index");
            }
            else
            {
                int mainIndex = (int)(address / BranchMaxCount);
                int branchIndex = (int)(address % BranchMaxCount);

                //add
                MainCollection[mainIndex].Insert(branchIndex, value);
                //change list
                for (int nowMainIndex = mainIndex; nowMainIndex < MainCollection.Count - 1; nowMainIndex++)
                {
                    //复制到下一行第一个
                    MainCollection[nowMainIndex + 1].Insert(0, MainCollection[nowMainIndex][BranchMaxCount]);
                    //删除本行最后一个
                    MainCollection[nowMainIndex].RemoveAt(BranchMaxCount);
                }

                //检测最后一个有没有超限
                if (MainCollection[MainCollection.Count - 1].Count == BranchMaxCount + 1)
                {
                    //添加新集合
                    ArrayList into = new ArrayList();
                    into.Add(MainCollection[MainCollection.Count - 1][BranchMaxCount]);
                    MainCollection.Add(into);
                    //移出已经成为上上一行的最后一个
                    MainCollection[MainCollection.Count - 2].RemoveAt(BranchMaxCount);
                }


                _count += 1;
            }
        }

        /// <summary>
        /// 输出集合文本表达，项数超过50不输出
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (_count > 50) { return "Item so much!"; }

            string str = "";

            for (int a = 0; a < MainCollection.Count; a++)
            {
                for (int b = 0; b < MainCollection[a].Count; b++)
                {
                    if (b != 0) { str += ","; }        
                    str += MainCollection[a][b].ToString();
                }

                str += Environment.NewLine;
            }

            return str;
        }

    }
}
