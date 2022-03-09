using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace FWebStore.ViewModels;

public class EmployeeViewModel
{
    [HiddenInput(DisplayValue = false)]     //Скрыть поле
    public int Id { get; set; }             //Идентификатор

    [Display(Name = "Фамилия")]             //Отобразить как "Фамилия"
    public string LastName { get; set; }    //Фамилия

    [Display(Name = "Имя")]             //Отобразить как "Имя"
    public string FirstName { get; set; }   //Имя

    [Display(Name = "Отчество")]             //Отобразить как "Отчество"
    public string Patronumic { get; set; }  //Отчество

    [Display(Name = "Возраст")]             //Отобразить как "Возраст"
    public int Age { get; set; }            //Возраст

}
