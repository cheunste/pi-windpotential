using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSIsoft.AF.Asset;
using OSIsoft.AF.PI;
using OSIsoft.AF.Time;


namespace derp
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
        private rtuSender rtu;
        //This is the array of all the potential tags in the Gorge 
        private String[] windNodePotentialTags =
        {
                "KL1.WF.WPot.CORE", "KL2.WF.WPot.CORE", "KL3A.WF.WPot.CORE",
                "KL3GE.WF.WPot.CORE", "KL3SW.WF.WPot.CORE", "KL3MHI.WF.WPot.CORE",
                "HC1.WF.WPot.CORE", "SP1.WF.WPot.CORE", "LJ2A.WF.WPot.CORE",
                "LJ2B.WF.WPot.CORE", "PS1.WF.WPot.CORE", "BH1.WF.WPot.CORE",
                "BH2.WF.WPot.CORE", "JC1.WF.WPot.CORE"
        };
        //This is the list of values that is fetched from PI
        private List<String[]> valueList;
        private AFTimeRange aFTimeRange;
        private TimeSpan interval;
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

            this.rtu = new rtuSender();
        }


        //This function gets called when an event changes
        public void waitForResponse() { }

        //This returns the array. This is called by other classes
        public List<String[]> getList() { return this.valueList; }

        //Gets the array
        private void composeArray()
        {
            this.aFTimeRange = new AFTimeRange(this.startDateTime, this.endDateTime);
            this.interval = new TimeSpan(0, 5, 0);
            this.span = new AFTimeSpan(interval);

            foreach (String windNodeTag in windNodePotentialTags)
            {
                PIPoint pi_point = PIPoint.FindPIPoint(this.piServer, windNodeTag);
                //tagList.Add(pi_point.RecordedValues(aFTimeRange, OSIsoft.AF.Data.AFBoundaryType.Inside, "", false).ToString());
                AFValues interpolated = pi_point.InterpolatedValues(this.aFTimeRange, this.span, "", false);

                foreach (AFValue value in interpolated)
                {
                    String[] temp ={windNodeTag, value.Value.ToString(), value.Timestamp.ToString()};
                    //Temp 0: Name of Wind Node Tag 
                    //Temp 1: Value of the tag
                    //Temp 2: Time Stamp of the tag
                    Console.WriteLine(temp[0]+", "+temp[1]+", "+temp[2]);
                    this.valueList.Add(temp);
                }
                this.rtu.setArray(this.valueList);
            }
            Console.WriteLine("Herpa derpa derp");
        }

        public void setStartDateTime(String date) { this.startDateTime = date; }
        public void setEndDateTime (String date) { this.endDateTime = date; }

        //Method to check if the program is active or not
        public void isActive(Boolean state)
        {
            if (state)
            {
                //Fetch PI Data
                composeArray();
                //Raise inerrupt
                
            }
            else
            {
                stopArray();
                //Stop interrupt


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
    }

}
