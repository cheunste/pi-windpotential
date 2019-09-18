using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace piWindPotential
{
   class ArticunoSender: Sender
    {

        //Reference to the Main WIndow
        MainWindow mainWindow;


        //updateInterval
        private TimeSpan updateInterval;

        public override void cancel()
        {
            source.Cancel();
        }

        public override void deleteAllLists()
        {
        }

        public override TimeSpan getUpdateInterval() { return updateInterval; }

        public override void setList(List<string[]> list)
        {
        }

        public override void setState(bool v)
        {
        }

        public override void setUpdateInterval(TimeSpan updateTime)
        {
            updateInterval = updateTime;
        }

        public override void setUpdateTime(TimeSpan timeSpan)
        {
        }

        //SendTo Dat
        public override void writeToOpcTags()
        {
        }

        TimeSpan UpdateInterval
        {
            set;
            get;
        }
    }
}
