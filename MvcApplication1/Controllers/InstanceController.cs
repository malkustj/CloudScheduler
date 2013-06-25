using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using CloudScheduler.Infrastructure;
using CloudScheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace CloudScheduler.Controllers
{
    public class InstanceController : Controller
    {
        // Assumning ec2 client is thread safe
        AmazonService aws = new AmazonService();
        public ActionResult List()
        {
            var model = new InstanceListVM
            {
                CurrentInstanceList = new List<Instance> { },  
            };
            model.CurrentInstanceList = aws.getInstanceList();

            return View(model);
        }

        public ActionResult StartInstance(String Id)
        {
            aws.StartInstance(Id);
            return RedirectToAction("List");
        }
        public ActionResult StopInstance(String Id)
        {
            aws.StopInstance(Id);
            return RedirectToAction("List");
        }
    }
}
