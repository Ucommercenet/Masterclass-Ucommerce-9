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
        private IIndex<Product> ProductIndex => ObjectFactory.Instance.Resolve<IIndex<Product>>();
        private ICatalogLibrary CatalogLibrary => ObjectFactory.Instance.Resolve<ICatalogLibrary>();
        private ITransactionLibrary TransactionLibrary => ObjectFactory.Instance.Resolve<ITransactionLibrary>();


        [HttpGet]
        public ActionResult Index(string slug)
        {
            Product product = ProductIndex.Find().Where(p => p.Slug == slug).Single();
            ResultSet<Product> variants = ProductIndex.Find().Where(v => product.Variants.Contains(v.Guid)).ToList();
            ProductPriceCalculationResult
                prices = CatalogLibrary.CalculatePrices(variants.Select(p => p.Guid).ToList());

            var productModel = MapProduct(product, null);
            productModel.Variants = variants.Select(v =>
                MapProduct(v, prices.Items.SingleOrDefault(price => price.ProductGuid == product.Guid))
            ).ToList();

            return View("/views/mc/product.cshtml", productModel);
        }

        private ProductViewModel MapProduct(Product product, ProductPriceCalculationResult.Item price)
        {
            return
                new ProductViewModel
                {
                    Name = product.DisplayName,
                    IsVariant = !string.IsNullOrEmpty(product.VariantSku),
                    LongDescription = product.LongDescription,
                    Sku = product.Sku,
                    VariantSku = product.VariantSku,
                    Url = $"product?slug={product.Slug}",
                    Price = MapPrice(price)
                };
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

        [HttpPost]
        public ActionResult Index(AddToBasketViewModel model)
        {
            TransactionLibrary.AddToBasket(1, model.Sku, model.VariantSku);

            return Index(model.Slug);
        }
    }
}