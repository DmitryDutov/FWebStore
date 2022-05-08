using FWebStore.Domain;
using FWebStore.Services.Interfaces;
using FWebStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FWebStore.Controllers;

public class CatalogController : Controller
{
    private readonly IProductData _ProductData;

    public CatalogController(IProductData ProductData) => _ProductData = ProductData;

    public IActionResult Index(int? BrandId, int? SectionId)
    {
        var filter = new ProductFilter
        {
            BrandId = BrandId,
            SectionId = SectionId,
        };

        var products = _ProductData.GetProducts(filter);

        //осталось передать данные в представление
        //для этого создадим ViewModel каталога + ViewModel каждого товара в отдельности

        var catalog_model = new CatalogViewModel
        {
            BrandId = BrandId,
            SectionId = SectionId,
            Products = products
                .OrderBy(p => p.Order)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                }),
        };

        return View(catalog_model);
    }
}
