//  events
//      save:success
//      save:fail
//      cancel
//      error
//  endpoints
//      save
//      getProductOperations

function Sb_ProductTurnover(options) {
    Component.apply(this, arguments);
    this._template = $('#sb_productTurnover_template').html();
    this._productId = 0;
    this._productName = '';
    this._operations = [];
    this._getProductOperations();
    this._formSelector = '[data-selector="productTurnover-form"]';

    this._initHandlers();
}

Sb_ProductTurnover.prototype = Object.create(Component.prototype);

Sb_ProductTurnover.prototype.update = function (postData, productName) {
    this._productId = postData.productId;
    this._productName = productName;
    this._render();
    this._initValidation();
};

Sb_ProductTurnover.prototype._initValidation = function () {
    $(this._formSelector).validate(sb.validation.validationOptions);
};

Sb_ProductTurnover.prototype._getProductOperations = function () {
    var self = this;

    $.post(this._endpoints.getProductOperations, function (data) {
        if (data.result) {
            self._operations = data.items;
        }
        else {
            self._trigger('error', data);
        }
    });
};

Sb_ProductTurnover.prototype._render = function () {
    var options = {
        id: this._productId,
        name: this._productName,
        operations: this._operations
    };

    var html = _.template(this._template)(options);
    this.getElement().html(html);
};

Sb_ProductTurnover.prototype._initHandlers = function () {
    this.on('submit', this._onsubmitHandler.bind(this));
    this.on('save:success', this._onSaveSuccesshandler.bind(this));
    this.on('save:fail', this._onSaveFailHandler.bind(this));
    this.on('click', '[data-command="cancel"]', this._onCanceCommandlHandler.bind(this));
    this.on('error', this._onErrorHandler.bind(this));
};

Sb_ProductTurnover.prototype._onsubmitHandler = function (e) {
    e.preventDefault();

    var params = $(this._formSelector).serializeObject();
    var self = this;

    $.post(this._endpoints.save, params, function (data) {
        if (data.result) {
            self._trigger('save:success', data);
        }
        else {
            self._trigger('save:fail', data);
        }
    });
}

Sb_ProductTurnover.prototype._onSaveSuccesshandler = function () {
    $('[data-selector="form-error-message"]', this.getElement()).addClass('hide');
};

Sb_ProductTurnover.prototype._onSaveFailHandler = function (e, data) {
    $('[data-selector="form-error-message"]', this.getElement())
        .removeClass('hide')
        .text(data.message);
};

Sb_ProductTurnover.prototype._onCanceCommandlHandler = function () {
    this._trigger('cancel');
}

Sb_ProductTurnover.prototype._onErrorHandler = Sb_ProductTurnover.prototype._onSaveFailHandler;