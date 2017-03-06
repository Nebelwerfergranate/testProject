//  endpoints
//      getProducts
//  options
//      pagerElementId
//      nameFormatterCallback
//      priceFormatterCallback
//      actionsFormatterCallback
function Sb_ProductList(options) {
    Component.apply(this, arguments);
    this._template = $('#sb_actions_template').html();
    this.SELECTED_CLASS = 'is-selected';

    this._render();
    this._initHandlers();
}

Sb_ProductList.prototype = Object.create(Component.prototype);

Sb_ProductList.prototype.getSelectedRow = function () {
    return this.getElement().find("." + this.SELECTED_CLASS);
};

Sb_ProductList.prototype.reloadGrid = function () {
    this._trigger('reloadGrid');
};

Sb_ProductList.prototype._render = function () {
    this.getElement().jqGrid({
        url: this._endpoints.getProducts,
        datatype: "json",
        colNames: ['Name', 'Cost', 'Actions'],
        colModel: [
            { name: 'Name', index: 'Name', width: 5, sortable: true, formatter: this._options.nameFormatterCallback },
            { name: 'Price', index: 'Price', width: 1, align: "center", sortable: true, formatter: this._options.priceFormatterCallback },
            { name: 'Id', index: 'Id', width: 2, align: 'center', sortable: false, formatter: this._options.actionsFormatterCallback }
        ],
        rowList: [10, 25, 50, 100],
        pager: this._options.pagerElementId,
        rowNum: 50, 
        loadonce: false,
        sortname: 'Name',
        sortorder: "asc", 
        caption: "Products",
        autowidth: true,
        shrinkToFit: true,
        fixed: true,
        height: '100%',
        beforeSelectRow: function (rowid, e) {
            return false; // remove default yellow highlight
        }
        //onSelectRow  can't use it because of inputs in table
    });
};

Sb_ProductList.prototype._initHandlers = function () {
    this.on('click', 'tr', this._onRowSelectedHandler.bind(this));
};

Sb_ProductList.prototype._onRowSelectedHandler = function (e) {
    this.getElement().find('tr').removeClass(this.SELECTED_CLASS);
    var $row = $(e.target).closest('tr').addClass(this.SELECTED_CLASS);
}