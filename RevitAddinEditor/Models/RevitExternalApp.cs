using CustomRevitControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddinEditor.Models
{
    public class RevitExternalApp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RevitTab> Tabs { get; set; }
    }
}
