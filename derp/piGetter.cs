using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //Constructor
        public piGetter()
        {

            Console.WriteLine("End Date: " + endDateTime);
            Console.WriteLine("State Date: " + startDateTime);

        }


        //This function gets called when an event changes
        public void waitForResponse() { }

        //This returns the array. This is called by other classes
        public void getArray() {  }

        //Gets the array
        private void composeArray()
        {

        }

        public void setStartDateTime(String date) { this.startDateTime = date; }
        public void setEndDateTime (String date) { this.endDateTime = date; }

        //Method to check if the program is active or not
        public void isActive(Boolean state)
        {
            if (state)
            {
                //Fetch PI Data

            }
            else
            {
                // Stop everything

            }
            
            
        }

        //This function stops the current array that is being processed
        public void stopArray(){

        }
    }

}
