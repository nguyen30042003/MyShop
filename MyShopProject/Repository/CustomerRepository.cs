using MyShopProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyShopProject.Repository
{
    internal interface CustomerRepository
    {
        void create(Customer customer);
        void update(Customer customer);
        void delete(Customer customer);
        List<Customer> findAll();
        List<Customer> findByName(string name);
        Customer findById(int id);

    }
}
