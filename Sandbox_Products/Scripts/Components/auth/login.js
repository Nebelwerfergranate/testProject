//  events
//      login:success
//      login:fail
//  endpoints
//      loginUrl
function Sb_Login(options) {    
    Component.apply(this, arguments);
    this._template = $('#sb_login_template').html();

    this._render();
    this._$form = $('[data-selector="login-form"]');
    this._initHandlers();
    this._initValidation();
}

Sb_Login.prototype = Object.create(Component.prototype);

Sb_Login.prototype._render = function () {
    var html = _.template(this._template);
    this.getElement().html(html);
};

Sb_Login.prototype._initHandlers = function () {
    this.on('submit', this._onsubmitHandler.bind(this));
    this.on('login:success', this._onLoginSuccessHandler.bind(this));
    this.on('login:fail', this._onLoginFailHandler.bind(this));
};

Sb_Login.prototype._initValidation = function () {
    this._$form.validate(sb.validation.validationOptions);
};

Sb_Login.prototype._onsubmitHandler = function (e) {
    e.preventDefault();

    var params = this._$form.serializeObject();
    var self = this;

    $.post(this._endpoints.loginUrl, params, function (data) {
        if (data.result) {
            self._trigger('login:success', data);
        }
        else {
            self._trigger('login:fail', data);
        }
    });
};

Sb_Login.prototype._onLoginSuccessHandler = function (e) {
    $('[data-selector="form-error-message"]', this.getElement()).addClass('hide');
};

Sb_Login.prototype._onLoginFailHandler = function (e, data) {
    $('[data-selector="form-error-message"]', this.getElement())
            .removeClass('hide')
            .text(data.message);
};