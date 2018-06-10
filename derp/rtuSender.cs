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
        //The rtu List
        private List<String[]> klondikeRTUList;
        private List<String[]> bigHornRTUList;
        private List<String[]> jonesRTUList;
        private List<String[]> juniperRTUList;

        //the site Lists
        private List<String[]> kl1;
        private List<String[]> kl2;
        private List<String[]> k3A;
        private List<String[]> k3GE;
        private List<String[]> k3S;
        private List<String[]> k3mhi;
        private List<String[]> hc1;
        private List<String[]> sp1;
        private List<String[]> lj2a;
        private List<String[]> lj2b;
        private List<String[]> ps1;
        private List<String[]> bh1;
        private List<String[]> bh2;
        private List<String[]> jc1;
        private List<List<String[]>> masterList;

        //Member variable to the interruptMnaager (reference)
        private InterruptManager im;

        //Constructor
        public rtuSender(){
            //Sets the dictionary that maps Tags to a site
            this.piToDNPDict = new Dictionary<String, String>();
            setUpPiToDNPDict();
            this.klondikeRTUList = new List<String[]>();
            this.bigHornRTUList = new List<String[]>();
            this.jonesRTUList = new List<String[]>();
            this.juniperRTUList = new List<String[]>();
            this.kl1 = new List<String[]>();
            this.kl2 = new List<String[]>();
            this.k3A = new List<String[]>();
            this.k3GE = new List<String[]>();
            this.k3S = new List<String[]>();
            this.k3mhi = new List<String[]>();
            this.hc1 = new List<String[]>();
            this.sp1 = new List<String[]>();
            this.lj2a = new List<String[]>();
            this.lj2b = new List<String[]>();
            this.ps1 = new List<String[]>();
            this.bh1 = new List<String[]>();
            this.bh2 = new List<String[]>();
            this.jc1 = new List<String[]>();
            this.masterList = new List<List<String[]>>();

            this.im = new InterruptManager();
        }

        public void deleteAllLists(){
            klondikeRTUList.RemoveAll();
            bigHornRTUList.RemoveAll();
            jonesRTUList.RemoveAll();
            juniperRTUList.RemoveAll();

            //the site Lists
            kl1.RemoveAll();
            kl2.RemoveAll();
            k3A.RemoveAll();
            k3GE.RemoveAll();
            k3S.RemoveAll();
            k3mhi.RemoveAll();
            hc1.RemoveAll();
            sp1.RemoveAll();
            lj2a.RemoveAll();
            lj2b.RemoveAll();
            ps1.RemoveAll();
            bh1.RemoveAll();
            bh2.RemoveAll();
            jc1.RemoveAll();
            masterList.RemoveAll();

        }

        private void buildMasterList(){
            this.masterList.Add(kl1);
            this.masterList.Add(kl2);
            this.masterList.Add(k3A);
            this.masterList.Add(k3GE);
            this.masterList.Add(k3S);
            this.masterList.Add(k3mhi);
            this.masterList.Add(hc1);
            this.masterList.Add(sp1);
            this.masterList.Add(lj2a);
            this.masterList.Add(lj2b);
            this.masterList.Add(ps1);
            this.masterList.Add(bh1);
            this.masterList.Add(bh2);
            this.masterList.Add(jc1);
        }
        public void sendToRTU(){
            buildMasterList();
            foreach(List<String[]> tempList in masterList){

                Console.WriteLine(tempList);
                //Start threading here
            }


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
                        addToList(this.kl1,tempArray);
                        break;
                    case "KL2.WF.WPot.CORE":
                        addToList(this.kl2,tempArray);
                        break;
                    case "KL3A.WF.WPot.CORE":
                        addToList(this.k3A,tempArray);
                        break;
                    case "KL3GE.WF.WPot.CORE":
                        addToList(this.k3GE,tempArray);
                        break;
                    case "KL3SW.WF.WPot.CORE":
                        addToList(this.k3S,tempArray);
                        break;
                    case "KL3MHI.WF.WPot.CORE":
                        addToList(this.k3mhi,tempArray);
                        break;
                    case "HC1.WF.WPot.CORE":
                        addToList(this.hc1,tempArray);
                        break;
                    case "SP1.WF.WPot.CORE":
                        addToList(this.sp1,tempArray);
                        break;
                    case "LJ2A.WF.WPot.CORE":
                        addToList(this.lj2a,tempArray);
                        break;
                    case "LJ2B.WF.WPot.CORE":
                        addToList(this.lj2b,tempArray);
                        break;
                    case "PS1.WF.WPot.CORE":
                        addToList(this.ps1,tempArray);
                        break;
                    case "BH1.WF.WPot.CORE":
                        addToList(this.bh1,tempArray);
                        break;
                    case "BH2.WF.WPot.CORE":
                        addToList(this.bh2,tempArray);
                        break;
                    case "JC1.WF.WPot.CORE":
                        addToList(this.jc1,tempArray);
                        break;
                }

            }
            //Put all the lists above into a master list. Now you have a way to iterate over everything
            //After splitting the arrays, you then need to send the data. 
            // This might be the point where you start threading

        }

        //This function simply adds the PI String to the List
        private void addToList(List<String[]>siteArray,String[]tempArray){
            siteArray.Add(tempArray);
        }

        //This function sends the data to the RTU
        private void compoundJSOn(){

            String derp = "";
            String data =
                "{\"index\": 1, \"overRange\": False, \"name\": \"STPOI_AGC_RampUp_I\", \"staticType\": {\"group\": 30, \"variation\": 3}, \"eventType\": {\"group\": 32, \"variation\": 3}, \"site\": \"Klondike\", \"value\": 111111.0, \"communicationsLost\": False, \"remoteForced\": False, \"online\": True, \"device\": \"Wind Node RTAC\", \"localForced\": False, \"eventClass\": 2, \"type\": \"analogInputPoint\", \"referenceError\": False, \"restart\": False}";

            //while both the toggle (in GUI) is enabled and the interval flag is raised, send the data
            foreach (String[] item in this.klondikeRTUList){
               Console.WriteLine(item); 
            }
            /*
            foreach (String[] item in this.jonesRTUList){
            }
            foreach (String[] item in this.juniperRTUList){
            }
            foreach (String[] item in this.bigHornRTUList){
            }
            */
        }
        
    }
}
