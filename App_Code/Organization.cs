﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Organization
/// </summary>
public class Organization
{
    private int id;
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

    public int Id { get { return this.id; } set { this.id = value; } }
    public string Name { get { return this.name; } set { this.name = value; } }
    public string Address { get { return this.address; } set { this.address = value; } }
    public string Industry { get { return this.industry; } set { this.industry = value; } }
    public string WebSiteUrl { get { return this.webSiteUrl; } set { this.webSiteUrl = value; } }
    public string PhoneNumber { get { return this.phoneNumber; } set { this.phoneNumber = value; } }
    public string LogoSrc { get { return this.logoSrc; } set { this.logoSrc = value; } }
    public string Description { get { return this.description; } set { this.description = value; } }
    public string FbWebsite { get { return this.fbWebsite; } set { this.fbWebsite = value; } }


    /// <summary>
    /// gets the organization profile info
    /// </summary>
    /// <param name="productId">productCounter of the product that bellongs to the organization</param>
    /// <returns>gets an object of the organization</returns>
    public Organization getOrganizationInfo(int productCounter)
    {
        DataBaseManager db = new DataBaseManager();
        return db.getOrganizationInfo(productCounter);
    }
}