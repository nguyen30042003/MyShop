using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MyShopProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopProject.Repository
{
    public class ExcelDataProcessor
    {

        private DataProvider dataProvider;

        public ExcelDataProcessor()
        {
            this.dataProvider = DataProvider.Instance; // Replace with your actual DataProvider instantiation.
        }

        public void ImportDataFromExcel()
        {
            string filename =  "C:\\HCMUS\\Nam 3\\C#\\ProjectMyShop\\test.xlsx";
            var document = SpreadsheetDocument.Open(filename, false);
            var wbPart = document.WorkbookPart;
            var sheet = wbPart.Workbook.Descendants<Sheet>().FirstOrDefault();

            if (sheet != null)
            {
                var worksheetPart = (WorksheetPart)wbPart.GetPartById(sheet.Id);
                var sheetData = worksheetPart.Worksheet.Elements<SheetData>().FirstOrDefault();

                foreach (var row in sheetData.Elements<Row>())
                {
                    // Assuming the columns are in order: ID, FullName, DOB, Email, Gender, Phone
                    var cells = row.Elements<Cell>().ToList();

                    int id = int.Parse(cells[0].InnerText);
                    string fullName = cells[2].InnerText;
                    string dobString = cells[2].InnerText;
                    DateTime dob = new DateTime(2022, 3, 13, 15, 30, 0);
                    string email = cells[3].InnerText;
                    string gender = cells[4].InnerText;
                    string phone = cells[5].InnerText;
                    string avatar = cells[6].InnerText;
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
