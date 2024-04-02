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
        bool create(Customer customer);
        bool update(Customer customer);
        bool delete(Customer customer);
        List<Customer> findAll();
        List<Customer> findByName(string name);
        Customer findById(int id);

        List<Customer> findPage(int skipCount, int takeCount);
        List<Customer> findPageByName(int skipCount, int takeCount);
    }
}
