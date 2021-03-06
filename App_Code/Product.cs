﻿using System;
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
    private int productCounter;
    private int id;
    string categoryName;
    private string name;
    private string description;
    private double price;
    private string imageUrl;
    private string discount;
    private DateTime dateModified;
    private Category category;

    public int ProductCounter { get { return this.productCounter; } set { this.productCounter = value; } }
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

        /// <summary>
    ///Get All Products On Sale
    /// </summary>
    /// <param name="productId">Name of the organization</param>
    /// <returns>list of products </returns>
    public List<Product> GetAllProductOnSale(string orgName)
    {

        DataBaseManager db = new DataBaseManager();
        return db.GetAllProductOnSale(orgName);
    }

    /// <summary>
    /// get the user products scan history FROM THE DBf
    /// </summary>
    /// <param name="userId">for identification</param>
    /// <returns>list of products objects</returns>
    public List<Product> GetProductScanHistory(int userId)
    {
        DataBaseManager db = new DataBaseManager();
        return db.GetProductScanHistory(userId);
    }

    /// <summary>
    /// update the db every time a user scanned a product
    /// </summary>
    /// <param name="userid">user's fb id</param>
    /// <param name="productCounter">the product uniq identifier</param>
    /// <returns>num of rows changed</returns>
    public int insertNewUserScanQr(string userid, string productCounter)
    {
        DataBaseManager db = new DataBaseManager();
        return db.insertNewUserScanQr(userid, productCounter);
    }
}