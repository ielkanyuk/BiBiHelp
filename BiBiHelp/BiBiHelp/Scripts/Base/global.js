(function (window) {

    window.Enums = window.Enums || {};

    // ToDo: реализовать JsEnumManager
    window.Enums.ReturnViewType = {
        JsonForTemplate: 1
    };

    window.Enums.hasFlag = function (enumForCheck, flag) {
        return !!(enumForCheck & flag);
    };

    window.Entities = window.Entities || {
        Message: {},
        User: {}
    };

    if (typeof Object.create !== 'function') {
        Object.create = function (obj) {
            function F() { };
            F.prototype = obj;
            return new F();
        };
    }
    
    window.util = {

        link: function (method, controller) {
            return "/" + controller + "/" + method;
        }
    };

})(window);