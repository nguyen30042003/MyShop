using MyShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Repository
{
    internal interface UserRepository
    {
        void Insert(User o);
        void Update(User o);

        void Delete(User o);
        void FindById(int id);
        void FindByName(string name);
        List<User> FindAll();
    }
}
