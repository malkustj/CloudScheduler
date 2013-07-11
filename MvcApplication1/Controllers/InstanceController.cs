using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using CloudScheduler.Infrastructure;
using CloudScheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
//Controls all of the functionality on the instance list page
namespace CloudScheduler.Controllers
{
    public class InstanceController : Controller
    {
        // Assumning ec2 client is thread safe
        public ActionResult List()
        {
            var model = new InstanceListVM
            {
                CurrentInstanceList = new List<Instance> { },  
            };
            model.CurrentInstanceList = AmazonService.GetInstanceList();

            return View(model);
        }
        public ActionResult StartInstance(string Id)
        {
            AmazonService.StartInstance(Id);
            return RedirectToAction("List");
        }
        public ActionResult StopInstance(string Id)
        {
            AmazonService.StopInstance(Id);
            return RedirectToAction("List");
        }
        public ActionResult DeactivateSchedule(String Id)
        {
            Instance instance = AmazonService.GetSpecificInstance(Id);
            instance.Schedule.IsActive = false;
            AWSTagRepository atr = new AWSTagRepository();
            atr.Encode(instance);
            return RedirectToAction("List");
        }
        public ActionResult ActivateSchedule(string Id)
        {
            Instance instance = AmazonService.GetSpecificInstance(Id);
            instance.Schedule.IsActive = true;
            AWSTagRepository atr = new AWSTagRepository();
            atr.Encode(instance);
            return RedirectToAction("List");
        }
        public ActionResult Edit(string Id)
        {
            return RedirectToAction("Edit", "Schedule", new { Id = Id });
        }
        
    }
}
