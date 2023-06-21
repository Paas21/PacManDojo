using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PacMan.Controller
{
    public class GhostController : VisualObjectController
    {
        

        public GhostController(string name,Color color, double left, double top):base(color,left,top)
        {
            Name = name;
        }
    }
}
