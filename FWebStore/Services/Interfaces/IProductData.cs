using FWebStore.Domain;
using FWebStore.Domain.Entities;

namespace FWebStore.Services.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();
        IEnumerable<Brand> GetBrands();
        IEnumerable<Product> GetProducts(ProductFilter? Filter = null);
    }
}

