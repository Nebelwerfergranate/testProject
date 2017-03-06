//  Options
//      loginComponentOptions
//      registerComponentOptions
//  Components
//      loginComponent
//      registerComponent
function Sb_Auth(options) {
    Component.apply(this, arguments);
    
    this._initComponents();
}

Sb_Auth.prototype = Object.create(Component.prototype);

Sb_Auth.prototype._initComponents = function () {
    if (this._options.registerComponentOptions != null) {
        this._components.registerComponent = new Sb_Register(this._options.registerComponentOptions);
    }
    
    if (this._options.loginComponentOptions != null) {
        this._components.loginComponent = new Sb_Login(this._options.loginComponentOptions);
    }    
};