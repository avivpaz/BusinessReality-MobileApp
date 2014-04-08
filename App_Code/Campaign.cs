using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Campaign
/// </summary>
public class Campaign
{
    private int id;
    private string name;
    private string description;
    private string imageUrl;
    private string linkUrl;
    private DateTime dateCreated;
    private int shareCount;
    private string voucher;
    private int expiration;
    private bool isActive;

	public Campaign()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int Id
    {
        get { return this.id; }
        set { this.id = value; }
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

    public string ImageUrl
    {
        get { return this.imageUrl; }
        set { this.imageUrl = value; }
    }

    public string LinkUrl
    {
        get { return this.linkUrl; }
        set { this.linkUrl = value; }
    }

    public DateTime DateCreated
    {
        get { return this.dateCreated; }
        set { this.dateCreated = value; }
    }

    public int Expiration
    {
        get { return this.expiration; }
        set { this.expiration = value; }
    }

    public int ShareCount
    {
        get { return this.shareCount; }
        set { this.shareCount = value; }
    }

    public string Voucher
    {
        get { return this.voucher; }
        set { this.voucher = value; }
    }

    public bool IsActive
    {
        get { return this.isActive; }
        set { this.isActive = value; }
    }

    /// <summary>
    /// gets the organization current active campaign info from the db
    /// </summary>
    /// <param name="organizationID">id for the org identification</param>
    /// <returns>object of this campaign</returns>
    public Campaign getOrgActinveCampaignInfo(int organizationID)
    {
        DataBaseManager db = new DataBaseManager();
        return db.getOrgActinveCampaignInfo(organizationID);
    }

    /// <summary>
    /// update user_share_campaign table each time a user share campaign
    /// </summary>
    /// <param name="campaignID">for identification</param>
    /// <param name="fbId">for identification</param>
    /// <returns>number of rows changed</returns>
    public int UpdateUserShareCampaign(int campaignID, int fbId)
    {
        DataBaseManager db = new DataBaseManager();
        return db.UpdateUserShareCampaign(campaignID, fbId);
    
    }

    public int getIfValid(string fbId, string orgName)
    {
        DataBaseManager db = new DataBaseManager();
        return db.getIfValid(fbId, orgName);
    }
    public int changeValidCampiagn(string fbId, string orgName)
    {
        DataBaseManager db = new DataBaseManager();
        return db.changeValidCampiagn(fbId, orgName);
    }
}