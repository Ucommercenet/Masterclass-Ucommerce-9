using System.Linq;
using System.Web.Mvc;
using MyUCommerceApp.Website.Models;
using Ucommerce.Api;
using UCommerce.Catalog.Models;
using UCommerce.Infrastructure;
using UCommerce.Search;
using UCommerce.Search.Models;

namespace MyUCommerceApp.Website.Controllers
{
    public class MasterClassProductController : Umbraco.Web.Mvc.RenderMvcController
    {
        [HttpGet]
        public ActionResult Index(string slug)
        {
            var productModel = new ProductViewModel
            {
            };

            return View("/views/mc/product.cshtml", productModel);
        }
        
        [HttpPost]
        public ActionResult Index(AddToBasketViewModel model)
        {

            return Index(model.Slug);
        }
    }
}