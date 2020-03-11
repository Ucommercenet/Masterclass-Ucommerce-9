using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MyUCommerceApp.Website.Models;
using Ucommerce.Api;
using UCommerce.Infrastructure;
using UCommerce.Search;
using UCommerce.Search.Models;

namespace MyUCommerceApp.Website.Controllers
{
    public class MasterClassPartialViewController : Umbraco.Web.Mvc.SurfaceController
    {
        private ICatalogLibrary CatalogLibrary => ObjectFactory.Instance.Resolve<ICatalogLibrary>();

        public ActionResult CategoryNavigation(string slug)
        {

            var categoryNavigation = new CategoryNavigationViewModel
            {
            };

            return View("/views/mc/PartialViews/CategoryNavigation.cshtml", categoryNavigation);
        }
    }
}