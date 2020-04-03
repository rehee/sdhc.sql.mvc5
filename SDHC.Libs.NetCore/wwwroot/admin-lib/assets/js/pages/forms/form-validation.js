$(function () {
    $('#form_validation').validate({
        rules: {
            firstname: "required",
            lastname: "required",
            username: {
                required: true,
                minlength: 4
            },
            url: "required",
            password: {
                required: true,
                minlength: 5
            },
            confirm_password: {
                required: true,
                minlength: 5,
                equalTo: "#password"
            },
            email: {
                required: true,
                email: true
            },
            agree: "required",
            'checkbox': {
                required: true
            },
            'gender': {
                required: true
            },
            skill:{
                required: true
            },
            description:{
                required: true
            }
        },
        messages: {
            firstname: "Please enter your firstname",
            lastname: "Please enter your lastname",
            username: {
                required: "Please enter a username",
                minlength: "Your username must consist of at least 2 characters"
            },
            url: "Please enter a valid url",
            password: {
                required: "Please provide a password",
                minlength: "Your password must be at least 5 characters long"
            },
            confirm_password: {
                required: "Please confirm a password",
                minlength: "Your password must be at least 5 characters long",
                equalTo: "Please enter the same password as above"
            },
            email: "Please enter a valid email address",
            agree: "Please accept our policy",
            description: "What can we do to become better?",
            skill: "Please select at least 1 skill",
            gender: "Please choose any one"
        },
        highlight: function (input) {
            $(input).parents('.form-line').addClass('error');
        },
        unhighlight: function (input) {
            $(input).parents('.form-line').removeClass('error');
        },
        errorPlacement: function (error, element) {
            $(element).parents('.form-group').append(error);
        }
    });
});
$(function () {
    $('#m_form_validation').validate({
        rules: {
            firstname2: "required",
            lastname2: "required",
            username2: {
                required: true,
                minlength: 2
            },
            url2: "required",
            password2: {
                required: true,
                minlength: 5
            },
            confirm_password2: {
                required: true,
                minlength: 5,
                equalTo: "#password"
            },
            email2: {
                required: true,
                email: true
            },
            agree2: "required",
            'checkbox2': {
                required: true
            },
            'gender2': {
                required: true
            },
            skill2:{
                required: true
            },
            description:{
                required: true
            }
        },
        messages: {
            firstname2: "Please enter your firstname",
            lastname2: "Please enter your lastname",
            username2: {
                required: "Please enter a username",
                minlength: "Your username must consist of at least 2 characters"
            },
            url2: "Please enter a valid url",
            password2: {
                required: "Please provide a password",
                minlength: "Your password must be at least 5 characters long"
            },
            confirm_password2: {
                required: "Please confirm a password",
                minlength: "Your password must be at least 5 characters long",
                equalTo: "Please enter the same password as above"
            },
            email2: "Please enter a valid email address",
            agree2: "Please accept our policy",
            description2: "What can we do to become better?",
            skill2: "Please select at least 1 skill",
            gender2: "Please choose any one"
        },
        highlight: function (input) {
            $(input).parents('.form-line').addClass('error');
        },
        unhighlight: function (input) {
            $(input).parents('.form-line').removeClass('error');
        },
        errorPlacement: function (error, element) {
            $(element).parents('.form-group').append(error);
        }
    });

    //Advanced Form Validation
    $('#form_advanced_validation').validate({
        rules: {
            'date': {
                customdate: true
            },
            'creditcard': {
                creditcard: true
            }
        },
        highlight: function (input) {
            $(input).parents('.form-line').addClass('error');
        },
        unhighlight: function (input) {
            $(input).parents('.form-line').removeClass('error');
        },
        errorPlacement: function (error, element) {
            $(element).parents('.form-group').append(error);
        }
    });

    //Custom Validations ===============================================================================
    //Date
    $.validator.addMethod('customdate', function (value, element) {
        return value.match(/^\d\d\d\d?-\d\d?-\d\d$/);
    },
        'Please enter a date in the format YYYY-MM-DD.'
    );

    //Credit card
    $.validator.addMethod('creditcard', function (value, element) {
        return value.match(/^\d\d\d\d?-\d\d\d\d?-\d\d\d\d?-\d\d\d\d$/);
    },
        'Please enter a credit card in the format XXXX-XXXX-XXXX-XXXX.'
    );
    //==================================================================================================
});