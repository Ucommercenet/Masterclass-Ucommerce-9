using System.Web.Mvc;
using MyUCommerceApp.Website.Models;
using Umbraco.Web.Mvc;

namespace MyUCommerceApp.Website.Controllers
{
	public class MasterClassBasketController : RenderMvcController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var basketModel = new PurchaseOrderViewModel();

            return View("/Views/mc/Basket.cshtml", basketModel);
        }

        [HttpPost]
        public ActionResult Index(PurchaseOrderViewModel model)
        {
            return Redirect(this.CurrentPage.Url);
        }
    }
}