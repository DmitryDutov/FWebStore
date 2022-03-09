using TestConsole.Data;
using TestConsole.Services.Interfaces;

namespace TestConsole.Services;

public class WriteToFileProcessor : IDataProcessor
{
    //Создаём свойство, определяющее путь к файлу
    public static string DataFileName { get; set; } = "data.txt";

    //Реализуем интерфейс IDataProcessor
    public void Process(DataValue Value)
    {
        var str = string.Format("[{0}], ({1}) : {2}", Value.Id, Value.Time, Value.Value);
        using var writer = File.AppendText(DataFileName);
        writer.WriteLine(str);
        //writer.WriteLine("[{0}], ({1}) : {2}", Value.Id, Value.Time, Value.Value); //в этом случае не нужна var str
    }
}