﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BusRodeDll {
    public class ResourcesDll {

        public ResourcesDll() {

            DllDependBusRodeVersion = 9000;
            DllWriter = "Sample Name";
            DllRegion = "Country-Province-City";
            DllVersion = "1.0.0.0";
            DllLanguage = "en-US";

        }

        //here are some information which will read when bus_rode is initializing.
        //you must know the mean of them and you need to read the book of develop bus_rode's mod.
        #region information

        public int DllDependBusRodeVersion;

        public string DllRegion;

        public string DllWriter;

        public string DllVersion;

        public string DllLanguage;

        #endregion

        /// <summary>
        /// the value which main application send it order
        /// </summary>
        public string DllCommand;

        /// <summary>
        /// the function which will be invoked when main application initialize this mode
        /// </summary>
        /// <returns></returns>
        public bool Initialize() {
            //type your code here
            return true;
        }

        /// <summary>
        /// the main function of getting data
        /// </summary>
        /// <returns></returns>
        public string GetData() {
            //type your code here
            return "";
        }

    }
}
