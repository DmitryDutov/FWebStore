using FWebStore.Data;
using FWebStore.Domain;
using FWebStore.Domain.Entities;
using FWebStore.Services.Interfaces;

namespace FWebStore.Services
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Section> GetSections()
        {
            return TestData.Sections;
        }

        public IEnumerable<Brand> GetBrands()
        {
            return TestData.Brands;
        }

        public IEnumerable<Product> GetProducts(ProductFilter? Filter = null)
        {
            IEnumerable<Product> query = TestData.Products; //обязательно указывать IEnuerable а не var. Иначе не отработает Linq

            ////Старый способ
            //if (Filter?.SectionId != null)
            //{
            //    query = query.Where(p => p.SectionId == Filter.SectionId);
            //}

            //Способ из C#9.0
            if (Filter?.SectionId is {} section_id)
                query = query.Where(p => p.SectionId == Filter.SectionId);

            if (Filter?.BrandId is { } brand_id)
                query = query.Where(p => p.BrandId == Filter.BrandId);

            return query;
        }
    }
}

