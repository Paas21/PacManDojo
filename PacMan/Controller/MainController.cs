using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PacMan.Controller
{
    public class MainController : ControllerBase
    {
        WorldController _worldController = new();
        UserController _userController = new();

        private ControllerBase _selectedView;

        public ControllerBase SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                NotifyPropertyChanged();
            }
        }

        


        public MainController()
        {
            NavigateToUserCmd = new RelayCommand(NavigateToUser);
            NavigateToGameCmd = new RelayCommand(NavigateToGame);

            _userController.Users.Add(new Model.UserModel { FirstName = "toto", LastName = "tata" });
            _userController.Users.Add(new Model.UserModel { FirstName = "toto", LastName = "tata" });
            _userController.Users.Add(new Model.UserModel { FirstName = "toto", LastName = "tata" });
            _worldController.GameSetup();
            NavigateToUser();
        }

        private void NavigateToUser()
        {
            _worldController.Pause();
            SelectedView = _userController;
        }

        private void NavigateToGame()
        {
            _worldController.Resume();
            SelectedView = _worldController;
        }

        public ICommand NavigateToUserCmd { get; }
        public ICommand NavigateToGameCmd { get; }

    }
}
