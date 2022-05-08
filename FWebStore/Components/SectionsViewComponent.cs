using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FWebStore.Components
{
    //[ViewComponent (Name = "sections")]
    public class SectionsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}

