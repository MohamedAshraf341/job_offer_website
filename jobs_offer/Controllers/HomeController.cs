using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jobs_offer.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Net.Mail;
using System.Net;

namespace jobs_offer.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext Db = new ApplicationDbContext();

        public ActionResult Index()
        {
            
            return View(Db.categories.ToList());
        }

        public ActionResult Details(int jobid)
        {
            var job = Db.jobs.Find(jobid);
            if(job==null)
            {
                return HttpNotFound();
            }
            Session["jobid"] = jobid;
            return View(job);
        }
        //[Authorize]
        public ActionResult Apply()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Apply(string message)
        {
            var userid = User.Identity.GetUserId();
            var jobid = (int)Session["jobid"];

            var check = Db.Applyforjobs.Where(a => a.jobid == jobid && a.userid == userid).ToList();

            if(check.Count<1)
            {
                var job = new Applyforjob();
                job.userid = userid;
                job.jobid = jobid;
                job.message = message;
                job.ApplyDate = DateTime.Now;

                Db.Applyforjobs.Add(job);
                Db.SaveChanges();
                ViewBag.Result = "تمت اضافة الطلب";

            }
            else
            {
                ViewBag.Result = "لقد تمت اضافة الطلب من قبل ";
            }


            return View();
        }
        //[Authorize]
        public ActionResult GetJobsByUser()
        {
            var Userid = User.Identity.GetUserId();
            var jobs = Db.Applyforjobs.Where(a => a.userid == Userid);
            return View(jobs.ToList());
        }

        public ActionResult DetailOfJobs(int id)
        {
            var job = Db.Applyforjobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        //[Authorize]
        public ActionResult GetJobsByPublisher()
        {
            var UserId = User.Identity.GetUserId();
            var jobs = from app in Db.Applyforjobs
                      join job in Db.jobs
                      on app.jobid equals job.id
                      where job.User.Id == UserId
                      select app;

            var grouped = from j in jobs
                          group j by j.jobs.jobtitle
                         into gr
                          select new JobsViewModel
                          {
                              JobTitle = gr.Key,
                              Items=gr
                          };

            return View(grouped.ToList());
        }

        // GET: apply/Edit apply/5
        public ActionResult Edit(int id)
        {
            var job = Db.Applyforjobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        // POST: apply/Edit apply/5
        [HttpPost]
        public ActionResult Edit(Applyforjob job)
        {
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                Db.Entry(job).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View(job);
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int id)
        {
            var job = Db.Applyforjobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Role/Delete/5
        [HttpPost]
        public ActionResult Delete(Applyforjob job)
        {

            // TODO: Add delete logic here
            var myjob = Db.Applyforjobs.Find(job.id);
            Db.Applyforjobs.Remove(myjob);
            Db.SaveChanges();
            return RedirectToAction("GetJobsByUser");

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();
            var LoginInfo = new NetworkCredential("email", "password");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("email"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            string body = "اسم المرسل :" + contact.Name + "<br>" +
                        "بريد المرسل :" + contact.Email + "<br>" +
                        "عنوان الرساله :" + contact.Subject + "<br>" +
                        "نص الرساله : <b>" + contact.Message+ "<br>";
            mail.Body = body;


            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = LoginInfo;
            smtpClient.Send(mail);
            return RedirectToAction("Index");
        }
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = Db.jobs.Where(a => a.jobtitle.Contains(searchName)
              || a.jobcontent.Contains(searchName)
              || a.cat.categoryname.Contains(searchName)
              || a.cat.categorydescription.Contains(searchName)).ToList();
            return View(result);
        }
    }
}