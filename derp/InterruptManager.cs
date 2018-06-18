using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piWindPotential
{
    /*
     * This class is resposible for managing interrupt throughout the entire program
     * This is mainly used for piGetter
     * 
     * Probably should be a singleton as there should be only one instance of this
    */
    class InterruptManager
    {

        private Boolean programEnabled;
        public InterruptManager()
        {
            //TODO:
            // 1) Create reference to the other classes 
            // 2) Fetch their update time prameters
            
            //Create new threads and pass their time parameters
            this.programEnabled=false;

        }

        private void onTimeChange()
        {
        }

        public void setprogramEnabled(Boolean programEnabled){
           this.programEnabled=programEnabled;
        }
        public Boolean isProgramEnabled(){
            return this.programEnabled;
        }

    }
}
