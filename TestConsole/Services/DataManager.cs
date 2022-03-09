using TestConsole.Data;
using TestConsole.Services.Interfaces;

namespace TestConsole.Services
{
    public class DataManager : IDataManager
    {
        //Получаем в конструктор сервис обрабочика данных и сохраняем в приватное поле
        private readonly IDataProcessor _Processor;
        public DataManager(IDataProcessor Processor)
        {
            _Processor = Processor;
        }
        //Реализуем интерфейс IDataManager
        public void Process(IEnumerable<DataValue> Values)
        {
            foreach (var value in Values)
            {
                _Processor.Process(value);
            }
        }
    }
}
