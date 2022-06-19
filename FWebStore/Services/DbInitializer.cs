using FWebStore.DAL.Context;
using FWebStore.Data;
using FWebStore.Domain.Entities;
using FWebStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FWebStore.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly FWebStoreDB _db;
        private readonly ILogger<DbInitializer> _logger;

        //В конструктор получаем контекст БД и логгер
        public DbInitializer(FWebStoreDB db, ILogger<DbInitializer> Logger)
        {
            _db = db;
            _logger = Logger;
        }

        public async Task<bool> RemoveAsync(CancellationToken Cancel = default)
        {
            _logger.LogInformation("Удаление БД...");
            var result = await _db.Database.EnsureDeletedAsync(Cancel).ConfigureAwait(false);
            if (result)
            {
                _logger.LogInformation("Удаление БД выполнено успешно");
            }
            else
            {
                _logger.LogInformation("Удаление БД не требуется (БД отсутствует");
            }

            return result;
        }

        public async Task InitializeAsync(bool RemoveBefore = false, CancellationToken Cansel = default)
        {
            _logger.LogInformation("Инициализация БД...");

            if (RemoveBefore)
            {
                await RemoveAsync(Cansel).ConfigureAwait(false);
            }

            //await _db.Database.EnsureCreatedAsync(); //Если хотим просто создать БД (маленькое приложение)

            var pending_migrations = await _db.Database.GetPendingMigrationsAsync(Cansel);
            if (pending_migrations.Any())
            {
                _logger.LogInformation("Выполнение миграции БД...");

                await _db.Database.MigrateAsync(Cansel).ConfigureAwait(false);

                _logger.LogInformation("Выполнение миграции БД выполено успешно");
            }

            await InitializeProductsAsync(Cansel).ConfigureAwait(false);
            await InitializeEmployeesAsync(Cansel).ConfigureAwait(false);

            _logger.LogInformation("Инициализация БД выполнена успешно");
        }

        private async Task InitializeProductsAsync(CancellationToken Cancel)
        {
            //Проверяем существует ли в секциях хотя-бы одна секция
            if (_db.Sections.Any())
            {
                _logger.LogInformation("Инициализация продуктов не требуется");
                return; //если да, то ничего делать не требуется
            }

            _logger.LogInformation("Инициализация продуктов ...");

            //Собираем данные по продуктам в словари по идентификаторам:
            var sections_pool = TestData.Sections.ToDictionary(s => s.Id);
            var breands_pool = TestData.Brands.ToDictionary(b => b.Id);
            //Разбираемся с иерархией секций:
            foreach (var child_section in TestData.Sections.Where(s => s.ParentId is not null))
            {
                //для каждой дочерней секции устанавливаем родительскую из пула секций
                child_section.Parent = sections_pool[(int)child_section.ParentId!];
            }
            //Теперь установим секцию каждому товару
            foreach (var product in TestData.Products)
            {
                product.Section = sections_pool[product.SectionId];
                //если у продукта есть брэнд, то устанавливаем его
                if (product.BrandId is { } brand_id)
                {
                    product.Brand = breands_pool[brand_id];
                }

                //сбрасываем идентификаторы
                product.Id = 0;
                product.SectionId=0;
                product.BrandId = null;
            }

            //Теперь можно обнулить идентификаторы секций
            foreach (var section in TestData.Sections)
            {
                section.Id=0;
                section.ParentId=0;
            }

            //Очищаем брэнды
            foreach (var brand in TestData.Brands)
            {
                brand.Id=0;
            }

            //Осталось добавить всё в БД
            await using (await _db.Database.BeginTransactionAsync(Cancel))
            {
                await _db.Sections.AddRangeAsync(TestData.Sections, Cancel); //добавляем секции
                await _db.Brands.AddRangeAsync(TestData.Brands, Cancel);     //добавляем брэнеды
                await _db.Products.AddRangeAsync(TestData.Products, Cancel); //дбавляем продукты

                await _db.SaveChangesAsync(Cancel); //сохраняем изменения

                await _db.Database.CommitTransactionAsync(Cancel); //применяем транзацию
            }

            _logger.LogInformation("Инициализация продуктов успешно завершена");
        }
        private async Task InitializeEmployeesAsync(CancellationToken Cancel)
        {
            //Проверяем существует ли в секциях хотя-бы одна секция
            if (_db.Employees.Any())
            {
                _logger.LogInformation("Инициализация сотрудников не требуется");
                return; //если да, то ничего делать не требуется
            }

            _logger.LogInformation("Инициализация сотрудников ...");
            _logger.LogInformation("Добавление сотрудников");
            await using (await _db.Database.BeginTransactionAsync(Cancel)) //Запрашиваем транзакцию
            {
                await _db.Employees.AddRangeAsync(TestData.Employees, Cancel); //Берём данные для заполнения таблиц из класса TestData

                await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Employees] ON", Cancel); //Поскольку в тестовых данные присвоены Id, то разрешаем программе добавлять такие данные для таблицы Sectons с помощью SQL-запроса
                await _db.SaveChangesAsync(Cancel); //Сохраняем изменения (все данные прилетают в БД именно во время сохранения)
                await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Employees] OFF", Cancel); //Отключаем возможность добавлять присвоенные Id

                await _db.Database.CommitTransactionAsync(Cancel);
            }

            _logger.LogInformation("Инициализация сотрудников успешно завершена");
        }
    }
}

