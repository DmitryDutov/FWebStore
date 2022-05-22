namespace FWebStore.Services.Interfaces
{
    public interface IDbInitializer
    {
        Task<bool> RemoveAsync(CancellationToken Cancel = default);
        Task InitializeAsync(bool RemoveBefore = false, CancellationToken Cansel = default);
    }
}

