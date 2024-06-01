using System;
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
        public CustomerJob(string name, string listId, List<string> locations)
        {
            this.CustomerListID = listId;
            this.Name = name;
            this.Locations = locations;
        }
    }
    
}