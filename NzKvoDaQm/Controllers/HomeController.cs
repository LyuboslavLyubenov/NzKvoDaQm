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
    using NzKvoDaQm.Services.Search;

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

        public ActionResult Index(string query)
        {
            var recipes = this.searchService.GetRecipes(query)
                .ToList()
                .ToArray();

            var viewModel = new SearchViewModel()
            {
                Recipes = recipes
            };

            return this.View(viewModel);
        }
    }
}