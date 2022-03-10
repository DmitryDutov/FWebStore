using FWebStore.Data;
using FWebStore.Domain.Entities;
using FWebStore.Services.Interfaces;

namespace FWebStore.Services
{
    public class InMemoryData : IProductData
    {
        public IEnumerable<Section> GetSections()
        {
            return TestData.Sections;
        }

        public IEnumerable<Brand> GetBrands()
        {
            return TestData.Brands;
        }
    }
}

