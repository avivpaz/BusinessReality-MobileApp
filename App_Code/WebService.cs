﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }


    /// <summary>
    /// send the product basic info from js to code behind
    /// </summary>
    /// <param name="productId">product id</param>
    /// <param name="email">manager email for identification</param>
    /// <returns>a json string of the product info</returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string getProductsInfo(int productCounter)
    {
        Product p = new Product();
        p = p.GetProductInfoBasic(productCounter);
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(p);

    }

    /// <summary>
    /// send the properties of a product from js to code behind
    /// </summary>
    /// <param name="productId">product id</param>
    /// <param name="email">manager email for identification</param>
    /// <returns>a json string of the properties</returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetProductPropertiesInfo(int productCounter)
    {
        List<Property> properties = new List<Property>();
        Property p = new Property();
        properties = p.GetProductPropertiesInfo(productCounter);
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(properties);
    }

    /// <summary>
    /// gets all the products on sale
    /// </summary>
    /// <param name="orgName">organization name</param>
    /// <returns>list of products</returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetAllProductOnSale(string name)
    {
        List<Product> products = new List<Product>();
        Product p = new Product();
        products = p.GetAllProductOnSale(name);
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(products);
    }

    /// <summary>
    /// get the user products scan history FROM THE DB
    /// </summary>
    /// <param name="userId">user id for identification</param>
    /// <returns>a list of products objects</returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetProductScanHistory(int userId)
    {
        List<Product> products = new List<Product>();
        Product p = new Product();
        products = p.GetProductScanHistory(userId);
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(products);
        return jsonString;
    }

    /// <summary>
    /// gets the organization info
    /// </summary>
    /// <param name="email">Product Counter</param>
    /// <returns>organization info</returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string getOrganizationInfo(int productCounter)       
    {
        Organization org = new Organization();
        org = org.getOrganizationInfo(productCounter);
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(org);
    }

    /// <summary>
    /// gets the current active campaign info from the db and send it to js
    /// </summary>
    /// <param name="organizationID">org id for identification</param>
    /// <returns>json string of the info</returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string getOrgActinveCampaignInfo(int organizationID)
    {
        Campaign camp = new Campaign();
        camp = camp.getOrgActinveCampaignInfo(organizationID);
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(camp);
    }

    /// <summary>
    /// update the user_sare_campaign table each time a user share a campaign
    /// </summary>
    /// <param name="campaignId">campign id for identification</param>
    /// <param name="fbID">user fb id for identification</param>
    /// <returns>json of number of rows changed</returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string UpdateUserShareCampaign(int campaignId, int fbID)
    {
        Campaign camp = new Campaign();
        int change = camp.UpdateUserShareCampaign(campaignId, fbID);
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(change.ToString());
    }


    /// <summary>
    ///  insert new user 
    /// </summary>
    /// <param name="organizationID">info about the user</param>
    /// <returns>number of chage rows</returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string insertNewUser(string id, string fname, string lname, string city, string gender, string age)
    {
        User u = new User();
        u.FbId = id;
        u.Fname = fname;
        u.Lname = lname;
        u.Age = Convert.ToInt16(age);
        u.City = city;
        if (gender == "male")
            u.Gender = "1";
        else
            u.Gender = "2";
        int change = u.insertNewUser(u);
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(change);

    }

    /// <summary>
    /// insert into User_scan_qrCode,Activity,Activity_activated
    /// </summary>
    /// <returns>number of change rows</returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string insertNewUserScanQr(string userid, string productCounter)
    {
        Product product = new Product();
        int id = product.insertNewUserScanQr(userid, productCounter);
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(id);
    }

    /// <summary>
    /// insert into User_scan_qrCode,Activity,Activity_activated
    /// </summary>
    /// <returns>number of change rows</returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string propertyClicked(string activity, string pcid)
    {
        Property p = new Property();
        int change = p.propertyClicked(activity, pcid);
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(change.ToString());
    }

    /// <summary>
    /// checks if the user has a valid voucher
    /// </summary>
    /// <returns>number of rows</returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string getIfValid(string fbId, string orgName)
    {
        Campaign cm = new Campaign();
        int change = cm.getIfValid(fbId, orgName);
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(change.ToString());
    }
    /// <summary>
    /// checks if the user has a valid voucher
    /// </summary>
    /// <returns>number of rows</returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string changeValidCampiagn(string fbId, string orgName)
    {
        Campaign cm = new Campaign();
        int change = cm.changeValidCampiagn(fbId, orgName);
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(change.ToString());
    }

}//class

