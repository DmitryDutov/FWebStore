using FWebStore.Domain.Entities.Base;
using FWebStore.Domain.Entities.Base.Interfaces;

namespace FWebStore.Domain.Entities
{
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
    }
}

