//  options
//      rules
sb.validation = {
    init: function (rules) {
        sb.validation.validationOptions = {
            errorElement: 'label',
            errorClass: 'text-danger',
            rules: rules,
            highlight: function (element) {
                $(element).closest('.form-group').addClass('has-error');
            },
            unhighlight: function (element) {
                $(element).closest('.form-group').removeClass('has-error');
            },
            messages: {
                login: {
                    regex: sb_messages.REGISTER_LOGIN_REGEXP_ERROR_MESSAGE
                },
                productName: {
                    regex: sb_messages.PRODUCT_NAME_REGEXT_ERROR_MESSAGE
                }
            }
        };
        sb.validation.validationOptions.rules.confirmPassword.equalTo = '[name="password"]';
    }
};