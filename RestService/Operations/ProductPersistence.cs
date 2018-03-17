using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using RestService.Models;

namespace RestService.Operations
{
    public  class DatabaseConn
    {
        private static MySqlConnection conn;

        public static MySqlConnection CreateConnection()
        {
            conn=new MySqlConnection(ConfigurationManager.ConnectionStrings["productConnectionString"].ConnectionString);
            conn.Open();
            return conn;
        }

        public static void CloseConnetion()
        {
            conn.Close();
        }
    }
    public class ProductPersistence
    {
        private MySqlConnection conn;

        public int CreateProduct(Product product)
        {
            var query =
                $"INSERT INTO tableproduct (Name, Price, Category) VALUES ('{product.Name}',{product.Price},'{product.Category}')";

            try
            {
                conn = DatabaseConn.CreateConnection();
                var cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.LastInsertedId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Product> GetAllProducts()
        {
            var query = "SELECT * FROM tableproduct";
            try
            {
                conn = DatabaseConn.CreateConnection();
                var cmd=new MySqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                var productLst = new List<Product>();
                while (reader.Read())
                {
                    var product = new Product()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDouble(2),
                        Category = reader.GetString(3)
                    };
                    productLst.Add(product);
                }

                return productLst;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Product GetProdut(int id)
        {
            var query = $"SELECT * FROM tableproduct WHERE Id={id}";
            try
            {
                conn = DatabaseConn.CreateConnection();
                var cmd = new MySqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    var product = new Product()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDouble(2),
                        Category = reader.GetString(3)
                    };
                    return product;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool UpdateProduct(int id, Product product)
        {
            var query = $"SELECT * FROM tableproduct WHERE Id={id}";
            try
            {
                conn = DatabaseConn.CreateConnection();
                var cmd = new MySqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    query =
                        $"UPDATE tableproduct SET Name='{product.Name}', Price='{product.Price}', Category='{product.Category}' WHERE Id={id}";
                    var cmd2 = new MySqlCommand(query, conn);
                    cmd2.ExecuteNonQuery();
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool DeleteProduct(int id)
        {
            var query = $"SELECT * FROM tableproduct WHERE Id={id}";
            try
            {
                conn = DatabaseConn.CreateConnection();
                var cmd = new MySqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    query = $"DELETE FROM tableproduct WHERE Id={id}";
                    var cmd2=new MySqlCommand(query, conn);
                    cmd2.ExecuteNonQuery();
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}