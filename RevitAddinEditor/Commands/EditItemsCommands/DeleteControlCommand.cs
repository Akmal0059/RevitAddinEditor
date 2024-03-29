﻿using CustomRevitControls;
using RevitAddinEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddinEditor.Commands.EditItemsCommands
{
    public class DeleteControlCommand : CommandBase
    {
        private PanelViewModel viewModel;

        public DeleteControlCommand(PanelViewModel vm) => viewModel = vm;

        public override bool CanExecute(object parameter) => viewModel.SelectedControl != null;
        public override void Execute(object parameter)
        {
            viewModel.Controls.Remove(viewModel.SelectedControl);
            if (viewModel.Controls.Count != 0)
                viewModel.SelectedControl = viewModel.Controls.Last();
        }
    }
}
