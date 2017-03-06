//  endpoints
//      getProductDetail
//  events
//      getProductDetail:success
//      error
function Sb_ProductStatistic(options) {
    Component.apply(this, arguments);
    this._template = $('#sb_productStatistic_template').html();

    this._initHandlers();
}

Sb_ProductStatistic.prototype = Object.create(Component.prototype);

Sb_ProductStatistic.prototype.update = function (productId) {
    var self = this;
    var options = {
        id: productId
    };

    $.post(this._endpoints.getProductDetail, options, function (data) {
        if (data.result) {
            self._trigger('getProductDetail:success', data);
        }
        else {
            self._trigger('error', data);
        }
    });    
};

Sb_ProductStatistic.prototype._render = function (options) {
    var html = _.template(this._template)(options);
    this.getElement().html(html);
};

Sb_ProductStatistic.prototype._initHandlers = function () {
    this.on('getProductDetail:success', this._getProductDetailSuccessHandler.bind(this));
    this.on('error', this._onErrorHandler.bind(this));
};

Sb_ProductStatistic.prototype._getProductDetailSuccessHandler = function (e, data) {
    this._render(data.model);
    $('[data-selector="form-error-message"]', this.getElement()).addClass('hide');
};

Sb_ProductStatistic.prototype._onErrorHandler = function (e, data) {
    $('[data-selector="form-error-message"]', this.getElement())
        .removeClass('hide')
        .text(data.message);
};