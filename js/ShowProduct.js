var productCounter;
var organizetionId;
var productsOnSale;
var userId;
$(document).ready(function () {
    productCounter = getUrlVars()["productCounter"];
    //    productCounter = 40;
    userId = getUrlVars()["Id"];
    getProductInfo(productCounter);
    ActivateActivity();
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
});

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
            alert(p);
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

function EnterProperties(propeties) {
    var accordion = $('#propertiesAccordion');
    $.each(propeties, function (index, Property) {
        var innerdiv = '<di  data-role="collapsible" data-collapsed="true"  >';
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
        }, // end of success
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

