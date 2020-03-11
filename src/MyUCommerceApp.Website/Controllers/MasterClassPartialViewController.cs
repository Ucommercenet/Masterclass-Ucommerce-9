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

        public ActionResult CategoryNavigation()
        {
            ResultSet<Category> categories = CatalogLibrary.GetRootCategories(new Guid("8C9037CC-F1F2-F477-7F15-999D1D55E29D"));

            var categoryNavigation = new CategoryNavigationViewModel
            {
                Categories = MapCategories(categories)
            };

            return View("/views/mc/PartialViews/CategoryNavigation.cshtml", categoryNavigation);
        }

        private IList<CategoryViewModel> MapCategories(IEnumerable<Category> categoriesToMap)
        {
            return categoriesToMap.Select(category => new CategoryViewModel
            {
                Name = category.DisplayName,
                Description = category.Description,
                Url = $"/category?slug={category.Slug}"
            }).ToList();
        }
    }
}