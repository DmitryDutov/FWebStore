using FWebStore.Domain.Entities.Base;
using FWebStore.Domain.Entities.Base.Interfaces;

namespace FWebStore.Domain.Entities
{
    public class Section : NamedEntity,IOrderedEntity
    {
        public int Order { get; set; }

        public int? ParentId { get; set; } //ссылка на родительскую секцию. ? -> может не быть родтельской секции
    }
}

