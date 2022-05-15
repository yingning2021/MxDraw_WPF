using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MxDrawXLib;

namespace WpfApp_MxDraw.lib.Shape
{
    public class MyPolygon
    {
        private List<MxDrawPoint> points = new List<MxDrawPoint>();
        public MyPolygon(List<MxDrawPoint> points)
        {
            this.points = points;
        }


        public void Clear()
        {
            this.points.Clear();
        }


        public MxDrawPoint getStartPoint()
        {
            return this.points[0];
        }


        /// <summary>
        /// 射线与线段相交性判断
        /// </summary>
        /// <param name="origin">原始点</param>
        /// <param name="p1">线段头</param>
        /// <param name="p2">线段尾</param>
        /// <returns>3: 点在线段上 0: 线段与射线没有交际 1: 线段与射线存在交点</returns>
        private int IsDetectIntersect(MxDrawPoint origin, MxDrawPoint p1, MxDrawPoint p2)
        {
            Double pointY;//交点Y坐标，x固定值
            var buttomPoint = DoubleUtils.Less(p1.y, p2.y) ? p1 : p2;
            var topPoint = DoubleUtils.Great(p1.y, p2.y) ? p1 : p2;

            // 传入的线段为垂直于y轴
            if (DoubleUtils.Equal(p1.x, p2.x))
            {
                if (DoubleUtils.Great(origin.y, topPoint.y) || DoubleUtils.Less(origin.y, buttomPoint.y))
                {
                    return 0;
                }
                return 3;
            }

            if (DoubleUtils.Equal(p1.y, p2.y))
            {
                pointY = p1.y;
            }
            else
            {
                //直线两点式方程：(y-y2)/(y1-y2) = (x-x2)/(x1-x2)
                Double a = p1.x - p2.x;
                Double b = p1.y - p2.y;
                Double c = p2.y / b - p2.x / a;

                pointY = b / a * origin.x + b * c;
            }

            Trace.WriteLine(origin.y.ToString() + " " + buttomPoint.y.ToString() + " " + pointY.ToString() + "  " + topPoint.y.ToString());

            if (DoubleUtils.Equal(pointY, origin.y))
            {
                return 3;
            }

            if (DoubleUtils.Less(pointY, origin.y))
            {
                //交点y小于射线起点y
                return 0;
            }


            if (DoubleUtils.Great(pointY, buttomPoint.y) && DoubleUtils.Less(pointY, topPoint.y))
            {
                // 交点在线段上
                return 1;
            }
            // 交点不在线段上
            return 0;
        }

        /// <summary>
        /// 点与多边形的位置关系
        /// </summary>
        /// <param name="point">判定点</param>
        /// <param name="polygonVerts">剩余顶点按顺序排列的多边形</param>
        /// <returns>true:点在多边形之内，false:相反</returns>
        public  bool IsPointInsidePolygon(MxDrawPoint point)
        {
            int interNum = 0;


            int len = points.Count;
            MxDrawPoint dummyPoint = this.getStartPoint();
            points.Add(dummyPoint);

            for (int i = 0; i < len; i++)
            {
                Trace.WriteLine(i + 1 < points.Count);
                var result = IsDetectIntersect(point, points[i], points[i + 1]);
                if (result == 3)
                {
                    return true;
                }
                interNum += result;
            }

            points.RemoveAt(points.Count - 1);

            Trace.WriteLine("interNum: " + interNum);

            int remainder = interNum % 2;
            return remainder == 1;
        }
    }
}
