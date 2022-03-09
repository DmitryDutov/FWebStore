using Microsoft.Extensions.DependencyInjection;
using TestConsole.Data;
using TestConsole.Services;
using TestConsole.Services.Interfaces;

#region Тестовые данные

    var data = Enumerable.Range(1, 100).Select(i => new DataValue
    {
        Id = i,
        Value = $"Value-{i}",
        Time = DateTime.Now.AddHours(-i * 10),
    });

#endregion

#region Контейнер сервисов

    //Создаём новый экземпляр коллекции сервисов (из пространства Microsoft.Extensions.DependencyInjection)
    var service_collection = new ServiceCollection();

    //Регистрируем сервисы
    service_collection.AddSingleton<IDataManager, DataManager>();               //Объясняем что интерфейс IDAtaManager реализуется классом DataManager
    service_collection.AddSingleton<IDataProcessor, ConsolePrintProcessor>();   //Объясняем что интерфейс IDAtaProcessor реализуется классом ConsolePrintProcessor
    //service_collection.AddSingleton<IDataProcessor, WriteToFileProcessor>();   //Объясняем что интерфейс IDAtaProcessor реализуется классом WriteToFileProcessor

    //Создаём(строим) провайдер сервисов
    var provider = service_collection.BuildServiceProvider(); //провайдер-это коллекция конкретных построенных сервисов? provider == контейнер сервисов?
    //Обращаемся к конкретному сервису
    var service = provider.GetService<IDataManager>();
    //Передаём данные в наш сервис для обработки
    service?.Process(data);

#endregion

Console.ReadLine();

