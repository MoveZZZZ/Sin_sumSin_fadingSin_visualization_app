using ebsis_3.Models;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
namespace ebsis_3.Repositories
{
    public class SinusoidReposirory : ISinusoidRepository
    {
        public void CreateSinusoidSeries(SinusoidModel _sinModedl)
        {
            _sinModedl.ErrorMSG = "";
            _sinModedl.xCoord = new List<double>();
            _sinModedl.yCoord = new List<double>();

            var points = Generate.Sinusoidal(Convert.ToInt32(_sinModedl.Time * _sinModedl.SampleRate), _sinModedl.SampleRate, _sinModedl.Frequency, _sinModedl.Amplitude, 0, _sinModedl.Phasse);

            for (int i = 0; i < points.Length; i++)
            {
                double x = 1 / _sinModedl.SampleRate * i;
                if (x >= _sinModedl.TimeStart && x <= _sinModedl.TimeEnd)
                {
                    _sinModedl.xCoord.Add(x);
                    _sinModedl.yCoord.Add(points[i]);
                }
            }
            WindowCalculate(points, _sinModedl);
        }

        public void WindowCalculate(double[] points, SinusoidModel _sinModel)
        {
            _sinModel.xCoordWidmo = new List<double>();
            _sinModel.yCoordWidmo = new List<double>();
            _sinModel.yCoordWidmoPhase = new List<double>();
            int WindowWidth = createWindowWidtch(_sinModel);
            if (WindowWidth > points.Length)
            {
                WindowWidth = points.Length;
                _sinModel.ErrorMSG = "*too much window width, change sample freq., window width=max";
                _sinModel.WindowWidthModel = WindowWidth;
            }
            if (_sinModel.WindowType == "Hann Periodic")
            {
                HannPeriodicWindowParameters(_sinModel, points, WindowWidth);
            }
            else if (_sinModel.WindowType == "Hann")
            {
                HannWindowParameters(_sinModel, points, WindowWidth);
            }
            else if (_sinModel.WindowType == "Lanczos")
            {
                LanczosWindowParameters(_sinModel, points, WindowWidth);
            }
            else if (_sinModel.WindowType == "Hamming")
            {
                HammingWindowParameters(_sinModel, points, WindowWidth);
            }
            else if (_sinModel.WindowType == "Hamming Periodic")
            {
                HammingPeriodicWindowParameters(_sinModel, points, WindowWidth);
            }
            else if(_sinModel.WindowType== "Barlett")
            {
                BarlettWindowParameters(_sinModel, points, WindowWidth);
            }
            else if (_sinModel.WindowType == "Barlett Hann")
            {
                BarlettHannWindowParameters(_sinModel, points, WindowWidth);
            }
            else if (_sinModel.WindowType == "Blackman")
            {
                BlackmanWindowParameters(_sinModel, points, WindowWidth);
            }
            else if (_sinModel.WindowType == "Dirchlet")
            {
                DirchletWindowParameters(_sinModel, points, WindowWidth);
            }
            else if (_sinModel.WindowType == "Nutall")
            {
                NutallWindowParameters(_sinModel, points, WindowWidth);
            }
            else
            {
                NoneWindowParameters(_sinModel, points, WindowWidth);
            }
        }
        private int createWindowWidtch(SinusoidModel _sinModel)
        {
            if (_sinModel.WindowWidthModel == 0)
            {
                return (int)Math.Round((1 / _sinModel.Frequency) / (1 / _sinModel.SampleRate) * 5 + 0.5f);
            }
            else
            {
                return _sinModel.WindowWidthModel;
            }
        }
        private void HannPeriodicWindowParameters(SinusoidModel _sinModel, double[] points, int WindowWidth)
        {
            var HannWindowPer = Window.HannPeriodic(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * HannWindowPer[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModel.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModel.xCoordWidmo.Add(scale[i]);
                _sinModel.yCoordWidmo.Add(window[i].Magnitude);
                _sinModel.yCoordWidmoPhase.Add(window[i].Phase);
            }
        }
        private void HannWindowParameters(SinusoidModel _sinModel, double[] points, int WindowWidth)
        {
            var HannWindow = Window.Hann(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * HannWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModel.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModel.xCoordWidmo.Add(scale[i]);
                _sinModel.yCoordWidmo.Add(window[i].Magnitude);
                _sinModel.yCoordWidmoPhase.Add(window[i].Phase);
            }
        }
        private void LanczosWindowParameters(SinusoidModel _sinModel, double[] points, int WindowWidth)
        {
            var LanczosWindow = Window.Lanczos(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * LanczosWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModel.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModel.xCoordWidmo.Add(scale[i]);
                _sinModel.yCoordWidmo.Add(Complex.Abs(window[i].Magnitude));
                _sinModel.yCoordWidmoPhase.Add(window[i].Phase);
            }
        }
        private void HammingWindowParameters(SinusoidModel _sinModel, double[] points, int WindowWidth)
        {
            var HammingWindow = Window.Hamming(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * HammingWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModel.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModel.xCoordWidmo.Add(scale[i]);
                _sinModel.yCoordWidmo.Add(window[i].Magnitude);
                _sinModel.yCoordWidmoPhase.Add(window[i].Phase);
            }
        }
        private void HammingPeriodicWindowParameters(SinusoidModel _sinModel, double[] points, int WindowWidth)
        {
            var HammingPerWindow = Window.HammingPeriodic(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * HammingPerWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModel.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModel.xCoordWidmo.Add(scale[i]);
                _sinModel.yCoordWidmo.Add(window[i].Magnitude);
                _sinModel.yCoordWidmoPhase.Add(window[i].Phase);
            }
        }
        private void NoneWindowParameters(SinusoidModel _sinModel, double[] points, int WindowWidth)
        {
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModel.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModel.xCoordWidmo.Add(scale[i]);
                _sinModel.yCoordWidmo.Add(window[i].Magnitude);
                _sinModel.yCoordWidmoPhase.Add(window[i].Phase);
            }
        }
        private void BarlettWindowParameters (SinusoidModel _sinModel, double[] points, int WindowWidth)
        {
            var BarlettWindow = Window.Bartlett(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * BarlettWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModel.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModel.xCoordWidmo.Add(scale[i]);
                _sinModel.yCoordWidmo.Add(window[i].Magnitude);
                _sinModel.yCoordWidmoPhase.Add(window[i].Phase);
            }
        }
        private void BarlettHannWindowParameters(SinusoidModel _sinModel, double[] points, int WindowWidth)
        {
            var BarlettHannWindow = Window.BartlettHann(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * BarlettHannWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModel.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModel.xCoordWidmo.Add(scale[i]);
                _sinModel.yCoordWidmo.Add(window[i].Magnitude);
                _sinModel.yCoordWidmoPhase.Add(window[i].Phase);
            }
        }
        private void BlackmanWindowParameters(SinusoidModel _sinModel, double[] points, int WindowWidth)
        {
            var BlackmanWindow = Window.Blackman(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * BlackmanWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModel.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModel.xCoordWidmo.Add(scale[i]);
                _sinModel.yCoordWidmo.Add(window[i].Magnitude);
                _sinModel.yCoordWidmoPhase.Add(window[i].Phase);
            }
        }
        private void DirchletWindowParameters(SinusoidModel _sinModel, double[] points, int WindowWidth)
        {
            var DirchletWindow = Window.Dirichlet(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * DirchletWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModel.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModel.xCoordWidmo.Add(scale[i]);
                _sinModel.yCoordWidmo.Add(window[i].Magnitude);
                _sinModel.yCoordWidmoPhase.Add(window[i].Phase);
            }
        }
        private void NutallWindowParameters(SinusoidModel _sinModel, double[] points, int WindowWidth)
        {
            var NutallWindow = Window.Nuttall(WindowWidth);
            var window = new Complex[WindowWidth];
            for (int i = 0; i < WindowWidth; i++)
            {
                window[i] = new Complex(points[i] * NutallWindow[i], 0.0);
            }
            Fourier.Forward(window);
            var scale = Fourier.FrequencyScale(WindowWidth, _sinModel.SampleRate);
            for (int i = 0; i < WindowWidth; i++)
            {
                _sinModel.xCoordWidmo.Add(scale[i]);
                _sinModel.yCoordWidmo.Add(window[i].Magnitude);
                _sinModel.yCoordWidmoPhase.Add(window[i].Phase);
            }
        }






    }
}
