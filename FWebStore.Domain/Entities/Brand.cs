using FWebStore.Domain.Entities.Base;
using FWebStore.Domain.Entities.Base.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FWebStore.Domain.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        public ICollection<Product> Products { get; set; } //Создаём связь один ко многим (Одна секция, много продуктов)
    }
}

