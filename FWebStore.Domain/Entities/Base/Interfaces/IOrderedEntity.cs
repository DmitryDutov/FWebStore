namespace FWebStore.Domain.Entities.Base.Interfaces;

public interface IOrderedEntity : IEntity //унаследованная от сущности
{
    int Order { get; set; } //упорядоченная сущьность - это то у чего есть порядковый номер
}