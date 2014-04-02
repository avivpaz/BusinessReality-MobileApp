using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Class representing the categories to which each product belongs
/// </summary>
public class Category
{
    private string name;
    private string description;
    private DateTime dateModified;

    public Category()
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

    public DateTime DateModified
    {
        get { return this.dateModified; }
        set { this.dateModified = value; }
    }

}