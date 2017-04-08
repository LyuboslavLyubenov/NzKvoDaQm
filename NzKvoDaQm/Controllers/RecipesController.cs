using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using NzKvoDaQm.Data;
using NzKvoDaQm.Models.EntityModels;

namespace NzKvoDaQm.Controllers
{

    using System.Drawing;
    using System.Text.RegularExpressions;

    using NzKvoDaQm.Extensions;
    using NzKvoDaQm.Models.ViewModels;

    public class RecipesController : Controller
    {
        private NzKvoDaQmContext db = new NzKvoDaQmContext();

        // GET: Recipes
        public ActionResult Index()
        {
            return View(db.Recipes.ToList());
        }

        // GET: Recipes/Details/5
        public ActionResult Details(int? id)
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

        // GET: Recipes/Create
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public JsonResult Create(CreateRecipeViewModel viewModel)
        {
            int minutesRequiredForCooking;

            if (string.IsNullOrWhiteSpace(viewModel.Title) ||
                viewModel.Images == null || 
                viewModel.Ingredients == null ||
                viewModel.Ingredients.Length == 0 ||
                viewModel.Ingredients.All(i => Regex.IsMatch(i, "\\W|_")) ||
                string.IsNullOrWhiteSpace(viewModel.MinutesRequiredForCooking) ||
                !int.TryParse(viewModel.MinutesRequiredForCooking, out minutesRequiredForCooking) ||
                viewModel.StepsTexts == null ||
                viewModel.StepsTexts.Length == 0 ||
                viewModel.StepsTexts.All(string.IsNullOrWhiteSpace))
            {
                return null;
            }
            


            Image[] images;

            return null;
        }

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

        // GET: Recipes/Delete/5
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
