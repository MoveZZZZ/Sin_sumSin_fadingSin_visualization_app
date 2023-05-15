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

            var points = Generate.Sinusoidal(Convert.ToInt32(_sinModedl.Time*_sinModedl.SampleRate), _sinModedl.SampleRate, _sinModedl.Frequency, _sinModedl.Amplitude, 0, _sinModedl.Phasse);
            
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
            int WindowWidth = (int)Math.Round((1 / _sinModel.Frequency) / (1 / _sinModel.SampleRate) * 5 + 0.5f);
            if (WindowWidth > points.Length)
            {
                WindowWidth = points.Length;
                _sinModel.ErrorMSG = "*too much window width, bad spectrum, change frequency or time range!";
            }
            if (_sinModel.WindowType== "Hann Periodic")
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
            else if(_sinModel.WindowType=="Hann")
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
            else if(_sinModel.WindowType == "Lanczos")
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
            else if(_sinModel.WindowType == "Hamming")
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
            else if(_sinModel.WindowType == "Hamming Periodic")
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
            else
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

           
            
        }
    }
}
