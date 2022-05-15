using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_MxDraw.lib
{
    public static class DoubleUtils {
        const double epsilon = 1e-7;

        public static bool Less(double value, double other)
        {
            return (other - value) > epsilon;
        }

        public static bool Great(double value, double other)
        {
            return (value - other) > epsilon;
        }

        public static bool Equal(double value, double other)
        {
            return Math.Abs(value - other) < epsilon;
        }
    }
}
