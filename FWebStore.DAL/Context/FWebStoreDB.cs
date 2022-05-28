using FWebStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FWebStore.DAL.Context
{
    public class FWebStoreDB : DbContext
    {
        //Описываем какие таблицы нам нужны для работы с БД
        public DbSet<Product> Products { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Employee> Employees { get; set; }

        //Создаём конструктор, в нём вызываем базовый конструктор в который передаём options(DbContextOptions)
        public FWebStoreDB(DbContextOptions<FWebStoreDB> options) : base(options)
        {
                
        }

        //Если хотим, то можно добавить "тюнинг" (переопредить OnModelCreating)
        protected override void OnModelCreating(ModelBuilder db)
        {
            base.OnModelCreating(db);

            ////теперь можем конкретизировать как выглядят сущности
            //db.Entity<Section>()
            //    .HasMany(section => section.Products)
            //    .WithOne(product => product.Section)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
