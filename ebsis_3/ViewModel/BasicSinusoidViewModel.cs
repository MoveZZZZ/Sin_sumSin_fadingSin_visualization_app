﻿using ebsis_3.Models;
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
    public class BasicSinusoidViewModel : ViewModelBase
    {
        private const string _basicFreq = "100";
        private const string _basicAmplitude = "1";
        private const string _basicPhase = "0";
        private const string _basicSampleFreq = "400";
        private const string _basicTimeStart = "0";
        private const string _basicTimeEnd = "1";



        private string _freqData = "Hz";

        private string _selectedItemFreqency = _basicFreq;
        private string _selectedIteAmplitude = _basicAmplitude;
        private string _selectedItemPhase = _basicPhase;
        private string _selectedItemSampleFreq = _basicSampleFreq;

        private string _selectedItemTimeStart = _basicTimeStart;
        private string _selectedItemTimeEnd = _basicTimeEnd;

        private string _selectedItemWindowTypeString = "None";
        private ComboBoxItem _selectedItemWindowType;
        private string _errMsg = "";

        private PlotModel _plotModel;
        private PlotModel _plotModelWidmo;

        public PlotModel PlotModel
        {
            get
            {
                return _plotModel;
            }
            set
            {
                _plotModel = value;
                OnPropertyChanged(nameof(PlotModel));

            }
        }
        public PlotModel PlotModelWidmo
        {
            get
            {
                return _plotModelWidmo;
            }
            set
            {
                _plotModelWidmo = value;
                OnPropertyChanged(nameof(PlotModelWidmo));

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
        public string FreqLabel
        {
            get
            {
                return "Frequency" + " [" + _freqData + "]";
            }
            set
            {
                if (_freqData != value)
                {
                    _freqData = value;
                    OnPropertyChanged(nameof(FreqLabel));
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
        public string ErrorMessage
        {
            get
            {
                return _errMsg;
            }
            set
            {
                if(_errMsg!=value)
                {
                    _errMsg = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }

        private ISinusoidRepository SinusRepository;
        private SinusoidModel _SinusoidModel;
        public ICommand ChangeCommand { get; }


        public BasicSinusoidViewModel()
        {
            SinusRepository = new SinusoidReposirory();
            _SinusoidModel = new SinusoidModel();
            ChangeCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
            calculate();
        }

        private bool CanExecuteChangeCommand(object obj)
        {
            return validateNull();
        }

        private void ExecuteChangeCommand(object obj)
        {
            calculate();
        }

        private void calculate()
        {
            PlotModel = new PlotModel();
            PlotModelWidmo = new PlotModel();

            validateData();
            updateValue();

            setParSinusoid();
            setParSinusoidWidmo();

            SinusRepository.CreateSinusoidSeries(_SinusoidModel);
            ErrorMessage = _SinusoidModel.ErrorMSG;
            addSeries();
            addSeriesSpectrum();

        }
        private void updateValue()
        {
            _SinusoidModel.Amplitude = Convert.ToDouble(_selectedIteAmplitude);
            _SinusoidModel.Frequency = Convert.ToDouble(_selectedItemFreqency);
            _SinusoidModel.SampleRate = Convert.ToDouble(_selectedItemSampleFreq);
            _SinusoidModel.Phasse = Convert.ToDouble(_selectedItemPhase);
            _SinusoidModel.TimeStart = Convert.ToDouble(_selectedItemTimeStart);
            _SinusoidModel.TimeEnd = Convert.ToDouble(_selectedItemTimeEnd);
            _SinusoidModel.WindowType = _selectedItemWindowTypeString;
        }
        private void setParSinusoid()
        {
            PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Time [s]" });
            PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Amplitude [m]" });
            PlotModel.Title = "Wykres sinusoidy w dziedzinie czasu";
        }
        private void setParSinusoidWidmo()
        {
            PlotModelWidmo.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Frequency [Hz]"});
            PlotModelWidmo.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Magnitude" });
            PlotModelWidmo.Title = "Wykres sinusoidy w dziedzinie częstotliwości";
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
        }
        private void validateSampleFreq()
        {
            if ((Convert.ToDouble(_selectedItemSampleFreq) * Convert.ToDouble(_selectedItemTimeEnd)) / 2  < Convert.ToDouble(_selectedItemFreqency))
            {
                double freqNew = Math.Floor((Convert.ToDouble(_selectedItemFreqency) * 2)/Convert.ToDouble(_selectedItemTimeEnd));
                SelectedItemSampleFreq = Convert.ToString(freqNew);
            }
        }
        private void addSeries()
        {
            var ser = new LineSeries()
            {
                Color = OxyColor.FromRgb(66, 0, 117)
            };

            for (int i = 0; i < _SinusoidModel.xCoord.Count; i++)
            {
                ser.Points.Add(new DataPoint(_SinusoidModel.xCoord[i], _SinusoidModel.yCoord[i]));

            }
            if (PlotModel.Series.Count != 0)
            {
                PlotModel.Series.Clear();
            }
            PlotModel.Series.Add(ser);
            PlotModel.InvalidatePlot(true);
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
            for (int i = 0; i < _SinusoidModel.xCoordWidmo.Count; i++)
            {
                serWidmo.Points.Add(new DataPoint(_SinusoidModel.xCoordWidmo[i], _SinusoidModel.yCoordWidmo[i]));
            }
            if (PlotModelWidmo.Series.Count != 0)
            {
                PlotModelWidmo.Series.Clear();
            }
            PlotModelWidmo.Series.Add(serWidmo);
            PlotModelWidmo.InvalidatePlot(true);
        }

        private bool validateNull()
        {
            if (SelectedItemAmplitude == "" || SelectedItemFreqency== "" || SelectedItemPhase== ""
                || SelectedItemSampleFreq== "" || SelectedItemTimeEnd== "" || SelectedItemTimeStart== "")
                return false;
            return true;

        }
    }

}