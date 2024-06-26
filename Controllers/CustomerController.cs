using Intuit.Ipp.Data;
using MvcCodeFlowClientManual.Models;
using MvcCodeFlowClientManual.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MvcCodeFlowClientManual.Controllers
{
    public class CustomerController : Controller
    {
        //public List<Customer> Customers = new List<Customer>();
        public CustomerJobService CustomerJob = new CustomerJobService();
        // GET: Customer
        public JsonResult GetCustomerJobs()
         {
            var customers = CustomerJob.GetCustomerJobs();

            return Json(customers, JsonRequestBehavior.AllowGet);

        }
    }
}