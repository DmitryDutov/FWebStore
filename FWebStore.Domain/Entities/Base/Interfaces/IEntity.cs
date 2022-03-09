using System.Dynamic;

namespace FWebStore.Domain.Entities.Base.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; } //сущьность - это то у чего есть Id
    }
    public interface INamedEntity : IEntity //унаследованная от сущности
    {
        string Name { get; set; } //именованная сущьность - это то у чего есть имя
    }

    public interface IOrderedEntity : IEntity //унаследованная от сущности
    {
        int Order { get; set; } //упорядоченная сущьность - это то у чего есть порядковый номер
    }
}

