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
            _sinModedl.xCoord = new List<double>();
            _sinModedl.yCoord = new List<double>();

            _sinModedl.xCoordWidmo = new List<double>();
            _sinModedl.yCoordWidmo = new List<double>();
            var points = Generate.Sinusoidal(Convert.ToInt32(_sinModedl.TimeEnd*_sinModedl.SampleRate), _sinModedl.SampleRate, _sinModedl.Frequency, _sinModedl.Amplitude, 0, _sinModedl.Phasse);
            
            for (int i = 0; i < points.Length; i++)
            {
                Double x = 1 / _sinModedl.SampleRate * i;
                if (x>=_sinModedl.TimeStart && x<=_sinModedl.TimeEnd)
                {
                    _sinModedl.xCoord.Add(x);
                    _sinModedl.yCoord.Add(points[i]);
                }

            }
            int WindowWidth = (int)Math.Round((1 / _sinModedl.Frequency) / (1 / _sinModedl.SampleRate) * 5 + 0.5f);
            //var HannWindow = Window.HannPeriodic(WindowWidth);
            var HannWindow = Window.Lanczos(WindowWidth);
            var window = new Complex[WindowWidth];
            try
            {
                for (int i = 0; i < WindowWidth; i++)
                {
                    window[i] = new Complex(points[i] *HannWindow[i], 0.0);
                }
                Fourier.Forward(window);
                var scale = Fourier.FrequencyScale(WindowWidth, _sinModedl.SampleRate);
                for (int i = 0; i < WindowWidth; i++)
                {
                    _sinModedl.xCoordWidmo.Add(scale[i]);
                    _sinModedl.yCoordWidmo.Add(window[i].Magnitude);
                }
            }
            catch
            {

            }
        }

    }
}
