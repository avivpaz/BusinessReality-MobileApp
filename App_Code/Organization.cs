using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Organization
/// </summary>
public class Organization
{
    private string name;
    private string address;
    private string industry;
    private string webSiteUrl;
    private string fbWebsite;
    private string phoneNumber;
    private string description;
    private string logoSrc;

    public Organization()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string Name { get { return this.name; } set { this.name = value; } }
    public string Address { get { return this.address; } set { this.address = value; } }
    public string Industry { get { return this.industry; } set { this.industry = value; } }
    public string WebSiteUrl { get { return this.webSiteUrl; } set { this.webSiteUrl = value; } }
    public string PhoneNumber { get { return this.phoneNumber; } set { this.phoneNumber = value; } }
    public string LogoSrc { get { return this.logoSrc; } set { this.logoSrc = value; } }
    public string Description { get { return this.description; } set { this.description = value; } }
    public string FbWebsite { get { return this.fbWebsite; } set { this.fbWebsite = value; } }

    public Organization getOrganizationInfo(int productCounter)
    {
        DataBaseManager db = new DataBaseManager();
        return db.getOrganizationInfo(productCounter);
    }
}