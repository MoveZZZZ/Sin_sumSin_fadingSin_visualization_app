using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebsis_3.Models
{
    public class DualSinusoidModel
    {
        public double AmplitudeFirstSin { get; set; }
        public double FrequencyFirstSin { get; set; }
        public double PhasseFirstSin { get; set; }
        public double AmplitudeSecondSin { get; set; }
        public double FrequencySecondSin { get; set; }
        public double PhasseSecondSin { get; set; }
        public double PhasseCustom { get; set; }
        public double Time { get; set; }
        public double TimeStart { get; set; }
        public double TimeEnd { get; set; }
        public double SampleRate { get; set; }
        public string WindowType { get; set; }
        public List<double> xCoord { get; set; }
        public List<double> yCoord { get; set; }
        public List<double> xCoordSpectrum { get; set; }
        public List<double> yCoordSpectrum { get; set; }
        public List<double> yCoordSpectrumPhase { get; set; }
        public string ErrorMSG { get; set; }
    }
}
