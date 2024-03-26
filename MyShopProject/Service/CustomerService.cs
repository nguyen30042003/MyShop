using MyShopProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopProject.Service
{
    internal interface CustomerService
    {
        bool save(Customer customer);
        bool update(Customer customer);
        List<Customer> findAll();
        List<Customer> findByName(string name);
        Customer findById(int id);
        bool delete(Customer customer);


    }
}
