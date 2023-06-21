using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace PacMan.Controller
{
    public partial class WorldController : ControllerBase
    {
        DispatcherTimer gameTimer = new DispatcherTimer(); // create a new instance of the dispatcher timer called game timer
        bool goLeft, goRight, goDown, goUp; // 4 boolean created to move player in 4 direction
        bool noLeft, noRight, noDown, noUp; // 4 more boolean created to stop player moving in that direction
        int speed = 8; // player speed
        Rect pacmanHitBox; // player hit box, this will be used to check for collision between player to walls, ghost and coints
        int ghostSpeed = 10; // ghost image speed
        int ghostMoveStep = 160; // ghost step limits
        int currentGhostStep; // current movement limit for the ghosts
        int score = 0; // score keeping integer

        private PlayerController? player;
        private GhostController? redGhost;
        private GhostController? orangeGhost;
        private GhostController? pinkGhost;

        private List<VisualObjectController> interactObjects;

        public List<VisualObjectController> InteractObjects
        {
            get { return interactObjects; }
            set
            {
                interactObjects = value;
                NotifyPropertyChanged();
            }
        }


        private string? scoreTxt = "Player 1 : 0";

        public string? ScoreTxt
        {
            get { return scoreTxt; }
            set
            {
                scoreTxt = value;
                NotifyPropertyChanged();
            }
        }

        public PlayerController? Player
        {
            get { return player; }
            set
            {
                player = value;
                NotifyPropertyChanged();
            }
        }


        public WorldController()
        {
            CanvasKeyDownCmd = new RelayCommand<KeyEventArgs>(e => CanvasKeyDown(e));
            player = new PlayerController(Colors.Yellow, 50, 104);
            redGhost = new GhostController("redGuy",Colors.Red, 173, 29);
            orangeGhost = new GhostController("orangeGuy",Colors.Orange, 651, 104);
            pinkGhost = new GhostController("pinkGuy", Colors.Pink, 173, 404);
            interactObjects = new List<VisualObjectController> {player, redGhost, orangeGhost, pinkGhost };
            AddInteractObjects();
        }

        private void AddInteractObjects()
        {
            interactObjects.Add(new WallController(Colors.Transparent, 660, 20, 20, 0));
            interactObjects.Add(new WallController(Colors.Transparent, 20, 770, 20, 0));
            interactObjects.Add(new WallController(Colors.Transparent, 20, 770, 20, 640));
            interactObjects.Add(new WallController(Colors.Transparent, 660, 20, 770, 0));
            interactObjects.Add(new WallController(Colors.Transparent, 20, 578, 142, 70));
            interactObjects.Add(new WallController(Colors.Transparent, 20, 402, 231, 159));
            interactObjects.Add(new WallController(Colors.Transparent, 20, 124, 142, 339));
            interactObjects.Add(new WallController(Colors.Transparent, 20, 124, 142, 467));
            interactObjects.Add(new WallController(Colors.Transparent, 20, 124, 596, 467));
            interactObjects.Add(new WallController(Colors.Transparent, 20, 124, 596, 339));
            interactObjects.Add(new WallController(Colors.Transparent, 187, 20, 142, 155));
            interactObjects.Add(new WallController(Colors.Transparent, 187, 20, 700, 155));
            interactObjects.Add(new WallController(Colors.Transparent, 109, 20, 613, 178));
            interactObjects.Add(new WallController(Colors.Transparent, 109, 20, 231, 178));
            interactObjects.Add(new WallController(Colors.Transparent, 82, 20, 142, 485));
            interactObjects.Add(new WallController(Colors.Transparent, 82, 20, 700, 485));
            interactObjects.Add(new WallController(Colors.Transparent, 82, 76, 395, 487));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 204, 114));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 160, 114));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 278, 114));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 241, 114));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 378, 114));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 325, 114));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 482, 114));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 421, 114));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 266, 248));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 266, 200));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 298, 248));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 298, 200));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 325, 248));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 325, 200));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 357, 248));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 357, 200));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 383, 248));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 383, 200));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 410, 248));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 410, 200));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 437, 248));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 437, 200));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 469, 248));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 469, 200));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 497, 248));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 497, 200));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 529, 248));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 529, 200));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 556, 248));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 556, 200));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 588, 248));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 588, 200));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 187, 29));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 143, 29));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 261, 29));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 224, 29));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 361, 29));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 308, 29));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 465, 29));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 404, 29));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 557, 29));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 520, 29));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 657, 29));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 604, 29));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 700, 29));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 530, 114));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 634, 114));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 574, 114));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 187, 404));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 143, 404));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 261, 404));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 224, 404));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 361, 404));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 308, 404));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 465, 404));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 404, 404));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 557, 404));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 520, 404));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 657, 404));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 604, 404));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 700, 404));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 504, 544));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 504, 510));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 531, 544));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 531, 510));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 563, 544));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 563, 510));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 592, 544));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 592, 510));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 619, 544));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 619, 510));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 651, 544));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 651, 510));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 204, 544));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 204, 510));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 230, 544));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 230, 510));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 262, 544));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 262, 510));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 292, 544));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 292, 510));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 318, 544));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 318, 510));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 350, 544));
            interactObjects.Add(new CoinController(Colors.Gold, 20, 20, 350, 510));
        }

        public ICommand CanvasKeyDownCmd { get; }

        private void CanvasKeyDown(KeyEventArgs e)
        {
            // this is the key down event
            if (e.Key == Key.Left && noLeft == false)
            {
                // if the left key is down and the boolean noLeft is set to false
                goRight = goUp = goDown = false; // set rest of the direction booleans to false
                noRight = noUp = noDown = false; // set rest of the restriction boolean to false
                goLeft = true; // set go left true
                player?.TurnLeft();
            }
            if (e.Key == Key.Right && noRight == false)
            {
                // if the right key pressed and no right boolean is false
                noLeft = noUp = noDown = false; // set rest of the direction boolean to false
                goLeft = goUp = goDown = false; // set rest of the restriction boolean to false
                goRight = true; // set go right to true
                player?.TurnRight();
            }
            if (e.Key == Key.Up && noUp == false)
            {
                // if the up key is pressed and no up is set to false
                noRight = noDown = noLeft = false; // set rest of the direction boolean to false
                goRight = goDown = goLeft = false; // set rest of the restriction boolean to false
                goUp = true; // set go up to true
                player?.TurnUp();
            }
            if (e.Key == Key.Down && noDown == false)
            {
                // if the down key is press and the no down boolean is false
                noUp = noLeft = noRight = false; // set rest of the direction boolean to false
                goUp = goLeft = goRight = false; // set rest of the restriction boolean to false
                goDown = true; // set go down to true
                player?.TurnDown();
            }
        }

        public void GameSetup()
        {
            // this function will run when the program loads
            gameTimer.Tick += GameLoop; // link the game loop event to the time tick
            gameTimer.Interval = TimeSpan.FromMilliseconds(20); // set time to tick every 20 milliseconds
            //gameTimer.Start(); // start the time
            currentGhostStep = ghostMoveStep; // set current ghost step to the ghost move step
            // below pac man and the ghosts images are being imported from the images folder and then we are assigning the image brush to the rectangles
            ImageBrush pacmanImage = new ImageBrush();
            pacmanImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pacman.jpg"));
            player.RectangleShape.Fill = pacmanImage;

            ImageBrush redGhostImg = new ImageBrush();
            redGhostImg.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/red.jpg"));
            redGhost.RectangleShape.Fill = redGhostImg;
            ImageBrush orangeGhostImg = new ImageBrush();
            orangeGhostImg.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/orange.jpg"));
            orangeGhost.RectangleShape.Fill = orangeGhostImg;
            ImageBrush pinkGhostImg = new ImageBrush();
            pinkGhostImg.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink.jpg"));
            pinkGhost.RectangleShape.Fill = pinkGhostImg;

        }
        private void GameLoop(object sender, EventArgs e)
        {
            // this is the game loop event, this event will control all of the movements, outcome, collision and score for the game
            ScoreTxt = $"Player 1 : {score}" ; // show the scoreo to the txtscore label. 
            // start moving the character in the movement directions
            if (goRight)
            {
                // if go right boolean is true then move pac man to the right direction by adding the speed to the left 
                player.X += speed;
            }
            if (goLeft)
            {
                // if go left boolean is then move pac man to the left direction by deducting the speed from the left
                player.X -= speed;
            }
            if (goUp)
            {
                // if go up boolean is true then deduct the speed integer from the top position of the pac man
                player.Y -= speed;
            }
            if (goDown)
            {
                // if go down boolean is true then add speed integer value to the pac man top position
                player.Y += speed;
            }
            // end the movement 
            // restrict the movement
            if (goDown && player?.Y + 80 > Application.Current.MainWindow.Height)
            {
                // if pac man is moving down the position of pac man is grater than the main window height then stop down movement
                noDown = true;
                goDown = false;
            }
            if (goUp && player?.Y < 1)
            {
                // is pac man is moving and position of pac man is less than 1 then stop up movement
                noUp = true;
                goUp = false;
            }
            if (goLeft && player?.X - 10 < 1)
            {
                // if pac man is moving left and pac man position is less than 1 then stop moving left
                noLeft = true;
                goLeft = false;
            }
            if (goRight && player?.X + 70 > Application.Current.MainWindow.Width)
            {
                // if pac man is moving right and pac man position is greater than the main window then stop moving right
                noRight = true;
                goRight = false;
            }
            pacmanHitBox = new Rect(player.X, player.Y, player.RectangleShape.Width, player.RectangleShape.Height); // asssign the pac man hit box to the pac man rectangle
            // below is the main game loop that will scan through all of the rectangles available inside of the game
            foreach (var obj in interactObjects)
            {
                // loop through all of the rectangles inside of the game and identify them using the x variable
                Rect hitBox = new Rect(obj.X, obj.Y, obj.RectangleShape.Width, obj.RectangleShape.Height); // create a new rect called hit box for all of the available rectangles inside of the game
                // find the walls, if any of the rectangles inside of the game has the tag wall inside of it
                if (obj is WallController)
                {
                    // check if we are colliding with the wall while moving left if true then stop the pac man movement
                    if (goLeft == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        player.X += 10;
                        noLeft = true;
                        goLeft = false;
                    }
                    // check if we are colliding with the wall while moving right if true then stop the pac man movement
                    if (goRight == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        player.X -= 10;
                        noRight = true;
                        goRight = false;
                    }
                    // check if we are colliding with the wall while moving down if true then stop the pac man movement
                    if (goDown == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        player.Y -= 10;
                        noDown = true;
                        goDown = false;
                    }
                    // check if we are colliding with the wall while moving up if true then stop the pac man movement
                    if (goUp == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        player.Y += 10;
                        noUp = true;
                        goUp = false;
                    }
                }
                // check if the any of the rectangles has a coin tag inside of them
                if (obj is CoinController)
                {
                    // if pac man collides with any of the coin and coin is still visible to the screen
                    if (pacmanHitBox.IntersectsWith(hitBox) && obj.RectangleShape.Visibility == Visibility.Visible)
                    {
                        // set the coin visiblity to hidden
                        obj.RectangleShape.Visibility = Visibility.Hidden;
                        // add 1 to the score
                        score++;
                    }
                }
                // if any rectangle has the tag ghost inside of it
                if (obj is GhostController)
                {
                    // check if pac man collides with the ghost 
                    if (pacmanHitBox.IntersectsWith(hitBox))
                    {
                        // if collision has happened, then end the game by calling the game over function and passing in the message
                        GameOver("Ghosts got you, click ok to play again");
                    }
                    // if there is a rectangle called orange guy in the game
                    if (obj.Name.ToString() == "orangeGuy")
                    {
                        // move that rectangle to towards the left of the screen
                        obj.X -= ghostSpeed;
                        //Canvas.SetLeft(obj.RectangleShape, Canvas.GetLeft(obj.RectangleShape) - ghostSpeed);
                    }
                    else
                    {
                        obj.X += ghostSpeed;
                        // other ones can move towards the right of the screen
                        //Canvas.SetLeft(obj.RectangleShape, Canvas.GetLeft(obj.RectangleShape) + ghostSpeed);
                    }
                    // reduce one from the current ghost step integer
                    currentGhostStep--;
                    // if the current ghost step integer goes below 1 
                    if (currentGhostStep < 1)
                    {
                        // reset the current ghost step to the ghost move step value
                        currentGhostStep = ghostMoveStep;
                        // reverse the ghost speed integer
                        ghostSpeed = -ghostSpeed;
                    }
                }
            }
            // if the player collected 85 coins in the game
            if (score == 85)
            {
                // show game over function with the you win message
                GameOver("You Win, you collected all of the coins");
            }
        }
        private void GameOver(string message)
        {
            // inside the game over function we passing in a string to show the final message to the game
            gameTimer.Stop(); // stop the game timer
            MessageBox.Show(message, "The Pac Man Game WPF"); // show a mesage box with the message that is passed in this function
            // when the player clicks ok on the message box
            // restart the application
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        public void Pause()
        {
            gameTimer.Stop();
        }

        public void Resume()
        {
            gameTimer.Start();
        }
        
    }
}
