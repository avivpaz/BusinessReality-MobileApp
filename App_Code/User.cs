using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    private string fName;
    private string lName;
    private string city;
    private int age;
    private string gender;
    private int fbId;

	public User()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string Fname { get { return this.fName; } set { this.fName = value; } }
    public string Lname { get { return this.lName; } set { this.lName = value; } }
    public string City { get { return this.city; } set { this.city = value; } }
    public int Age { get { return this.age; } set { this.age = value; } }
    public string Gender { get { return this.gender; } set { this.gender = value; } }
    public int FbId { get { return this.fbId; } set { this.fbId = value; } }



}