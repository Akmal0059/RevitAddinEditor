using CustomRevitControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddinEditor.Models
{
    public class AddingControl
    {
        public string Name { get; set; }
        public bool Visible { get; set; }
        public ControlType Type { get; set; }

        public AddingControl(string name, ControlType type)
        {
            Name = name;
            Type = type;
        }
    }
    public enum ControlType
    {
        Separator = -1,
        Regular = 0,
        Pulldown = 1,
        SplitButton = 2,
        StackButton = 3,
        StackedPulldown = 4,
        StackedSplitItem = 5,
        StackedRegButton = 6,
        TextBox = 7,
        Textblock = 8,
        Combobox = 9,
        Checkbox = 10
    }
}
