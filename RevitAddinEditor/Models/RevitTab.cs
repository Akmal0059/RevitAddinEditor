using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddinEditor.Models
{
    public class RevitTab
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public List<RevitPanel> Panels { get; set; }
    }
}
