
var clicked = false;

function clickOnButton() {
    clicked = true;
    console.log("trueado");
}

    function statusChangeCallback(response) {
        console.log(response.status);

        if (response.status === 'connected') {
                testAPI();
            
        } else {
            console.log("Logueate in this app");
        }
    }

    function checkLoginState() {
        FB.getLoginStatus(function (response) {
            statusChangeCallback(response);
        });


    }
  
window.fbAsyncInit = function () {
  
        FB.init({
            appId: '2268676100010510',
            cookie: true,  
            xfbml: true, 
            version: 'v3.2'
    });
    /*FB.getLoginStatus(function (response) {
        console.log("getLogin");
            statusChangeCallback(response);     
    });*/
    };

    (function(d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = 'https://connect.facebook.net/en_US/sdk.js';
        fjs.parentNode.insertBefore(js, fjs);
    }
    (document, 'script', 'facebook-jssdk'));

    function testAPI() {
        FB.api('/me', { "fields": "email,id,name,last_name,first_name" }, function (response) {
            console.log(response.first_name);
            console.log(response.last_name);
            console.log(response.email);
            

            var model = {
                FirstName: response.first_name,
                FirstLastName: response.last_name,
                Password: '1234abcd',
                Email: response.email

            };

            $.ajax({
                url: "/Account/RegisterByExternalLogin",
                method: "POST",
                data: JSON.stringify(model),
                dataType: "JSON",
                contentType: 'application/json',
                async: true,
                success: function (response) {
                    if (response.content == "1") {
                        window.location = '@Url.Action("Index", "Home")';

                    } else if (response.content == "2" && response.content == "4") {
                        Swal({
                            type: 'error',
                            title: '@Language.GetMessageIn("Register", "2", vLanguage, "Title")',
                            text: '@Language.GetMessageIn("Register", "2", vLanguage, "Text")'
                        });
                    } else if (response.content == "3") {
                        $.ajax({
                            url: "/Account/LoginExternal",
                            method: "POST",
                            data: JSON.stringify(model),
                            dataType: "JSON",
                            contentType: 'application/json',
                            async: true,
                            success: function (response) {
                                if (response.content == "1") {
                                    window.location = '@Url.Action("Index", "Home")';
                                }
                            }
                        });
                    }
                },
                beforeSend: function () {
                    $("body").fadeIn(function () {
                        $(".modaldisplay").show();
                    });
                },
                complete: function () {
                    $(".modaldisplay").hide();
                },
                error: function (xhr) {
                    console.log(xhr.status + ": " + xhr.responseText);
                }
            });
            
        });
    }
