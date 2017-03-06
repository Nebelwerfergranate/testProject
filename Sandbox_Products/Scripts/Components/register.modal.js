//  endpoints
//      mainUrl
function Sb_RegisterModal(options) {
	Component.apply(this, arguments);
	this._template = $('#sb_registerModalBody_template').html();
}

Sb_RegisterModal.prototype = Object.create(Component.prototype);

Sb_RegisterModal.prototype.showMessage = function (options) {
	options.link = this._endpoints.mainUrl;

	var html = _.template(this._template)(options);	

	$modal = this.getElement();
	$modal.html(html);
	$modal.modal({
		backdrop: 'static',
		keyboard: false
	});
};