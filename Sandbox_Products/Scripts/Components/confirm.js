//  events
//      confirm
//  options
//      global
function Sb_Confirm(options) {
    Component.apply(this, arguments);
    this._template = $('#sb_confirm_template').html();

    if (this._options.global != null) {
        this._options.global.confirm = this.confirm.bind(this);
    }

    this._initHandlers();
}

Sb_Confirm.prototype = Object.create(Component.prototype);

//  options
//      type
Sb_Confirm.prototype.confirm = function (message, callback, options) {
    if (this._isActive()) {
        throw new Error(sb_messages.CONFIRM_IS_SHOWN_ERROR_MESSAGE);
    }

    var options = options || {};
    var html = _.template(this._template)({
        message: message,
        type: options.type || 'danger'
    });

    this.one('confirm', callback);

    $modal = this.getElement();
    $modal.html(html);
    $modal.modal();
};

Sb_Confirm.prototype._initHandlers = function () {
    this.on('click', '[data-command="confirm"]', this._onConfirmHandler.bind(this));
};

Sb_Confirm.prototype._onConfirmHandler = function() {
    this._trigger('confirm');
};

Sb_Confirm.prototype._isActive = function () {
    return this.getElement().hasClass('in')
};