using MyShopProject.Model;
using MyShopProject.Repository;
using MyShopProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopProject.ServiceImpl
{
    public class CustomerServiceImpl : CustomerService
    {
        private static CustomerServiceImpl instance;
        public static CustomerServiceImpl Instance
        {
            get
            {
                if (instance == null)
                    instance = new CustomerServiceImpl();
                return instance;
            }
            set { instance = value; }
        }
        public bool delete(Customer customer)
        {
            if(ICustomerRepository.Instance.delete(customer))
            {
                return true;
            }    
            return false;
        }

        public List<Customer> findAll()
        {
            List<Customer> customers = new List<Customer>();
            customers = ICustomerRepository.Instance.findAll();
            return customers;
        }

        public Customer findById(int id)
        {
            Customer customer = new Customer(); 
            customer = ICustomerRepository.Instance.findById(id);   
            return customer;
        }

        public List<Customer> findByName(string name)
        {
            List<Customer> customers = new List<Customer>();
            customers = ICustomerRepository.Instance.findByName(name);
            return customers;
        }

        public bool save(Customer customer)
        {
            if (ICustomerRepository.Instance.create(customer))
            {
                return true;
            }
            return false;
        }

        public bool update(Customer customer)
        {
            if (ICustomerRepository.Instance.update(customer))
            {
                return true;
            }
            return false;
        }

        public Tuple<List<Customer>, int> findAllPage(DateTime start, DateTime end, int skip, int take, string name = "", int sortOption = 1)
        {
            var customers = ICustomerRepository.Instance.findAll()
                .Where(p => (p.DOB <= end && p.DOB >= start)
                    && p.Full_Name.ToLower().Contains(name.ToLower()));
            int count = customers.Count();
            if (sortOption == 0)
                return Tuple.Create(customers.OrderBy(p => p.DOB).Skip(skip).Take(take).ToList(), count);
            else
                return Tuple.Create(customers.OrderByDescending(p => p.DOB).Skip(skip).Take(take).ToList(), count);


        }

    }
}
