
    using FWebStore.DAL.Context;
    using FWebStore.Domain.Entities.Identity;
    using FWebStore.Infrastructure.Conventions;
    using FWebStore.Infrastructure.Middleware;
    using FWebStore.Services;
    using FWebStore.Services.InSQL;
    using FWebStore.Services.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

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

    services.AddDbContext<FWebStoreDB>(opt => 
        opt.UseSqlServer(/*указывем строку подключения*/ builder.Configuration.GetConnectionString("SqlServer"))); //в данном случаем строка подключения указана в файле конфигурации
    services.AddTransient<IDbInitializer, DbInitializer>();

    //Подключаем систему Identity
    services.AddIdentity<User, Role>()
        .AddEntityFrameworkStores<FWebStoreDB>()
        .AddDefaultTokenProviders();

    //Конфигурируем систему Identity
    services.Configure<IdentityOptions>(opt =>
    {
    #if DEBUG //Требования к паролю будут выполняться только в режиме Debug
        opt.Password.RequireDigit = false; //Убираем требование наличия цифры в пароле
        opt.Password.RequireLowercase = false; //Убираем требования к нижнему регистру
        opt.Password.RequireUppercase = false; //Убираем требования к вернему регистру
        opt.Password.RequireNonAlphanumeric = false; //Отключаем не алфавитные символы
        opt.Password.RequiredLength = 3; //Длинна пароля = 3 символа
        opt.Password.RequiredUniqueChars = 3; //Должно быть миниммум три ункальных символа
    #endif

        opt.User.RequireUniqueEmail = false; //Отключаем ункальность Email'ов для пользователей, т.к. мы не будем ими пользоваться
        opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"; //Указваем разрешённый набор символов для имени пользователя

        opt.Lockout.AllowedForNewUsers = false; //Отключаем блокировку новых пользователей (чтобы полсле создания пользователя не требовалось подтверждение)
        opt.Lockout.MaxFailedAccessAttempts = 10; //Максимальное количество попыток угадать свой пароль
        opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); //Указываем время блокировки
    });

    //Конфигурируем систему Cookies

    //services.AddSingleton<IEmployeesData, InMemoryEmpoyeesData>(); //Singleton - потому что InMemory !!!
    services.AddScoped<IEmployeesData, SqlEmployeesData>();
    //services.AddSingleton<IProductData, InMemoryProductData>();  /Singleton - потому что InMemory !!!
    services.AddScoped<IProductData, SqlProductData>();

    #endregion

    var app = builder.Build(); //после построения сервисы превращаются в провайдер сервисов и могут выполнять основную работу
    //app.Urls.Add("http://80"); //доступ через localhost (видимость в локальной сети)
    await using (var scope = app.Services.CreateAsyncScope())
    {
        var db_initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        await db_initializer.InitializeAsync(RemoveBefore: false);
    }

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

