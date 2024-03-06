using MyShop.DB;
using MyShop.Model;
using MyShop.Pages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyShop.Repository
{
    public class UserRepositoryImpl : UserRepository
    {
        private ConnectionToDB ConnectionToDB;
        public void FindById(int id)
        {
            
            throw new NotImplementedException();
        }

        public void FindByName(string name)
        {
            throw new NotImplementedException();
        }

        void UserRepository.Delete(User o)
        {
            throw new NotImplementedException();
        }

        List<User> UserRepository.FindAll()
        {
            throw new NotImplementedException();
        }

        void UserRepository.Insert(User o)
        {
            throw new NotImplementedException();
        }

        void UserRepository.Update(User o)
        {
            throw new NotImplementedException();
        }

        private static UserRepositoryImpl INSTANCE;
        private UserRepositoryImpl()
        {

        }


        public static UserRepositoryImpl getInstance()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new UserRepositoryImpl();

            }
            return INSTANCE;
        }
    }
}
