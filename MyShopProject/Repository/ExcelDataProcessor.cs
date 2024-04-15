using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelDataReader;
using MyShopProject.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace MyShopProject.Repository
{
    public class ExcelDataProcessor
    {

        private DataProvider dataProvider;

        public ExcelDataProcessor()
        {
            this.dataProvider = DataProvider.Instance; 
        }
        
        public void ImportDataFromExcel(string filename)
        {
           
            bool firstRow = true;
            using (var stream = System.IO.File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        while (reader.Read())
                        {
                            if (firstRow)
                            {
                                firstRow = false;
                                continue; 
                            }
                            bool hasValues = false;
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (!reader.IsDBNull(i))
                                {
                                    hasValues = true;
                                    break;
                                }
                            }

                            if (hasValues)
                            {
                                string excelDateFormat = "dd/MM/yyyy";

                                // Chuyển đổi chuỗi ngày tháng từ Excel thành DateTime
                                DateTime dob;
                                if (DateTime.TryParseExact(reader.GetValue(3).ToString(), excelDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
                                {
                                    // Tạo đối tượng Customer và thiết lập các thuộc tính
                                    Customer customer = new Customer()
                                    {
                                        ID = 0,
                                        Full_Name = reader.GetValue(1).ToString(),
                                        Email = reader.GetValue(2).ToString(),
                                        DOB = dob.Date,
                                        Gender = reader.GetValue(4).ToString(),
                                        Phone = reader.GetValue(5).ToString(),
                                        Avatar = reader.GetValue(6).ToString()
                                    };

                                    // Lưu Customer vào database
                                    SaveCustomerToDatabase(customer);
                                }
                                else
                                {
                                    // Xử lý khi không thể chuyển đổi ngày tháng từ chuỗi Excel
                                    // Ví dụ: Hiển thị thông báo lỗi cho người dùng hoặc ghi log
                                    System.Windows.MessageBox.Show("Error: Invalid date format in Excel.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                    } while (reader.NextResult());
                }
            }

            
        }
        public void ImportDataProductFromExcel(string filename)
        {
            bool firstRow = true;
            using (var stream = System.IO.File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        while (reader.Read())
                        {
                            if (firstRow)
                            {
                                firstRow = false;
                                continue;
                            }
                            bool hasValues = false;
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (!reader.IsDBNull(i))
                                {
                                    hasValues = true;
                                    break;
                                }
                            }

                            if (hasValues)
                            {
                                Product product = new Product();
                                product.ID = 0;
                                product.Name = reader.GetValue(1).ToString();
                                product.CreateDate = DateTime.Today;
                                product.IDCategory = null;
                                product.PriceImport = double.Parse(reader.GetValue(2).ToString());
                                product.PriceSale = double.Parse(reader.GetValue(3).ToString());
                                product.Discount = int.Parse(reader.GetValue(4).ToString());
                                product.Description = reader.GetValue(5).ToString();    
                                product.Image = reader.GetValue(6).ToString();
                                product.Quantity = int.Parse(reader.GetValue(7).ToString());
                                Customer customer = new Customer();
                                IProductRepository.Instance.create(product);
                            }
                        }
                    } while (reader.NextResult());
                }
            }
        }
        private void SaveCustomerToDatabase(Customer customer)
        {
            if (customer != null)
            {
                dataProvider.DB.Customers.Add(customer);
                dataProvider.DB.SaveChanges();
            }
        }
    }
}
