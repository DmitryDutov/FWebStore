using FWebStore.Domain.Entities.Base.Interfaces;

namespace FWebStore.Domain.Entities.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}

