using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWebStore.ViewModels
{
    public class SectionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public SectionViewModel? Parent { get; set; }
        public List<SectionViewModel> ChidSection { get; set; } = new();
    }
}
