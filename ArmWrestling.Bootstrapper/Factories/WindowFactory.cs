using ArmWrestling.View.CreateCompetitionWindow;
using ArmWrestling.View.Factories;
using ArmWrestling.View.MainWindow;
using ArmWrestling.View.ManagerCompetitionWindow;
using ArmWrestling.View.ParticipantListWindow;
using ArmWrestling.View.RegistrationOfPersonsWindow;
using ArmWrestling.View.ResultsCompetitionWindow;
using ArmWrestling.View.SelectTableCategoriesWindow;
using ArmWrestling.ViewModel.CreateCompetitionWindow;
using ArmWrestling.ViewModel.MainWindow;
using ArmWrestling.ViewModel.ManagerCompetitionWindow;
using ArmWrestling.ViewModel.ParticipantListWindow;
using ArmWrestling.ViewModel.RegistrationOfPersonsWindow;
using ArmWrestling.ViewModel.ResultsCompetitionWindow;
using ArmWrestling.ViewModel.SelectTableCategoriesWindow;
using ArmWrestling.ViewModel.Windows;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Bootstrapper.Factories
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IComponentContext _componentContext;

        //compliance card
        private readonly Dictionary<Type, Type> _map = new()
        {
        {typeof(IMainWindowViewModel), typeof(IMainWindow) },
        {typeof(ICreateCompetitionWindowViewModel), typeof(ICreateCompetitionWindow) },
        {typeof(IRegistrationOfPersonsWindowViewModel), typeof(IRegistrationOfPersonsWindow) },
        {typeof(IManagerCompetitionWindowViewModel), typeof(IManagerCompetitionWindow) },
        {typeof(ISelectTableCategoriesWindowViewModel), typeof(ISelectTableCategoriesWindow) },
        {typeof(IParticipantListWindowViewModel), typeof(IParticipantListWindow) },
        {typeof(IResultsCompetitionWindowViewModel), typeof(IResultsCompetitionWindow) }
        };

        public WindowFactory(IComponentContext componentContext) 
        {
            _componentContext = componentContext;
        }

        //Function for create the Window from the ViewModel
        public IWindow Create<TWindowViewModel>(TWindowViewModel viewModel) where TWindowViewModel : IWindowViewModel
        {

            if (!_map.TryGetValue(typeof(TWindowViewModel), out var windowType))
            {
                throw new InvalidOperationException(
                    $"There is no window registered for {typeof(TWindowViewModel)}");
            }

            var instance = _componentContext.Resolve(windowType, TypedParameter.From(viewModel));

            return (IWindow)instance;

        }
    }
}
