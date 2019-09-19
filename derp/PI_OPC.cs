using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piWindPotential

{
    /*
     * This is a class that stores PI tags and its OPC tag counterpart.
     * It is only used by the opcWriter class when attempting to read a csv file
     * Also, try to NOT change this as the header in the csv file MUST match the variable names here
     */
    class PI_OPC
    {
        public string PITag { get; set; }
        public string OpcTag { get; set; }
    }
}
