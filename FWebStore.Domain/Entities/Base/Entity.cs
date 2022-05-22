using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FWebStore.Domain.Entities.Base.Interfaces;

namespace FWebStore.Domain.Entities.Base
{
    public abstract class Entity : IEntity
    {
        [Key] //Объявляем свойство как первичный ключ
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Заставляем БД автомитически генерировать ключи (обеспечиваем уникальность значений)
        public int Id { get; set; }
    }
}

