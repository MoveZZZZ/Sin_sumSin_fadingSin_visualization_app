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
        void CreateSinusoidSeriesOffsetTime(DualSinusoidModel _sinModeelDual);
        void CalculateCustomPhaseOffsetTime(DualSinusoidModel _sinModeelDual, int option);
        void CreateSinusoidSeriesOffsetTimeAndFrequency(DualSinusoidModel _sinModeelDual);
        void CalculateCustomPhaseOffsetTimeAndFrequency(DualSinusoidModel _sinModeelDual, int option);
        void WindowCalculate(double[] points, DualSinusoidModel _sinModelDual);
    }
}
