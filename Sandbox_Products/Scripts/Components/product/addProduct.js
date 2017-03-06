//  endpoints
//      addProduct
//  events
//      addProduct:success
//      addProduct:fail
function Sb_AddProduct(oprions) {
    Component.apply(this, arguments);

    this._template = $('#sb_addProduct_template').html();

    this._render();
    this._$form = $('[data-selector="addProduct-form"]');
    this._initHandlers();
    this._initValidation();
}

Sb_AddProduct.prototype = Object.create(Component.prototype);

Sb_AddProduct.prototype._render = function () {
    var html = _.template(this._template);
    this.getElement().html(html);
};

Sb_AddProduct.prototype._initHandlers = function () {
    this.on('submit', this._onsubmitHandler.bind(this));
    this.on('addProduct:success', this._onAddProductSuccesshandler.bind(this));
    this.on('addProduct:fail', this._onAddProductFailhandler.bind(this));
    this.on('click', '[data-command="toggle"]', this._onToggleCommandHandler.bind(this));
};

Sb_AddProduct.prototype._initValidation = function (options) {
    this._$form.validate(sb.validation.validationOptions);
};

Sb_AddProduct.prototype._onsubmitHandler = function (e) {
    e.preventDefault();

    var params = this._$form.serializeObject();
    var self = this;

    $.post(this._endpoints.addProduct, params, function (data) {
        if (data.result) {
            self._trigger('addProduct:success', data);
        }
        else {
            self._trigger('addProduct:fail', data);
        }
    });
};

Sb_AddProduct.prototype._onAddProductSuccesshandler = function (e) {
    $('[data-selector="form-error-message"]', this.getElement()).addClass('hide');
    this._$form[0].reset();
};

Sb_AddProduct.prototype._onAddProductFailhandler = function (e, data) {
    $('[data-selector="form-error-message"]', this.getElement())
            .removeClass('hide')
            .text(data.message);
};

Sb_AddProduct.prototype._onToggleCommandHandler = function () {
    this._$form.slideToggle();
};