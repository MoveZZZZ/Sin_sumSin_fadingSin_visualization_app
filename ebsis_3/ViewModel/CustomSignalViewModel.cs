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
        private const string _basicTimeSignal = "1";
        private const string _tFirst = "1";
        private const string _tSecond = "2";
        private const string _nPar="0";
        private const string _kPar="10";


        private const string _basicWidnowWidth = "0";

        private string _selectedItemFreqency = _basicFreq;
        private string _selectedIteAmplitude = _basicAmplitude;
        private string _selectedItemPhase = _basicPhase;
        private string _selectedItemSampleFreq = _basicSampleFreq;

        private string _selectedItemTimeStart = _basicTimeStart;
        private string _selectedItemTimeEnd = _basicTimeEnd;
        private string _selectedItemSignalTime = _basicTimeSignal;
        private string _selectedItemTFirst = _tFirst;
        private string _selectedItemTSecond = _tSecond;

        private string _selectedItemParametrK = _kPar;
        private string _selectedItemParametrN = _nPar;

        private string _selectedItemWindowWidth = _basicWidnowWidth;

        private string _selectedItemWindowTypeString = "None";
        private ComboBoxItem _selectedItemWindowType;
        private string _errMsg = "";

        private PlotModel _plotModelCustomSignal;
        private PlotModel _plotModelSpectrum;
        private PlotModel _plotModelSpectrumPhase;

        private System.Windows.Visibility _isVisibleWindowWidth = System.Windows.Visibility.Collapsed;

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
        public PlotModel PlotModelSpectrumPhase
        {
            get
            {
                return _plotModelSpectrumPhase;
            }
            set
            {
                _plotModelSpectrumPhase = value;
                OnPropertyChanged(nameof(PlotModelSpectrumPhase));

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
                if (String.Equals(_selectedItemWindowTypeString, "None") == true)
                {
                    IsVisibleWindowWidth = System.Windows.Visibility.Collapsed;
                    SelectedItemWindowWidth = "0";
                }
                else
                {
                    IsVisibleWindowWidth = System.Windows.Visibility.Visible;
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
        public string SelectedItemSignalTime
        {
            get
            {
                return _selectedItemSignalTime;
            }
            set
            {
                if (_selectedItemSignalTime != value)
                {
                    _selectedItemSignalTime = value;
                    OnPropertyChanged(nameof(SelectedItemSignalTime));
                }
            }
        }
        public string SelectedItemParametrN
        {
            get
            {
                return _selectedItemParametrN;
            }
            set
            {
                if(_selectedItemParametrN!=value)
                {
                    _selectedItemParametrN = value;
                    OnPropertyChanged(nameof(SelectedItemParametrN));
                }
            }
        }
        public string SelectedItemParametrK
        {
            get
            {
                return _selectedItemParametrK;
            }
            set
            {
                if (_selectedItemParametrK != value)
                {
                    _selectedItemParametrK = value;
                    OnPropertyChanged(nameof(SelectedItemParametrK));
                }
            }
        }
        public string SelectedItemWindowWidth
        {
            get
            {
                return _selectedItemWindowWidth;
            }
            set
            {
                if (_selectedItemWindowWidth != value)
                {
                    _selectedItemWindowWidth = value;
                    OnPropertyChanged(nameof(SelectedItemWindowWidth));
                }
            }
        }
        public System.Windows.Visibility IsVisibleWindowWidth
        {
            get
            {
                return _isVisibleWindowWidth;
            }
            set
            {
                if (_isVisibleWindowWidth != value)
                {
                    _isVisibleWindowWidth = value;
                    OnPropertyChanged(nameof(IsVisibleWindowWidth));
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
            PlotModelSpectrumPhase = new PlotModel();
            try
            {
                validateData();
                updateValue();

                setParSinusoid();
                setParSinusoidSpectrum();
                setParSinusoidSpectrumPhase();


                CustomRepository.CreateSinusoidSeries(_CustomModel);
                ErrorMessage = _CustomModel.ErrorMSG;
                addSeriesCustomSignal();
                addSeriesSpectrum();
                addSeriesSpectrumPhase();

            }
            catch
            {
                ErrorMessage = "Bad value!";
            }


        }
        private void updateValue()
        {

            _CustomModel.Amplitude = Convert.ToDouble(_selectedIteAmplitude);
            _CustomModel.Frequency = Convert.ToDouble(_selectedItemFreqency);
            _CustomModel.SampleRate = Convert.ToDouble(_selectedItemSampleFreq);
            _CustomModel.Phasse = Convert.ToDouble(_selectedItemPhase);
            _CustomModel.Time = Convert.ToDouble(_selectedItemSignalTime);
            _CustomModel.Nvalue = Convert.ToDouble(_selectedItemParametrN);
            _CustomModel.Kvalue = Convert.ToDouble(_selectedItemParametrK);
            _CustomModel.TimeStart = Convert.ToDouble(_selectedItemTimeStart);
            _CustomModel.TimeEnd = Convert.ToDouble(_selectedItemTimeEnd);
            _CustomModel.TimeFirst = Convert.ToDouble(_selectedItemTFirst);
            _CustomModel.TimeSecond = Convert.ToDouble(_selectedItemTSecond);
            _CustomModel.WindowType = _selectedItemWindowTypeString;
            _CustomModel.WindowWidthModel = Convert.ToInt32(_selectedItemWindowWidth);


        }
        private void setParSinusoid()
        {
            PlotModelCustomSignal.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Time [s]" });
            PlotModelCustomSignal.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Amplitude [m]" });
            PlotModelCustomSignal.Title = "Graph of the sine wave in the time domain";
        }
        private void setParSinusoidSpectrum()
        {
            PlotModelSpectrum.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Frequency [Hz]" });
            PlotModelSpectrum.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Magnitude" });
            PlotModelSpectrum.Title = "Graph of the sine wave in the frequency domain [Magnitude]";
        }
        private void setParSinusoidSpectrumPhase()
        {
            PlotModelSpectrumPhase.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Frequency [Hz]" });
            PlotModelSpectrumPhase.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Phase" });
            PlotModelSpectrumPhase.Title = "Graph of the sine wave in the frequency domain [Phase]";
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
            if (Convert.ToDouble(_selectedItemTimeEnd) > Convert.ToDouble(_selectedItemSignalTime))
            {
                SelectedItemSignalTime = SelectedItemTimeEnd;
            }
        }
        private void figureValidate()
        {
            SelectedItemTimeStart = SelectedItemTimeStart.Replace(".", ",");
            SelectedItemTimeEnd = SelectedItemTimeEnd.Replace(".", ",");            
            SelectedItemTFirst = SelectedItemTFirst.Replace(".", ",");
            SelectedItemTSecond = SelectedItemTSecond.Replace(".", ",");
            SelectedItemAmplitude = SelectedItemAmplitude.Replace(".", ",");
            SelectedItemParametrK = SelectedItemParametrK.Replace(".", ",");
            SelectedItemParametrN = SelectedItemParametrN.Replace(".", ",");
            SelectedItemPhase = SelectedItemPhase.Replace(".", ",");
            SelectedItemSignalTime = SelectedItemSignalTime.Replace(".", ",");
            SelectedItemWindowWidth = SelectedItemWindowWidth.Replace(".", ",");


            foreach (char c in SelectedItemFreqency)
            {
                if (!char.IsDigit(c))
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
                if (!char.IsDigit(c))
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
            foreach (char c in SelectedItemSignalTime)
            {
                if (!char.IsDigit(c) && c != '.' && c != ',')
                {
                    SelectedItemTimeEnd = _basicTimeSignal;
                }
            }
            foreach (char c in SelectedItemParametrN)
            {
                if (!char.IsDigit(c) && c != '.' && c != ',' && c != '-')
                {
                    SelectedItemParametrN = _nPar;
                }
            }
            foreach (char c in SelectedItemParametrK)
            {
                if (!char.IsDigit(c) && c != '.' && c != ',' && c != '-')
                {
                    SelectedItemParametrK = _kPar;
                }
            }
            foreach (char c in SelectedItemWindowWidth)
            {
                if (!char.IsDigit(c) && c != ',')
                {
                    SelectedItemWindowWidth = _basicWidnowWidth;
                }
            }
            if (SelectedItemWindowWidth.IndexOf(",") > -1)
            {
                SelectedItemWindowWidth = Math.Ceiling(Convert.ToDouble(SelectedItemWindowWidth)).ToString();
            }
        }
        private void validateSampleFreq()
        {
            if ((Convert.ToDouble(_selectedItemSampleFreq) * Convert.ToDouble(_selectedItemSignalTime)) / 2 < Convert.ToDouble(_selectedItemFreqency))
            {
                double freqNew = Math.Floor((Convert.ToDouble(_selectedItemFreqency) * 2) / Convert.ToDouble(_selectedItemSignalTime));
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
            var serSpectrum = new StemSeries()
            {
                MarkerType = MarkerType.Circle,
                MarkerStroke = OxyColor.FromRgb(0, 0, 0),
                Color = OxyColor.FromRgb(66, 0, 117)
            };
            for (int i = 0; i < _CustomModel.xCoordSpectrum.Count; i++)
            {
                serSpectrum.Points.Add(new DataPoint(_CustomModel.xCoordSpectrum[i], _CustomModel.yCoordSpectrum[i]));
            }
            if (PlotModelSpectrum.Series.Count != 0)
            {
                PlotModelSpectrum.Series.Clear();
            }
            PlotModelSpectrum.Series.Add(serSpectrum);
            PlotModelSpectrum.InvalidatePlot(true);
        }
        private void addSeriesSpectrumPhase()
        {
            var serSpectrumPhase = new StemSeries()
            {
                MarkerType = MarkerType.Circle,
                MarkerStroke = OxyColor.FromRgb(0, 0, 0),
                Color = OxyColor.FromRgb(66, 0, 117)
            };
            for (int i = 0; i < _CustomModel.xCoordSpectrum.Count; i++)
            {
                serSpectrumPhase.Points.Add(new DataPoint(_CustomModel.xCoordSpectrum[i], _CustomModel.yCoordSpectrumPhase[i]));
            }
            if (PlotModelSpectrumPhase.Series.Count != 0)
            {
                PlotModelSpectrumPhase.Series.Clear();
            }
            PlotModelSpectrumPhase.Series.Add(serSpectrumPhase);
            PlotModelSpectrumPhase.InvalidatePlot(true);
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
