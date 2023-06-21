using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Model
{
    public class UserModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override string ToString()
        {
            return $"{LastName} {FirstName}";
        }
    }
}
