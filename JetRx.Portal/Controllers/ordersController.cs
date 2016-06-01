using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JetRx.Common.Data.SqlServer;
using JetRx.Common.Services;
using JetRx.Entities;
using System.IO;
using System.Drawing;

namespace JetRx.Portal.Controllers
{
    public class ordersController : Controller
    {
        private JetRxContext db = new JetRxContext();

        public object ProviderFactory { get; private set; }

        // GET: orders
        public async Task<ActionResult> Index()
        {

            var Orders = await db.Orders.Include("customer_prescription.Prescription")
                .Include("customer_prescription.Customer")
                    .Include("customer_prescription.Customer.Identification")
                    .Include("customer_prescription.Customer.Insurance")
                .ToListAsync();

            return View(Orders);
        }

        // GET: orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,created_at,updated_at")] order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: orders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var Order = await db.Orders.Include("customer_prescription.Prescription")
               .Include("customer_prescription.Customer")
                   .Include("customer_prescription.Customer.Identification")
                   .Include("customer_prescription.Customer.Insurance")
               .FirstOrDefaultAsync();

            if (Order == null)
            {
                return HttpNotFound();
            }
            return View(Order);
        }

        // POST: orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,created_at,updated_at")] order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: orders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            order order = await db.Orders.FindAsync(id);
            db.Orders.Remove(order);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [AcceptVerbs(HttpVerbs.Get)]
        [OutputCache(CacheProfile = "CustomerImages")]
        public FileResult GetImage(int id)
        {
            this.Response.Clear();

            this.Response.ContentType = "image/jpeg";
            MemoryStream stream = new ImageService().DownloadImage(id);
            byte[] ImageArray = stream.ToArray();
            Bitmap img = new Bitmap(stream);
            img.Save(Server.MapPath("~/Images/captcha.jpg"));
            stream.Close();
            
            return File(ImageArray,"image/jpeg");
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
