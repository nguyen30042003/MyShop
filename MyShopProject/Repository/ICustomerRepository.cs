using DocumentFormat.OpenXml.Wordprocessing;
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
        public bool create(Customer customer)
        {
            if (customer != null)
            {
                try
                {
                    DataProvider.Instance.DB.Customers.Add(customer);
                    DataProvider.Instance.DB.SaveChanges();
                    return true; // Thành công
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error while creating customer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false; // Thất bại
                }
            }
            return false; // Thất bại
        }

        public bool delete(Customer customer)
        {
            Customer c = findById(customer.ID);
            if (c != null)
            {
                try
                {
                    DataProvider.Instance.DB.Customers.Remove(c);
                    DataProvider.Instance.DB.SaveChanges();
                    return true; // Thành công
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error while deleting customer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false; // Thất bại
                }
            }
            return false; // Thất bại
        }

        public List<Customer> findAll()
        {
            return DataProvider.Instance.DB.Customers.ToList();
        }



        public List<Customer> findByName(string name)
        {
            return DataProvider.Instance.DB.Customers.Where(c=>c.Full_Name.Contains(name)).ToList();
        }

        public bool update(Customer customer)
        {
            Customer c = findById(customer.ID);
            if (c != null)
            {
                try
                {
                    c.Full_Name = customer.Full_Name;
                    c.Phone = customer.Phone;
                    c.Email = customer.Email;
                    c.DOB = customer.DOB;
                    c.Gender = customer.Gender;
                    c.Avatar = customer.Avatar;
                    DataProvider.Instance.DB.SaveChanges();
                    return true; // Thành công
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error while updating customer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false; // Thất bại
                }
            }
            return false; // Thất bại
        }

        public Customer findById(int id)
        {
            Customer customer = DataProvider.Instance.DB.Customers.FirstOrDefault(c => c.ID == id);
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

        public List<Customer> findPage(int skipCount, int takeCount)
        {
            List<Customer> customers = DataProvider.Instance.DB.Customers.OrderBy(c => c.ID).Skip(skipCount).Take(takeCount).ToList();
            return customers;
        }

        public List<Customer> findPageByName(int skipCount, int takeCount, string name)
        {
            List<Customer> customers = DataProvider.Instance.DB.Customers.OrderBy(c => c.Full_Name.Contains(name) && c.Gender.Contains("Male")).Skip(skipCount).Take(takeCount).ToList();
            return customers;
        }

        public List<Customer> findPageByName(int skipCount, int takeCount)
        {
            throw new NotImplementedException();
        }
    }
}
