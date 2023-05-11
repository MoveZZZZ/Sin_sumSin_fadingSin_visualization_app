using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebsis_3.Models
{
    public class SinusoidModel
    {
        public double Amplitude { get; set; }
        public double Frequency { get; set; }
        public double Phasse { get; set; }
        public double Delta { get; set; }
        public double TimeStart { get; set; }
        public double TimeEnd { get; set; }
        public double SampleRate { get; set; }
        public List<double> xCoord{ get; set; }
        public List<double> yCoord{ get; set; }
        public List<double> xCoordWidmo { get; set; }
        public List<double> yCoordWidmo { get; set; }
    }
}
