using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp_MxDraw.lib
{

    public class RenderContext
    {
        private AxMxDrawXLib.AxMxDrawX axMxDrawX1 = new AxMxDrawXLib.AxMxDrawX();
        public RenderContext(Grid grid)
        {
            ((System.ComponentModel.ISupportInitialize)axMxDrawX1).BeginInit();
            // 创建 host 对象
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost() { Child = axMxDrawX1 };
            // 结束初始化
            ((System.ComponentModel.ISupportInitialize)axMxDrawX1).EndInit();
            // 将对象加入到面板中
            grid.Children.Add(host);// mxdraw为Window/Grid名称(name属性)
        }

        public AxMxDrawXLib.AxMxDrawX getContext()
        {
            return axMxDrawX1;
        }
    }
}
