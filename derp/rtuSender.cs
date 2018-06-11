using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thread;

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
        private Dictionary<String,int> dnpIndexDict;
        private Dictionary<String,String> ipAddressDict;
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
            this.dnpIndexDict = new Dictionary<String, int>();
            this.ipAddressDict = new Dictionary<String,String>();
            setUpPiToDNPDict();
            setUpdnpIndexDict();
            setUpIPAddressDict();
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

        private void setUpdnpIndexDict(){
            this.dnpIndexDict.Add("KLON1_AGC_AvailablePwr_I",26);
            this.dnpIndexDict.Add("KLON2_AGC_AvailablePwr_I",38);
            this.dnpIndexDict.Add("KLONA_AGC_AvailablePwr_I",86);
            this.dnpIndexDict.Add("KLONG_AGC_AvailablePwr_I",74);
            this.dnpIndexDict.Add("KLONS_AGC_AvailablePwr_I",50);
            this.dnpIndexDict.Add("KLONM_AGC_AvailablePwr_I",62);
            this.dnpIndexDict.Add("HAYCA_AGC_AvailablePwr_I",14);
            this.dnpIndexDict.Add("STPOI_AGC_AvailablePwr_I",2);
            this.dnpIndexDict.Add("LEJUN_AGC_AvailablePwr_I",2);
            this.dnpIndexDict.Add("LEJU2_AGC_AvailablePwr_I",14);
            this.dnpIndexDict.Add("PESPR_AGC_AvailablePwr_I",26);
            this.dnpIndexDict.Add("BIGHO_AGC_AvailablePwr_I",2);
            this.dnpIndexDict.Add("BIGH2_AGC_AvailablePwr_I",14);
            this.dnpIndexDict.Add("JUNCA_AGC_AvailablePwr_I",2);
        }

        private void isetUpIPAddressDict(){
            this.ipAddressDict.Add("KLON1_AGC_AvailablePwr_I","10.41.58.124");
            this.ipAddressDict.Add("KLON2_AGC_AvailablePwr_I","10.41.58.124");
            this.ipAddressDict.Add("KLONA_AGC_AvailablePwr_I","10.41.58.124");
            this.ipAddressDict.Add("KLONG_AGC_AvailablePwr_I","10.41.58.124");
            this.ipAddressDict.Add("KLONS_AGC_AvailablePwr_I","10.41.58.124");
            this.ipAddressDict.Add("KLONM_AGC_AvailablePwr_I","10.41.58.124");
            this.ipAddressDict.Add("HAYCA_AGC_AvailablePwr_I","10.41.58.124");
            this.ipAddressDict.Add("STPOI_AGC_AvailablePwr_I","10.41.58.124");
            this.ipAddressDict.Add("LEJUN_AGC_AvailablePwr_I","172.26.21.34");
            this.ipAddressDict.Add("LEJU2_AGC_AvailablePwr_I","172.26.21.34");
            this.ipAddressDict.Add("PESPR_AGC_AvailablePwr_I","172.26.21.34");
            this.ipAddressDict.Add("BIGHO_AGC_AvailablePwr_I","10.41.55.20");
            this.ipAddressDict.Add("BIGH2_AGC_AvailablePwr_I","10.41.55.20");
            this.ipAddressDict.Add("JUNCA_AGC_AvailablePwr_I","172.21.1.19");
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


        //Function to send to RTU
        public void sendToRTU(){
            buildMasterList();
            foreach(List<String[]> tempList in masterList){

                Console.WriteLine(tempList);
                //Start threading here
                var thread = new Thread(sendJSON);
                String dnpTag = this.dnpIndexDict[tempList[0][0]];
                String ipAddress = this.ipAddressDict[dnpTag];
                int indexNumber = this.dnpIndexDict[dnpTag];
                //thread.Start(ipAddress,indexNumber,dnpTag,value);
                /*
                 * TODO:
                 * - Start thread with the four parmameters
                 */ 
                
            }
        }
        //This function sends the data to the RTU
        private void sendJSON(String ipAddress,String indexNumber,String tagName, List<String[]> piDataList){
            int temp = 0;

            while(this.im.isProgramEnabled){
                String data =
                    "{\"index\": "+indexNumber+", \"overRange\": False, \"name\": "
                    +tagName+", \"staticType\": {\"group\": 30, \"variation\": 3}, \"eventType\": {\"group\": 32, \"variation\": 3}, \"site\": \"Klondike\", \"value\": "
                    +value+", \"communicationsLost\": False, \"remoteForced\": False, \"online\": True, \"device\": \"Wind Node RTAC\", \"localForced\": False, \"eventClass\": 2, \"type\": \"analogInputPoint\", \"referenceError\": False, \"restart\": False}";

                //TODO:
                /*
                 * Finish constcuting JSON
                 * Add a wait function that uses the interval time (might need to get from interrupt manager (im)
                 * update the temp varaible, but make sure it doesn't exceed max list. You may need to check the maximum value
                 * of said list
                 * Build a stream writer for all this stuff and then send it through the IP address
                 * You might want to encapsulate this in a try{} catch{}
                 * You then need to pass all this shit back to the GUI's textbox
                 * 
                 */
                
                temp++;
            }


        }

        
    }
}
