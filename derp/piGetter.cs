using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSIsoft.AF.Asset;
using OSIsoft.AF.PI;
using OSIsoft.AF.Time;
using CsvHelper;


namespace piWindPotential
{
    /*
    This class is responsible for fetching data from PI
    This depends on several factors including
     * Sampling time (TBA)
     * PI Tags (This is the potential tags, so it is spretty much set)
     * Timestamp

    This class is also responsible for
     * Formatting data to an array. This array will be then sent to the rtuSender class
    */
    class piGetter
    {
        private String startDateTime;
        private String endDateTime;
        private PIServers pIServers;
        private PIServer piServer;
        //This is the array of all the potential tags in the Gorge 
        private String[] windNodePotentialTags =
        {
            "HA1.T0001.WNAC.WdSpd",    "HA1.T0002.WNAC.WdSpd",    "HA1.T0003.WNAC.WdSpd",    "HA1.T0001.WROT.RotSpd",   "HA1.T0002.WROT.RotSpd",   "HA1.T0003.WROT.RotSpd",   "HA1.T0001.WTUR.NoiseLev", "HA1.T0002.WTUR.NoiseLev", "HA1.T0003.WTUR.NoiseLev", "HA1.T0001.WTUR.TurSt.actSt",
            "HA1.T0002.WTUR.TurSt.actSt",  "HA1.T0003.WTUR.TurSt.actSt",  "HA1.T0001.WNAC.ExTmp",    "HA1.T0002.WNAC.ExTmp",    "HA1.T0003.WNAC.ExTmp",    "HA1.MET1.WMET1.AvMetAlt1Hum", "HA1.MET2.WMET1.AvMetAlt1Tmp", "HA1.MET2.WMET1.AvMetAlt6Hum", "HA1.MET2.WMET1.AvMetAlt6Tmp",
        };
        private List<String> piTags;
        private List<String> opcTags;
        private List<PI_OPC> piToOpcList;
        //This is the list of values that is fetched from PI
        private List<String[]> valueList;
        private AFTimeRange aFTimeRange;
        private TimeSpan samplingInterval;
        private AFTimeSpan span;


        //Constructor
        public piGetter()
        {
            Console.WriteLine("End Date: " + endDateTime);
            Console.WriteLine("State Date: " + startDateTime);

            //initate the server shit
            this.pIServers = new PIServers();
            this.piServer = pIServers.DefaultPIServer;

            this.valueList = new List<String[]>();
            this.piToOpcList = new List<PI_OPC>();
            this.piTags = new List<String>();
            this.opcTags = new List<String>();

        }


        //This function gets called when an event changes
        public void waitForResponse() { }

        //This returns the array. This is called by other classes
        public List<String[]> getList() { return this.valueList; }

        //Gets the array
        private void composeArray()
        {
            this.aFTimeRange = new AFTimeRange(this.startDateTime, this.endDateTime);
            //this.interval = new TimeSpan(0, 5, 0);
            this.span = new AFTimeSpan(this.samplingInterval);

            //Read the csv file pi-opc.csv here
            var reader = new StreamReader("./pi-opc.csv");
            var csv = new CsvReader(reader);
            //STore the contents of the csv into an array
            using (csv = new CsvReader(reader))
            {
                var records = csv.GetRecords<PI_OPC>();
                foreach(var temp in records.ToList())
                {
                    piToOpcList.Add(temp);
                }
            }
            
                foreach (PI_OPC item in piToOpcList)
                {
                    String piTag = item.PITag;
                    PIPoint pi_point = PIPoint.FindPIPoint(this.piServer, piTag);
                    //tagList.Add(pi_point.RecordedValues(aFTimeRange, OSIsoft.AF.Data.AFBoundaryType.Inside, "", false).ToString());
                    AFValues interpolated = pi_point.InterpolatedValues(this.aFTimeRange, this.span, "", false);

                    foreach (AFValue value in interpolated)
                    {
                        String[] temp = { piTag, value.Value.ToString(), value.Timestamp.ToString() };
                        //Temp 0: Name of Wind Node Tag 
                        //Temp 1: Value of the tag
                        //Temp 2: Time Stamp of the tag
                        Console.WriteLine(temp[0] + ", " + temp[1] + ", " + temp[2]);
                        this.valueList.Add(temp);
                    }
                    //Replace the following
                    //this.rtu.setArray(this.valueList);
                }
            //Replace the following
            //this.rtu.sendToRTU();
        }


        public void setStartDateTime(String date) { this.startDateTime = date; }
        public void setEndDateTime (String date) { this.endDateTime = date; }

        //Method to check if the program is active or not
        public void isActive(Boolean state)
        {
            if (state)
            {
                //Raise inerrupt
                //Fetch PI Data
                composeArray();
            }
            else
            {
                stopArray();
            }
            
            
        }

        //This function stops the current array that is being processed
        public void stopArray(){

        }

        //This function essentually restarts the whole PI Gattering process from scratch,
        // which means it will clear the valueList array,  set the time
        // rebuild the array, etc
        public void restart() {

            //TODO:
            //clear the valuelist array
            //update the time methods
            //recompile the array
            
        }
        
        //This function clears the entire valueList.
        public void clearList()
        {
            this.valueList.Clear();
        }

        //method to set the PI Interval (in TimeSpan of minutes)
        //Seriously, a timespan looks something like 
        //this.samplingInterval = new TimeSpan(0, 5, 0);
        public void setSamplingInterval(TimeSpan samplingInterval){
            this.samplingInterval=samplingInterval;
        }
        //method to get the interval. 
        public TimeSpan getInterval(){
            return this.samplingInterval;
        }
    }

}
