var productCounter;
$(document).ready(function () {
    //productCounter = getUrlVars()["productCounter"];
    productCounter = 40;
    getProductInfo(productCounter);
    getOrganizationInfo(productCounter);

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
    $('#productImage').attr("src", 'http://proj.ruppin.ac.il/bgroup16/prod/' + p.ImageUrl);
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


//getting the selected product properties from the db
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
    $('#comapnyName').text(org.Name);
    $('#orgDescription').text(org.Description);
    $('#orgPhone').text(org.PhoneNumber);
    $('#webSiteUrl').text(org.WebSiteUrl);
    $('#orgFB').text(org.FbWebsite);
    $('#orgFB').attr("href",org.FbWebsite);

}



