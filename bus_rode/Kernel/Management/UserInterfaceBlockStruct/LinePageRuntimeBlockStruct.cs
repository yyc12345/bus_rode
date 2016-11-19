using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_rode.Kernel.Management.UserInterfaceBlockStruct {

    public class LinePageRuntimeBlockStruct {

        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// message 1
        /// </summary>
        public string Message1 { get; set; }
        /// <summary>
        /// message 2
        /// </summary>
        public string Message2 { get; set; }
        /// <summary>
        /// message 3
        /// </summary>
        public string Message3 { get; set; }
        /// <summary>
        /// all message
        /// </summary>
        public string Description { get { return Message1 + " " + Message2 + " " + Message3; } }

        /// <summary>
        /// 从属于车站列表的站台坐标
        /// </summary>
        public int BelongToStopIndex { get; set; }

        /// <summary>
        /// 是否是上行线路的部分，true=上行 false=下行
        /// </summary>
        public bool IsUpLine { get; set; }
    }
}
