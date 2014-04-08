using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

/// <summary>
/// Summary description for DataBaseManager
/// </summary>
public class DataBaseManager
{
    /////////////////////////connection/////////////////////

    /// <summary>
    /// the connection string to the database
    /// </summary>
    private string connectionString;

    /// <summary>
    /// Creates a new data base manager
    /// </summary>
    public DataBaseManager()
    {
        this.connectionString = WebConfigurationManager.ConnectionStrings["databaseConnection"].ConnectionString;
    }
    /// <summary>
    /// Get an open connection to the data base
    /// </summary>
    /// <returns>n SqlConnection instance of an open connection</returns>
    private SqlConnection GetOpenConnection()
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        return connection;
    }

    private void closeConnection()
    {
        SqlConnection con = GetOpenConnection();
        if (con != null)
        {
            // close the db connection
            con.Close();
        }

    }

    //////////////////////////////////////////onLoad procedures////////////////////////////

    /// <summary>
    /// get the basic info (not incude properties) of all the existing products of an organization
    /// </summary>
    /// <param name="managerEmail">manager email for identification</param>
    /// <returns>a list of product objects</returns>
    public Product GetProductInfoBasic(int productCounter)
    {
        List<SqlParameter> paraList = new List<SqlParameter>();
        Product product = new Product();
        try
        {
            paraList.Add(new SqlParameter("@productCounter", productCounter));
            SqlDataReader dr = ActivateStoredProc("GetSpecificProductInfo", paraList);
            while (dr.Read())
            {// Read till the end of the data into a row
                // read first field from the row into the list collection
                product.CategoryName = dr["Name"].ToString();
                product.Id = Convert.ToInt32(dr["ProductId"]);
                product.Name = dr["productName"].ToString();
                product.Price = Convert.ToDouble(dr["Price"]);
                if (dr["Discount"] != null || dr["Discount"] != "")
                    product.Discount = dr["Discount"].ToString();
                product.ImageUrl = dr["img"].ToString();
                product.Description = dr["ShortDescription"].ToString();
            }
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
        finally
        {
            closeConnection();
        }
        return product;
    }


    /// <summary>
    /// get the properties info of a specific product from the db
    /// </summary>
    /// <param name="managerEmail">manager's email for identification</param>
    /// <param name="productId">to select a specific product from the db</param>
    /// <returns>a list of the product info</returns>
    public List<Property> GetProductPropertiesInfo(int productCounter)
    {
        List<SqlParameter> paraList = new List<SqlParameter>();
        List<Property> properties = new List<Property>();

        try
        {
            paraList.Add(new SqlParameter("@productCounter", productCounter));
            SqlDataReader dr = ActivateStoredProc("proc_GetSpecificProductProperties", paraList);
            while (dr.Read())
            {// Read till the end of the data into a row
                // read first field from the row into the list collection
                Property prop = new Property();
                prop.Name = dr["Name"].ToString();
                prop.Description = dr["description"].ToString();
                properties.Add(prop);
            }
        }


        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
        finally
        {
            closeConnection();
        }
        return properties;
    }


    /// <summary>
    ///Get All Products On Sale
    /// </summary>
    /// <param name="productId">Name of the organization</param>
    /// <returns>list of products </returns>
    public List<Product> GetAllProductOnSale(string orgName)
    {
        List<SqlParameter> paraList = new List<SqlParameter>();
        List<Product> products = new List<Product>();

        try
        {
            paraList.Add(new SqlParameter("@orgName", orgName));
            SqlDataReader dr = ActivateStoredProc("GetAllProductOnSale", paraList);
            while (dr.Read())
            {// Read till the end of the data into a row
                // read first field from the row into the list collection
                Product product = new Product();
                product.Name = dr["Name"].ToString();
                product.ProductCounter = Convert.ToInt32(dr["productCounter"]);
                product.Description = dr["ShortDescription"].ToString();
                product.ImageUrl = dr["img"].ToString();
                products.Add(product);
            }
        }


        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
        finally
        {
            closeConnection();
        }
        return products;
    }
    /// <summary>
    /// gets the organization profile
    /// </summary>
    /// <param name="productId">productCounter of the product that bellongs to the organization</param>
    /// <returns>gets the organization profile</returns>
    public Organization getOrganizationInfo(int productCounter)
    {
        List<SqlParameter> paraList = new List<SqlParameter>();
        Organization org = new Organization();

        try
        {
            paraList.Add(new SqlParameter("@productCounter", productCounter));
            SqlDataReader dr = ActivateStoredProc("getOrganizationInfo", paraList);
            while (dr.Read())
            {// Read till the end of the data into a row
                // read first field from the row into the list collection
                org.Name = dr["Name"].ToString();
                org.Description = dr["Description"].ToString();
                org.Address = dr["Address"].ToString();
                org.LogoSrc = dr["LogoURL"].ToString();
                org.PhoneNumber = dr["PhoneNumber"].ToString();
                org.WebSiteUrl = dr["Website"].ToString();
                org.FbWebsite = dr["FbWebsite"].ToString();
            }
        }


        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
        finally
        {
            closeConnection();
        }
        return org;
    }

    /// <summary>
    /// gets the organization current active campaign info from the db
    /// </summary>
    /// <param name="organizationID">id for the org identification</param>
    /// <returns>object of this campaign</returns>
    public Campaign getOrgActinveCampaignInfo(int organizationID)
    {
        List<SqlParameter> paraList = new List<SqlParameter>();
        Campaign camp = new Campaign();

        try
        {
            paraList.Add(new SqlParameter("@organizationID", organizationID));
            SqlDataReader dr = ActivateStoredProc("proc_GetOrgActiveCampaign", paraList);
            while (dr.Read())
            {// Read till the end of the data into a row
                // read first field from the row into the list collection
                camp.Name = dr["Name"].ToString();
                camp.Description = dr["Description"].ToString();
                camp.Voucher = dr["Voucher"].ToString();
                camp.Expiration = Convert.ToInt32(dr["Expiration"]);
                if (dr["Img"] != "" || dr["Img"] != null)
                    camp.ImageUrl = dr["Img"].ToString();
                if (dr["Link"] != "" || dr["Link"] != null)
                    camp.LinkUrl = dr["Img"].ToString();
            }
        }


        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
        finally
        {
            closeConnection();
        }
        return camp;
    }


    /// <summary>
    /// insert a new app user into the db
    /// </summary>
    /// <param name="user">an object of a new user</param>
    /// <param name="email">manager email for identification</param>
    /// <returns></returns>
    public int insertNewUser(User user)
    {

        try
        {
            String command;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}','{4}','{5}')", user.Fname, user.Lname, user.City, user.Age.ToString(), user.Gender, user.FbId);
            String prefix = "INSERT INTO Users " + "(FirstName, LastName,City,Age,genderID,FacebookID)";
            command = prefix + sb.ToString();
            return insertCommand(command);

        }
        catch (Exception)
        {
            return 0;
        }

    }



    public int insertNewUserScanQr(string userid, string productCounter)
    {
        SqlConnection con;

        try
        {

            con = GetOpenConnection(); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
        SqlCommand command = new SqlCommand("insertNewUserScanQr", con);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add("@productCounter", SqlDbType.Int).Value = productCounter;
        command.Parameters.Add("@userId", SqlDbType.Int).Value = userid;
        SqlParameter outp = new SqlParameter("@activityId", SqlDbType.Int);
        outp.Direction = ParameterDirection.Output;
        command.Parameters.Add(outp);
        command.ExecuteNonQuery();
        int id = Convert.ToInt16(command.Parameters["@activityId"].Value);
        con.Close();
        return id;
    }
    /////////////////////////////////////// Execution of commands && procedures  ///////////////////


    /// <summary>
    /// activate stored procedure
    /// </summary>
    /// <param name="procName">procedure name</param>
    /// <param name="parametersList">a list of input parameters for the procedure</param>
    /// <returns>sqlreader object</returns>
    private SqlDataReader ActivateStoredProc(string procName, List<SqlParameter> parametersList)
    {
        SqlConnection con;
        SqlDataReader dr;
        Dictionary<string, int> names = new Dictionary<string, int>();

        try
        {

            con = GetOpenConnection(); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        try
        {
            SqlCommand cmd = new SqlCommand(procName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parametersList)
            {
                cmd.Parameters.Add(parameter);
            }
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }

        return dr;
    }

    /// <summary>
    /// insert the command
    /// </summary>
    /// <param name="command">command string</param>
    /// <returns>num of rows changed</returns>
    private int insertCommand(string command)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = GetOpenConnection(); // create a connection to the database using the connection String defined in the web config file
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommand(command, con);  // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
            // write to log

        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    /// <summary>
    /// create the command
    /// </summary>
    /// <param name="CommandSTR">command string</param>
    /// <param name="con">connetion string</param>
    /// <returns>sqlcommand object</returns>
    private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

        return cmd;
    }

    ////////////////////////////////////// End of Execution of commands && procedures  ///////////////////
}