using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MyUCommerceApp.Website.Models;
using Ucommerce.Api;
using UCommerce.Infrastructure;
using UCommerce.Search.Models;
using UCommerce.Search;
using UCommerce.Catalog.Models;
using System;
using UCommerce;

namespace MyUCommerceApp.Website.Controllers
{
    public class MasterClassCategoryController : Umbraco.Web.Mvc.RenderMvcController
    {

        [HttpGet]
        public ActionResult Index(string slug)
        {
            var categoryViewModel = new CategoryViewModel
            {
            };

            return View("/views/mc/category.cshtml", categoryViewModel);
        }
    }
}