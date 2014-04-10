using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Property
/// </summary>
public class Property
{
    string pcid;
    private string name;
    private string description;

    public Property()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string Name
    {
        get { return this.name; }
        set { this.name = value; }
    }


    public string Description
    {
        get { return this.description; }
        set { this.description = value; }
    }

    public string Pcid
    {
        get { return this.pcid; }
        set { this.pcid = value; }
    }

    /// <summary>
    /// call the db class to get the properties info of a specific product from the db
    /// </summary>
    /// <param name="managerEmail">Product Counter</param>
    /// <returns>a list of the product info</returns>
    public List<Property> GetProductPropertiesInfo(int productCounter)
    {
        DataBaseManager db = new DataBaseManager();
        return db.GetProductPropertiesInfo(productCounter);
    }


    /// <summary>
    /// pdate the db each time a property has been clicked
    /// </summary>
    /// <param name="activity">the current activity</param>
    /// <param name="pcid">the property of a category id</param>
    /// <returns>num of rows changed</returns>
    public int propertyClicked(string activity, string pcid)
    {
        DataBaseManager db = new DataBaseManager();
        return db.propertyClicked(activity,pcid);
    }
}