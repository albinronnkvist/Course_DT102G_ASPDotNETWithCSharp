using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Webbshop.Models
{
    public class CategoryMethods
    {
        // SELECT
        // Select all categories
        public List<CategoryDetail> SelectAllCategories(out string errormsg)
        {
            // DB-connect
            // Create SQL-connection
            SqlConnection dbConnection = new SqlConnection
            {
                // Connect to SQL Server database
                ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Webbshop;Integrated Security=True"
            };



            // SQL-query
            // Create SQL-string
            String sqlstring = "SELECT * FROM [Categories]";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);



            // Create adapter
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            // Create new list
            List<CategoryDetail> CategoryList = new List<CategoryDetail>();



            // Execute SQL-query
            try
            {
                // Open DB-connection
                dbConnection.Open();

                // Fill dataset with data in a table
                myAdapter.Fill(myDS, "myCategory");

                // Create a counter for amount of rows
                int i = 0;
                int count = 0;
                count = myDS.Tables["myCategory"].Rows.Count;

                // If there are more than 0 rows in the DataSet
                if (count > 0)
                {
                    // Loop through entire DataSet
                    while (i < count)
                    {
                        // Read data from DataSet and store in object
                        CategoryDetail cd2 = new CategoryDetail
                        {
                            CategoryName = myDS.Tables["myCategory"].Rows[i]["CategoryName"].ToString(),
                            Id = Convert.ToInt32(myDS.Tables["myCategory"].Rows[i]["Id"])
                        };

                        // Iterate
                        i++;

                        // Add objects to list
                        CategoryList.Add(cd2);
                    }

                    // Send empty error-message
                    errormsg = "";

                    // Return list
                    return CategoryList;
                }
                // If DataSet is empty
                else
                {
                    // Send error-message
                    errormsg = "Hittade inga kategorier.";
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
        public CategoryDetail SelectSingleCategory(int id, out string errormsg)
        {
            // DB-connect
            // Create SQL-connection
            SqlConnection dbConnection = new SqlConnection
            {
                // Connect to SQL Server database
                ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Webbshop;Integrated Security=True"
            };



            // SQL-query
            // Create SQL-string
            String sqlstring = "SELECT * FROM [Categories] WHERE Id = @id";
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
                myAdapter.Fill(myDS, "myCategory");

                // Create a counter for amount of rows
                int i = 0;
                int count = 0;
                count = myDS.Tables["myCategory"].Rows.Count;

                // If there are more than 0 rows in the DataSet
                if (count > 0)
                {
                    // Read data from DataSet and store in object
                    CategoryDetail cd2 = new CategoryDetail
                    {
                        CategoryName = myDS.Tables["myCategory"].Rows[i]["CategoryName"].ToString(),
                        Id = Convert.ToInt32(myDS.Tables["myCategory"].Rows[i]["Id"])
                    };

                    // Send empty error-message
                    errormsg = "";

                    // Return object
                    return cd2;
                }
                // If DataSet is empty
                else
                {
                    // Send error-message
                    errormsg = "Kategori finns ej.";
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
        // Insert new category
        public int InsertCategory(CategoryDetail cd, out string errormsg)
        {
            // DB-connect
            // Create SQL-connection
            SqlConnection dbConnection = new SqlConnection
            {
                // Connect to SQL Server database
                ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Webbshop;Integrated Security=True"
            };



            // SQL-query
            // Create SQL-string
            String sqlstring = "INSERT INTO [Categories] (CategoryName) VALUES (@categoryName)";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            // Add parameters
            dbCommand.Parameters.Add("categoryName", System.Data.SqlDbType.NVarChar, 60).Value = cd.CategoryName;



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
        // Update a category
        public int UpdateCategory(CategoryDetail cd, out string errormsg)
        {
            // DB-connect
            // Create SQL-connection
            SqlConnection dbConnection = new SqlConnection
            {
                // Connect to SQL Server database
                ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Webbshop;Integrated Security=True"
            };



            // SQL-query
            // Create SQL-string
            String sqlstring = "UPDATE [Categories] SET CategoryName =  @categoryName WHERE Id = @id";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            // Add parameters
            dbCommand.Parameters.Add("categoryName", System.Data.SqlDbType.NVarChar, 60).Value = cd.CategoryName;
            dbCommand.Parameters.Add("id", System.Data.SqlDbType.Int).Value = cd.Id;



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
        // Delete a category
        public int DeleteCategory(int id, out string errormsg)
        {
            // DB-connect
            // Create SQL-connection
            SqlConnection dbConnection = new SqlConnection
            {
                // Connect to SQL Server database
                ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Webbshop;Integrated Security=True"
            };



            // SQL-query
            // Create SQL-string
            String sqlstring = "DELETE FROM [Categories] WHERE Id = @id";
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
    }
}