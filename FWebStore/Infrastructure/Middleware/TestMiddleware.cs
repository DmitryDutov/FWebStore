namespace FWebStore.Infrastructure.Middleware
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _Next;

        public TestMiddleware(RequestDelegate Next)
        {
            _Next=Next;
        }

        public async Task Invoke(HttpContext Context)
        {
            //При каждом подключении сюда будет передаваться объект контекста
            //Мы сможем обработать информацию из Context.Request
            //если хотим чтобы конвейер продолжил работу то вызываем делегат _Next:
            //await _Next(Context); //и передаём ему Context
            //после окончания работы конвейера мы можем дообработать данные в Context.Responce

            //Так же можно выполнять действия параллельно обработке конвейера
            //запускаем делегат и получаем задачу:
            var processing_task = _Next(Context);
            //выполняем необходимые действия параллельно
            //синхронизируемся с задачей:
            await processing_task;
            //Дообработка данных в Context.Responce
        }
    }
}
