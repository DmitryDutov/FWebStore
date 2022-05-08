using FWebStore.Services.Interfaces;
using FWebStore.ViewModels;
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
            //Сначала находим секции с незаполненными родительскими Id заполняем их и складываем в список
            var parevt_sections = sections.Where(s => s.ParentId is null);
            var parrent_sections_views = parevt_sections
                .Select(s => new SectionViewModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Order = s.Order,
                })
                .ToList();

            //Теперь для каждой родительской секции надо найти её дочерние секции
            foreach (var parent_section in parrent_sections_views)
            {
                var childs = sections.Where(s => s.ParentId == parent_section.Id);

                foreach (var chid_section in childs)
                {
                    parent_section.ChidSection.Add(new SectionViewModel
                    {
                        Id = chid_section.Id,
                        Name = chid_section.Name,
                        Order = chid_section.Order,
                        Parent = parent_section.Parent,
                    });
                }
                //Сортируем дочерние категории внутри родительской по Id
                parent_section.ChidSection.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            }
            parrent_sections_views.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));

            return View(parrent_sections_views);
        }
    }
}

