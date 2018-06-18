using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace piWindPotential
{
    class csvOuptut
    {
        private String fileName;

        public void createFile(List<List<String[]>> masterList)
        {
            FileStream fs = new FileStream("meh.csv", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            String line = "";

            //Write the tag Names Headers
            for(int i=0; i <= masterList.Count - 1; i++)
            {
               line += masterList[i][0][0]+"\t\t\t";
            }
            sw.WriteLine(line);

            //Write the headers [Timestamp, Value (kW)]
            line = "";
            for(int i=0; i <= masterList.Count - 1; i++)
            {
                line += "Time Stamp\tPotential (kW)\t\t";
            }
            sw.WriteLine(line);

            //Write the data for all sites
            for (int i = 0; i <= masterList[0].Count - 1; i++)
            {
                line = "";
                for (int j = 0; j <= masterList.Count - 1; j++)
                {
                    double value = double.Parse(masterList[j][i][1]) * 1000;
                    String timestamp = masterList[j][i][2];
                    line += timestamp+"\t"+value.ToString()+"\t\t";
                }
                sw.WriteLine(line);

            }
            line = "";
            sw.Close();

        }

        public void setFileName(String fileName) { this.fileName = fileName; }
        public String getFileName() { return this.fileName; }


        public void openFile()
        {
            Process.Start("notepad.exe", getFileName());

        }
    }
}
