using FWebStore.DAL.Context;
using FWebStore.Domain;
using FWebStore.Domain.Entities;
using FWebStore.Services.Interfaces;

namespace FWebStore.Services.InSQL
{
    public class SqlProductData : IProductData
    {
        private readonly FWebStoreDB _db;

        public SqlProductData(FWebStoreDB db)
        {
            _db = db;
        }

        public IEnumerable<Section> GetSections()
        {
            return _db.Sections;
        }

        public IEnumerable<Brand> GetBrands()
        {
            return _db.Brands;
        }

        public IEnumerable<Product> GetProducts(ProductFilter? Filter = null)
        {
            IQueryable<Product> query = _db.Products;

            if (Filter?.SectionId is {} section_id)
            {
                query = query.Where(p => p.SectionId == section_id);
            }

            if (Filter?.BrandId is {} brand_id)
            {
                query = query.Where(p => p.BrandId == brand_id);
            }

            return query;
        }
    }
}

