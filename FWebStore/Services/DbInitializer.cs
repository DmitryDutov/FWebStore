using FWebStore.DAL.Context;
using FWebStore.Data;
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

            _logger.LogInformation("Инициализация БД выполнена успешно");
        }

        private async Task InitializeProductsAsync(CancellationToken Cancel)
        {
            //Проверяем существует ли в секциях хотя-бы одна секция
            if (_db.Sections.Any())
            {
                _logger.LogInformation("Инициализация тестовых данных не требуется");
                return; //если да, то ничего делать не требуется
            }

            _logger.LogInformation("Инициализация тестовых данных ...");
            _logger.LogInformation("Добавление секций");
            await using (await _db.Database.BeginTransactionAsync(Cancel)) //Запрашиваем транзакцию
            {
                await _db.Sections.AddRangeAsync(TestData.Sections, Cancel); //Берём данные для заполнения таблиц из класса TestData

                await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] ON", Cancel); //Поскольку в тестовых данные присвоены Id, то разрешаем программе добавлять такие данные для таблицы Sectons с помощью SQL-запроса
                await _db.SaveChangesAsync(Cancel); //Сохраняем изменения (все данные прилетают в БД именно во время сохранения)
                await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF", Cancel); //Отключаем возможность добавлять присвоенные Id

                await _db.Database.CommitTransactionAsync(Cancel);
            }

            _logger.LogInformation("Добавление брендов");
            await using (await _db.Database.BeginTransactionAsync(Cancel)) //Запрашиваем транзакцию
            {
                await _db.Brands.AddRangeAsync(TestData.Brands, Cancel); //Берём данные для заполнения таблиц из класса TestData

                await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] ON", Cancel);
                await _db.SaveChangesAsync(Cancel); 
                await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF", Cancel);

                await _db.Database.CommitTransactionAsync(Cancel);
            }

            _logger.LogInformation("Добавление товаров");
            await using (await _db.Database.BeginTransactionAsync(Cancel)) //Запрашиваем транзакцию
            {
                await _db.Products.AddRangeAsync(TestData.Products, Cancel); //Берём данные для заполнения таблиц из класса TestData

                await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] ON", Cancel);
                await _db.SaveChangesAsync(Cancel); 
                await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] OFF", Cancel);

                await _db.Database.CommitTransactionAsync(Cancel);
            }
            _logger.LogInformation("Инициализация тестовых данных успешно завершена");
        }
    }
}

