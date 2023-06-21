using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PacMan.Controller
{
    public class CoinController : VisualObjectController
    {

        public CoinController(Color color, double height, double width, double left, double top) : base(color, left, top)
        {
            ImageBrush img = new ImageBrush();
            img.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/kebab.png"));
            RectangleShape.Fill = img;
        }
    }
}
