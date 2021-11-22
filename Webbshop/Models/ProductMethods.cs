using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Webbshop.Models
{
    public class ProductMethods
    {

        // SELECT
        // Select all products
        public List<ProductDetail> SelectAllProducts(out string errormsg)
        {
            // DB-connect
            // Create SQL-connection
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);



            // SQL-query
            // Create SQL-string
            String sqlstring = "SELECT * FROM [Products]";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);



            // Create adapter
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            // Create new list
            List<ProductDetail> ProductList = new List<ProductDetail>();



            // Execute SQL-query
            try
            {
                // Open DB-connection
                dbConnection.Open();

                // Fill dataset with data in a table
                myAdapter.Fill(myDS, "myProduct");

                // Create a counter for amount of rows
                int i = 0;
                int count = 0;
                count = myDS.Tables["myProduct"].Rows.Count;

                // If there are more than 0 rows in the DataSet
                if (count > 0)
                {
                    // Loop through entire DataSet
                    while (i < count)
                    {
                        // Read data from DataSet and store in object
                        ProductDetail pd = new ProductDetail
                        {
                            ProductName = myDS.Tables["myProduct"].Rows[i]["ProductName"].ToString(),
                            ProductKeywords = myDS.Tables["myProduct"].Rows[i]["ProductKeywords"].ToString(),
                            ProductDescription = myDS.Tables["myProduct"].Rows[i]["ProductDescription"].ToString(),
                            ProductPrice = Convert.ToDecimal(myDS.Tables["myProduct"].Rows[i]["ProductPrice"]),
                            ProductImage = myDS.Tables["myProduct"].Rows[i]["ProductImage"] as byte[],
                            ProductImageDescription = myDS.Tables["myProduct"].Rows[i]["ProductImageDescription"].ToString(),
                            CategoryId = Convert.ToInt32(myDS.Tables["myProduct"].Rows[i]["CategoryId"]),
                            Id = Convert.ToInt32(myDS.Tables["myProduct"].Rows[i]["Id"])
                        };

                        // Iterate
                        i++;

                        // Add objects to list
                        ProductList.Add(pd);
                    }

                    // Send empty error-message
                    errormsg = "";

                    // Return list
                    return ProductList;
                }
                // If DataSet is empty
                else
                {
                    // Send error-message
                    errormsg = "Hittade inga produkter.";
                    return (null);
                }
            }
            // Catch errors
            catch (Exception e)
            {
                // Get error-message
                errormsg = e.Message;
                return null;
            }
            finally
            {
                // Close DB-connection
                dbConnection.Close();
            }
        }

        // Select single category
        public ProductDetail SelectSingleProduct(int id, out string errormsg)
        {
            // DB-connect
            // Create SQL-connection
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);



            // SQL-query
            // Create SQL-string
            string sqlstring = "SELECT * FROM [Products] WHERE Id = @id";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            // Add parameters
            dbCommand.Parameters.Add("id", System.Data.SqlDbType.Int).Value = id;



            // Create adapter
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();



            // Execute SQL-query
            try
            {
                // Open DB-connection
                dbConnection.Open();

                // Fill dataset with data in a table
                myAdapter.Fill(myDS, "myProduct");

                // Create a counter for amount of rows
                int i = 0;
                int count = 0;
                count = myDS.Tables["myProduct"].Rows.Count;

                // If there are more than 0 rows in the DataSet
                if (count > 0)
                {
                    // Read data from DataSet and store in object
                    ProductDetail pd = new ProductDetail
                    {
                        ProductName = myDS.Tables["myProduct"].Rows[i]["ProductName"].ToString(),
                        ProductKeywords = myDS.Tables["myProduct"].Rows[i]["ProductKeywords"].ToString(),
                        ProductDescription = myDS.Tables["myProduct"].Rows[i]["ProductDescription"].ToString(),
                        ProductPrice = Convert.ToDecimal(myDS.Tables["myProduct"].Rows[i]["ProductPrice"]),
                        ProductImage = myDS.Tables["myProduct"].Rows[i]["ProductImage"] as byte[],
                        ProductImageDescription = myDS.Tables["myProduct"].Rows[i]["ProductImageDescription"].ToString(),
                        CategoryId = Convert.ToInt32(myDS.Tables["myProduct"].Rows[i]["CategoryId"]),
                        Id = Convert.ToInt32(myDS.Tables["myProduct"].Rows[i]["Id"])
                    };

                    // Send empty error-message
                    errormsg = "";

                    // Return object
                    return pd;
                }
                // If DataSet is empty
                else
                {
                    // Send error-message
                    errormsg = "Hittade ingen produkt.";
                    return (null);
                }
            }
            // Catch errors
            catch (Exception e)
            {
                // Get error-message
                errormsg = e.Message;
                return null;
            }
            finally
            {
                // Close DB-connection
                dbConnection.Close();
            }
        }



        // INSERT
        // Insert new product
        public int InsertProduct(ProductDetail pd, out string errormsg)
        {
            // DB-connect
            // Create SQL-connection
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);



            // SQL-query
            // Create SQL-string
            string sqlstring = "INSERT INTO [Products] " +
                "(CategoryId, ProductName, ProductKeywords, ProductDescription, ProductPrice, ProductImage, ProductImageDescription) " +
                "VALUES (@categoryId, @productName, @productKeywords, @productDescription, @productPrice, @productImage, @productImageDescription)";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            // Add parameters
            dbCommand.Parameters.Add("categoryId", System.Data.SqlDbType.Int).Value = pd.CategoryId;
            dbCommand.Parameters.Add("productName", System.Data.SqlDbType.NVarChar, 120).Value = pd.ProductName;
            dbCommand.Parameters.Add("productKeywords", System.Data.SqlDbType.NVarChar, 256).Value = pd.ProductKeywords;
            dbCommand.Parameters.Add("productDescription", System.Data.SqlDbType.NVarChar, 8000).Value = pd.ProductDescription;
            dbCommand.Parameters.Add("productPrice", System.Data.SqlDbType.Decimal).Value = pd.ProductPrice;
            dbCommand.Parameters.Add("productImage", System.Data.SqlDbType.VarBinary).Value = pd.ProductImage;
            dbCommand.Parameters.Add("productImageDescription", System.Data.SqlDbType.NVarChar, 120).Value = pd.ProductImageDescription;



            // Execute SQL-query
            try
            {
                // Open DB-connection
                dbConnection.Open();

                // Execute SQL-query and store result in "i"
                int i = 0;
                i = dbCommand.ExecuteNonQuery();

                // If 1 or more rows were affected: 
                // don't display error-message
                if (i >= 1)
                {
                    errormsg = "";
                }
                // Else if no rows were affected: 
                // display error-message
                else
                {
                    errormsg = "Lagring misslyckades.";
                }

                return i;
            }
            // Catch errors
            catch (Exception e)
            {
                // Get error-message
                errormsg = e.Message;
                return 0;
            }
            finally
            {
                // Close DB-connection
                dbConnection.Close();
            }
        }



        // UPDATE
        // Update a product
        public int UpdateProduct(ProductDetail pd, out string errormsg)
        {
            // DB-connect
            // Create SQL-connection
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);



            // SQL-query
            // Create SQL-string
            String sqlstring = "UPDATE [Products] SET CategoryId = @categoryId, ProductName =  @productName, ProductKeywords = @productKeywords," +
                "ProductDescription = @productDescription, ProductPrice = @productPrice, ProductImage = @productImage," +
                "ProductImageDescription = @productImageDescription" +
                " WHERE Id = @id";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            // Add parameters
            dbCommand.Parameters.Add("categoryId", System.Data.SqlDbType.Int).Value = pd.CategoryId;
            dbCommand.Parameters.Add("productName", System.Data.SqlDbType.NVarChar, 120).Value = pd.ProductName;
            dbCommand.Parameters.Add("productKeywords", System.Data.SqlDbType.NVarChar, 256).Value = pd.ProductKeywords;
            dbCommand.Parameters.Add("productDescription", System.Data.SqlDbType.NVarChar, 8000).Value = pd.ProductDescription;
            dbCommand.Parameters.Add("productPrice", System.Data.SqlDbType.Decimal).Value = pd.ProductPrice;
            dbCommand.Parameters.Add("productImage", System.Data.SqlDbType.VarBinary).Value = pd.ProductImage;
            dbCommand.Parameters.Add("productImageDescription", System.Data.SqlDbType.NVarChar, 120).Value = pd.ProductImageDescription;
            dbCommand.Parameters.Add("id", System.Data.SqlDbType.Int).Value = pd.Id;



            // Execute SQL-query
            try
            {
                // Open DB-connection
                dbConnection.Open();

                // Execute SQL-query and store result in "i"
                int i = 0;
                i = dbCommand.ExecuteNonQuery();

                // If 1 or more rows were affected: 
                // don't display error-message
                if (i >= 1)
                {
                    errormsg = "";
                }
                // Else if no rows were affected: 
                // display error-message
                else
                {
                    errormsg = "Lagring misslyckades.";
                }

                return i;
            }
            // Catch errors
            catch (Exception e)
            {
                // Get error-message
                errormsg = e.Message;
                return 0;
            }
            finally
            {
                // Close DB-connection
                dbConnection.Close();
            }
        }



        // DELETE
        // Delete a product
        public int DeleteProduct(int id, out string errormsg)
        {
            // DB-connect
            // Create SQL-connection
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);



            // SQL-query
            // Create SQL-string
            String sqlstring = "DELETE FROM [Products] WHERE Id = @id";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            // Add parameters
            dbCommand.Parameters.Add("id", System.Data.SqlDbType.Int).Value = id;



            // Execute SQL-query
            try
            {
                // Open DB-connection
                dbConnection.Open();

                // Execute SQL-query and store result in "i"
                int i = 0;
                i = dbCommand.ExecuteNonQuery();

                // If 1 or more rows were affected: 
                // don't display error-message
                if (i >= 1)
                {
                    errormsg = "";
                }
                // Else if no rows were affected: 
                // display error-message
                else
                {
                    errormsg = "Radering misslyckades.";
                }

                return i;
            }
            // Catch errors
            catch (Exception e)
            {
                // Get error-message
                errormsg = e.Message;
                return 0;
            }
            finally
            {
                // Close DB-connection
                dbConnection.Close();
            }
        }



        // FILTERS
        // Select products by category
        public List<ProductDetail> SelectProductsByCategory(int categoryId, out string errormsg)
        {
            // DB-connect
            // Create SQL-connection
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);


            // SQL-query
            // Create SQL-string
            String sqlstring = "SELECT * FROM [Products] WHERE [CategoryId] = @categoryId";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            // Add parameter
            dbCommand.Parameters.Add("categoryId", SqlDbType.Int).Value = categoryId;



            // Create adapter
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            // Create new list
            List<ProductDetail> ProductList = new List<ProductDetail>();



            // Execute SQL-query
            try
            {
                // Open DB-connection
                dbConnection.Open();

                // Fill dataset with data in a table
                myAdapter.Fill(myDS, "myProduct");

                // Create a counter for amount of rows
                int i = 0;
                int count = 0;
                count = myDS.Tables["myProduct"].Rows.Count;

                // If there are more than 0 rows in the DataSet
                if (count > 0)
                {
                    // Loop through entire DataSet
                    while (i < count)
                    {
                        // Read data from DataSet and store in object
                        ProductDetail pd = new ProductDetail
                        {
                            ProductName = myDS.Tables["myProduct"].Rows[i]["ProductName"].ToString(),
                            ProductKeywords = myDS.Tables["myProduct"].Rows[i]["ProductKeywords"].ToString(),
                            ProductDescription = myDS.Tables["myProduct"].Rows[i]["ProductDescription"].ToString(),
                            ProductPrice = Convert.ToDecimal(myDS.Tables["myProduct"].Rows[i]["ProductPrice"]),
                            ProductImage = myDS.Tables["myProduct"].Rows[i]["ProductImage"] as byte[],
                            ProductImageDescription = myDS.Tables["myProduct"].Rows[i]["ProductImageDescription"].ToString(),
                            CategoryId = Convert.ToInt32(myDS.Tables["myProduct"].Rows[i]["CategoryId"]),
                            Id = Convert.ToInt32(myDS.Tables["myProduct"].Rows[i]["Id"])
                        };

                        // Iterate
                        i++;

                        // Add objects to list
                        ProductList.Add(pd);
                    }

                    // Send empty error-message
                    errormsg = "";

                    // Return list
                    return ProductList;
                }
                // If DataSet is empty
                else
                {
                    // Send error-message
                    errormsg = "Hittade inga produkter.";
                    return (null);
                }
            }
            // Catch errors
            catch (Exception e)
            {
                // Get error-message
                errormsg = e.Message;
                return null;
            }
            finally
            {
                // Close DB-connection
                dbConnection.Close();
            }
        }

        // Select products by name
        public List<ProductDetail> SelectProductsByName(string productName, out string errormsg)
        {
            // DB-connect
            // Create SQL-connection
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);



            // SQL-query
            // Create SQL-string
            string sqlstring = "SELECT * FROM [Products] WHERE [ProductName] LIKE '%'+@productName+'%'";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            // Add parameter
            dbCommand.Parameters.Add("productName", SqlDbType.NVarChar, 120).Value = productName;



            // Create adapter
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            // Create new list
            List<ProductDetail> ProductList = new List<ProductDetail>();



            // Execute SQL-query
            try
            {
                // Open DB-connection
                dbConnection.Open();

                // Fill dataset with data in a table
                myAdapter.Fill(myDS, "myProduct");

                // Create a counter for amount of rows
                int i = 0;
                int count = 0;
                count = myDS.Tables["myProduct"].Rows.Count;

                // If there are more than 0 rows in the DataSet
                if (count > 0)
                {
                    // Loop through entire DataSet
                    while (i < count)
                    {
                        // Read data from DataSet and store in object
                        ProductDetail pd = new ProductDetail
                        {
                            ProductName = myDS.Tables["myProduct"].Rows[i]["ProductName"].ToString(),
                            ProductKeywords = myDS.Tables["myProduct"].Rows[i]["ProductKeywords"].ToString(),
                            ProductDescription = myDS.Tables["myProduct"].Rows[i]["ProductDescription"].ToString(),
                            ProductPrice = Convert.ToDecimal(myDS.Tables["myProduct"].Rows[i]["ProductPrice"]),
                            ProductImage = myDS.Tables["myProduct"].Rows[i]["ProductImage"] as byte[],
                            ProductImageDescription = myDS.Tables["myProduct"].Rows[i]["ProductImageDescription"].ToString(),
                            CategoryId = Convert.ToInt32(myDS.Tables["myProduct"].Rows[i]["CategoryId"]),
                            Id = Convert.ToInt32(myDS.Tables["myProduct"].Rows[i]["Id"])
                        };

                        // Iterate
                        i++;

                        // Add objects to list
                        ProductList.Add(pd);
                    }

                    // Send empty error-message
                    errormsg = "";

                    // Return list
                    return ProductList;
                }
                // If DataSet is empty
                else
                {
                    // Send error-message
                    errormsg = "Hittade inga produkter.";
                    return (null);
                }
            }
            // Catch errors
            catch (Exception e)
            {
                // Get error-message
                errormsg = e.Message;
                return null;
            }
            finally
            {
                // Close DB-connection
                dbConnection.Close();
            }
        }
    }
}