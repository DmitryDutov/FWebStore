
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("newFile", true, true); //добавляем ещё один файл конфигурации
builder.Configuration.AddCommandLine(args); //возможность конфигурации через cmd
var app = builder.Build();

//Загрузка информации из файла конфигурации

//var configuration = app.Configuration;
//var greetings = configuration["CustomGreetings"];

app.MapGet("/", () => app.Configuration["CustomGreetings"]);
app.Run();


