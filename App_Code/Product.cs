using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

/// <summary>
/// Class representing an SQL product table entry
/// </summary>
public class Product
{
    private int id;
    string categoryName;
    private string name;
    private string description;
    private double price;
    private string imageUrl;
    private string discount;
    private DateTime dateModified;
    private Category category;

    public string CategoryName { get { return this.categoryName; } set { this.categoryName = value; } }
    public int Id { get { return this.id; } set { this.id = value; } }
    public string Name { get { return this.name; } set { this.name = value; } }
    public string Description { get { return this.description; } set { this.description = value; } }
    public double Price { get { return this.price; } set { this.price = value; } }
    public string Discount { get { return this.discount; } set { this.discount = value; } }
    public string ImageUrl { get { return this.imageUrl; } set { this.imageUrl = value; } }
    public DateTime DateModified { get { return this.dateModified; } set { this.dateModified = value; } }
    public Category Category { get { return this.category; } set { this.category = value; } }

    /// <summary>
    /// call the db class to get the basic info (not incude properties) of all the existing products of an organization
    /// </summary>
    /// <param name="managerEmail">manager email for identification</param>
    /// <returns>a list of product objects</returns>
    public Product GetProductInfoBasic(int productCounter)
    {
        DataBaseManager db = new DataBaseManager();
        return db.GetProductInfoBasic(productCounter);
    }

}