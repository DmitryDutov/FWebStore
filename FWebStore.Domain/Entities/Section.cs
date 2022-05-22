using System.ComponentModel.DataAnnotations.Schema;
using FWebStore.Domain.Entities.Base;
using FWebStore.Domain.Entities.Base.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FWebStore.Domain.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Section : NamedEntity,IOrderedEntity
    {
        public int Order { get; set; }

        //ссылка на родительскую секцию. ? -> может не быть родтельской секции
        public int? ParentId { get; set; } //= null; //присвоение null означает что свойство необязательно

        [ForeignKey(nameof(ParentId))] //Назначение навигационного свойства
        public Section Parent { get; set; } //Cоздание навигационного свойства

        public ICollection<Product> Products { get; set; } //Создаём связь один ко многим (Одна секция, много продуктов)
    }
}

