using ArmWrestling.View.StartWindow;
using ArmWrestling.ViewModel.StartWindow;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArmWrestling
{
    public partial class MainWindow : IMainWindow
    {
        public MainWindow(IMainWindowViewModel mainWindowViewModel)
        {
            DataContext = mainWindowViewModel;
            InitializeComponent();
        }
    }
}