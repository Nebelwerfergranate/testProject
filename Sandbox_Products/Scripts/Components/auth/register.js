//  events  
//      register:success
//      register:fail
//  endpoints
//      registerUrl
function Sb_Register(options) {
    Component.apply(this, arguments);
    this._template = $('#sb_register_template').html();

    this._render();
    this._$form = $('[data-selector="register-form"]');
    this._initHandlers();
    this._initValidation();
}

Sb_Register.prototype = Object.create(Component.prototype);

Sb_Register.prototype._render = function () {
    var html = _.template(this._template);
    this.getElement().html(html);
};

Sb_Register.prototype._initHandlers = function () {
    this.on('submit', this._onsubmitHandler.bind(this));
    this.on('register:success', this._onRegisterSuccessHandler.bind(this));
    this.on('register:fail', this._onRegisterFailHandler.bind(this));
};

Sb_Register.prototype._initValidation = function () {
    this._$form.validate(sb.validation.validationOptions);
};

Sb_Register.prototype._onsubmitHandler = function (e) {
    e.preventDefault();

    if (this._$form.valid()) {
        var params = this._$form.serializeObject();
        var self = this;

        $.post(this._endpoints.registerUrl, params, function (data) {
            if (data.result) {
                self._trigger('register:success', data);
            }
            else {
                self._trigger('register:fail', data);
            }
        })
    }
};

Sb_Register.prototype._onRegisterSuccessHandler = function (e) {
    $('[data-selector="form-error-message"]', this.getElement()).addClass('hide');
};

Sb_Register.prototype._onRegisterFailHandler = function (e, data) {
    $('[data-selector="form-error-message"]', this.getElement())
            .removeClass('hide')
            .text(data.message);
};