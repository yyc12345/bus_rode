using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_rode.Kernel.Management.UserInterfaceBlockStruct {

    public class LinePageSubwayBlockStruct {

        public LinePageSubwayBlockStruct() {
            exitName = "";
            exitDescription = "";
        }

        /// <summary>
        /// 出口名
        /// </summary>
        private string exitName;
        /// <summary>
        /// 出口名
        /// </summary>
        public string ExitName { get { return exitName; } set { exitName = value; } }
        /// <summary>
        /// 出口描述
        /// </summary>
        private string exitDescription;
        /// <summary>
        /// 出口描述
        /// </summary>
        public string ExitDescription { get { return exitDescription; } set { exitDescription = value; } }

    }
}
