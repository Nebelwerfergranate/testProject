//  endpoints
//      getPerUserOperationsSum
//  options
//      productOperationStatisticComponentOptions
//  events
//      getPerUserOperationsSum:success
//      error
function Sb_PerUserStatistic(options) {
    Component.apply(this, arguments);
    this._template = $('#sb_perUserStatistic_template').html();
    this._resetButtonTemplate = $('#sb_resetButtonTemplate').html();
    this._table = null;
    this._productId;

    this._render();
    this._initComponents();
    this._initHandlers();
}

Sb_PerUserStatistic.prototype = Object.create(Component.prototype);

Sb_PerUserStatistic.prototype.update = function (postData, caption) {
    this._productId = postData.productId;
    this._table.jqGrid('setGridParam', { postData: postData }).trigger("reloadGrid");
    this._table.jqGrid('setCaption', caption + this._resetButtonTemplate);
    this._table.setGridWidth(this.getElement().find('.modal-dialog').width() - sb_constants.TABLE_PADDING_IN_MODAL * 2);
};

Sb_PerUserStatistic.prototype._render = function () {
    var html = _.template(this._template);
    this.getElement().html(html);
    this._table = $('#perUserOperationsList');
    var self = this;

    this._table.jqGrid({
        url: this._endpoints.getPerUserOperationsSum,
        datatype: "json",
        colNames: ['Operator', 'Sum'],
        colModel: [
            { name: 'Operator', index: 'Operator', width: 5, sortable: true },
            { name: 'Sum', index: 'Sum', width: 1, align: 'center', sortable: true, search: false },
        ],
        rowList: [10, 25, 50, 100],
        pager: '#perUserOperationsPager',
        rowNum: 10,
        loadonce: false,
        sortname: 'Operator',
        sortorder: "asc",
        caption: "Product name",
        autowidth: true,
        shrinkToFit: true,
        height: '100%',
        loadComplete: function (data) {
            if (data.result) {
                self._trigger('getPerUserOperationsSum:success');
            } else {
                self._trigger('error', data);
            }
        },
        subGrid: true,
        reloadOnExpand: false,
        subGridRowExpanded: this._subGridRowExpandedCallback.bind(this)
    });

    this._table.jqGrid('filterToolbar', {});
};


Sb_PerUserStatistic.prototype._initComponents = function () {
    this._components.notificatorComponent = new Sb_Notificator({ element: $('[data-selector="form-error-message"]', this.getElement()) });
};

Sb_PerUserStatistic.prototype._initHandlers = function () {
    this.on('getProductOperations:succes', this._onLoadSuccessHandler.bind(this));
    this.on('error', this._onErrorHandler.bind(this));
    this.on('click', '[data-command="clear"]', this._onClearCommandHandler.bind(this));
};

Sb_PerUserStatistic.prototype._onLoadSuccessHandler = function () {
    this._components.notificatorComponent.cleanError('load');
};

Sb_PerUserStatistic.prototype._onErrorHandler = function (e, data) {
    this._components.notificatorComponent.addError('load', data);
};

Sb_PerUserStatistic.prototype._onClearCommandHandler = function (e) {
    var tableId = this._table.attr('id');
    var isSubgrid = false;

    $(e.target).parents('table').each(function (index, value) {
        if (value.id === tableId) {
            isSubgrid = true;

            return false;
        }
    }); 

    if (isSubgrid) {

        return
    }
    this._table[0].clearToolbar();
};

Sb_PerUserStatistic.prototype._subGridRowExpandedCallback = function (subgrid_id, row_id) {
    var options = this._options.productOperationStatisticComponentOptions; 

    if (options == null) {

        return;
    }

    var subgrid_table_id;
    subgrid_table_id = subgrid_id + "_t";            

    options.element = $("#" + subgrid_id);
    options.display = "subgrid";
    options.options = $.extend({}, options.options, {
        tableId: subgrid_table_id
    });

    var subgrid = new Sb_ProdictOperationStatistic(options);
    var operator = this._table.getRowData(row_id).Operator;
    var postData = {
        productId: this._productId,
        OperatorStrict: operator
    }
    subgrid.hideCol(['Operator']);
    subgrid.update(postData, operator);
};