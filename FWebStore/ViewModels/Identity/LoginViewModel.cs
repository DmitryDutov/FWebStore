using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace FWebStore.ViewModels.Identity;

public class LoginViewModel
{
    [Required] //Обязательно
    [Display(Name = "Имя пользователя")] //Отображение на форме
    public string UserName { get; set; }

    [Required] //Обязательно
    [Display(Name = " Пароль")] //Отображение на форме
    [DataType(DataType.Password)] //Чтобы пароль отображался как ****
    public string Password { get; set; }

    [Display(Name = "Запомнить")]
    public bool RememberMe { get; set; } //Запоминать пользователя или нет

    [HiddenInput(DisplayValue = false)] //Скрываем поле от отображения
    public string ReturnUrl { get; set; } //С какой страницы нас перебросило на форму входа (для редиректа при успешном входе)
}