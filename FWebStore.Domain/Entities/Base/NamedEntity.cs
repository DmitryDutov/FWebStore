using System.ComponentModel.DataAnnotations;
using FWebStore.Domain.Entities.Base.Interfaces;

namespace FWebStore.Domain.Entities.Base;

public abstract class NamedEntity : Entity, INamedEntity
{
    //[Required] //Обязательный атрибут (если не указать, то будет ошибка)
    public string Name { get; set; }
}
