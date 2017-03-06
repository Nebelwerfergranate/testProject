function Component(options) {
    this._$el = options.element;
    this._display = options.display;
    this._options = $.extend({}, this._options, options.options || {});
    this._endpoints = $.extend({}, this._endpoints, options.endpoints || {});
    this._components = {};
}

Component.prototype.getElement = function () {
    return this._$el;
}

Component.prototype.hide = function () {
    this._$el.addClass('hide');
}

Component.prototype.show = function () {
    this._$el.removeClass('hide');
}

Component.prototype.on = function (eventName, selector, data, handler) {
    this._$el.on(eventName, selector, data, handler);
}

Component.prototype.one = function (eventName, selector, data, handler) {
    this._$el.one(eventName, selector, data, handler);
};

Component.prototype._trigger = function (eventName, paramArr) {
    this._$el.trigger(eventName, paramArr);
}

// todo - maybe create default _render method in component.
// todo - maybe add display param (mobile, popover or something like this)
//  component should pick proper template on this param
// todo - maybe try a factory 