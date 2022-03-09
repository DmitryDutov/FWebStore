using FWebStore.Domain.Entities.Base.Interfaces;

namespace FWebStore.Domain.Entities.Base;

public abstract class NamedEntity : Entity, INamedEntity
{
    public string Name { get; set; }
}