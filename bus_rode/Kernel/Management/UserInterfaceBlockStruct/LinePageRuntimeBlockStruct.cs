using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_rode.Kernel.Management.UserInterfaceBlockStruct {

    public class LinePageRuntimeBlockStruct {

        public LinePageRuntimeBlockStruct() {
            id = "";
        }

        /// <summary>
        /// ID
        /// </summary>
        private string id;
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get { return id; } set { id = value; } }

    }
}
