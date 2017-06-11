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

        public static Tuple<double, double> GetMiddleCoordinates(double lat1, double lat2, double lng1, double lng2)
        {
            double lat;
            double lng;

            double dLon = DegreesToRadians(lng2 - lng1);
            double Bx = Math.Cos(DegreesToRadians(lat2)) * Math.Cos(dLon);
            double By = Math.Cos(DegreesToRadians(lat2)) * Math.Sin(dLon);

            lat = RadiansToDegrees(Math.Atan2(
                Math.Sin(DegreesToRadians(lat1)) + Math.Sin(DegreesToRadians(lat2)),
                Math.Sqrt(
                    (Math.Cos(DegreesToRadians(lat1)) + Bx) *
                    (Math.Cos(DegreesToRadians(lat1)) + Bx) + By * By)));

            lng = lng1 + RadiansToDegrees(Math.Atan2(By, Math.Cos(DegreesToRadians(lat1)) + Bx));

            return new Tuple<double, double>(lat, lng);
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }

        private static double RadiansToDegrees(double radians)
        {
            return radians * (180.0 / Math.PI);
        }
    }
}
