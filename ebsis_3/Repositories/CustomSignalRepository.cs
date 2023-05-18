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
    public class CustomSignalRepository : ICustomSignalRepository
    {
        public void CreateSinusoidSeries(CustomSignalModel _customSignal)
        {
            _customSignal.ErrorMSG = "";
            _customSignal.xCoord = new List<double>();
            _customSignal.yCoord = new List<double>();

            List<double> points = new List<double>();

            for (double i = 0, t = 0; i < _customSignal.SampleRate * _customSignal.Time; i++, t += 1 / _customSignal.SampleRate)
            {
                double tempPoints = _customSignal.Amplitude * _customSignal.Kvalue * (Math.Pow((t/_customSignal.TimeFirst),_customSignal.Nvalue)/(1+Math.Pow((t / _customSignal.TimeFirst), _customSignal.Nvalue)))
                    * Math.Exp(-(t/_customSignal.TimeSecond)) * Math.Cos(2*Math.PI*_customSignal.Frequency*t+_customSignal.Phasse);

                points.Add(tempPoints);
                if (t >= _customSignal.TimeStart && t <= _customSignal.TimeEnd)
                {
                    _customSignal.yCoord.Add(tempPoints);
                    _customSignal.xCoord.Add(t);
                }
            }
            WindowCalculate(points.ToArray(), _customSignal);

        }

        public void WindowCalculate(double[] points, CustomSignalModel _customSignal)
        {
            _customSignal.xCoordSpectrum = new List<double>();
            _customSignal.yCoordSpectrum = new List<double>();
            _customSignal.yCoordSpectrumPhase = new List<double>();
            int WindowWidth = createWindowWidtch(_customSignal);
            if (WindowWidth > points.Length)
            {
                WindowWidth = points.Length;
                _customSignal.ErrorMSG = "*too much window width, change sample freq., window width=max";
                _customSignal.WindowWidthModel = points.Length;
            }

            if (_customSignal.WindowType == "Hann Periodic")
            {
                HannPeriodicWindowParameters(_customSignal, points, WindowWidth);
            }
            else if (_customSignal.WindowType == "Hann")
            {
               HannWindowParameters(_customSignal, points, WindowWidth);
            }
            else if (_customSignal.WindowType == "Lanczos")
            {
               LanczosWindowParameters(_customSignal, points, WindowWidth); 
            }
            else if (_customSignal.WindowType == "Hamming")
            {
               HammingWindowParameters(_customSignal, points, WindowWidth); 
            }
            else if (_customSignal.WindowType == "Hamming Periodic")
            {
               HammingPeriodicWindowParameters(_customSignal, points, WindowWidth);
            }
            else if (_customSignal.WindowType == "Barlett")
            {
                BarlettWindowParameters(_customSignal, points, WindowWidth);
            }
            else if (_customSignal.WindowType == "Barlett Hann")
            {
                BarlettHannWindowParameters(_customSignal, points, WindowWidth);
            }
            else if (_customSignal.WindowType == "Blackman")
            {
                BlackmanWindowParameters(_customSignal, points, WindowWidth);
            }
            else if (_customSignal.WindowType == "Dirchlet")
            {
                DirchletWindowParameters(_customSignal, points, WindowWidth);
            }
            else if (_customSignal.WindowType == "Nutall")
            {
                NutallWindowParameters(_customSignal, points, WindowWidth);
            }
            else
            {
               NoneWindowParameters(_customSignal, points, WindowWidth);
            }
        }
        private int createWindowWidtch(CustomSignalModel _customSignal)
        {
            if (_customSignal.WindowWidthModel == 0)
            {
                return (int)Math.Round((1 / _customSignal.Frequency) / (1 / _customSignal.SampleRate) * 5 + 0.5f);
            }
            else
            {
                return _customSignal.WindowWidthModel;
            }
        }
        private void HannPeriodicWindowParameters(CustomSignalModel _customSignal, double[] points, int WindowWidth)
        {
            var HannWindowPer = Window.HannPeriodic(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * HannWindowPer[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _customSignal.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _customSignal.xCoordSpectrum.Add(scale[i]);
                _customSignal.yCoordSpectrum.Add(Complex.Abs(window[i].Magnitude));
                _customSignal.yCoordSpectrumPhase.Add(Complex.Abs(window[i].Phase));
            }
        }
        private void HannWindowParameters(CustomSignalModel _customSignal, double[] points, int WindowWidth)
        {
            var HannWindow = Window.Hann(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * HannWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _customSignal.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _customSignal.xCoordSpectrum.Add(scale[i]);
                _customSignal.yCoordSpectrum.Add(Complex.Abs(window[i].Magnitude));
                _customSignal.yCoordSpectrumPhase.Add(Complex.Abs(window[i].Phase));
            }
        }
        private void LanczosWindowParameters(CustomSignalModel _customSignal, double[] points, int WindowWidth)
        {
            var LanczosWindow = Window.Lanczos(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * LanczosWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _customSignal.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _customSignal.xCoordSpectrum.Add(scale[i]);
                _customSignal.yCoordSpectrum.Add(Complex.Abs(window[i].Magnitude));
                _customSignal.yCoordSpectrumPhase.Add(Complex.Abs(window[i].Phase));
            }
        }
        private void HammingWindowParameters(CustomSignalModel _customSignal, double[] points, int WindowWidth)
        {
            var HammingWindow = Window.Hamming(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * HammingWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _customSignal.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _customSignal.xCoordSpectrum.Add(scale[i]);
                _customSignal.yCoordSpectrum.Add(Complex.Abs(window[i].Magnitude));
                _customSignal.yCoordSpectrumPhase.Add(Complex.Abs(window[i].Phase));
            }
        }
        private void HammingPeriodicWindowParameters(CustomSignalModel _customSignal, double[] points, int WindowWidth)
        {
            var HammingPerWindow = Window.HammingPeriodic(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * HammingPerWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _customSignal.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _customSignal.xCoordSpectrum.Add(scale[i]);
                _customSignal.yCoordSpectrum.Add(Complex.Abs(window[i].Magnitude));
                _customSignal.yCoordSpectrumPhase.Add(Complex.Abs(window[i].Phase));
            }
        }
        private void NoneWindowParameters(CustomSignalModel _customSignal, double[] points, int WindowWidth)
        {
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _customSignal.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _customSignal.xCoordSpectrum.Add(scale[i]);
                _customSignal.yCoordSpectrum.Add(Complex.Abs(window[i].Magnitude));
                _customSignal.yCoordSpectrumPhase.Add(Complex.Abs(window[i].Phase));
            }
        }
        private void BarlettWindowParameters(CustomSignalModel _customSignal, double[] points, int WindowWidth)
        {
            var BarlettWindow = Window.Bartlett(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * BarlettWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _customSignal.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _customSignal.xCoordSpectrum.Add(scale[i]);
                _customSignal.yCoordSpectrum.Add(Complex.Abs(window[i].Magnitude));
                _customSignal.yCoordSpectrumPhase.Add(Complex.Abs(window[i].Phase));
            }
        }
        private void BarlettHannWindowParameters(CustomSignalModel _customSignal, double[] points, int WindowWidth)
        {
            var BarlettHannWindow = Window.BartlettHann(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * BarlettHannWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _customSignal.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _customSignal.xCoordSpectrum.Add(scale[i]);
                _customSignal.yCoordSpectrum.Add(Complex.Abs(window[i].Magnitude));
                _customSignal.yCoordSpectrumPhase.Add(Complex.Abs(window[i].Phase));
            }
        }
        private void BlackmanWindowParameters(CustomSignalModel _customSignal, double[] points, int WindowWidth)
        {
            var BlackmanWindow = Window.Blackman(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * BlackmanWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _customSignal.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _customSignal.xCoordSpectrum.Add(scale[i]);
                _customSignal.yCoordSpectrum.Add(Complex.Abs(window[i].Magnitude));
                _customSignal.yCoordSpectrumPhase.Add(Complex.Abs(window[i].Phase));
            }
        }
        private void DirchletWindowParameters(CustomSignalModel _customSignal, double[] points, int WindowWidth)
        {
            var DirchletWindow = Window.Dirichlet(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * DirchletWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _customSignal.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _customSignal.xCoordSpectrum.Add(scale[i]);
                _customSignal.yCoordSpectrum.Add(Complex.Abs(window[i].Magnitude));
                _customSignal.yCoordSpectrumPhase.Add(Complex.Abs(window[i].Phase));
            }
        }
        private void NutallWindowParameters(CustomSignalModel _customSignal, double[] points, int WindowWidth)
        {
            var NutallWindow = Window.Nuttall(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * NutallWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _customSignal.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _customSignal.xCoordSpectrum.Add(scale[i]);
                _customSignal.yCoordSpectrum.Add(Complex.Abs(window[i].Magnitude));
                _customSignal.yCoordSpectrumPhase.Add(Complex.Abs(window[i].Phase));
            }
        }
    }
}

