using System;
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
        string jsonString = js.Serialize(p);
        return jsonString;
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
        string jsonString = js.Serialize(properties);
        return jsonString;
    }
    /// <summary>
    /// send the properties of a product from js to code behind
    /// </summary>
    /// <param name="productId">product id</param>
    /// <param name="email">manager email for identification</param>
    /// <returns>a json string of the properties</returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string getOrganizationInfo(int productCounter)
    {
        Organization org = new Organization();
        org = org.getOrganizationInfo(productCounter);
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(org);
        return jsonString;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="organizationID"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string getOrgActinveCampaignInfo(int organizationID)
    {
        Campaign camp = new Campaign();
        camp = camp.getOrgActinveCampaignInfo(organizationID);
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(camp);
        return jsonString;
    }

}//class

