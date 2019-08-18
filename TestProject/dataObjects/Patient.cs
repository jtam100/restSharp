using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.dataObjects
{
    public class Patient
    {
        public string fName { get; set; }
        public string lname { get; set; }
        public List<Medication> medications { get; set; }
    }
}
