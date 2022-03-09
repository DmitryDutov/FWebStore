using TestConsole.Data;

namespace TestConsole.Services.Interfaces;

public interface IDataProcessor
{
    void Process(DataValue Value);
}