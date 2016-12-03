using System;
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
            //do NOT change this code in this function.
            //if you want to process input data. you can use the sample without automatic process.
            //and you need to read the command's format.

            //try split command
            var spCommand = Regex.Split(DllCommand, "@", RegexOptions.IgnoreCase);

            switch (spCommand[0]) {
                case "GetLine":
                    return GetLine(spCommand[1]);
                case "GetStop":
                    return GetStop(spCommand[1]);
                case "GetLineList":
                    return GetLineList(System.Convert.ToInt32(spCommand[1]));
                case "GetStopList":
                    return GetStopList(System.Convert.ToInt32(spCommand[1]));
                case "GetSubway":
                    return GetSubway(spCommand[1]);
                default:
                    return "";
            }
        }

        //store actual function which get data. please type your code here.
        #region actual function

        private string GetLine(string lineName) {
            //type your code here
            return "";
        }

        private string GetStop(string stopName) {
            //type your code here
            return "";
        }

        private string GetLineList(int indexOfBlock) {
            //type your code here
            return "";
        }

        private string GetStopList(int indexOfBlock) {
            //type your code here
            return "";
        }

        private string GetSubway(string stopName) {
            //type your code here
            return "";
        }

        #endregion


    }
}
