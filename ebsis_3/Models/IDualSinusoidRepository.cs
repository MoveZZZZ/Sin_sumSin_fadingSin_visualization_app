using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebsis_3.Models
{
    interface IDualSinusoidRepository
    {
        void CreateSinusoidSeries(DualSinusoidModel _sinModeelDual);
        void WindowCalculate(double[] points, DualSinusoidModel _sinModelDual);
    }
}
