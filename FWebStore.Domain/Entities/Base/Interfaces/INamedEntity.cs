namespace FWebStore.Domain.Entities.Base.Interfaces;

public interface INamedEntity : IEntity //унаследованная от сущности
{
    string Name { get; set; } //именованная сущьность - это то у чего есть имя
}