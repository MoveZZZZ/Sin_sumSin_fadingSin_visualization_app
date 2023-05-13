using ebsis_3.ViewModels;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using ebsis_3.Models;
using ebsis_3.Repositories;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace ebsis_3.ViewModel
{
    public class DualSinusoidViewModel : ViewModelBase
    {
        private const string _basicFreqSin1 = "100";
        private const string _basicAmplitudeSin1 = "1";
        private const string _basicPhaseSin1 = "0";

        private const string _basicFreqSin2 = "200";
        private const string _basicAmplitudeSin2 = "1";
        private const string _basicPhaseSin2 = "0";

        private const string _basicSampleFreq = "800";
        private const string _basicTimeStart = "0";
        private const string _basicTimeEnd = "1";


        private string _selectedItemFreqencyFirstSin = _basicFreqSin1;
        private string _selectedItemAmplitudeFirstSin = _basicAmplitudeSin1;
        private string _selectedItemPhaseFirstSin = _basicPhaseSin1;
        private string _selectedItemFreqencySecondSin = _basicFreqSin2;
        private string _selectedItemAmplitudeSecondSin = _basicAmplitudeSin2;
        private string _selectedItemPhaseSecondSin = _basicPhaseSin2;


        private string _selectedItemSampleFreq = _basicSampleFreq;

        private string _selectedItemTimeStart = _basicTimeStart;
        private string _selectedItemTimeEnd = _basicTimeEnd;
        private string _errMsg = "";

        private bool _checkBoxBasicChecked=true;
        private bool _checkBoxTimeChecked;
        private bool _checkBoxTimeFreqChecked;

        private string _selectedItemWindowTypeString = "None";
        private ComboBoxItem _selectedItemWindowType;

        private PlotModel _plotModelDualSin;
        private PlotModel _plotModelSpectrum;


        public PlotModel PlotModelDualSin
        {
            get
            {
                return _plotModelDualSin;
            }
            set
            {
                _plotModelDualSin = value;
                OnPropertyChanged(nameof(PlotModelDualSin));

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
        public string SelectedItemFreqencyFirstSinusoid
        {
            get
            {
                return _selectedItemFreqencyFirstSin;
            }
            set
            {
                if (_selectedItemFreqencyFirstSin != value)
                {
                    _selectedItemFreqencyFirstSin = value;
                    OnPropertyChanged(nameof(SelectedItemFreqencyFirstSinusoid));
                }

            }
        }
        public string SelectedItemFreqencySecondSinusoid
        {
            get
            {
                return _selectedItemFreqencySecondSin;
            }
            set
            {
                if (_selectedItemFreqencySecondSin != value)
                {
                    _selectedItemFreqencySecondSin = value;
                    OnPropertyChanged(nameof(SelectedItemFreqencySecondSinusoid));
                }

            }
        }
        public string SelectedItemAmplitudeFirstSinusoid
        {
            get
            {
                return _selectedItemAmplitudeFirstSin;
            }
            set
            {
                if(_selectedItemAmplitudeFirstSin!=value)
                {
                    _selectedItemAmplitudeFirstSin = value;
                    OnPropertyChanged(nameof(SelectedItemAmplitudeFirstSinusoid));
                }
            }
        }
        public string SelectedItemAmplitudeSecondSinusoid
        {
            get
            {
                return _selectedItemAmplitudeSecondSin;
            }
            set
            {
                if (_selectedItemAmplitudeSecondSin != value)
                {
                    _selectedItemAmplitudeSecondSin = value;
                    OnPropertyChanged(nameof(SelectedItemAmplitudeSecondSinusoid));
                }
            }
        }
        public string SelectedItemPhaseFirstSinusoid
        {
            get
            {
                return _selectedItemPhaseFirstSin;
            }
            set
            {
                if (_selectedItemPhaseFirstSin != value)
                {
                    _selectedItemPhaseFirstSin = value;
                    OnPropertyChanged(nameof(SelectedItemPhaseFirstSinusoid));
                }
            }
        }
        public string SelectedItemPhaseSecondSinusoid
        {
            get
            {
                return _selectedItemPhaseSecondSin;
            }
            set
            {
                if (_selectedItemPhaseSecondSin != value)
                {
                    _selectedItemPhaseSecondSin = value;
                    OnPropertyChanged(nameof(SelectedItemPhaseSecondSinusoid));
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
        public bool CheckBoxBasicChecked
        {
            get { return _checkBoxBasicChecked; }
            set
            {
                _checkBoxBasicChecked = value;
                if(value)
                {
                    CheckBoxTimeChecked = false;
                    CheckBoxTimeFreqChecked = false;

                }
                OnPropertyChanged(nameof(CheckBoxBasicChecked));
            }
        }
        public bool CheckBoxTimeChecked
        {
            get { return _checkBoxTimeChecked; }
            set
            {
                _checkBoxTimeChecked = value;
                if (value)
                {
                    CheckBoxBasicChecked = false;
                    CheckBoxTimeFreqChecked = false;
                }
                OnPropertyChanged(nameof(CheckBoxTimeChecked));
            }
        }
        public bool CheckBoxTimeFreqChecked
        {
            get { return _checkBoxTimeFreqChecked; }
            set
            {
                _checkBoxTimeFreqChecked = value;
                if (value)
                {
                    CheckBoxBasicChecked = false;
                    CheckBoxTimeChecked = false;
                }
                OnPropertyChanged(nameof(CheckBoxTimeFreqChecked));
            }
        }


        private IDualSinusoidRepository DualSinusRepository;
        private DualSinusoidModel _DualSinusoidModedl;
        public ICommand ChangeCommand { get; }



        public DualSinusoidViewModel()
        {
            DualSinusRepository = new DualSinusoidRepository();
            _DualSinusoidModedl = new DualSinusoidModel();
            ChangeCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
            calculateSinusoidParameters(); 
        }

        private bool CanExecuteChangeCommand(object obj)
        {
            return validateNull();
        }

        private void ExecuteChangeCommand(object obj)
        {
            calculateSinusoidParameters();
        }
        private void calculateSinusoidParameters()
        {
            PlotModelDualSin = new PlotModel();
            PlotModelSpectrum = new PlotModel();

            validateData();
            updateValue();

            setParDualSinusoid();
            setParSpectrum();
            if(_checkBoxBasicChecked)
                DualSinusRepository.CreateSinusoidSeries(_DualSinusoidModedl);
            ErrorMessage = _DualSinusoidModedl.ErrorMSG;
            addSeriesDualSin();
            addSeriesSpectrum();


        }
        private void validateData()
        {
            figureValidate();
            validateTimeValue();
            validateSampleFreq();
        }
        private void figureValidate()
        {
            SelectedItemTimeStart = SelectedItemTimeStart.Replace(".", ",");
            SelectedItemTimeEnd = SelectedItemTimeEnd.Replace(".", ",");
            SelectedItemAmplitudeFirstSinusoid = SelectedItemAmplitudeFirstSinusoid.Replace(".", ",");
            SelectedItemAmplitudeSecondSinusoid = SelectedItemAmplitudeSecondSinusoid.Replace(".", ",");
            foreach (char c in SelectedItemFreqencyFirstSinusoid)
            {
                if (!char.IsDigit(c) && c != '.' && c != ',')
                {
                    SelectedItemFreqencyFirstSinusoid = _basicFreqSin1;
                }
            }
            foreach (char c in SelectedItemFreqencySecondSinusoid)
            {
                if (!char.IsDigit(c) && c != '.' && c != ',')
                {
                    SelectedItemFreqencySecondSinusoid = _basicFreqSin2;
                }
            }
            foreach (char c in SelectedItemAmplitudeFirstSinusoid)
            {
                if (!char.IsDigit(c) && c != '.' && c != ',')
                {
                    SelectedItemAmplitudeFirstSinusoid = _basicAmplitudeSin1;
                }
            }
            foreach (char c in SelectedItemAmplitudeSecondSinusoid)
            {
                if (!char.IsDigit(c) && c != '.' && c != ',')
                {
                    SelectedItemAmplitudeSecondSinusoid = _basicAmplitudeSin2;
                }
            }
            foreach (char c in SelectedItemPhaseFirstSinusoid)
            {
                if (!char.IsDigit(c) && c != '.' && c != ',')
                {
                    SelectedItemPhaseFirstSinusoid = _basicPhaseSin1;
                }
            }
            foreach (char c in SelectedItemPhaseSecondSinusoid)
            {
                if (!char.IsDigit(c) && c != '.' && c != ',')
                {
                    SelectedItemPhaseSecondSinusoid = _basicPhaseSin2;
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
        private void validateSampleFreq()
        {
            System.Console.WriteLine(Math.Max(Convert.ToDouble(_selectedItemFreqencyFirstSin), Convert.ToDouble(_selectedItemFreqencySecondSin)));
            if ((Convert.ToDouble(_selectedItemSampleFreq)* Convert.ToDouble(_selectedItemTimeEnd)) / 2 
                < Math.Max(Convert.ToDouble(_selectedItemFreqencyFirstSin), Convert.ToDouble(_selectedItemFreqencySecondSin)))
            {
                double freqNew = (Math.Max(Convert.ToDouble(_selectedItemFreqencyFirstSin), Convert.ToDouble(_selectedItemFreqencySecondSin)) * 2)/Convert.ToDouble(_selectedItemTimeEnd);
                SelectedItemSampleFreq = Convert.ToString(freqNew);
            }
        }
        private void updateValue()
        {
            _DualSinusoidModedl.AmplitudeFirstSin= Convert.ToDouble(_selectedItemAmplitudeFirstSin);
            _DualSinusoidModedl.AmplitudeSecondSin= Convert.ToDouble(_selectedItemAmplitudeSecondSin);
            _DualSinusoidModedl.FrequencyFirstSin = Convert.ToDouble(_selectedItemFreqencyFirstSin);
            _DualSinusoidModedl.FrequencySecondSin = Convert.ToDouble(_selectedItemFreqencySecondSin);
            _DualSinusoidModedl.SampleRate = Convert.ToDouble(_selectedItemSampleFreq);
            _DualSinusoidModedl.PhasseFirstSin = Convert.ToDouble(_selectedItemPhaseFirstSin);
            _DualSinusoidModedl.PhasseSecondSin = Convert.ToDouble(_selectedItemPhaseSecondSin);
            _DualSinusoidModedl.TimeStart = Convert.ToDouble(_selectedItemTimeStart);
            _DualSinusoidModedl.TimeEnd = Convert.ToDouble(_selectedItemTimeEnd);
            _DualSinusoidModedl.WindowType = _selectedItemWindowTypeString;
        }
        private void setParDualSinusoid()
        {
            PlotModelDualSin.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Time [s]" });
            PlotModelDualSin.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Amplitude [m]" });
            PlotModelDualSin.Title = "Wykres sumy sinusoid w dziedzinie czasu";
        }
        private void setParSpectrum()
        {
            PlotModelSpectrum.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Frequency [Hz]" });
            PlotModelSpectrum.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Magnitude" });
            PlotModelSpectrum.Title = "Wykres sumy sinusoid w dziedzinie częstotliwości";
        }
        private void addSeriesDualSin()
        {
            var series = new LineSeries()
            {
                Color = OxyColor.FromRgb(66, 0, 117)
            };

            for (int i = 0; i < _DualSinusoidModedl.xCoord.Count; i++)
            {
                series.Points.Add(new DataPoint(_DualSinusoidModedl.xCoord[i], _DualSinusoidModedl.yCoord[i]));

            }
            if (PlotModelDualSin.Series.Count != 0)
            {
                PlotModelDualSin.Series.Clear();
            }
            PlotModelDualSin.Series.Add(series);
            PlotModelDualSin.InvalidatePlot(true);
        }
        private void addSeriesSpectrum()
        {
            var seriesSpectrum = new LineSeries()
            {
                LineStyle = LineStyle.Automatic,
                MarkerType = MarkerType.Circle,
                MarkerStroke = OxyColor.FromRgb(0, 0, 0),
                Color = OxyColor.FromRgb(66, 0, 117)
            };
            for (int i = 0; i < _DualSinusoidModedl.xCoordSpectrum.Count; i++)
            {
                seriesSpectrum.Points.Add(new DataPoint(_DualSinusoidModedl.xCoordSpectrum[i], _DualSinusoidModedl.yCoordSpectrum[i]));
            }
            if (PlotModelSpectrum.Series.Count != 0)
            {
                PlotModelSpectrum.Series.Clear();
            }
            PlotModelSpectrum.Series.Add(seriesSpectrum);
            PlotModelSpectrum.InvalidatePlot(true);
        }
        private bool validateNull()
        {
            if (SelectedItemAmplitudeFirstSinusoid == "" || SelectedItemAmplitudeSecondSinusoid == "" || SelectedItemFreqencyFirstSinusoid == ""
                || SelectedItemFreqencySecondSinusoid == "" || SelectedItemPhaseFirstSinusoid == "" || SelectedItemPhaseSecondSinusoid == ""
                || SelectedItemSampleFreq == "" || SelectedItemTimeEnd == "" || SelectedItemTimeStart == "")
                return false;
            return true;

        }
    }
}
