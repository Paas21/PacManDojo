using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PacMan.Controller
{
    public class WallController : VisualObjectController
    {


        public WallController(Color color,double height, double width, double left, double top) : base(color, left, top)
        {
            RectangleShape.Stroke = new SolidColorBrush(Colors.Aqua);
            RectangleShape.StrokeThickness = 3;
            RectangleShape.Width = width;
            RectangleShape.Height = height;
        }
    }
}
