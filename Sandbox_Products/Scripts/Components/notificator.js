function Sb_Notificator(options) {
    Component.apply(this, arguments);
    this._template = $('#sb_notificator_template').html();
    this._errors = {};
}

Sb_Notificator.prototype = Object.create(Component.prototype);

Sb_Notificator.prototype.addError = function (name, data) {
    this._errors[name] = {
        message: data.message,
        errors: data.errors
    };

    this._render();
};

Sb_Notificator.prototype.cleanError = function (name) {
    delete this._errors[name];
    this._render();
};

Sb_Notificator.prototype.showAlert = function (data) {
    var message = _.template(this._template)(data);

    $.bootstrapGrowl(message, {
        type: 'danger',
        delay: 0
    });
}

Sb_Notificator.prototype._render = function () {
    if (isEmpty(this._errors)) {
        this.hide();
    } else {
        var htmlArr = [];
        
        for (var error in this._errors) {
            htmlArr.push(_.template(this._template)(this._errors[error]));
        }

        this.getElement().html(htmlArr.join(''));
        this.show();
    }    
};