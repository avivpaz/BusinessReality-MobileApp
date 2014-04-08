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
            getUserInfo()
            //window.location.href = '../BusinessReality-MobileApp/ShowProduct.htm';
            
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


//share automaticlly on user wall 
function ShareCampaign() {
    var params = {};
    params['message'] = '';
    params['name'] = '';
    params['description'] = '';
    params['link'] = '';
    params['picture'] = '';
    params['caption'] = '';

    FB.api('/me/feed', 'post', params, function (response) {
        if (!response || response.error) {
            // an error occured
            alert(JSON.stringify(response.error));
        } else {
            // Done
            alert('Published to Facebook!');
        }
    });

}

//share with user inserting the text
function invokeDialog() {
    FB.ui({
        method: 'feed',
        name: 'name',
        description: 'hi there!',
        link: 'www.danvetom.com',
        caption: 'An example caption'
    }, function (response) { });
}


// Load the facebook SDK asynchronously. must have it. 
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=295197610636721";
    fjs.parentNode.insertBefore(js, fjs);
} (document, 'script', 'facebook-jssdk'));

//get the current user basic info
function getUserInfo() {
    FB.api('/me', function (response) {
        var str = "First Name: " + response.first_name + ", ";
        str += "Last Name: " + response.last_name + ", ";
        str += "Username: " + response.username + ", ";
        str += "Id: " + response.id + ", ";
        str += "Email: " + response.email + ", ";
        str += "gender: " + response.gender + ", ";
        str += "age: " + response.age_range + ", ";
        str += "birthday: " + response.birthday + ", ";
        str += "location: " + response.location.name + ", ";
        alert(str);
    });
}
