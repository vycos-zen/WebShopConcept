

$(document).ready(function () {
    $('#ContentBody_setImageNames').hide();
    $('#ContentBody_imageUpload').change(function (e) {
        var files = this.files;
        if (files.length > 0) {
            $('#ContentBody_setImageNames').show();
        }
        else {
            $('#ContentBody_setImageNames').hide();
        }
    });
});

$(document).ready(function () {
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        $('#ContentBody_productDescription').keypress(function (e) {
            // var numberOfChar = ;
            if ($(this).val().length < 1000) {
                $('#ContentBody_productDescriptionLimit').html('(' + $(this).val().length + '/1000)').removeClass('errorText');
            }
            else {
                $('#ContentBody_productDescriptionLimit').addClass('errorText');
            }
        });
    });
});

$(document).ready(function () {
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        $('#ContentBody_productDescription').change(function (e) {
            if ($(this).val().length < 1000) {
                $('#ContentBody_productDescriptionLimit').html('(' + $(this).val().length + '/1000)').removeClass('errorText');
            }
            else {
                $('#ContentBody_productDescriptionLimit').addClass('errorText');
            }
        });
    });
});

//$(document).ready(function () {
//    ($('#loginPanelButton')).click(function () {
//        var buttonText = $(this).val();
//        var regVisible = $('#registerPanel').is(':visible');
//        var logVisible = $('#loginPanel').is(':visible');
//        if (buttonText === 'Login') {
//            $(this).val('Cancel');
//            if (!regVisible) {
//                $('#headerBody').toggleClass('headerBodyExpanded');
//            }
//        }
//        else {
//            $(this).val('Login');
//            $('#headerBody').toggleClass('headerBodyExpanded');
//        }

//    });
//});

//$(document).ready(function () {
//    ($('#registerPanelButton')).click(function () {
//        var buttonText = $(this).val();
//        var regVisible = $('#registerPanel').is(':visible');
//        var logVisible = $('#loginPanel').is(':visible');
//        if (buttonText === 'Register') {
//            $(this).val('Cancel');
//            if (!logVisible) {
//                $('#headerBody').toggleClass('headerBodyExpanded');
//            }
//        }
//        else {
//                $(this).val('Register');
//                $('#headerBody').toggleClass('headerBodyExpanded');
//        }


//    });
//});

//still need to implement validation of login registration result
//$(document).ready(function () {
//    var prm = Sys.WebForms.PageRequestManager.getInstance();

//    prm.add_endRequest(function () {
//        $(document).ready(function () {
//            ($('#loginControl_loginButton')).click(function () {
//                $('#headerBody').removeClass('headerBodyExpanded').addClass('headerBodyNormal');
//            });
//        });

//        $(document).ready(function () {
//            ($('#registerUser')).click(function () {
//                $('#headerBody').removeClass('headerBodyExpanded').addClass('headerBodyNormal');
//            });
//        });
//    });
//});
