window.fbAsyncInit = function () {
    FB.init({
        appId: '295197610636721',
        status: true, // check login status
        cookie: true, // enable cookies to allow the server to access the session
        xfbml: true  // parse XFBML
    });

    // Here we subscribe to the auth.authResponseChange JavaScript event. This event is fired
    // for any authentication related change, such as login, logout or session refresh. This means that
    // whenever someone who was previously logged out tries to log in again, the correct case below 
    // will be handled. 
    FB.Event.subscribe('auth.authResponseChange', function (response) {
        // Here we specify what we do with the response anytime this event occurs. 
        //In this case, we're handling the situation where they have logged in to the app.
        if (response.status === 'connected') {
            EnterUserInfo()
            var productCounter = getUrlVars()["productCounter"];
            FB.api('/me', function (response) {
                window.location.href = 'ShowProduct.htm?productCounter=' + productCounter + '&Id=' + response.id;
            });
        } else if (response.status === 'not_authorized') {
            // In this case, the person is logged into Facebook, but not into the app, so we call
            // FB.login() to prompt them to do so. 
            // In real-life usage, you wouldn't want to immediately prompt someone to login 
            // like this, for two reasons:
            // (1) JavaScript created popup windows are blocked by most browsers unless they 
            // result from direct interaction from people using the app (such as a mouse click)
            // (2) it is a bad experience to be continually prompted to login upon page load.
            FB.login();
        } else {
            // In this case, the person is not logged into Facebook, so we call the login() 
            // function to prompt them to do so. Note that at this stage there is no indication
            // of whether they are logged into the app. If they aren't then they'll see the Login
            // dialog right after they log in to Facebook. 
            // The same caveats as above apply to the FB.login() call here.
            //FB.login();
        }
    });
};


//extract the productCounter from the queryString in the url
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


// Load the facebook SDK asynchronously. must have it in every fb app page.
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=295197610636721";
    fjs.parentNode.insertBefore(js, fjs);
} (document, 'script', 'facebook-jssdk'));

//enter the current user info into the db
function EnterUserInfo() {
    var city;
    var age
    FB.api('/me', function (response) {
        if (typeof response.location !== 'undefined') {
            city = response.location.name.split(",")[0];
        }
        else {
            city = '';
        }

        if (typeof response.birthday !== 'undefined') {
            age = getAge(response.birthday);
        }
        else {
            age = null;
        }
        $.ajax({ // ajax call starts
            url: 'WebService.asmx/insertNewUser',   // JQuery loads serverside.php
            data: '{id:"' + response.id + '",fname:"' + response.first_name + '",lname:"' + response.last_name + '",city:"' + city + '",gender:"' + response.gender + '",age:"' + age + '"}',
            type: 'POST',
            dataType: 'json', // Choosing a JSON datatype
            contentType: 'application/json; charset = utf-8',
            success: function (data) // Variable data contains the data we get from serverside
            {

                var change = $.parseJSON(data.d);

            }, // end of success
            error: function (e) {

            } // end of error
        }) // end of ajax call
    });
}

//coverting the user's birthday date to an exact age
function getAge(dateString) {
    var today = new Date();
    var birthDate = new Date(dateString);
    var age = today.getFullYear() - birthDate.getFullYear();
    var m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
        age--;
    }
    return age;
}