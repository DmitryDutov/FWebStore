using FWebStore.Domain.Entities;
using FWebStore.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FWebStore.DAL.Context
{
    //public class FWebStoreDB : IdentityDbContext //Такой вариант можно использовать если мы не определили User и Role в FWebStore.Domain/Entities
    //public class FWebStoreDB : IdentityDbContext<User> //Такой вариант можно использовать если указан только User
    public class FWebStoreDB : IdentityDbContext<User, Role, string> //Такой вариант можно использовать когда опредены и User и Role
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
