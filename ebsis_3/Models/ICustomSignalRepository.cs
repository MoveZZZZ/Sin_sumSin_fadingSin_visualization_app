using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebsis_3.Models
{
    interface ICustomSignalRepository
    {
        void CreateSinusoidSeries(CustomSignalModel _customSignal);
        void WindowCalculate(double[] points, CustomSignalModel _customSignal);
    }
}
