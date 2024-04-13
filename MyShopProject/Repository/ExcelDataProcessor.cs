using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelDataReader;
using MyShopProject.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
        public void ImportDataFromExcel()
        {
            /*OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png|All files (*.*)|*.*";
            openFileDialog.ShowDialog();*/
            //string filename = openFileDialog.FileName;
            /* var document = SpreadsheetDocument.Open("C:\\HCMUS\\Nam 3\\C#\\nguyen\\test.xlsx", false);
             var wbPart = document.WorkbookPart;
             var sheet = wbPart.Workbook.Descendants<Sheet>().FirstOrDefault();*/
            //Encoding RegisterProvider(CodePagesEncodingProvider.Instance);
            bool firstRow = true;
            using (var stream = System.IO.File.Open("C:\\HCMUS\\Nam 3\\C#\\nguyen\\t.xlsx", FileMode.Open, FileAccess.Read))
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
                                Customer customer = new Customer();
                                customer.ID = 0;
                                customer.Full_Name = reader.GetValue(1).ToString();
                                customer.Email = reader.GetValue(2).ToString();
                                string dob = reader.GetValue(3).ToString();
                                customer.DOB = new DateTime(2022, 3, 13, 15, 30, 0);
                                customer.Gender = reader.GetValue(4).ToString();
                                customer.Phone = reader.GetValue(5).ToString();
                                customer.Avatar = reader.GetValue(6).ToString();
                                SaveCustomerToDatabase(customer);
                            }
                        }
                    } while (reader.NextResult());
                }
            }

            /*if (sheet != null)
            {
                var worksheetPart = (WorksheetPart)wbPart.GetPartById(sheet.Id);
                var sheetData = worksheetPart.Worksheet.Elements<SheetData>().FirstOrDefault();

                foreach (var row in sheetData.Elements<Row>())
                {
                    var cellValues = row.Elements<Cell>()
                        .Select(cell =>
                        {
                            string value = cell.InnerText;

                            // If the cell has a value, return it; otherwise, return an empty string
                            return value != null ? value : "";
                        })
                        .ToList();

                    // Lấy ra các giá trị của các ô trong hàng và tách chúng bằng dấu tab ("\t")
                    var cellTexts = string.Join("\t", cellValues).Split('\t');

                    // Giả sử cột ID ở vị trí 0, Full_Name ở vị trí 1, DOB ở vị trí 2, và cứ tiếp tục...
                    int id = int.Parse(cellTexts[0]);
                    string fullName = cellTexts[1];
                    string dobString = cellTexts[2];
                    DateTime dob = new DateTime(2022, 3, 13, 15, 30, 0);
                    //DateTime dob = DateTime.Parse(cellTexts[2]);
                    string email = cellTexts[3];
                    string gender = cellTexts[4];
                    string phone = cellTexts[5];
                    string avatar = cellTexts[6];

                    Customer customer = new Customer
                    {
                        ID = id,
                        Full_Name = fullName,
                        DOB = dob,
                        Email = email,
                        Gender = gender,
                        Phone = phone,
                        Avatar = avatar
                    };

                    SaveCustomerToDatabase(customer);
                }
            }*/
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
