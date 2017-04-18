namespace NzKvoDaQm.Controllers
{
    using System.Data.Entity;
    using System.Net;
    using System.Web.Mvc;
    using NzKvoDaQm.Data;
    using NzKvoDaQm.Models.EntityModels;

    using System;

    using NzKvoDaQm.Attirbutes;
    using NzKvoDaQm.Models.BindingModels;

    public class RecipesController : Controller
    {
        private readonly IDbContext db;

        public RecipesController() : this(new NzKvoDaQmContext())
        {
        }

        public RecipesController(IDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.db = context;
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var recipe = db.Recipes.Find(id);

            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // GET: Recipes/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [ValidateJsonAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(CreateRecipeBindingModel bindingModel)
        {
            return null;
        }

        [Authorize]
        // GET: Recipes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,MinutesRequiredToCook")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recipe);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recipe recipe = db.Recipes.Find(id);
            db.Recipes.Remove(recipe);
            db.SaveChanges();
            return RedirectToAction("Index");
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
