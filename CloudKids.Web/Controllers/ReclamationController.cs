using CloudKids.Data;
using CloudKids.Data.Infrastructure;
using CloudKids.Domain.Entities;
using CloudKids.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudKids.Web.Controllers
{
    public class ReclamationController : Controller
    {




        IReclamationService reclamservices;
        CloudKidsContext db = new CloudKidsContext();
        IDBFactory dbf;
        IUnitOfWork uow;
        IService<Reclamation> serviceRep;
        IService<Profil_Jardin> serviceJardin;
        IService<Profil_Parent> serviceParents;

        public ReclamationController()
        {
            reclamservices = new ReclamationService();
            dbf = new DBFactory();
            uow = new UnitOfWork(dbf);
            serviceRep = new Service<Reclamation>(uow);
            serviceJardin = new Service<Profil_Jardin>(uow);
            serviceParents = new Service<Profil_Parent>(uow);
        }


       

        public ActionResult order(FormCollection form)
        {
          
            
            return View(serviceRep.GetAll());

        }


        // GET: Reclamation
        public ActionResult IndexByParent(int id )
        {

            ViewBag.Profil_Jardin = new SelectList(serviceJardin.GetAll(), "ID", "Name");
            ViewBag.Profil_Parent = serviceParents.GetById(id);
            return View(serviceRep.GetAll().Where(t=>t.ProfilParentId==id));

        }



        public ActionResult Index(string search)
        {

            ViewBag.Profil_Jardin = new SelectList(serviceJardin.GetAll(), "ID", "Name");
            ViewBag.Profil_Parent = new SelectList(serviceParents.GetAll(), "ID", "Name");



            return View(serviceRep.GetAll());
            
        }


        

        public  ActionResult Myreclmation(FormCollection form)
        {
            ViewBag.Profil_Jardin = new SelectList(serviceJardin.GetAll(), "ID", "Name");
            ViewBag.Profil_Parent = new SelectList(serviceParents.GetAll(), "ID", "Name");
            ViewBag.Profil_Jardin1 = serviceJardin.GetById(int.Parse(form["ProfilJardinId"]));
            var reclamation = reclamservices.GetReclamationByJardin(int.Parse(form["ProfilJardinId"]));

            ViewBag.idjardin = int.Parse(form["ProfilJardinId"]);
           ViewBag.num  = reclamservices.GetAll().Where(c => c.ProfilJardinId == int.Parse(form["ProfilJardinId"])).Count();
            return View(reclamation);
            ;

        }

        

        // GET: Reclamation/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Profil_Jardin = new SelectList(serviceJardin.GetAll(), "ID", "Name");
            ViewBag.Profil_Parent = new SelectList(serviceParents.GetAll(), "ID", "Name");
            return View(serviceRep.GetById(id));
        }

        // GET: Reclamation/Create
        public ActionResult Create()
        {
            ViewBag.Profil_Jardin = new SelectList(serviceJardin.GetAll(), "ID", "Name");
            ViewBag.Profil_Parent = new SelectList(serviceParents.GetAll(), "ID", "Name");
            return View();
        }

        // POST: Reclamation/Create
        [HttpPost]
        public ActionResult Create(Reclamation R)
        {
            try
            {
                serviceRep.Add(R);
                serviceRep.Commit();

                return RedirectToAction("Index");
            }
            catch ( Exception ex)
            {
                return View();
            }
        }


        public ActionResult Deletejardin(int id)
        {
            ViewBag.idd = id;
            return View(serviceJardin.GetById(id));

        }


        
        public ActionResult DeleteJardinF (int id )
        {

            ViewBag.Profil_Jardin = new SelectList(serviceJardin.GetAll(), "ID", "Name");
            serviceJardin.Delete(serviceJardin.GetById(id));
            serviceJardin.Commit();
            return View(serviceRep.GetAll());
            

        }
        // GET: Reclamation/Edit/5
        public ActionResult Edit(int id)
        {

            ViewBag.Profil_Jardin = new SelectList(serviceJardin.GetAll(), "ID", "Name");
            ViewBag.Profil_Parent = new SelectList(serviceParents.GetAll(), "ID", "Name");
            var rep = serviceRep.GetById(id);
            return View(rep);
        }

        // POST: Reclamation/Edit/5
        [HttpPost]
        public ActionResult Edit(Reclamation reclamation)
        {
            try
            {

                var Rec = db.Reclamations.Single(c => c.Id == reclamation.Id);

                    Rec.Motif = reclamation.Motif;  
                    Rec.ProfilJardinId = reclamation.ProfilJardinId;
                Rec.catreclamation = reclamation.catreclamation;
                db.SaveChanges();
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reclamation/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.Profil_Jardin = new SelectList(serviceJardin.GetAll(), "ID", "Name");
            ViewBag.Profil_Parent = new SelectList(serviceParents.GetAll(), "ID", "Name");
            return View(serviceRep.GetById(id));
        }

        // POST: Reclamation/Delete/5
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
