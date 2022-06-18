using System.ComponentModel.DataAnnotations;

namespace FWebStore.ViewModels.Identity
{
    public class RegisterUserViewModel
    {
        [Required] //Обязательно
        [Display(Name = "Имя пользователя")] //Отображение на форме
        public string UserName { get; set; }

        [Required] //Обязательно
        [Display(Name = " Пароль")] //Отображение на форме
        [DataType(DataType.Password)] //Чтобы пароль отображался как ****
        public string Password { get; set; }

        [Required] //Обязательно
        [Display(Name = "Подтвеждение пароля")] //Отображение на форме
        [DataType(DataType.Password)] //Чтобы подтверждение отображалось как ****
        [Compare(nameof(Password))] //Значение Password должно совпадать с PasswordConfirm
        public string PasswordConfirm { get; set; }
    }
}
