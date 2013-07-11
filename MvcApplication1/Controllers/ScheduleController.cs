using CloudScheduler.Infrastructure;
using CloudScheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//(In progress) Controller to manage the changing of schedules
namespace CloudScheduler.Infrastructure
{
    public class ScheduleController : Controller
    {
        public Instance MyInstance { get; set; }
        [HttpGet]
        public ActionResult Edit(string Id)
        {
            var list = AmazonService.GetInstanceList();
            foreach (Instance instance in list)
            {
                if (instance.Id.Equals(Id))
                {
                    MyInstance = instance;
                }
            }
            var myModel = new ScheduleVM(MyInstance);
            return View(myModel);  
        }
        [HttpPost]
        public ActionResult Edit(ScheduleVM model)
        {
            UpdateModel(model);
            MyInstance = AmazonService.GetSpecificInstance(model.InstanceId);
            MyInstance.Schedule= new Schedule(model.InstanceString());
            AWSTagRepository atr = new AWSTagRepository();
            atr.Encode(MyInstance);
            return RedirectToAction("List", "Instance");
        }
    }
}
