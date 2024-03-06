using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    class User
    {
        private int ID {  get; set; }
        private string UserName { get; set; }
        private string Password { get; set; }
        private string Email { get; set; }
        private String Fullname { get; set; }
        private DateTime DateOfBirth { get; set; }

        private string Avatar {  get; set; }
    }
}
