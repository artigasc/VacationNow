

    function onSignIn(googleUser) {
  
        var profile = googleUser.getBasicProfile();
        console.log(googleUser.isSignedIn());
       
        var model = {
            FirstName: profile.getGivenName(),
            FirstLastName: profile.getFamilyName(),
            Password: '1234abcd',
            Email: profile.getEmail()

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
                    //Swal({
                    //    type: 'success',
                    //    title: '@Language.GetMessageIn("Register", "1", vLanguage, "Title")',
                    //    text: '@Language.GetMessageIn("Register", "1", vLanguage, "Text")'
                    //}).then(function () {

                        //window.location = '@Url.Action("Index", "Home")';
                        window.location = '../Index';

                    //});
                } else if (response.content == "2" && response.content == "4") {
                    Swal({
                        type: 'error',
                        title: '@Language.GetMessageIn("Register", "2", vLanguage, "Title")',
                        text: '@Language.GetMessageIn("Register", "2", vLanguage, "Text")'
                    });
                } else if (response.content == "3") {
                    Swal({
                        type: 'warning',
                        title: '@Language.GetMessageIn("Register", "3", vLanguage, "Title")',
                        text: '@Language.GetMessageIn("Register", "3", vLanguage, "Text")'
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
                        
}



