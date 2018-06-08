﻿using System;
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

        public rtuSender(int updateTime){
            this.rtuUpdateTime = updateTime;

        }

        public void setUpdateTime(int time)
        {
            this.rtuUpdateTime = time;

        }

        public int getUpdateTime(int time)
        {
            return this.rtuUpdateTime;
        }
    }
}
