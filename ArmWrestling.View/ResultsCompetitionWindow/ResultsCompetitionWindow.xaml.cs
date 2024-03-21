using ArmWrestling.ViewModel.ResultsCompetitionWindow;
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

namespace ArmWrestling.View.ResultsCompetitionWindow
{
    public partial class ResultsCompetitionWindow : IResultsCompetitionWindow
    {
        public ResultsCompetitionWindow(IResultsCompetitionWindowViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
