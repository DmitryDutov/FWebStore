using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace FWebStore.Infrastructure.Conventions;

public class TestConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller) //сюда будут передаваться и перебираться все контроллеры которые обранужила система
    {
        //Debug.WriteLine(controller.ControllerName); //вывод названия контроллера

        //controller.Actions //получаем доступ к действиям контроллера
        //controller.RouteValues //можно изменять сущестующие маршруты, либо добавлять новые
    }
}

