using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PacMan.Controller
{

    public partial class VisualObjectController : ControllerBase
    {


        private Rectangle? rectangleShape;

        public Rectangle? RectangleShape
        {
            get { return rectangleShape; }
            set
            {
                rectangleShape = value;
                NotifyPropertyChanged();
            }
        }

        private string? name;


        public string? Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }
        private double x;

        public double X
        {
            get { return x; }
            set
            {
                x = value;
                NotifyPropertyChanged();
            }
        }

        private double y;

        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                NotifyPropertyChanged();
            }
        }


        public VisualObjectController(Color color, double left, double top)
        {
            rectangleShape = new Rectangle
            {
                Tag = "Ghost",
                Width = 30,
                Height = 30,
                Fill = new SolidColorBrush(color)
            };
            //Canvas.SetLeft(rectangleShape, left);
            //Canvas.SetTop(rectangleShape, top);
            X = left;
            Y = top;
        }

    }
}
