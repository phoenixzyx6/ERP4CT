using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZLERP.Model.ViewModels;

namespace ZLERP.Web.Helpers
{
    public class LatLonUtils
    {
        private const double ParaE1 = 6.69438499958795E-03;//椭球体第一偏心率
        private const double Parak0 = 1.57048687472752E-07;//有关椭球体的常量
        private const double Parak1 = 5.05250559291393E-03;//有关椭球体的常量
        private const double Parak2 = 2.98473350966158E-05;//有关椭球体的常量
        private const double Parak3 = 2.41627215981336E-07;//有关椭球体的常量
        private const double Parak4 = 2.22241909461273E-09;//有关椭球体的常量
        private const double ParaC = 6399596.65198801;//极点子午圈曲率半径
        private const double EARTH_RADIUS = 6378.137;//地球半径

        /// <summary>
        /// 高斯坐标转经纬度算法
        /// </summary>
        /// <param name="x">大地坐标X</param>
        /// <param name="y">大地坐标Y</param>
        /// <param name="B">纬度（单位：弧度）</param>
        /// <param name="L">经度（单位：弧度）</param>
        /// <param name="center">中央经线（单位：弧度）</param>
        private void ComputeXYGeo(double x, double y, ref double B, ref double L, double center, int n)
        {

            double y1, bf, e, se, v, t, N, nl, vt, yn, t2, cbf;
            y1 = y - 500000 - 1000000 * n;//减去带号
            e = Parak0 * x;
            se = Math.Sin(e);
            bf = e + Math.Cos(e) * (Parak1 * se - Parak2 * Math.Pow(se, 3) + Parak3 * Math.Pow(se, 5) - Parak4 * Math.Pow(se, 7));

            t = Math.Tan(bf);
            nl = ParaE1 * Math.Pow(Math.Cos(bf), 2);
            v = Math.Sqrt(1 + nl);
            N = ParaC / v;
            yn = y1 / N;
            vt = Math.Pow(v, 2) * t;
            t2 = Math.Pow(t, 2);
            B = bf - vt * Math.Pow(yn, 2) / 2.0 + (5.0 + 3.0 * t2 + nl - 9.0 * nl * t2) * vt * Math.Pow(yn, 4) / 24.0 - (61.0 + 90.0 * t2 + 45 * Math.Pow(t2, 2)) * vt * Math.Pow(yn, 6) / 720.0;
            cbf = 1 / Math.Cos(bf);
            L = cbf * yn - (1.0 + 2.0 * t2 + nl) * cbf * Math.Pow(yn, 3) / 6.0 + (5.0 + 28.0 * t2 + 24.0 * Math.Pow(t2, 2) + 6.0 * nl + 8.0 * nl * t2) * cbf * Math.Pow(yn, 5) / 120.0 + center;

        }

        private static double rad(double d)
        {
            return d * Math.PI / 180.0;
        }

        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = rad(lat1);
            double radLat2 = rad(lat2);
            double a = radLat1 - radLat2;
            double b = rad(lng1) - rad(lng2);

            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
            Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            s = Math.Round(s * 10000) / 10000;
            return s;
        }

        //解决经度和纬度和MAPBAR中经纬度的偏差函数
        //获取经度var lon = coordOffsetEncrypt(x, y)[0];
        //获取纬度var lat = coordOffsetEncrypt(x, y)[1];
        public static Point GpsLatLonToMapbarLatLon(Point point) {
            double x = (point.X) * 100000 % 36000000;
            double y = (point.Y) * 100000 % 36000000;
            var _X = (int)(((Math.Cos(y / 100000)) * (x / 18000)) + ((Math.Sin(x / 100000)) * (y / 9000)) + x);
            var _Y = (int)(((Math.Sin(y / 100000)) * (x / 18000)) + ((Math.Cos(x / 100000)) * (y / 9000)) + y);
            return new Point(_X / 100000.0, _Y / 100000.0);
        }
    }
}