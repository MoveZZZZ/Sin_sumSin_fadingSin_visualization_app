using ebsis_3.Models;
using ebsis_3.Repositories;
using ebsis_3.ViewModels;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ebsis_3.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _currenChildView;

        private string _labelWindow = "";

        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currenChildView;
            }
            set
            {
                _currenChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string LabelMainWindow
        {
            get { return _labelWindow; }
            set
            {
                if(_labelWindow!=value)
                {
                    _labelWindow = value;
                    OnPropertyChanged(nameof(LabelMainWindow));
                }
            }
        }


        public ICommand ShowBasicSinusoidCommand { get; }
        public ICommand ShowDualSinusoidCommand { get; }
        public ICommand ShowCustomSignalCommand { get; }

        public MainWindowViewModel()
        {
            ShowBasicSinusoidCommand = new ViewModelCommand(ExecuteShowBasicSinusoidCommand);
            ShowDualSinusoidCommand = new ViewModelCommand(ExecuteShowDualSinusoidCommand);
            ShowCustomSignalCommand = new ViewModelCommand(ExecuteShowCustomSignalCommand);
            ExecuteShowBasicSinusoidCommand(null);
        }

        private void ExecuteShowCustomSignalCommand(object obj)
        {
            CurrentChildView = new CustomSignalViewModel();
            LabelMainWindow = "Fading Sinusoid";
        }

        private void ExecuteShowDualSinusoidCommand(object obj)
        {
            CurrentChildView = new DualSinusoidViewModel();
            LabelMainWindow = "Dual Sinusoid";
        }

        private void ExecuteShowBasicSinusoidCommand(object obj)
        {
            CurrentChildView = new BasicSinusoidViewModel();
            LabelMainWindow = "Basic Sinusoid";
        }
    }
   
}
