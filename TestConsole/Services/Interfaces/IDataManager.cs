using TestConsole.Data;

namespace TestConsole.Services.Interfaces
{
    public interface IDataManager
    {
        void Process(IEnumerable<DataValue> Values);
    }
}
