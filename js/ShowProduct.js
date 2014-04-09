var productCounter
var productsOnSale;
var userId
var campaignInfo;
var activity;
var orgName;
var properties;
$(document).ready(function () {
    //    productCounter = getUrlVars()["productCounter"];
    productCounter = 40;
    userId = getUrlVars()["Id"];
    getProductInfo(productCounter);
    ActivateActivity();
    $('#btnCheckIfValid').hide();
    getOrganizationInfo(productCounter);
    getActiveCampaignInfo(1);
    $('#productsOnSale').on('click', 'li', function () {
        var name = $(this).find('h3').text();
        $.each(productsOnSale, function (index, Product) {
            if (name == Product.Name) {
                window.location.href = window.location.pathname + "?productCounter=" + Product.ProductCounter;
            }
        });
    });
    $(document).on("expand", ".collepse", function (event, ui) {
        var desc = $(this).find('p').text();
        $.each(properties, function (index, Property) {
            if (desc == Property.Description) {
                insertPropertyClick(Property.Pcid);
            }
        });
    });
    CheckIfValid(2);

});

// Load the facebook SDK asynchronously. must have it. 
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=295197610636721";
    fjs.parentNode.insertBefore(js, fjs);
} (document, 'script', 'facebook-jssdk'));

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function ActivateActivity() {
    $.ajax({ // ajax call starts
        url: 'WebService.asmx/insertNewUserScanQr',   // JQuery loads serverside.php
        data: '{userid:"' + userId + '",productCounter:"' + productCounter + '"}',
        type: 'POST',
        dataType: 'json', // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8',
        success: function (data) // Variable data contains the data we get from serverside
        {
            p = $.parseJSON(data.d);
            activity = p.toString();
        }, // end of success
        error: function (e) {
            alert(e.responseText);
        } // end of error
    }) // end of ajax call
}
//getting the selected product informaiton from the db
function getProductInfo(productCounter) {
    $.ajax({ // ajax call starts
        url: 'WebService.asmx/getProductsInfo',   // JQuery loads serverside.php
        data: '{productCounter:"' + productCounter + '"}',
        type: 'POST',
        dataType: 'json', // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8',
        success: function (data) // Variable data contains the data we get from serverside
        {
            p = $.parseJSON(data.d);
            enterProductInfomation(p);
        }, // end of success
        error: function (e) {
            alert(e.responseText);
        } // end of error
    }) // end of ajax call
}

function enterProductInfomation(p) {
    $('#productName').text(p.Name);
    $('#productDescription').text(p.Description);
    $('#productImage').attr("src", p.ImageUrl);
    $('#price').append(p.Price);
    $('#price').append(' ש"ח');
    if (p.Discount != null && p.Discount != "") {
        $("#myPopup").popup({ overlayTheme: "a" });
        $('#discount').text(p.Discount);
        $("#myPopup").popup("open");
    }
    GetProductPropertiesInfo(productCounter);
}

//getting the selected product properties from the db
function GetProductPropertiesInfo(productCounter) {
    $.ajax({ // ajax call starts
        url: 'WebService.asmx/GetProductPropertiesInfo',   // JQuery loads serverside.php
        data: '{productCounter:"' + productCounter + '"}',
        type: 'POST',
        dataType: 'json', // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8',
        success: function (data) // Variable data contains the data we get from serverside
        {
            p = $.parseJSON(data.d);
            EnterProperties(p);
        }, // end of success
        error: function (e) {
            alert(e.responseText);
        } // end of error
    }) // end of ajax call
}

function EnterProperties(p) {
    properties = p;
    var accordion = $('#propertiesAccordion');
    $.each(properties, function (index, Property) {
        var innerdiv = '<div class="collepse" data-role="collapsible" data-collapsed="true"  >';
        innerdiv += '<h2 >' + Property.Name + '</h2>';
        innerdiv += '<p>' + Property.Description + '</p>';
        innerdiv += '</div>';
        accordion.append(innerdiv);
    });
    $("#propertiesAccordion").collapsibleset("refresh");
}
////////////////////////////////////////////////////////////////


//getting the organization info' from the db
function getOrganizationInfo(productCounter) {
    $.ajax({ // ajax call starts
        url: 'WebService.asmx/getOrganizationInfo',   // JQuery loads serverside.php
        data: '{productCounter:"' + productCounter + '"}',
        type: 'POST',
        dataType: 'json', // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8',
        success: function (data) // Variable data contains the data we get from serverside
        {
            o = $.parseJSON(data.d);
            EnterOrganizationInformation(o);
        }, // end of success
        error: function (e) {
            alert(e.responseText);
        } // end of error
    }) // end of ajax call
}

function EnterOrganizationInformation(org) {
    orgName = org.Name;
    $('#orgName').text(org.Name);
    $('#orgName2').text(org.Name);
    $('#comapnyName').text(org.Name);
    $('#orgDescription').text(org.Description);
    $('#orgPhone').text(org.PhoneNumber);
    $('#webSiteUrl').text(org.WebSiteUrl);
    $('#fbLogo').attr("href", org.FbWebsite);
    GetAllProductOnSale(org.Name);
}

///////////////////////////////////////////


//getting the selected product properties from the db
function GetAllProductOnSale(orgName) {
    $.ajax({ // ajax call starts
        url: 'WebService.asmx/GetAllProductOnSale',   // JQuery loads serverside.php
        data: '{name:"' + orgName + '"}',
        type: 'POST',
        dataType: 'json', // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8',
        success: function (data) // Variable data contains the data we get from serverside
        {
            p = $.parseJSON(data.d);
            productsOnSale = p;
            EnterOnSaleProducts(p);
        }, // end of success**
        error: function (e) {
            alert(e.responseText);
        } // end of error
    }) // end of ajax call
}

function EnterOnSaleProducts(p) {

    $.each(p, function (index, Product) {
        $("#productsOnSale").append($("<li  class='liList'  data-icon-position='left'  data-icon= 'arrow-l'><a  href=''><img src='" + Product.ImageUrl + "' /> <div class='left'><h3>" + Product.Name + "</h3><p> " + Product.Description + "</p></div></a></li>"));
    });
    $("#productsOnSale").listview("refresh");

}


//getting the current active campaign info from the db
function getActiveCampaignInfo(organizationID) {
    $.ajax({ // ajax call starts
        url: 'WebService.asmx/getOrgActinveCampaignInfo',   // JQuery loads serverside.php
        data: '{organizationID:"' + organizationID + '"}',
        type: 'POST',
        dataType: 'json', // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8',
        success: function (data) // Variable data contains the data we get from serverside
        {
            o = $.parseJSON(data.d);
            campaignInfo = o;
            EnterActiveCampaignInformation(o);
        }, // end of success
        error: function (e) {
            alert(e.responseText);
        } // end of error
    }) // end of ajax call
}

//enter the details to the shareCampaign page
function EnterActiveCampaignInformation(camp) {
    $('#voucher').text(camp.Voucher);
    $('#CampDescription').text(camp.Description);

}

//once the user swipe to the left, the side panel opens
$(document).on("pageinit", "#showProductPage", function () {
    $(document).on("swipeleft swiperight", "#showProductPage", function (e) {
        // We check if there is no open panel on the page because otherwise
        // a swipe to close the left panel would also open the right panel (and v.v.).
        // We do this by checking the data that the framework stores on the page element (panel: open).
        if ($.mobile.activePage.jqmData("panel") !== "open") {
            if (e.type === "swipeleft") {
                $("#sales").panel("open");
            }
        }
    });
});


//share campaign automaticlly on user fb wall 
function ShareCampaign() {
    var params = {};
    params['message'] = campaignInfo.Description;
    params['name'] = campaignInfo.Name;
    params['description'] = '';
    params['caption'] = '';
    params['link'] = campaignInfo.LinkUrl
    params['picture'] = campaignInfo.ImageUrl;

    if ($("#btnShareCampaign .ui-btn-text").text() != "זכאי לקבלת ההטבה") {
        FB.api('/me/feed', 'post', params, function (response) {
            if (!response || response.error) {
                // an error occured
                alert(JSON.stringify(response.error));
            } else {
                // Done
                $("#popupCampaign").popup({ overlayTheme: "a" });
                $('#shareSuccess').text("אנו מודים לך על השיתוף! גש לקופה על מנת לממש את ההטבה. הטבה זאת תקפה למשך " + campaignInfo.Expiration + " שעות.");
                $("#popupCampaign").popup("open");
            }
        });
        //after the post the button text is changing
        $("#btnShareCampaign").hide();
        $("#btnCheckIfValid").show();
        UpdateUserShareCampaign(campaignInfo.Id, userId);
    }
}

//update the user_sare_campaign table each time a user share a campaign.
function UpdateUserShareCampaign(campaignId, fbid) {
    $.ajax({ // ajax call starts
        url: 'WebService.asmx/UpdateUserShareCampaign',   // JQuery loads serverside.php
        data: '{campaignId:"' + campaignId + '",fbID:"' + fbid + '"}',
        type: 'POST',
        dataType: 'json', // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8',
        success: function (data) // Variable data contains the data we get from serverside
        {
            p = $.parseJSON(data.d);
        }, // end of success
        error: function (e) {
            alert(e.responseText);
        } // end of error
    }) // end of ajax call

}


function insertPropertyClick(pcid) {
    $.ajax({ // ajax call starts
        url: 'WebService.asmx/propertyClicked',   // JQuery loads serverside.php
        data: '{activity:"' + activity + '",pcid:"' + pcid + '"}',
        type: 'POST',
        dataType: 'json', // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8',
        success: function (data) // Variable data contains the data we get from serverside
        {
            p = $.parseJSON(data.d);
        }, // end of success
        error: function (e) {
            alert(e.responseText);
        } // end of error
    }) // end of ajax call
}

function CheckIfValid(option) {
    $.ajax({ // ajax call starts
        url: 'WebService.asmx/getIfValid',   // JQuery loads serverside.php
        data: '{fbId:"' + userId + '",orgName:"' + orgName + '"}',
        type: 'POST',
        dataType: 'json', // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8',
        success: function (data) // Variable data contains the data we get from serverside
        {
            var p = $.parseJSON(data.d);
            alert(p);
            if (option == 1) {
                if (p == '0') {
                    $("#notValidVoucher").popup({ overlayTheme: "a" });
                    $('#notValidVoucherB').text("אנו מצטערים, אין לך הטבה בתוקף ");
                    $("#notValidVoucher").popup("open");
                }
                else {
                    $("#validPopUp").popup({ overlayTheme: "a" });
                    $("#validPopUp").popup("open");
                }
            }
            else if (option == 2) {
            alert(p);
                if (p == '0') {
               
                }
                else {
                    $("#btnShareCampaign").hide();
                    $('#btnCheckIfValid').show();
                }
                $('#btnCheckIfValid').button('refresh');
            }
        }, // end of success
        error: function (e) {
            alert(e.responseText);
        } // end of error
    }) // end of ajax call
  
}

function deactivate() {
    var password = $("#pw").val();
    if (password == '1234') {

        $.ajax({ // ajax call starts
            url: 'WebService.asmx/changeValidCampiagn',   // JQuery loads serverside.php
            data: '{fbId:"' + userId + '",orgName:"' + orgName + '"}',
            type: 'POST',
            dataType: 'json', // Choosing a JSON datatype
            contentType: 'application/json; charset = utf-8',
            success: function (data) // Variable data contains the data we get from serverside
            {
                p = $.parseJSON(data.d);
                $("#btnShareCampaign").show();
                $("#btnCheckIfValid").hide();
                $("#validPopUp").popup("close");
            }, // end of success
            error: function (e) {
                alert(e.responseText);
            } // end of error
        }) // end of ajax call
    }

}