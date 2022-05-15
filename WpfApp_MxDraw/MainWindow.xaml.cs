using System.Collections.Generic;
using System.Windows;
using MxDrawXLib;
using WpfApp_MxDraw.lib;
using WpfApp_MxDraw.lib.Shape;

namespace WpfApp_MxDraw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AxMxDrawXLib.AxMxDrawX axMxDrawX1 ;
        private List<MyPolygon> polygons = new List<MyPolygon>();

        public MainWindow()
        {
            InitializeComponent();
            var context = new RenderContext(mxdraw);
            this.axMxDrawX1 = context.getContext();
            axMxDrawX1.ImplementCommandEvent += new AxMxDrawXLib._DMxDrawXEvents_ImplementCommandEventEventHandler(this.axMxDrawX_ImplementCommandEvent);
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            axMxDrawX1.DoCommand(1);
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            axMxDrawX1.DoCommand(2);
        }

        private void axMxDrawX_ImplementCommandEvent(object sender, AxMxDrawXLib._DMxDrawXEvents_ImplementCommandEventEvent e)
        {
            if (e.iCommandId == 1)
            {
                DrawPolygon();
            }
            if (e.iCommandId == 2)
            {
                GetPointAndJudge();
            }
        }

        private void GetPointAndJudge()
        {
            MxDrawUtility mxUtility = new MxDrawUtility();
            MxDrawPoint point = (MxDrawPoint)(mxUtility.GetPoint(null, ""));

            while (point != null)
            {
                var isInside = false;
                for(var i = 0; i < polygons.Count; i++)
                {
                    var polygon = polygons[i];
                    isInside = polygon.IsPointInsidePolygon(point) || isInside;
                }
                myTextBox.Text =isInside ? "点在多边形内" : "点不在多边形内";
                point = (MxDrawPoint)(mxUtility.GetPoint(null, ""));
            }
        }

        //绘制
        // 应该写一个绘制的方法，通过点连线的类似的图形，然后传入polygon，调用polygon的方法，然后绘制。
        // 一个收集顶点
        private void DrawPolygon()
        {
            MxDrawPoint pointBefore = null;
            MxDrawUtility mxUtility = new MxDrawUtility();
            var points = new List<MxDrawPoint>();

            MxDrawPoint point = (MxDrawPoint)(mxUtility.GetPoint(null, ""));
            while (point != null)
            {
                if(pointBefore != null)
                {
                    axMxDrawX1.DrawLine(pointBefore.x, pointBefore.y, point.x, point.y);
                }
                points.Add(point);
                pointBefore = point;
                point = (MxDrawPoint)(mxUtility.GetPoint(null, ""));
            }
            var startPoint = points[0];
            if(!isSamePoint(startPoint, pointBefore))
            {
                myTextBox.Text = "将自动闭合";
                axMxDrawX1.DrawLine(pointBefore.x, pointBefore.y, startPoint.x, startPoint.y);
            } else
            {
                points.RemoveAt(points.Count - 1);
                myTextBox.Text = "是一个闭环,删除最后重复的点";
            }
            //添加到存储序列中
            this.polygons.Add(new MyPolygon(points));
            myTextBox.Text = "绘制结束";
        }

        private static bool isSamePoint(MxDrawPoint point1, MxDrawPoint point2)
        {
            return point1.x == point2.x && point1.y == point2.y && point1.z == point2.z;
        }


    }
}
