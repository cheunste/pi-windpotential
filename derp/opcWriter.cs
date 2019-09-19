using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using OpcLabs.EasyOpc.DataAccess;

namespace piWindPotential
{

    /*
     * This class is responsible for packaging the JSON data and then sending it off to the RTU
     * 
    */

    class opcWriter
    {
        private TimeSpan rtuUpdateTime;
        private Dictionary<String,String> piToDNPDict;
        private Dictionary<String,int> dnpIndexDict;
        private Dictionary<String,String> ipAddressDict;
        private Dictionary<String,String> siteDict;

        //Reference to the MainWindow
        MainWindow mainWindow;

        //State
        private Boolean state;

        //Cancelation tokens
        private CancellationTokenSource source;
        private CancellationToken token;

        //update updateInterval
        private TimeSpan updateInterval;

        //Member vairabesl
        EasyDAClient client = new EasyDAClient();
        static EasyDAClient opcServer = new EasyDAClient();
        private static string serverName = "SV.OPCDA.1";

        //Constructor
        public opcWriter()
        {
        }



        //This method sets the wait time update interval for the program to push out to the RTU
        public void setUpdateInterval(TimeSpan updateInterval){
            this.updateInterval = updateInterval;
        }

        //get the update time interval for the program
        public TimeSpan getUpdateInterval(){
            return this.updateInterval;
        }                                      

        private void  recreateToken(){
            this.source = new CancellationTokenSource();
            this.token= source.Token;
        }

        //Method to delete all lists. This is used when you need to call
        public void deleteAllLists(){
                    }

        //Build master lists
        private void buildMasterList(){
        }

        //This method cancels all ongoing RTU calls
        public void cancelRTUCalls(){
            this.source.Cancel();
        }

        //Setter funtion to set the RTU update time, in seconds
        public void setUpdateTime(TimeSpan time)
        {
            this.rtuUpdateTime = time;
        }

        //Getter funtion to set the RTU update time, in seconds
        public TimeSpan getUpdateTime(TimeSpan time)
        {
            return this.rtuUpdateTime;
        }


        //Sets the array passed by the piGetter class to the rtuArrays in this class
        public void setList(List<String[]> valueList){

            foreach (String[] tempArray in valueList){
                //At this point, you'll just need to write this to the OPC Server
            }
            //Put all the lists above into a master list. Now you have a way to iterate over everything
            //After splitting the arrays, you then need to send the data. 
            // This might be the point where you start threading

        }

        //This function simply adds the PI String to the List
        private void addToList(List<String[]>siteArray,String[]tempArray){
            siteArray.Add(tempArray);
        }


        public void setState(Boolean state){
            this.state = state;
        }

        public Boolean getState(){
            return this.state;
        }

        private void writeToOPCServer(List<String[]> piDataList){
            int temp = 0;

            while(this.state== true){
                String value = (double.Parse(piDataList.ElementAt(temp)[1]) * 1000).ToString("0.00");

                //Add the delay here
                //Wait one time interval. Only escape this if there is an interrupt (Change in either timestamps, update/sampling time or toggle button state)
                var cancelled = this.token.WaitHandle.WaitOne(getUpdateInterval());
                if (cancelled && this.state ==false ){
                    Console.WriteLine("Cancelled");
                    recreateToken();
                    break;
                }

                temp++;
                if (temp >= piDataList.Count){
                    temp = 0;
                }

            }
        }

        private static void writeTagValue(string tag,object value)
        {
            try
            {
                opcServer.WriteItemValue(serverName, tag, value);
            }
            catch(Exception e)
            {
            }

        }
               
    }
}
