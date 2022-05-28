using FWebStore.Domain.Entities.Base;
using FWebStore.Domain.Entities.Base.Interfaces;
using Microsoft.EntityFrameworkCore;

//Для работы с индексами БД нужен пакет Microsoft.EntityFrameworkCore.Abstractions
namespace FWebStore.Domain.Entities
{
    [Index(nameof(Id))]
    public class Employee : Entity, IOrderedEntity
    {
        public new int Id { get; set; }             //Идентификатор
        public string LastName { get; set; }    //Фамилия
        public string FirstName { get; set; }   //Имя
        public string Patronumic { get; set; }  //Отчество
        public int Age { get; set; }            //Возраст
        public int Order { get; set; }
    }
}

