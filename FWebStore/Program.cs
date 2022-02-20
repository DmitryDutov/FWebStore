
    using FWebStore.Infrastructure.Conventions;
    using FWebStore.Infrastructure.Middleware;
    using FWebStore.Services;
    using FWebStore.Services.Interfaces;

    var builder = WebApplication.CreateBuilder(args);

    #region Настройка построителя приложения - определение содержимого. В этой части подключается набор сервисов и бизнесс-логика приложения

    var services = builder.Services;
    //services.AddControllersWithViews(); //Основная инфраструктура MVC
    //services.AddMvc();                //базовая реализация
    //services.AddControllers();        //для WebApi

    services.AddControllersWithViews(opt =>
    {
        opt.Conventions.Add(new TestConvention());
    });

    services.AddSingleton<IEmployeesData, InMemoryEmpoyeesData>(); //Singleton - потому что InMemory !!!

    #endregion

    var app = builder.Build();
    //app.Urls.Add("http://80"); //доступ через localhost (видимость в локальной сети)

    #region Конфигурирование объекта обработки входящих соединений. В этой части определяется конвейер обработки входящих подключений

    //Поведение приложения в режиме Development
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage(); //промежуточное ПО(для удобной отладки)
    }

    app.UseStaticFiles(); //конфигурируем приложение для работы со статическими файлами
    app.UseRouting(); //Система маршрутизации
    app.Map("/testmid", async context => await context.Response.WriteAsync("Test middleware")); //custom middleware
    app.UseMiddleware<TestMiddleware>(); //custom middleware
    app.UseWelcomePage("/welcome");
    app.MapControllerRoute( //Обработка входящих подключений системы MVC
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}" //Установили значения по умолчанию
        ); //кастомный маршрут

    #endregion

    app.Run(); //Запуск приложения

