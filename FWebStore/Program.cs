
#region Части приложения

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddControllersWithViews(); //Основная инфраструктура MVC
var app = builder.Build();

#endregion

#region Поведение приложения

//Поведение приложения в режиме Development
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); //промежуточное ПО(для удобной отладки)
}

//Маршрут чтения из файла настроек
app.MapGet("/", () => app.Configuration["CustomGreetings"]);
//Машрут чтения ошибки
app.MapGet("/throw", () =>
{
    throw new ApplicationException("Ошибка приложения");
});
app.Run();

#endregion


