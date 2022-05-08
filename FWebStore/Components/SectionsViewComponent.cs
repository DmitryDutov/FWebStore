using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWebStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FWebStore.Components
{
    //[ViewComponent (Name = "sections")]
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;

        public SectionsViewComponent(IProductData ProductData) => _ProductData = ProductData;
        public IViewComponentResult Invoke()
        {
            //Получаем все секции
            var sections = _ProductData.GetSections(); 

            //Теперь создаём иерархическую структуру этих секций (для удбного отображения будем получать структуру через ViewModel)


            return View();
        }
    }
}

