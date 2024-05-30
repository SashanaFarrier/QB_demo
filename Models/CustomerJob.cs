﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCodeFlowClientManual.Models
{
    public class CustomerJob
    {
        public string CustomerListID { get; set; }
        public string Name { get; set; }
        public List<string> Locations { get; set; }
        //public Location Location { get; set; }
        public CustomerJob(string name, List<string> locations)
        {
            //this.CustomerListID = id;
            this.Name = name;
            this.Locations = locations;
        }
    }

    //public class Location
    //{
    //    public List<string> Locations { get; set; }
    //}
}