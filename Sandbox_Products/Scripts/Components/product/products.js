//  options
//      addProductComponentOptions
//      productListComponentOptions
//      productTurnoverComponentOptions
//      productStatisticComponentOptions
//      productOperationStatisticComponentOptions
//      perUserStatisticComponentOptions
//  endpoints
//      updateProduct
//      deleteProduct
//  components
//      addProductComponent
//      productList
//      productTurnoverComponent
//      productStatisticComponent
//      productOperationStatisticComponent
//      perUserStatisticComponent
//  events
//      productUpdate:success
//      productUpdate:fail
//      productDelete:success
//      productDelete:fail

function Sb_Products(options) {
    Component.apply(this, arguments);
    this._template = $('#sb_login_template').html();
    this._nameTemplate = $('#sb_productName_template').html();
    this._priceTemplate = $('#sb_productPrice_template').html();
    this._actionsTemplate = $('#sb_actions_template').html();
    
    this._modes = {
        view: 'actions-view',
        edit: 'actions-edit'
    };

    this._initComponents();
    this._initHandlers();
}

Sb_Products.prototype = Object.create(Component.prototype);

Sb_Products.prototype._initComponents = function () {
    if (this._options.addProductComponentOptions != null) {
        this._components.addProductComponent = new Sb_AddProduct(this._options.addProductComponentOptions);
    }

    if (this._options.productListComponentOptions != null) {
        this._options.productListComponentOptions.options.nameFormatterCallback = this._nameFormatterCallback.bind(this);
        this._options.productListComponentOptions.options.priceFormatterCallback = this._priceFormatterCallback.bind(this);
        this._options.productListComponentOptions.options.actionsFormatterCallback = this._actionsFormatterCallback.bind(this);
        this._components.productListComponent = new Sb_ProductList(this._options.productListComponentOptions);
    }

    if (this._options.productTurnoverComponentOptions != null) {
        this._components.productTurnoverComponent = new Sb_ProductTurnover(this._options.productTurnoverComponentOptions)
    }

    if (this._options.productStatisticComponentOptions != null) {
        this._components.productStatisticComponent = new Sb_ProductStatistic(this._options.productStatisticComponentOptions);
    }

    if (this._options.productOperationStatisticComponentOptions != null) {
        this._components.productOperationStatisticComponent = new Sb_ProdictOperationStatistic(this._options.productOperationStatisticComponentOptions);
    }

    if (this._options.perUserStatisticComponentOptions != null) {
        this._components.perUserStatisticComponent = new Sb_PerUserStatistic(this._options.perUserStatisticComponentOptions);
    }

    this._components.notificatorComponent = new Sb_Notificator({ element: $('[data-selector="form-error-message"]', this.getElement()) });
};

Sb_Products.prototype._initHandlers = function () {
    var self = this;

    this._components.addProductComponent.on('addProduct:success', function () {
        self._components.productListComponent.reloadGrid();
    });

    this.on('click', '[data-command="edit"]', this._onEditCommandHandler.bind(this));
    this.on('click', '[data-command="cancel"]', this._onCancelCommandHandler.bind(this));
    this.on('click', '[data-command="save"]', this._onSaveCommandHandler.bind(this));
    this.on('click', '[data-command="delete"]', this._onDeleteCommandHandler.bind(this));
    this.on('click', '[data-command="inventoryTurnover"]', this._onInventoryTurnoverCommandHandler.bind(this));
    this.on('click', '[data-command="statistics"]', this._onStatisticsCommandHandler.bind(this));
    this.on('click', '[data-command="perOperatorStatistic"]', this._onPerOperatorStatisticCommand.bind(this));
    this.on('click', 'tr', this._onRowSelectedHandler.bind(this));
    this.on('keypress', 'input', this._onkeypressHandler.bind(this));
   
    this.on('productUpdate:success', this._onProductUpdateSuccessHandler.bind(this));
    this.on('productUpdate:fail', this._onProductUpdateFailHandler.bind(this));
    this.on('productDelete:success', this._onProductDeleteSuccessHandler.bind(this));
    this.on('productDelete:fail', this._onProductDeleteFailHandler.bind(this));

    if (this._components.productTurnoverComponent != null) {
        this._components.productTurnoverComponent.on('save:success', this._onProductOperationSaveSuccessHandler.bind(this));
    }
};

Sb_Products.prototype._nameFormatterCallback = function (cellVal, options, rowObject) {
    var html = _.template(this._nameTemplate)({ value: cellVal });
    return $.trim(html);
};

Sb_Products.prototype._priceFormatterCallback = function (cellVal, options, rowObject) {
    var html = _.template(this._priceTemplate)({ value: cellVal });
    return $.trim(html);
};

Sb_Products.prototype._actionsFormatterCallback = function (cellVal, options, rowObject) {
    var html = _.template(this._actionsTemplate)({ id: cellVal })
    return $.trim(html);
};

Sb_Products.prototype._onEditCommandHandler = function (e) {
    var $row = $(e.target).closest('tr');
    $row.addClass('is-editing');
    this._setMode($row, this._modes.edit);

    var rowData = this._getRowData($row);
    rowData.$oldNameInput.val(rowData.name);
    rowData.$oldPriceInput.val(rowData.price);
    this._enableInputs($row);
};

Sb_Products.prototype._onCancelCommandHandler = function (e) {
    var $row = $(e.target).closest('tr');
    $row.removeClass('is-editing');
    this._setMode($row, this._modes.view);

    var rowData = this._getRowData($row);
    rowData.$nameInput.val(rowData.oldName);
    rowData.$priceInput.val(rowData.oldPrice);
    this._disableInputs($row);
    this._cleanError();
};

Sb_Products.prototype._onSaveCommandHandler = function (e) {
    var $row = $(e.target).closest('tr');
    var rowData = this._getRowData($row);
    var token = this._getToken();
    var params = {
        id: this._getId($row),
        name: rowData.name,
        price: rowData.price,
        __RequestVerificationToken: token
    };
    var self = this;
    
    $.post(this._endpoints.updateProduct, params, function (data) {
        if (data.result) {
            self._trigger('productUpdate:success', [data, $row]);
        }
        else {
            self._trigger('productUpdate:fail', [data, $row]);
        }
    });
};

Sb_Products.prototype._onDeleteCommandHandler = function (e) {
    var $row = $(e.target).closest('tr');
    sb.confirm(sb_messages.PRODUCT_DELETE_CONFIRM_MESSAGE, this._onDeleteConfirmedHandler.bind(this, $row));
};

Sb_Products.prototype._onDeleteConfirmedHandler = function ($row, e) {
    var self = this;
    var params = {
        id: this._getId($row),
        __RequestVerificationToken: this._getToken()
    };

    $.post(this._endpoints.deleteProduct, params, function (data) {
        if (data.result) {
            self._trigger('productDelete:success', [data, $row]);
        }
        else {
            self._trigger('productDelete:fail', [data, $row]);
        }
    });
}

Sb_Products.prototype._onInventoryTurnoverCommandHandler = function (e) {
    this._triggerModalComponent(e, this._components.productTurnoverComponent);  
};

Sb_Products.prototype._onStatisticsCommandHandler = function (e) {
    this._triggerModalComponent(e, this._components.productOperationStatisticComponent);  
};

Sb_Products.prototype._onPerOperatorStatisticCommand = function (e) {
    this._triggerModalComponent(e, this._components.perUserStatisticComponent);
};

Sb_Products.prototype._onProductOperationSaveSuccessHandler = function () {
    var $modal = this._components.productTurnoverComponent.getElement();
    $modal.modal('hide');

    var componentsExist = this._components.productListComponent != null && this._components.productStatisticComponent != null;

    if (componentsExist) {
        var $row = this._components.productListComponent.getSelectedRow();
        var rowData = this._getRowData($row);
        this._components.productStatisticComponent.update(rowData.id);
    }    
};

Sb_Products.prototype._onRowSelectedHandler = function (e) {
    var $row = $(e.target).closest('tr');
    var rowData = this._getRowData($row);

    this._components.productStatisticComponent.update(rowData.id);
};

Sb_Products.prototype._onkeypressHandler = function (e) {
    if (e.which == 13 || e.keyCode == 13) {
        $(e.target).closest('tr').find('[data-command="save"]').trigger('click');

        return false;
    }

    return true;
};

Sb_Products.prototype._onProductUpdateSuccessHandler = function (e, data, $row) {    
    $row.removeClass('is-editing');
    this._setMode($row, this._modes.view);
    this._disableInputs($row);

    this._cleanError(); 
};

Sb_Products.prototype._onProductUpdateFailHandler = function (e, data, $row) {
    this._addError( $row, data);
};

Sb_Products.prototype._onProductDeleteSuccessHandler = function (e, data, $row) {
    this._components.productListComponent.reloadGrid();
    this._cleanError();
};

Sb_Products.prototype._onProductDeleteFailHandler = function (e, data, $row) {
    this._addError($row, data);
}

Sb_Products.prototype._setMode = function ($row, mode) {
    var $actions = $row.find('.actions');
    var modesArr = [];

    for (var m in this._modes) {
        modesArr.push(this._modes[m]);
    }
    $actions.removeClass(modesArr.join(' '));       

    $actions.addClass(mode);
};

Sb_Products.prototype._getRowData = function ($row) {
    var rowData = {
        id: $row.find('[data-selector="actions"]').attr('data-id'),

        $nameInput: $row.find('[data-selector="name"]'),
        $oldNameInput: $row.find('[data-selector="oldName"]'),
        
        $priceInput:  $row.find('[data-selector="price"]'),
        $oldPriceInput: $row.find('[data-selector="oldPrice"]')
    };

    rowData.name = rowData.$nameInput.val();
    rowData.oldName =rowData.$oldNameInput.val();
    rowData.price = rowData.$priceInput.val();
    rowData.oldPrice = rowData.$oldPriceInput.val();

    return rowData;
};

Sb_Products.prototype._getId = function ($row) {
    return $('[data-selector="actions"]', $row).attr('data-id');
};

Sb_Products.prototype._getToken = function () {
    return $('input[name="__RequestVerificationToken"]', this.getElement()).val();
};

Sb_Products.prototype._enableInputs = function ($row) {
    var rowData = this._getRowData($row);
    rowData.$nameInput.attr('readonly', false);
    rowData.$priceInput.attr('readonly', false);
};

Sb_Products.prototype._disableInputs = function ($row) {
    var rowData = this._getRowData($row);
    rowData.$nameInput.attr('readonly', true);
    rowData.$priceInput.attr('readonly', true);
};

Sb_Products.prototype._addError = function ($row, data) {
    this._components.notificatorComponent.showAlert(data);
    $row.addClass('hasError');
};

Sb_Products.prototype._cleanError = function () {    
    if (this._components.productListComponent != null) {
        this._components.productListComponent.getElement().find('tr').removeClass('hasError');
    }
};

Sb_Products.prototype._triggerModalComponent = function (e, component) {
    if (component != null) {
        var $row = $(e.target).closest('tr');
        var rowData = this._getRowData($row);

        component.update({ productId: rowData.id }, rowData.name);

        var $modal = component.getElement();
        $modal.modal();
    }
};