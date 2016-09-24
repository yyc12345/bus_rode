﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
//字符串分割的命名空间
using System.Text.RegularExpressions;

namespace bus_rode.Kernel.Tools {

    public class StringGroup {
        /// <summary>
        /// 源数据
        /// </summary>
        private string _sourceString;

        /// <summary>
        /// 源数据
        /// </summary>
        public string sourceString { get { return _sourceString; } }

        /// <summary>
        /// 分割文本
        /// </summary>
        private string _SplitString;

        /// <summary>
        /// 分割文本
        /// </summary>
        public string SplitString { get { return _SplitString; } }

        #region 初始化

        /// <summary>
        /// 以指定字符串初始化
        /// </summary>
        /// <param name="value">字符</param>
        /// <param name="splitValue">分割字符</param>
        public StringGroup(string value, string splitValue) {
            _sourceString = value;
            _SplitString = splitValue;
        }

        /// <summary>
        /// 以指定字符组初始化
        /// </summary>
        /// <param name="value">字符</param>
        /// <param name="splitValue">分割字符</param>
        public StringGroup(string[] value, string splitValue) {
            _sourceString = string.Join(splitValue, value);
            _SplitString = splitValue;
        }

        /// <summary>
        /// 以arraylist初始化
        /// </summary>
        /// <param name="value">字符</param>
        /// <param name="splitValue">分割字符</param>
        public StringGroup(ArrayList value, string splitValue) {
            _sourceString = "";

            foreach (string a in value) {
                if (_sourceString == "") { _sourceString = a; } else {
                    _sourceString += splitValue;
                    _sourceString += a;
                }
            }
            _SplitString = splitValue;
        }

        /// <summary>
        /// 以list(string)初始化
        /// </summary>
        /// <param name="value">字符</param>
        /// <param name="splitValue">分割字符</param>
        public StringGroup(List<string> value, string splitValue) {
            _sourceString = "";

            foreach (string a in value) {
                if (_sourceString == "") { _sourceString = a; } else {
                    _sourceString += splitValue;
                    _sourceString += a;
                }
            }
            _SplitString = splitValue;
        }

        /// <summary>
        /// 以bigarraylist初始化
        /// </summary>
        /// <param name="value">字符</param>
        /// <param name="splitValue">分割字符</param>
        public StringGroup(BigArrayList value, string splitValue) {
            _sourceString = "";

            for (int a = 0; a < value.Count; a++) {
                if (_sourceString == "") { _sourceString = value.Item(a).ToString(); } else {
                    _sourceString += splitValue;
                    _sourceString += value.Item(a).ToString();
                }
            }
            _SplitString = splitValue;
        }

        #endregion

        #region 输出

        /// <summary>
        /// 常规输出
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return _sourceString;
        }

        /// <summary>
        /// 输出到字符组
        /// </summary>
        /// <returns></returns>
        public string[] ToStringGroup() {
            //if (_sourceString == "") { return null; }
            return Regex.Split(_sourceString, _SplitString, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 输出到arraylist
        /// </summary>
        /// <returns></returns>
        public ArrayList ToArrayList() {
            if (_sourceString == "") { return new ArrayList(); }

            ArrayList sendOut = new ArrayList();
            string[] sp = Regex.Split(_sourceString, _SplitString, RegexOptions.IgnoreCase);
            foreach (string a in sp) {
                sendOut.Add(a);
            }

            return sendOut;
        }

        /// <summary>
        /// 输出到list(string)
        /// </summary>
        /// <returns></returns>
        public List<string> ToList() {
            if (_sourceString == "") { return new List<string>(); }

            List<string> sendOut = new List<string>();
            string[] sp = Regex.Split(_sourceString, _SplitString, RegexOptions.IgnoreCase);
            foreach (string a in sp) {
                sendOut.Add(a);
            }

            return sendOut;
        }

        /// <summary>
        /// 输出到bigarraylist
        /// </summary>
        /// <returns></returns>
        public BigArrayList ToBigArrayList() {
            if (_sourceString == "") { return new BigArrayList(); }

            BigArrayList sendOut = new BigArrayList();
            string[] sp = Regex.Split(_sourceString, _SplitString, RegexOptions.IgnoreCase);
            foreach (string a in sp) {
                sendOut.Add(a);
            }

            return sendOut;
        }

        /// <summary>
        /// 输出到一个新的分割符文本
        /// </summary>
        /// <param name="splitValue">新分隔符</param>
        /// <returns></returns>
        public string ToNewSplitWord(string splitValue) {
            if (_sourceString == "") { return null; }
            if (splitValue == _SplitString) { return _sourceString; }

            return _sourceString.Replace(_SplitString, splitValue);
        }

        #endregion


    }
}
