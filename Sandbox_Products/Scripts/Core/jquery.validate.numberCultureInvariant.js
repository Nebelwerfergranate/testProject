// CultureInfo.InvariantCulture uses . as a decimal separator, and , as a thousands separator.  
// That is why coma fails on back end. 

//$.validator.methods.range = function (value, element, param) {
//	var globalizedValue = value.replace(",", ".");
//	return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
//}

//$.validator.methods.number = function (value, element) {
//	return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
//}