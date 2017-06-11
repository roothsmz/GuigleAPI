using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuigleAPI
{
    public static class Helper
    {
        public static double EarhRadius { get; set; } = 6371;

        public static int GetDistance(double lat1, double lat2, double lng1, double lng2)
        {
            var slat = Math.Sin((lat2 - lat1) / 2);
            var slon = Math.Sin((lng2 - lng1) / 2);
            var q = slat * slat + Math.Cos(lat1) * Math.Cos(lat2) * slon * slon;
            var r = 2 * EarhRadius * Math.Asin(Math.Sqrt(q));
            return Convert.ToInt32(Math.Round(r));
        }

        public static double GetPreciseDistance(double lat1, double lat2, double lng1, double lng2)
        {
            var slat = Math.Sin((lat2 - lat1) / 2);
            var slon = Math.Sin((lng2 - lng1) / 2);
            var q = slat * slat + Math.Cos(lat1) * Math.Cos(lat2) * slon * slon;
            return 2 * EarhRadius * Math.Asin(Math.Sqrt(q));
        }
    }
}
