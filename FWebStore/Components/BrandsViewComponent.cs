using Microsoft.AspNetCore.Mvc;

namespace FWebStore.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
