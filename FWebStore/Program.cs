
#region Настройка построителя приложения - определение содержимого

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddControllersWithViews(); //Основная инфраструктура MVC

#endregion

#region Сборка приложения

var app = builder.Build();

#endregion

#region Конфигурирование объекта обработки входящих соединений

//Поведение приложения в режиме Development
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); //промежуточное ПО(для удобной отладки)
}

//Система маршрутизации
app.UseRouting();

////Маршрут чтения из файла настроек
//app.MapGet("/", () => app.Configuration["CustomGreetings"]);
//Машрут чтения ошибки
app.MapGet("/throw", () =>
{
    throw new ApplicationException("Ошибка приложения");
});

//Обработка входящих подключений системы MVC
//app.MapDefaultControllerRoute(); //стандартный маршрут
app.MapControllerRoute(
    name:"default",
    pattern: "{controller=Home}/{action=Index}/{id?}" //Установили значения по умолчанию
    ); //кастомный маршрут

#endregion

#region Запуск приложения

//Запуск приложения
app.Run();

#endregion

