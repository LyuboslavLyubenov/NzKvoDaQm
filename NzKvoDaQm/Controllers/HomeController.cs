namespace NzKvoDaQm.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;

    using NzKvoDaQm.Data;
    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Models.ViewModels;
    using NzKvoDaQm.Services;
    using NzKvoDaQm.Services.Interfaces;

    public class HomeController : Controller
    {
        private readonly IDbContext context;

        private readonly ISearchService searchService;

        public HomeController(IDbContext context, ISearchService searchService)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (searchService == null)
            {
                throw new ArgumentNullException("searchService");
            }

            this.context = context;
            this.searchService = searchService;
        }

        public HomeController() : this(new NzKvoDaQmContext(), new SearchService())
        {
            
        }

        public ActionResult Index()
        {
            return View();
        }
        
        [Route("Search")]
        public ActionResult Search(string query)
        {
            Recipe[] recipes;

            if (string.IsNullOrWhiteSpace(query) || query.ToUpper() == "всички")
            {
                recipes = this.context.Recipes.Include(r => r.Images)
                    .Include(r => r.Ingredients)
                    .Include(r => r.Ingredients.Select(i => i.IngredientType))
                    .ToList()
                    .ToArray();
            }
            else
            {
                recipes = this.searchService.GetRecipes(query).ToArray();
            }
            
            var viewModel = new SearchViewModel()
                            {
                                Recipes = recipes
                            };

            return this.View();
        }
    }
}