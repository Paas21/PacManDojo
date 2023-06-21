using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PacMan.Model;
using System.Windows.Input;

namespace PacMan.Controller
{
    public class UserController : ControllerBase
    {
        private ObservableCollection<UserModel> _users;

        public ObservableCollection<UserModel> Users
        {
            get { return _users; }
            set { _users = value; NotifyPropertyChanged(); }
        }

        private UserModel _userSelected;

        public UserController()
        {
            _users = new ObservableCollection<UserModel>();
        }

        public UserModel UserSelected
        {
            get { return _userSelected; }
            set { _userSelected = value; NotifyPropertyChanged(); }
        }

        private RelayCommand submitCmd;
        public ICommand SubmitCmd => submitCmd ??= new RelayCommand(PerformSubmitCmd);

        private void PerformSubmitCmd()
        {
            _userSelected = new UserModel() { FirstName="toto new", LastName="toto new"};
            _users.Add(_userSelected);

        }
    }
}
