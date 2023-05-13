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
    public class CustomSignalViewModel: ViewModelBase
    {
        private const string _basicFreq = "100";
        private const string _basicAmplitude = "1";
        private const string _basicPhase = "0";
        private const string _basicSampleFreq = "400";
        private const string _basicTimeStart = "0";
        private const string _basicTimeEnd = "1";
        private const string _tFirst = "1";
        private const string _tSecond = "2";




        private string _selectedItemFreqency = _basicFreq;
        private string _selectedIteAmplitude = _basicAmplitude;
        private string _selectedItemPhase = _basicPhase;
        private string _selectedItemSampleFreq = _basicSampleFreq;

        private string _selectedItemTimeStart = _basicTimeStart;
        private string _selectedItemTimeEnd = _basicTimeEnd;
        private string _selectedItemTFirst = _tFirst;
        private string _selectedItemTSecond = _tSecond;

        private string _selectedItemWindowTypeString = "None";
        private ComboBoxItem _selectedItemWindowType;
        private string _errMsg = "";

        private PlotModel _plotModelCustomSignal;
        private PlotModel _plotModelSpectrum;
        public PlotModel PlotModelCustomSignal
        {
            get
            {
                return _plotModelCustomSignal;
            }
            set
            {
                _plotModelCustomSignal = value;
                OnPropertyChanged(nameof(PlotModelCustomSignal));

            }
        }
        public PlotModel PlotModelSpectrum
        {
            get
            {
                return _plotModelSpectrum;
            }
            set
            {
                _plotModelSpectrum = value;
                OnPropertyChanged(nameof(PlotModelSpectrum));

            }
        }
        public ComboBoxItem SelectedItemWindowType
        {
            get { return _selectedItemWindowType; }
            set
            {
                if (_selectedItemWindowType != value)
                {
                    _selectedItemWindowType = value;
                    OnPropertyChanged(nameof(SelectedItemWindowType));
                    _selectedItemWindowTypeString = Convert.ToString(_selectedItemWindowType.Content);
                }
            }
        }
        public string SelectedItemFreqency
        {
            get
            {
                return _selectedItemFreqency;
            }
            set
            {
                if (_selectedItemFreqency != value)
                {
                    _selectedItemFreqency = value;
                    OnPropertyChanged(nameof(SelectedItemFreqency));
                }

            }
        }
        public string SelectedItemAmplitude
        {
            get
            {
                return _selectedIteAmplitude;
            }
            set
            {
                if (_selectedIteAmplitude != value)
                {
                    _selectedIteAmplitude = value;
                    OnPropertyChanged(nameof(SelectedItemAmplitude));
                }
            }
        }
        public string SelectedItemPhase
        {
            get
            {
                return _selectedItemPhase;
            }
            set
            {
                if (_selectedItemPhase != value)
                {
                    _selectedItemPhase = value;
                    OnPropertyChanged(nameof(SelectedItemPhase));
                }
            }
        }
        public string SelectedItemSampleFreq
        {
            get
            {
                return _selectedItemSampleFreq;
            }
            set
            {
                if (_selectedItemSampleFreq != value)
                {
                    _selectedItemSampleFreq = value;
                    OnPropertyChanged(nameof(SelectedItemSampleFreq));
                }
            }
        }
        public string SelectedItemTimeStart
        {
            get
            {
                return _selectedItemTimeStart;
            }
            set
            {
                if (_selectedItemTimeStart != value)
                {
                    _selectedItemTimeStart = value;
                    OnPropertyChanged(nameof(SelectedItemTimeStart));
                }
            }
        }
        public string SelectedItemTimeEnd
        {
            get
            {
                return _selectedItemTimeEnd;
            }
            set
            {
                if (_selectedItemTimeEnd != value)
                {
                    _selectedItemTimeEnd = value;
                    OnPropertyChanged(nameof(SelectedItemTimeEnd));
                }
            }
        }
        public string SelectedItemTFirst
        {
            get
            {
                return _selectedItemTFirst;
            }
            set
            {
                if(_selectedItemTFirst!=value)
                {
                    _selectedItemTFirst = value;
                    OnPropertyChanged(nameof(SelectedItemTFirst));
                }
            }
        }
        public string SelectedItemTSecond
        {
            get
            {
                return _selectedItemTSecond;
            }
            set
            {
                if (_selectedItemTSecond != value)
                {
                    _selectedItemTSecond = value;
                    OnPropertyChanged(nameof(SelectedItemTSecond));
                }
            }
        }
        public string ErrorMessage
        {
            get
            {
                return _errMsg;
            }
            set
            {
                if (_errMsg != value)
                {
                    _errMsg = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }


        private ICustomSignalRepository CustomRepository;
        private CustomSignalModel _CustomModel;
        public ICommand ChangeCommand { get; }


        public CustomSignalViewModel()
        {
            CustomRepository = new CustomSignalRepository();
            _CustomModel = new CustomSignalModel();
            ChangeCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
            calculateCustomSignalSeries();
        }

        private bool CanExecuteChangeCommand(object obj)
        {
            return validateNull();
        }

        private void ExecuteChangeCommand(object obj)
        {
            calculateCustomSignalSeries();
        }
        private void calculateCustomSignalSeries()
        {
            PlotModelCustomSignal = new PlotModel();
            PlotModelSpectrum = new PlotModel();

            validateData();
            updateValue();

            setParSinusoid();
            setParSinusoidWidmo();

            
            CustomRepository.CreateSinusoidSeries(_CustomModel);
            ErrorMessage = _CustomModel.ErrorMSG;
            addSeriesCustomSignal();
            addSeriesSpectrum();

        }
        private void updateValue()
        {
            _CustomModel.Amplitude = Convert.ToDouble(_selectedIteAmplitude);
            _CustomModel.Frequency = Convert.ToDouble(_selectedItemFreqency);
            _CustomModel.SampleRate = Convert.ToDouble(_selectedItemSampleFreq);
            _CustomModel.Phasse = Convert.ToDouble(_selectedItemPhase);
            _CustomModel.TimeStart = Convert.ToDouble(_selectedItemTimeStart);
            _CustomModel.TimeEnd = Convert.ToDouble(_selectedItemTimeEnd);
            _CustomModel.TimeFirst = Convert.ToDouble(_selectedItemTFirst);
            _CustomModel.TimeSecond = Convert.ToDouble(_selectedItemTSecond);
            _CustomModel.WindowType = _selectedItemWindowTypeString;
        }
        private void setParSinusoid()
        {
            PlotModelCustomSignal.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Time [s]" });
            PlotModelCustomSignal.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Amplitude [m]" });
            PlotModelCustomSignal.Title = "Wykres sinusoidy w dziedzinie czasu";
        }
        private void setParSinusoidWidmo()
        {
            PlotModelSpectrum.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Magnitude" });
            PlotModelSpectrum.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Frequency [Hz]" });
            PlotModelSpectrum.Title = "Wykres sinusoidy w dziedzinie częstotliwości";
        }
        private void validateData()
        {
            figureValidate();
            validateTimeValue();
            validateSampleFreq();
        }
        private void validateTimeValue()
        {

            if (Convert.ToDouble(_selectedItemTimeStart) > Convert.ToDouble(_selectedItemTimeEnd) && _selectedItemTimeStart != null && _selectedItemTimeEnd != null)
            {
                string t = SelectedItemTimeEnd;
                SelectedItemTimeEnd = SelectedItemTimeStart;
                SelectedItemTimeStart = t;
            }
        }
        private void figureValidate()
        {
            SelectedItemTimeStart = SelectedItemTimeStart.Replace(".", ",");
            SelectedItemTimeEnd = SelectedItemTimeEnd.Replace(".", ",");            
            SelectedItemTFirst = SelectedItemTFirst.Replace(".", ",");
            SelectedItemTSecond = SelectedItemTSecond.Replace(".", ",");
            SelectedItemAmplitude = SelectedItemAmplitude.Replace(".", ",");
            foreach (char c in SelectedItemFreqency)
            {
                if (!char.IsDigit(c) && c != '.' && c != ',')
                {
                    SelectedItemFreqency = _basicFreq;
                }
            }
            foreach (char c in SelectedItemAmplitude)
            {
                if (!char.IsDigit(c) && c != '.' && c != ',')
                {
                    SelectedItemAmplitude = _basicAmplitude;
                }
            }
            foreach (char c in SelectedItemPhase)
            {
                if (!char.IsDigit(c) && c != '.' && c != ',')
                {
                    SelectedItemPhase = _basicPhase;
                }
            }
            foreach (char c in SelectedItemSampleFreq)
            {
                if (!char.IsDigit(c) && c != '.' && c != ',')
                {
                    SelectedItemSampleFreq = _basicSampleFreq;
                }
            }
            foreach (char c in SelectedItemTimeStart)
            {
                if (!char.IsDigit(c) && c != '.' && c != ',')
                {
                    SelectedItemTimeStart = _basicTimeStart;
                }
            }
            foreach (char c in SelectedItemTimeEnd)
            {
                if (!char.IsDigit(c) && c != '.' && c != ',')
                {
                    SelectedItemTimeEnd = _basicTimeEnd;
                }
            }
            foreach (char c in SelectedItemTFirst)
            {
                if (!char.IsDigit(c) && c != '.' && c != ',')
                {
                    SelectedItemTFirst = _tFirst;
                }
            }
            foreach (char c in SelectedItemTSecond)
            {
                if (!char.IsDigit(c) && c != '.' && c != ',')
                {
                    SelectedItemTSecond = _tSecond;
                }
            }
        }
        private void validateSampleFreq()
        {
            if ((Convert.ToDouble(_selectedItemSampleFreq) * Convert.ToDouble(_selectedItemTimeEnd)) / 2 < Convert.ToDouble(_selectedItemFreqency))
            {
                double freqNew = Math.Floor((Convert.ToDouble(_selectedItemFreqency) * 2) / Convert.ToDouble(_selectedItemTimeEnd));
                SelectedItemSampleFreq = Convert.ToString(freqNew);
            }
        }
        private void addSeriesCustomSignal()
        {
            var ser = new LineSeries()
            {
                Color = OxyColor.FromRgb(66, 0, 117)
            };

            for (int i = 0; i < _CustomModel.xCoord.Count; i++)
            {
                ser.Points.Add(new DataPoint(_CustomModel.xCoord[i], _CustomModel.yCoord[i]));

            }
            if (PlotModelCustomSignal.Series.Count != 0)
            {
                PlotModelCustomSignal.Series.Clear();
            }
            PlotModelCustomSignal.Series.Add(ser);
            PlotModelCustomSignal.InvalidatePlot(true);
        }
        private void addSeriesSpectrum()
        {
            var serWidmo = new LineSeries()
            {
                LineStyle = LineStyle.Automatic,
                MarkerType = MarkerType.Circle,
                MarkerStroke = OxyColor.FromRgb(0, 0, 0),
                Color = OxyColor.FromRgb(66, 0, 117)
            };
            for (int i = 0; i < _CustomModel.xCoordSpectrum.Count; i++)
            {
                serWidmo.Points.Add(new DataPoint(_CustomModel.xCoordSpectrum[i], _CustomModel.yCoordSpectrum[i]));
            }
            if (PlotModelSpectrum.Series.Count != 0)
            {
                PlotModelSpectrum.Series.Clear();
            }
            PlotModelSpectrum.Series.Add(serWidmo);
            PlotModelSpectrum.InvalidatePlot(true);
        }
        private bool validateNull()
        {
            if (SelectedItemAmplitude == "" || SelectedItemFreqency == "" || SelectedItemPhase == ""
                || SelectedItemSampleFreq == "" || SelectedItemTimeEnd == "" || SelectedItemTimeStart == ""
                || SelectedItemTFirst==""|| SelectedItemTSecond=="")
                return false;
            return true;

        }
    }
}
