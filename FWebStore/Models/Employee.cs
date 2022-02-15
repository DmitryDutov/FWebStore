using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWebStore.Models;
public class Employee
{
    public int Id { get; set; }             //Идентификатор
    public string LastName { get; set; }    //Фамилия
    public string FirstName { get; set; }   //Имя
    public string Patronumic { get; set; }  //Отчество
    public int Age { get; set; }            //Возраст
}
