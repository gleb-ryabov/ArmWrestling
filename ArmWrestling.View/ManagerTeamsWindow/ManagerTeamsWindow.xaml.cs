using ArmWrestling.ViewModel.ManagerTeamsWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ArmWrestling.View.ManagerTeamsWindow
{
    public partial class ManagerTeamsWindow : IManagerTeamsWindow
    {
        public ManagerTeamsWindow(IManagerTeamsWindowViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
