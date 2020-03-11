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
        private IIndex<Category> CategoryIndex => ObjectFactory.Instance.Resolve<IIndex<Category>>();
        private ICatalogLibrary CatalogLibrary => ObjectFactory.Instance.Resolve<ICatalogLibrary>();

        [HttpGet]
        public ActionResult Index(string slug)
        {
            var category = CategoryIndex.Find().Where(cat => cat.Slug == slug).FirstOrDefault();
            
            var products = CatalogLibrary.GetProducts(category.Guid);
            
            ProductPriceCalculationResult
                prices = CatalogLibrary.CalculatePrices(products.Select(p => p.Guid).ToList());

            
            var categoryViewModel = new CategoryViewModel
            {
                Name = category.DisplayName,
                Description = category.Description,
                Url = $"/category/{category.Slug}",
                Products = MapProducts(products, prices)
            };

            return View("/views/mc/category.cshtml", categoryViewModel);
        }

        private IList<ProductViewModel> MapProducts(
            IEnumerable<Product> productsInCategory,
            ProductPriceCalculationResult prices)
        {
            IList<ProductViewModel> productViews = productsInCategory
                .Select(product =>
                    new ProductViewModel
                    {
                        Name = product.DisplayName,
                        Url = $"/product?slug={product.Slug}",
                        Price = MapPrice(prices.Items.FirstOrDefault(price => price.ProductGuid == product.Guid))
                    }).ToList();

            return productViews;
        }

        private PriceViewModel MapPrice(ProductPriceCalculationResult.Item price)
        {
            if (price == null) return new PriceViewModel();
            
            return new PriceViewModel
            {
                IsDiscounted = price.DiscountPercentage > 0M,
                ListPrice = price.ListPriceInclTax.ToString("N"),
                YourPrice = price.PriceInclTax.ToString("N")
            };
        }
    }
}