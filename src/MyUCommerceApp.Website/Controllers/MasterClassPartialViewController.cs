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
            ResultSet<Category> categories = CatalogLibrary.GetRootCategories(Constants.CatalogId);

            var categoryNavigation = new CategoryNavigationViewModel
            {
                Categories = MapCategories(categories, slug)
            };

            return View("/views/mc/PartialViews/CategoryNavigation.cshtml", categoryNavigation);
        }

        private IList<CategoryViewModel> MapCategories(IEnumerable<Category> categoriesToMap, string selectedSlug = null)
        {
            return categoriesToMap.Select(category => new CategoryViewModel
            {
                Name = category.DisplayName,
                Description = category.Description,
                Url = $"/category?slug={category.Slug}",
                Categories = category.Slug == selectedSlug
                    ? MapCategories(CatalogLibrary.GetCategories(category.Categories))
                    : new List<CategoryViewModel>(),
            }).ToList();
        }
    }
}