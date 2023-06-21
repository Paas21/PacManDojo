using PacManServer.Model;

namespace PacManServer.Controlles
{
    public class CharctersController
    {
        public Sprite packman;
        public CharctersController()
        {
            this.packman = new Sprite(CharacterType.Player,"yellow", 50, 104, 30,30);
        }

    }
}
