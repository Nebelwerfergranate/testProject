//  endpoints
//      getProductOperations
//      getProductOperationsTypesHtml
//  events
//      getProductOperations:succes
//      error
//  options
//      tableId
//  display
//      subgrid
function Sb_ProdictOperationStatistic(options) {
    this._options = {
        tableId: "productOperationList"
    };
    Component.apply(this, arguments);

    this._setTemplate();
    this._tableSelector = "#" + this._options.tableId;
    this._resetButtonTemplate = $('#sb_resetButtonTemplate').html();

    this._render();
    this._initComponents();
    this._initHandlers();
}

Sb_ProdictOperationStatistic.prototype = Object.create(Component.prototype);

Sb_ProdictOperationStatistic.prototype.update = function (postData, caption) {
    this._getTable().jqGrid('setGridParam', {
        postData: postData,
        datatype: 'json'
    }).trigger("reloadGrid");
    this._getTable().jqGrid('setCaption', caption + this._resetButtonTemplate);
    this._getTable().setGridWidth(this._getTableWidth());
};

Sb_ProdictOperationStatistic.prototype.hideCol = function (colArr) {
    this._getTable().jqGrid('hideCol', colArr);
};

Sb_ProdictOperationStatistic.prototype._setTemplate = function () {
    switch (this._display) {
        case 'subgrid':
            this._template = $('#sb_productOperationStatistic_subgrid_template').html();
            break;
        default:
            this._template = $('#sb_productOperationStatistic_template').html();
            break;
    }
};

Sb_ProdictOperationStatistic.prototype._render = function () {
    var options = { tableId: this._options.tableId };
    var html = _.template(this._template)(options);
    this.getElement().html(html);
    var self = this;

    this._getTable().jqGrid({
        url: this._endpoints.getProductOperations,
        datatype: "local",
        colNames: ['Operator', 'Operation', 'Count', 'Date'],
        colModel: [
            { name: 'Operator', index: 'Operator', width: 5, sortable: true },
            {
                name: 'Operation', index: 'Operation', width: 1, align: 'center', sortable: true, stype: "select",
                cellattr: function(rowId, tv, rawObject, cm, rdata){
                    var color = rawObject.DisplayColor || "inherit";
                    return 'style="color:' + color + '"';
                },
                searchoptions: { dataUrl: this._endpoints.getProductOperationsTypesHtml, attr: { title: 'Select Date' } }
            },
            { name: 'Count', index: 'Count', width: 1, align: 'center', sortable: true, search: false },
            { name: 'Date', index: 'Date', width: 3, align: 'center', sortable: true, stype: "datetimerange" }
        ],
        rowList: [10, 25, 50, 100],
        pager: '#productOperationPager',
        rowNum: 50,
        loadonce: false,
        sortname: 'Operator',
        sortorder: "asc",
        caption: "Products Operations",
        autowidth: true,
        shrinkToFit: true,
        height: '100%',
        loadComplete: function (data) {
            if (data.result) {
                self._trigger('getProductOperations:succes');
            } else {
                self._trigger('error', data);
            }
        },serializeGridData: function (postData) {
            postData.DateFrom = self.getElement().find('[name="DateFrom"]', self.getElement()).val();
            postData.DateTo = self.getElement().find('[name="DateTo"]', self.getElement()).val();

            return postData;
        },
        gridComplete: function () {
            var options = {
                autoclose: true,
                todayBtn: true,
                minuteStep: 15
            };

            $dateFrom = $('[name="DateFrom"]', self.getElement());
            $dateTo = $('[name="DateTo"]', self.getElement());            

            $dateFrom.datetimepicker(options);
            $dateTo.datetimepicker(options);

            $dateFrom.on('changeDate', function (e) {
                if (e.date.valueOf() > $dateTo.datetimepicker("getDate").valueOf()) {
                    $dateTo.val('');
                }

                self._getTable().trigger("reloadGrid");
            });

            $dateTo.on('changeDate', function (e) {
                if (e.date.valueOf() < $dateFrom.datetimepicker("getDate").valueOf()) {
                    $dateFrom.val('');
                }

                self._getTable().trigger("reloadGrid");
            });
        }
    });
    this._getTable().jqGrid('filterToolbar', {});
};

Sb_ProdictOperationStatistic.prototype._initComponents = function () {
    this._components.notificatorComponent = new Sb_Notificator({ element: $('[data-selector="form-error-message"]', this.getElement()) });
};

Sb_ProdictOperationStatistic.prototype._initHandlers = function () {
    this.on('getProductOperations:succes', this._onLoadSuccessHandler.bind(this));
    this.on('error', this._onErrorHandler.bind(this));
    this.on('click', '[data-command="clear"]', this._onClearCommandHandler.bind(this));
};

Sb_ProdictOperationStatistic.prototype._onLoadSuccessHandler = function () {
    this._components.notificatorComponent.cleanError('load');
};

Sb_ProdictOperationStatistic.prototype._onErrorHandler = function (e, data) {
    this._components.notificatorComponent.addError('load', data);
};

Sb_ProdictOperationStatistic.prototype._getTable = function () {
    return $(this._tableSelector);
};

Sb_ProdictOperationStatistic.prototype._getTableWidth = function () {
    var result = 0;

    switch (this._display) {
        case 'subgrid':
            result = this.getElement().closest('td').width() - sb_constants.TABLE_PADDING_IN_TABLE_CELL * 2;
            break;
        default:
            result = this.getElement().find('.modal-dialog').width() - sb_constants.TABLE_PADDING_IN_MODAL * 2;
            break;
    }

    return result;
};

Sb_ProdictOperationStatistic.prototype._onClearCommandHandler = function (e) {
    this._getTable()[0].clearToolbar();
};

