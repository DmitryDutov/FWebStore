using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace FWebStore.ViewModels;

public class EmployeeViewModel
{
    [HiddenInput(DisplayValue = false)]     //Скрыть поле
    public int Id { get; set; }             //Идентификатор

    [Display(Name = "Фамилия")]             //Отобразить как "Фамилия"
    [Required(ErrorMessage = "Фамилия обязательна")]
    [RegularExpression(@"(([А-ЯЁ][а-яё]+)|([A-Z][a-z]+))?",ErrorMessage = "Ошибка формата ввода")]
    public string LastName { get; set; }    //Фамилия

    [Display(Name = "Имя")]             //Отобразить как "Имя"
    [Required(ErrorMessage = "Имя обязательно")]
    [StringLength(255,MinimumLength = 2, ErrorMessage = "Имя должно быть от 2 до 255 символов")]
    public string FirstName { get; set; }   //Имя

    [Display(Name = "Отчество")]             //Отобразить как "Отчество"
    public string Patronumic { get; set; }  //Отчество

    [Display(Name = "Возраст")]             //Отобразить как "Возраст"
    [Range(18,80, ErrorMessage = "Возраст должен быть от 18 до 80 лет")]
    public int Age { get; set; }            //Возраст

}

