using CloudKids.Data;
using CloudKids.Data.Infrastructure;
using CloudKids.Domain.Entities;
using CloudKids.Service;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace CloudKids.Web.Controllers
{
    public class ReputationController : Controller
    {

       


        CloudKidsContext db = new CloudKidsContext();
        IReputationservice  Myrep;
        IDBFactory dbf;
        IUnitOfWork uow;
        IService<Reputation> serviceRep;
        IService<Profil_Jardin> serviceJardin;

        public ReputationController()
        {
            Myrep = new Reputationservice();
            dbf = new DBFactory();
            uow = new UnitOfWork(dbf);
            serviceRep = new Service<Reputation>(uow);
            serviceJardin = new Service<Profil_Jardin>(uow);
        }
        // GET: Reputation
        public ActionResult Index()
        {

            ViewBag.Profil_Jardin = new SelectList(serviceJardin.GetAll(), "ID", "Name","Email");
         
            return View(serviceRep.GetAll());
          


        }

        public ActionResult IndexByJardin(int id)
        {

            ViewBag.jardin = serviceJardin.GetById(id);
            ViewBag.Profil_Jardin = new SelectList(serviceJardin.GetAll(), "ID", "Name", "Email");

            return View(serviceRep.GetAll().Where(c=>c.ProfilJardinId==id));



        }





        public ActionResult order(FormCollection form)
        {
            ViewBag.Profil_Jardin1 = new SelectList(serviceJardin.GetAll(), "ID", "Name", "Email");

            ViewBag.Profil_Jardin = new SelectList(serviceJardin.GetAll(), "ID", "Name", "Email");
            string order = form["orderr"];

            if (order == "Type")
            {
                return View(serviceRep.GetAll().OrderBy(model => model.Type));
            }
            if (order == "Prix")
            {
                return View(serviceRep.GetAll().OrderBy(model => model.Prix));
            }
            return View(serviceRep.GetAll().OrderBy(model => model.Prix));
        }



        public ActionResult MyReputation(FormCollection form)
        {
            
            ViewBag.Profil_Jardin = new SelectList(serviceJardin.GetAll(), "ID", "Name","Email");
            ViewBag.Profil_Jardin1 = new SelectList(serviceJardin.GetAll(), "ID", "Name", "Email");

            //string  a = form["ProfilJardinId"];
            //int search = int.Parse(a);


            var reputation = Myrep.Getreputation(int.Parse(form["ProfilJardinId"]));
            return View(reputation);
        
        }



        // GET: Reputation/Details/5
        public ActionResult Details(int id)
        {
            return View(serviceRep.GetById(id));
        }

        // GET: Reputation/Create
        public ActionResult Create()
        {
            ViewBag.Profil_Jardin = new SelectList(serviceJardin.GetAll(),"ID","Name");
            return View();
        }

        // POST: Reputation/Create
        [HttpPost]
        public ActionResult Create(Reputation p)
        {
            try
            {

                serviceRep.Add(p);
                serviceRep.Commit();
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reputation/Edit/5
        public ActionResult Edit(int id)
        {
            var rep = serviceRep.GetById(id);
            return View(rep);
        }

        // POST: Reputation/Edit/5
        [HttpPost]
        public ActionResult Edit(Reputation R)  
        {
            if (ModelState.IsValid)

            {
               var Rep =  db.Reputations.Single(c => c.Id == R.Id);
                Rep.Description = R.Description;
                Rep.Prix = R.Prix;
                Rep.Type = R.Type;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
              
                // TODO: Add update logic here

                return RedirectToAction("Index");
            
              return View();
            
        }

        // GET: Reputation/Delete/5
        public ActionResult Delete(int id)
        {
            return View(serviceRep.GetById(id));
        }

        // POST: Reputation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                serviceRep.Delete(serviceRep.GetById(id));
                serviceRep.Commit();
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}