using System.Collections.Generic;
using RevitAddinBase;

namespace RevitAddinEditor.Models
{
    public class RevitPanel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string ClassName { get; set; }
        public string AssemblyName { get; set; }
        public string AvailabilityName { get; set; }
        public List<RibbonItemBase> Items { get; set; }
    }
}
