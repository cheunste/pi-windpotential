using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace derp
{

    /*
     * This class is responsible for packaging the JSON data and then sending it off to the RTU
     * 
    */

    class rtuSender
    {
        private int rtuUpdateTime;
        private List<String[]> valueList;
        private Dictionary<String,String> piToDNPDict;
        //The rtu arrays
        private List<String[]> klondikeRTUList;
        private List<String[]> bigHornRTUList;
        private List<String[]> jonesRTUList;
        private List<String[]> juniperRTUList;

        public rtuSender(){
            //Sets the dictionary that maps Tags to a site
            this.piToDNPDict = new Dictionary<String, String>();
            setUpPiToDNPDict();
            this.klondikeRTUList = new List<String[]>();
            this.bigHornRTUList = new List<String[]>();
            this.jonesRTUList = new List<String[]>();
            this.juniperRTUList = new List<String[]>();
        }

        private void setUpPiToDNPDict(){
            this.piToDNPDict.Add("KL1.WF.WPot.CORE","KLON1_AGC_AvailablePwr_I");
            this.piToDNPDict.Add("KL2.WF.WPot.CORE","KLON2_AGC_AvailablePwr_I");
            this.piToDNPDict.Add("KL3A.WF.WPot.CORE","KLONA_AGC_AvailablePwr_I");
            this.piToDNPDict.Add("KL3GE.WF.WPot.CORE","KLONG_AGC_AvailablePwr_I");
            this.piToDNPDict.Add("KL3SW.WF.WPot.CORE","KLONS_AGC_AvailablePwr_I");
            this.piToDNPDict.Add("KL3MHI.WF.WPot.CORE","KLONM_AGC_AvailablePwr_I");
            this.piToDNPDict.Add("HC1.WF.WPot.CORE","HAYCA_AGC_AvailablePwr_I");
            this.piToDNPDict.Add("SP1.WF.WPot.CORE","STPOI_AGC_AvailablePwr_I");
            this.piToDNPDict.Add("LJ2A.WF.WPot.CORE","LEJUN_AGC_AvailablePwr_I");
            this.piToDNPDict.Add("LJ2B.WF.WPot.CORE","LEJU2_AGC_AvailablePwr_I");
            this.piToDNPDict.Add("PS1.WF.WPot.CORE","PESPR_AGC_AvailablePwr_I");
            this.piToDNPDict.Add("BH1.WF.WPot.CORE","BIGHO_AGC_AvailablePwr_I");
            this.piToDNPDict.Add("BH2.WF.WPot.CORE","BIGH2_AGC_AvailablePwr_I");
            this.piToDNPDict.Add("JC1.WF.WPot.CORE","JUNCA_AGC_AvailablePwr_I");
        }

        //Setter funtion to set the RTU update time, in seconds
        public void setUpdateTime(int time)
        {
            this.rtuUpdateTime = time;

        }

        //Getter funtion to set the RTU update time, in seconds
        public int getUpdateTime(int time)
        {
            return this.rtuUpdateTime;
        }

        //Sets the array passed by the piGetter class to the rtuArrays in this class
        public void setArray(List<String[]> valueList){

            foreach (String[] tempArray in valueList){
                //At this point, you'll need to split this into different arrays. One for each of the RTUs
                //based on the PI tag.
                switch(tempArray[0]){
                    case "KL1.WF.WPot.CORE":
                        addToList(this.klondikeRTUList,tempArray);
                        break;
                    case "KL2.WF.WPot.CORE":
                        addToList(this.klondikeRTUList,tempArray);
                        break;
                    case "KL3A.WF.WPot.CORE":
                        addToList(this.klondikeRTUList,tempArray);
                        break;
                    case "KL3GE.WF.WPot.CORE":
                        addToList(this.klondikeRTUList,tempArray);
                        break;
                    case "KL3SW.WF.WPot.CORE":
                        addToList(this.klondikeRTUList,tempArray);
                        break;
                    case "KL3MHI.WF.WPot.CORE":
                        addToList(this.klondikeRTUList,tempArray);
                        break;
                    case "HC1.WF.WPot.CORE":
                        addToList(this.klondikeRTUList,tempArray);
                        break;
                    case "SP1.WF.WPot.CORE":
                        addToList(this.klondikeRTUList,tempArray);
                        break;
                    case "LJ2A.WF.WPot.CORE":
                        addToList(this.jonesRTUList,tempArray);
                        break;
                    case "LJ2B.WF.WPot.CORE":
                        addToList(this.jonesRTUList,tempArray);
                        break;
                    case "PS1.WF.WPot.CORE":
                        addToList(this.jonesRTUList,tempArray);
                        break;
                    case "BH1.WF.WPot.CORE":
                        addToList(this.bigHornRTUList,tempArray);
                        break;
                    case "BH2.WF.WPot.CORE":
                        addToList(this.bigHornRTUList,tempArray);
                        break;
                    case "JC1.WF.WPot.CORE":
                        addToList(this.juniperRTUList,tempArray);
                        break;
                }

            }
            //After splitting the arrays, you then need to send the data. 
            sendToRTU();

        }

        //This function simply adds the PI String to the List
        private void addToList(List<String[]>siteArray,String[]tempArray){
            siteArray.Add(tempArray);
        }

        //This function sends the data to the RTU
        private void sendToRTU(){
}
        
    }
}
