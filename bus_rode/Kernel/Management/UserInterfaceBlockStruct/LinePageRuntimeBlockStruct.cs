using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_rode.Kernel.Management.UserInterfaceBlockStruct {

    public class LinePageRuntimeBlockStruct {

        public LinePageRuntimeBlockStruct() {
            id = "";
            message1 = "";
            message2 = "";
            message3 = "";
        }

        /// <summary>
        /// ID
        /// </summary>
        private string id;
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get { return id; } set { id = value; } }

        /// <summary>
        /// message 1
        /// </summary>
        private string message1;
        /// <summary>
        /// message 2
        /// </summary>
        private string message2;
        /// <summary>
        /// message 3
        /// </summary>
        private string message3;
        /// <summary>
        /// message 1
        /// </summary>
        public string Message1 { get { return message1; } set { message1 = value; } }
        /// <summary>
        /// message 2
        /// </summary>
        public string Message2 { get { return message2; } set { message2 = value; } }
        /// <summary>
        /// message 3
        /// </summary>
        public string Message3 { get { return message3; } set { message3 = value; } }
        /// <summary>
        /// all message
        /// </summary>
        public string Description { get { return message1 + " " + message2 + " " + message3; } }

        /// <summary>
        /// 从属于车站列表的站台坐标
        /// </summary>
        private int belongToStopIndex;
        /// <summary>
        /// 从属于车站列表的站台坐标
        /// </summary>
        public int BelongToStopIndex { get { return belongToStopIndex; } set { belongToStopIndex = value; } }
    }
}
