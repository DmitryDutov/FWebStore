
var builder = WebApplication.CreateBuilder(args);
#region Настройка построителя приложения - определение содержимого

var services = builder.Services;
services.AddControllersWithViews(); //Основная инфраструктура MVC

#endregion

#region Сборка приложения

var app = builder.Build();
//app.Urls.Add("http://80"); //доступ через localhost (видимость в локальной сети)

#endregion

#region Конфигурирование объекта обработки входящих соединений

//Поведение приложения в режиме Development
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); //промежуточное ПО(для удобной отладки)
}

app.UseStaticFiles(); //конфигурируем приложение для работы со статическими файлами

//Система маршрутизации
app.UseRouting();

//Машрут чтения ошибки
app.MapGet("/throw", () =>
{
    throw new ApplicationException("Ошибка приложения");
});

//Обработка входящих подключений системы MVC
app.MapControllerRoute(
    name:"default",
    pattern: "{controller=Home}/{action=Index}/{id?}" //Установили значения по умолчанию
    ); //кастомный маршрут

#endregion

#region Запуск приложения

//Запуск приложения
app.Run();

#endregion

