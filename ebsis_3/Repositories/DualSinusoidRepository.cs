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


            for (double i=0, t =0; i<_sinModeelDual.SampleRate*_sinModeelDual.TimeEnd;i++, t+=1/_sinModeelDual.SampleRate)
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

        public void WindowCalculate(double[] points, DualSinusoidModel _sinModelDual)
        {
            _sinModelDual.xCoordSpectrum= new List<double>();
            _sinModelDual.yCoordSpectrum = new List<double>();
            int WindowWidth = (int)Math.Round((1 / Math.Max(_sinModelDual.FrequencyFirstSin, _sinModelDual.FrequencySecondSin)) / (1 / _sinModelDual.SampleRate) * 5 + 0.5f);
            if(WindowWidth>points.Length)
            {
                WindowWidth = points.Length;
                _sinModelDual.ErrorMSG= "*too much window width, bad spectrum, change frequency or time range!";
            }
            if (_sinModelDual.WindowType == "Hann Periodic")
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
                    _sinModelDual.yCoordSpectrum.Add(Complex.Abs(window[i].Magnitude));
                }
            }
            else if (_sinModelDual.WindowType == "Hann")
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
                    _sinModelDual.yCoordSpectrum.Add(Complex.Abs(window[i].Magnitude));
                }
            }
            else if (_sinModelDual.WindowType == "Lanczos")
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
                }
            }
            else if (_sinModelDual.WindowType == "Hamming")
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
                    _sinModelDual.yCoordSpectrum.Add(Complex.Abs(window[i].Magnitude));
                }
            }
            else if (_sinModelDual.WindowType == "Hamming Periodic")
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
                    _sinModelDual.yCoordSpectrum.Add(Complex.Abs(window[i].Magnitude));
                }
            }
            else
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
                    _sinModelDual.yCoordSpectrum.Add(Complex.Abs(window[i].Magnitude));
                }
            }
        }
    }
}
