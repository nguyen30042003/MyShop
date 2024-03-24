using MyShopProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyShopProject.Repository
{
    public class ICustomerRepository : CustomerRepository
    {

        private static ICustomerRepository instance;
        public static ICustomerRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new ICustomerRepository();
                return instance;
            }
            set { instance = value; }
        }


        public ICustomerRepository() { }
        public void create(Customer customer)
        {
            if (customer != null)
            {
                DataProvider.Instance.DB.Customer.Add(customer);
                DataProvider.Instance.DB.SaveChanges();
                /*dataProvider.DB.Customers.Add(customer);
                 dataProvider.DB.SaveChanges();*/
            }
        }

        public void delete(Customer customer)
        {
            Customer c = findById(customer.ID);
            if(c != null)
            {
                DataProvider.Instance.DB.Customer.Remove(c);
                DataProvider.Instance.DB.SaveChanges();
            }
        }

        public List<Customer> findAll()
        {
            return DataProvider.Instance.DB.Customer.ToList();
        }



        public List<Customer> findByName(string name)
        {
            return DataProvider.Instance.DB.Customer.Where(c=>c.Full_Name.Contains(name)).ToList();
        }

        public void update(Customer customer)
        {
            Customer c = findById(customer.ID);
            if (c != null)
            {
                c.Full_Name = customer.Full_Name;
                c.Phone = customer.Phone;
                c.Email = customer.Email;
                c.DOB = customer.DOB;
                c.Gender = customer.Gender;
                c.Avatar = customer.Avatar;
                DataProvider.Instance.DB.SaveChanges();
            }
        }

        public Customer findById(int id)
        {
            Customer customer = DataProvider.Instance.DB.Customer.FirstOrDefault(c => c.ID == id);
            if (customer != null)
            {
                Console.WriteLine(customer.Full_Name);
            }
            else
            {

                Console.WriteLine($"Customer with ID {id} not found.");
            }
            return customer;
        }


    }
}
