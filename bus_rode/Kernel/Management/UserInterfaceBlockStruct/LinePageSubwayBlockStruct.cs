using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_rode.Kernel.Management.UserInterfaceBlockStruct {

    public class LinePageSubwayBlockStruct {

        public LinePageSubwayBlockStruct() {
            ExitName = "";
            ExitDescription = "";
        }

        /// <summary>
        /// 出口名
        /// </summary>
        public string ExitName { get; set; }
        /// <summary>
        /// 出口描述
        /// </summary>
        public string ExitDescription { get; set; }

    }
}
