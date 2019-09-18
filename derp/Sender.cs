using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace piWindPotential
{
    /*
     * The sender class is an abstract class that allows you to send
     * data from PI to your desired OPC tags.
     * 
     * This is meant to be implemented, so you'll have to do that youself
     * Note that this class is writen a year affter the rtuSender class. So a lot of stuff 
     * might be weird here
     * 
     */
    abstract class Sender
    {

        //Cancelation tokens. This is used 
        protected CancellationTokenSource source;
        protected CancellationToken token;


        //update updateInterval. Typically in minutes. Might be in seconds
        //Depending on application
        private TimeSpan updateInterval;

        //State
        private Boolean state;

        /// <summary>
        /// method to set whether the user activate the program or not
        /// </summary>
        /// <param name="v"></param>
        public abstract void setState(bool v);
        /// <summary>
        /// Method used to cancel ongoing tasks. Method originally used to cancel all ongoing RTU calls
        /// </summary>
        public abstract void cancel();
        /// <summary>
        /// getter to rectrieve the time update interval 
        /// </summary>
        /// <returns></returns>
        public abstract TimeSpan getUpdateInterval();
        /// <summary>
        /// Set the update interval of the Timer
        /// </summary>
        /// <param name="updateTime"></param>
        public abstract void setUpdateInterval(TimeSpan updateTime);

        /// <summary>
        /// Set the list of all the PI tags that you want to write to the OPC tags
        /// </summary>
        /// <param name="list"></param>
        public abstract void setList(List<string[]> list);
        public abstract void deleteAllLists();

        /// <summary>
        /// This function was originally suppose to set  the RTU update. 
        /// </summary>
        /// <param name="timeSpan"></param>
        public abstract void setUpdateTime(TimeSpan timeSpan);

        /// <summary>
        /// Method to write the PI tag values to the OPC tags
        /// </summary>
        public abstract void writeToOpcTags();

        /// <summary>
        /// Method to recreate a cancellation token. Cancellation tokens are used to 
        /// cancel tasks. You might need this for parallel tasks across multiple 
        /// OPC Servers or multiple RTUs, or if you require multiple tasks, then you
        /// don't need this
        /// </summary>
        private void recreateToken()
        {
            this.source = new CancellationTokenSource();
            this.token = source.Token;
        }

    }
}
