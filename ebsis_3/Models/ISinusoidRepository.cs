using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebsis_3.Models
{
    interface ISinusoidRepository
    {
        void CreateSinusoidSeries(SinusoidModel _sinModeel);
        void WindowCalculate(double[] points, SinusoidModel _sinModel);
    }
}
