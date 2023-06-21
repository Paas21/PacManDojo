using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PacMan.Controller
{
    public class PlayerController : VisualObjectController
    {
        public PlayerController(Color color, double left, double top) : base(color, left, top)
        {

        }

        public void TurnRight()
        {
            if (RectangleShape != null)
                RectangleShape.RenderTransform = new RotateTransform(0, RectangleShape.Width / 2, RectangleShape.Height / 2); // rotate the pac man image to face right
        }
        public void TurnLeft()
        {
            if (RectangleShape != null)
                RectangleShape.RenderTransform = new RotateTransform(-180, RectangleShape.Width / 2, RectangleShape.Height / 2); // rotate the pac man image to face left
        }

        public void TurnUp()
        {
            if (RectangleShape != null)
                RectangleShape.RenderTransform = new RotateTransform(-90, RectangleShape.Width / 2, RectangleShape.Height / 2); // rotate the pac man character to face up
        }
        public void TurnDown()
        {
            if (RectangleShape != null)
                RectangleShape.RenderTransform = new RotateTransform(90, RectangleShape.Width / 2, RectangleShape.Height / 2); // rotate the pac man character to face down
        }
    }
}
