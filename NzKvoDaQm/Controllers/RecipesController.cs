namespace NzKvoDaQm.Controllers
{
    using System.Data.Entity;
    using System.Net;
    using System.Web.Mvc;
    using NzKvoDaQm.Data;
    using NzKvoDaQm.Models.EntityModels;

    using System;
    using System.Web.Script.Serialization;

    using Microsoft.AspNet.Identity;

    using NzKvoDaQm.Attirbutes;
    using NzKvoDaQm.Models.BindingModels;
    using NzKvoDaQm.Services.Interfaces;
    using NzKvoDaQm.Services.Recipe;

    public class RecipesController : Controller
    {
        private readonly IDbContext context;
        private readonly IRecipesService recipeService;

        public RecipesController()
        {
            //TODO: Decouple and refactor
            this.context = new NzKvoDaQmContext();

            var ingredientsTypesService = new IngredientTypesService(this.context.IngredientTypes, this.context);
            var recipeIngredientsService = new RecipeIngredientsService(this.context.Ingredients, this.context);
            var recipeImagesSevice = new RecipeImagesService(this.context.RecipeImages, this.context);
            var recipeStepsService = new RecipeStepsService(this.context.RecipeSteps, this.context);

            this.recipeService =
                new RecipeService(
                    this.context.Recipes,
                    ingredientsTypesService,
                    recipeIngredientsService,
                    recipeImagesSevice,
                    recipeStepsService,
                    this.context);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var recipe = this.context.Recipes.Find(id);

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
        public ActionResult Create(CreateRecipeBindingModel createRecipeBindingModel)
        {
            var userId = this.User.Identity.GetUserId();
            var user = this.context.Users.Find(userId);

            try
            {
                this.recipeService.Create(createRecipeBindingModel, user);
            }
            catch (Exception exception)
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var error = exception.Message;
                return Json(
                    new
                    {
                        error
                    });
            }

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
            Recipe recipe = this.context.Recipes.Find(id);
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
                this.context.Entry(recipe).State = EntityState.Modified;
                this.context.SaveChanges();
                return null;
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
            Recipe recipe = this.context.Recipes.Find(id);
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
            Recipe recipe = this.context.Recipes.Find(id);
            this.context.Recipes.Remove(recipe);
            this.context.SaveChanges();
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
