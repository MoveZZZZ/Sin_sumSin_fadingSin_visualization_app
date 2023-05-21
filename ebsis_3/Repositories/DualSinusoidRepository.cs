using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ebsis_3.Models;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;

namespace ebsis_3.Repositories
{
    public class DualSinusoidRepository : IDualSinusoidRepository
    {


        public void CreateSinusoidSeries(DualSinusoidModel _sinModeelDual)
        {
            _sinModeelDual.ErrorMSG = "";
            _sinModeelDual.xCoord = new List<double>();
            _sinModeelDual.yCoord = new List<double>();


            List<double> points = new List<double>();


            for (double i=0, t =0; i<_sinModeelDual.SampleRate*_sinModeelDual.Time;i++, t+=1/_sinModeelDual.SampleRate)
            {
                double tempPoints = _sinModeelDual.AmplitudeFirstSin * Math.Sin(2 * Math.PI * _sinModeelDual.FrequencyFirstSin * t + _sinModeelDual.PhasseFirstSin)
                    + _sinModeelDual.AmplitudeSecondSin * Math.Sin(2 * Math.PI * _sinModeelDual.FrequencySecondSin * t + _sinModeelDual.PhasseSecondSin);
                points.Add(tempPoints);

                if(t >= _sinModeelDual.TimeStart && t <= _sinModeelDual.TimeEnd)
                {
                    _sinModeelDual.yCoord.Add(tempPoints);
                    _sinModeelDual.xCoord.Add(t);
                }
            }
            WindowCalculate(points.ToArray(), _sinModeelDual);
        }
        public void CalculateCustomPhaseOffsetTime(DualSinusoidModel _sinModeelDual, int option)
        {
            double periodSecondSinusoid = 1 / _sinModeelDual.FrequencySecondSin;
            double observationTime = _sinModeelDual.TimeEnd - _sinModeelDual.TimeStart;
            double customPhase = 0;

            if (option ==0)
               customPhase = periodSecondSinusoid * _sinModeelDual.Time/observationTime + _sinModeelDual.PhasseSecondSin;
            else
                customPhase = periodSecondSinusoid * _sinModeelDual.Time / observationTime *20*option + _sinModeelDual.PhasseSecondSin;

            _sinModeelDual.PhasseCustom = customPhase;
        }
        public void CreateSinusoidSeriesOffsetTime(DualSinusoidModel _sinModeelDual)
        {
            _sinModeelDual.ErrorMSG = "";
            _sinModeelDual.xCoord = new List<double>();
            _sinModeelDual.yCoord = new List<double>();
            List<double> points = new List<double>();
          
            for (double i = 0, t = 0; i < _sinModeelDual.SampleRate * _sinModeelDual.Time; i++, t += 1 / _sinModeelDual.SampleRate)
            {
                double tempPoints = _sinModeelDual.AmplitudeFirstSin * Math.Sin(2 * Math.PI * _sinModeelDual.FrequencyFirstSin * t + _sinModeelDual.PhasseFirstSin)
                    + _sinModeelDual.AmplitudeSecondSin * Math.Sin(2 * Math.PI * _sinModeelDual.FrequencySecondSin * t + _sinModeelDual.PhasseCustom);
                points.Add(tempPoints);

                if (t >= _sinModeelDual.TimeStart && t <= _sinModeelDual.TimeEnd)
                {
                    _sinModeelDual.yCoord.Add(tempPoints);
                    _sinModeelDual.xCoord.Add(t);
                }
            }
            WindowCalculate(points.ToArray(), _sinModeelDual);
        }
        public void CalculateCustomPhaseOffsetTimeAndFrequency(DualSinusoidModel _sinModeelDual, int option)
        {
            double observationTime = _sinModeelDual.TimeEnd - _sinModeelDual.TimeStart;
            double periodBasicSinus = Convert.ToDouble(LCM(1, 1)) / Convert.ToDouble(GCF(Convert.ToInt32(_sinModeelDual.FrequencyFirstSin), Convert.ToInt32(_sinModeelDual.FrequencySecondSin)));
            double customPhase = 0;
            if (option == 0)
                customPhase = periodBasicSinus/100 * _sinModeelDual.FrequencySecondSin* _sinModeelDual.Time / observationTime + _sinModeelDual.PhasseSecondSin;
            else
                customPhase = periodBasicSinus / 100 * _sinModeelDual.FrequencySecondSin/(2*option) * (_sinModeelDual.Time / observationTime) *(20*option+10) + _sinModeelDual.PhasseSecondSin;

            _sinModeelDual.PhasseCustom = customPhase;
        }
        public void CreateSinusoidSeriesOffsetTimeAndFrequency(DualSinusoidModel _sinModeelDual)
        {
            _sinModeelDual.ErrorMSG = "";
            _sinModeelDual.xCoord = new List<double>();
            _sinModeelDual.yCoord = new List<double>();
            List<double> points = new List<double>();

            for (double i = 0, t = 0; i < _sinModeelDual.SampleRate * _sinModeelDual.Time; i++, t += 1 / _sinModeelDual.SampleRate)
            {
                double tempPoints = _sinModeelDual.AmplitudeFirstSin * Math.Sin(2 * Math.PI * _sinModeelDual.FrequencyFirstSin * t + _sinModeelDual.PhasseFirstSin)
                    + _sinModeelDual.AmplitudeSecondSin * Math.Sin(2 * Math.PI * _sinModeelDual.FrequencySecondSin * t + _sinModeelDual.PhasseCustom);
                points.Add(tempPoints);

                if (t >= _sinModeelDual.TimeStart && t <= _sinModeelDual.TimeEnd)
                {
                    _sinModeelDual.yCoord.Add(tempPoints);
                    _sinModeelDual.xCoord.Add(t);
                }
            }
            WindowCalculate(points.ToArray(), _sinModeelDual);
        }

        public void WindowCalculate(double[] points, DualSinusoidModel _sinModelDual)
        {
            _sinModelDual.xCoordSpectrum= new List<double>();
            _sinModelDual.yCoordSpectrum = new List<double>();
            _sinModelDual.yCoordSpectrumPhase = new List<double>();
            int WindowWidth = createWindowWidtch(_sinModelDual);
            if(WindowWidth>points.Length)
            {
                WindowWidth = points.Length;
                _sinModelDual.ErrorMSG= "*too much window width, change sample freq., window width=max";
                _sinModelDual.WindowWidthModel = WindowWidth;
            }
            if (_sinModelDual.WindowType == "Hann Periodic")
            {
                HannPeriodicWindowParameters(_sinModelDual, points, WindowWidth); ;
            }
            else if (_sinModelDual.WindowType == "Hann")
            {
                HannWindowParameters(_sinModelDual, points, WindowWidth); 
            }
            else if (_sinModelDual.WindowType == "Lanczos")
            {
                LanczosWindowParameters(_sinModelDual, points, WindowWidth);
            }
            else if (_sinModelDual.WindowType == "Hamming")
            {
                HammingWindowParameters(_sinModelDual, points, WindowWidth);
            }
            else if (_sinModelDual.WindowType == "Hamming Periodic")
            {
                HammingPeriodicWindowParameters(_sinModelDual, points, WindowWidth);
            }
            else if (_sinModelDual.WindowType == "Barlett")
            {
                BarlettWindowParameters(_sinModelDual, points, WindowWidth);
            }
            else if (_sinModelDual.WindowType == "Barlett Hann")
            {
                BarlettHannWindowParameters(_sinModelDual, points, WindowWidth);
            }
            else if (_sinModelDual.WindowType == "Blackman")
            {
                BlackmanWindowParameters(_sinModelDual, points, WindowWidth);
            }
            else if (_sinModelDual.WindowType == "Dirchlet")
            {
                DirchletWindowParameters(_sinModelDual, points, WindowWidth);
            }
            else if (_sinModelDual.WindowType == "Nutall")
            {
                NutallWindowParameters(_sinModelDual, points, WindowWidth);
            }
            else
            {
                NoneWindowParameters(_sinModelDual, points, WindowWidth);
            }
        }
        private static int GCF (int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
        private static int LCM (int a, int b)
        {
            return (a / GCF(a, b)) * b;
        }


        private int createWindowWidtch(DualSinusoidModel _sinModelDual)
        {
            if (_sinModelDual.WindowWidthModel == 0)
            {
                return (int)Math.Round((1 / Math.Max(_sinModelDual.FrequencyFirstSin, _sinModelDual.FrequencySecondSin)) / (1 / _sinModelDual.SampleRate) * 5 + 0.5f);
            }
            else
            {
                return _sinModelDual.WindowWidthModel;
            }
        }
        private void HannPeriodicWindowParameters(DualSinusoidModel _sinModelDual, double[] points, int WindowWidth)
        {
            var HannWindowPer = Window.HannPeriodic(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * HannWindowPer[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModelDual.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModelDual.xCoordSpectrum.Add(scale[i]);
                _sinModelDual.yCoordSpectrum.Add(window[i].Magnitude);
                _sinModelDual.yCoordSpectrumPhase.Add(window[i].Phase);
            }
        }
        private void HannWindowParameters(DualSinusoidModel _sinModelDual, double[] points, int WindowWidth)
        {
            var HannWindow = Window.Hann(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * HannWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModelDual.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModelDual.xCoordSpectrum.Add(scale[i]);
                _sinModelDual.yCoordSpectrum.Add(window[i].Magnitude);
                _sinModelDual.yCoordSpectrumPhase.Add(window[i].Phase);
            }
        }
        private void LanczosWindowParameters(DualSinusoidModel _sinModelDual, double[] points, int WindowWidth)
        {
            var LanczosWindow = Window.Lanczos(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * LanczosWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModelDual.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModelDual.xCoordSpectrum.Add(scale[i]);
                _sinModelDual.yCoordSpectrum.Add(Complex.Abs(window[i].Magnitude));
                _sinModelDual.yCoordSpectrumPhase.Add(window[i].Phase);
            }
        }
        private void HammingWindowParameters(DualSinusoidModel _sinModelDual, double[] points, int WindowWidth)
        {
            var HammingWindow = Window.Hamming(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * HammingWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModelDual.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModelDual.xCoordSpectrum.Add(scale[i]);
                _sinModelDual.yCoordSpectrum.Add(window[i].Magnitude);
                _sinModelDual.yCoordSpectrumPhase.Add(window[i].Phase);
            }
        }
        private void HammingPeriodicWindowParameters(DualSinusoidModel _sinModelDual, double[] points, int WindowWidth)
        {
            var HammingPerWindow = Window.HammingPeriodic(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * HammingPerWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModelDual.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModelDual.xCoordSpectrum.Add(scale[i]);
                _sinModelDual.yCoordSpectrum.Add(window[i].Magnitude);
                _sinModelDual.yCoordSpectrumPhase.Add(window[i].Phase);
            }
        }
        private void NoneWindowParameters(DualSinusoidModel _sinModelDual, double[] points, int WindowWidth)
        {
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModelDual.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModelDual.xCoordSpectrum.Add(scale[i]);
                _sinModelDual.yCoordSpectrum.Add(window[i].Magnitude);
                _sinModelDual.yCoordSpectrumPhase.Add(window[i].Phase);
            }
        }
        private void BarlettWindowParameters(DualSinusoidModel _sinModelDual, double[] points, int WindowWidth)
        {
            var BarlettWindow = Window.Bartlett(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * BarlettWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModelDual.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModelDual.xCoordSpectrum.Add(scale[i]);
                _sinModelDual.yCoordSpectrum.Add(window[i].Magnitude);
                _sinModelDual.yCoordSpectrumPhase.Add(window[i].Phase);
            }
        }
        private void BarlettHannWindowParameters(DualSinusoidModel _sinModelDual, double[] points, int WindowWidth)
        {
            var BarlettHannWindow = Window.BartlettHann(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * BarlettHannWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModelDual.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModelDual.xCoordSpectrum.Add(scale[i]);
                _sinModelDual.yCoordSpectrum.Add(window[i].Magnitude);
                _sinModelDual.yCoordSpectrumPhase.Add(window[i].Phase);
            }
        }
        private void BlackmanWindowParameters(DualSinusoidModel _sinModelDual, double[] points, int WindowWidth)
        {
            var BlackmanWindow = Window.Blackman(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * BlackmanWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModelDual.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModelDual.xCoordSpectrum.Add(scale[i]);
                _sinModelDual.yCoordSpectrum.Add(window[i].Magnitude);
                _sinModelDual.yCoordSpectrumPhase.Add(window[i].Phase);
            }
        }
        private void DirchletWindowParameters(DualSinusoidModel _sinModelDual, double[] points, int WindowWidth)
        {
            var DirchletWindow = Window.Dirichlet(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * DirchletWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModelDual.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModelDual.xCoordSpectrum.Add(scale[i]);
                _sinModelDual.yCoordSpectrum.Add(window[i].Magnitude);
                _sinModelDual.yCoordSpectrumPhase.Add(window[i].Phase);
            }
        }
        private void NutallWindowParameters(DualSinusoidModel _sinModelDual, double[] points, int WindowWidth)
        {
            var NutallWindow = Window.Nuttall(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * NutallWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModelDual.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModelDual.xCoordSpectrum.Add(scale[i]);
                _sinModelDual.yCoordSpectrum.Add(window[i].Magnitude);
                _sinModelDual.yCoordSpectrumPhase.Add(window[i].Phase);
            }
        }


    }
}
