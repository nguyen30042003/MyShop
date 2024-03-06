using MyShop.DB;
using MyShop.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Repository
{
    internal class CategoryRepositoryImpl : CategoryRepository
    {
        public bool Delete(Category category)
        {
            int rowsAffected = 0;
            SqlConnection connection = ConnectionToDB.Instance.GetConnection();
            string query = "DELETE FROM category WHERE ID = @Id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", category.Id);

                rowsAffected = command.ExecuteNonQuery();

                Console.WriteLine($"{rowsAffected} row(s) deleted successfully.");
            }

            return rowsAffected > 0;
        }
            
        public List<Category> FindAll()
        {
            List<Category> categories = new List<Category>();
            SqlConnection connection = ConnectionToDB.Instance.GetConnection();
            string query = "SELECT * FROM category";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int Id = Convert.ToInt32(reader["Id"]);
                        string Name = reader["Name"].ToString();
                        Category category = new Category(Id, Name);

                        categories.Add(category);
                    }
                }
            }

            return categories;
        }

        public Category FindById(int id)
        {
            Category category = null;
            SqlConnection connection = ConnectionToDB.Instance.GetConnection();
            string query = "SELECT * FROM category WHERE Id = @Id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int Id = Convert.ToInt32(reader["Id"]);
                        string Name = reader["Name"].ToString();
                        category = new Category(Id, Name);
                    }
                }
            }

            return category;
        }


        public Category FindByName(string name)
        {
            Category category = null;
            SqlConnection connection = ConnectionToDB.Instance.GetConnection();
            string query = "SELECT * FROM category WHERE Name = @Name";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", name);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int Id = Convert.ToInt32(reader["Id"]);
                        string Name = reader["Name"].ToString();
                        category = new Category(Id, Name);
                    }
                }
            }

            return category;
        }

        public bool Insert(string name)
        {
            int rowsAffected = 0;
            SqlConnection connection = ConnectionToDB.Instance.GetConnection();
            string query = "INSERT INTO category (name) VALUES (@Name)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {

                command.Parameters.AddWithValue("@Name", name);

                rowsAffected = command.ExecuteNonQuery();

                Console.WriteLine($"{rowsAffected} row(s) inserted successfully.");
            }
            if(rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        public bool Update(Category category)
        {
            int rowsAffected = 0;
            SqlConnection connection = ConnectionToDB.Instance.GetConnection();
            string query = "UPDATE category SET Name = @Name WHERE Id = @Id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", category.Id);
                command.Parameters.AddWithValue("@Name", category.Name);

                rowsAffected = command.ExecuteNonQuery();

                Console.WriteLine($"{rowsAffected} row(s) updated successfully.");
            }

            return rowsAffected > 0;
        }
    }
}
