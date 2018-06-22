using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace piWindPotential
{
    class csvOuptut
    {
        private String fileName;

        public void createFile(List<List<String[]>> masterList)
        {
            try
            {
                String fileName = "piTrend.csv";
                FileStream fs = new FileStream(fileName, FileMode.Create);
                this.fileName = fileName;
                StreamWriter sw = new StreamWriter(fs);
                String line = "";

                //Write the tag Names Headers
                for(int i=0; i <= masterList.Count - 1; i++)
                {
                   line += masterList[i][0][0]+",,,";
                }
                sw.WriteLine(line);

                //Write the headers [Timestamp, Value (kW)]
                line = "";
                for(int i=0; i <= masterList.Count - 1; i++)
                {
                    line += "Time Stamp,Potential (kW),,";
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
                        line += timestamp+","+value.ToString()+",,";
                    }
                    sw.WriteLine(line);

                }
                line = "";
                sw.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("csv file needs to be closed before program can recreate one. Please close the csv file", "Error");

            }


        }

        public void setFileName(String fileName) { this.fileName = fileName; }
        public String getFileName() { return this.fileName; }


        public void openFile()
        {
            //Process csvProcess = new Process();
            //Process.Start("C:\\Program Files (x86)\\LibreOffice 5\\program\\scalc.exe", getFileName());
            //ProcessStartInfo info = new ProcessStartInfo(@"C:\Program Files (x86)\Notepad++\notepad++.exe", getFileName());
            try
            {
                Process.Start(getFileName());
            }
            catch(Exception e)
            {

            }
        }

        public void closeFile()
        {
            foreach (var process in Process.GetProcessesByName("soffice.bin"))
            {
                process.Kill();
            }
        }
    }
}
