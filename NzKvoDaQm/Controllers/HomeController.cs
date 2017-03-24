namespace NzKvoDaQm.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private string[] FilterProductsNames(string products)
        {
            const int MinLengthForProductName = 3;
            return products
                .Trim()
                .Split(new char[] {}, StringSplitOptions.RemoveEmptyEntries)
                .Select(productName => productName.Trim())
                .Where(productName => productName.Length >= MinLengthForProductName)
                .ToArray();
        }

        [Route("Search")]
        public ActionResult Search(string products)
        {
            var productNames = this.FilterProductsNames(products);

            return this.View();
        }
    }
}