; app = typeof (app) === 'undefined' ? {} : app;

app.common = {
    currentHref: function () { return window.location.href; },
    currentPath: function () {
        var href = app.common.currentHref().replace("http://", "").replace("https://", "");
        //remove querystring
        var q = href.indexOf("?");
        if (q > 0) { href = href.substr(0, q); }
        return href;
    },
    siteroot: function () {
        var ret = "/";
        if (app.common.currentPath().contains("/email-templating/")) {
            ret = "/email-templating/";
        }
        return ret;
    },
    siteprefix: function () {
        if (window.location.hostname == "localhost") {
            return window.location.protocol + "//" + window.location.host;
        } else {
            return window.location.protocol + "//" + window.location.hostname;
        }
    },

    isJqueryObject: function (obj) {
        return (obj && obj.length);
    },
    isJQueryObject: function (obj) { return app.common.isJqueryObject(obj); },
    namespaces: function (namespace) {
        var obj = app;
        $.each(namespace.split("."), function (i, name) {
            if (name != "app") {
                if (obj[name] == "undefined" || obj[name] == null) { obj[name] = {}; }
                obj = obj[name];
            }
        });
    },
    getFunctionByName: function (functionName, context) {
        context = (typeof (context) === 'undefined' || context == null) ? window : context;
        var namespaces = functionName.split(".");
        var func = namespaces.pop();
        for (var i = 0; i < namespaces.length; i++) {
            context = context[namespaces[i]];
        }
        return context[func];
    },
    formatException: function (ex) {
        var ret = ex.toString();
        if (typeof (ex.stack) !== 'undefined') {
            console.log('stack', ex.stack);
        }

        return ret;
    },
    //for momentJS ...
    date_formats: ["MM-DD-YYYY", "YYYY-MM-DD"],
    datetime_formats: ["MM-DD-YYYY hh:mma", "YYYY-MM-DD hh:mma", "MM-DD-YYYY HH:mm", "YYYY-MM-DD HH:mm", "YYYY-MM-DD hh:mm:ss", moment.ISO_8601]
};

app.common.namespaces("app.dataservices");

//#region -- app.dataservices.helper --
app.dataservices.helper = function ($) {

    var _validateApiResult = function (result, errorHandler) {
        try {

            if (result == null) { throw "Invalid response from server: no data returned!"; }
            else if (typeof (result) === 'string') {

                if (result == "parsererror") {
                    throw "Error from server... Often, this means your session timed out and you will need to log in again!";
                } else {
                    throw "Unknown error from the server: " + result;
                }
            }
            else if (typeof (result) !== 'object') { throw "Invalid response from server: data was in an unknown format!"; }
            else if (!isWrappedResult(result)) {
                //public api
                if (typeof (result.Message) === 'string' && typeof (result.ExceptionMessage) === 'string') {
                    if (result.Message == result.ExceptionMessage) {
                        throw result.Message;
                    } else {
                        throw result.Message + ' ' + result.ExceptionMessage;
                    }
                }
            }
            else if (result.Status != "ok") {
                if (typeof (result.ErrorMessage) === "string") { throw "Error: " + result.ErrorMessage; }
                else { throw "Invalid response from server: unknown error!"; }
            }

            return true;

        } catch (e) {
            if (typeof (errorHandler) === 'undefined') {
                //console.log("passing it along", errorHandler);
                throw e;    //pass it along
            } else {
                errorHandler.apply(result, [e]);
            }
            return false;
        }

        //else .. all is ok
    };
    var isWrappedResult = function (result) {
        return typeof (result.IsWrapped) !== 'undefined' && typeof (result.IsWrapped) === 'boolean' && result.IsWrapped === true;
    };

    var _GetRaw = function (data, possibleKeys) {
        var ret = null;
        $.each(possibleKeys, function (index, key) {
            if (key in data) {
                ret = data[key];
                return false;   // break out of loop //
            }
        });
        return ret;
    };
    var _Get = function (data, possibleKeys, defaultValue) {
        if (typeof (possibleKeys) == 'string') { possibleKeys = new Array(possibleKeys); }
        if (typeof (defaultValue) == 'undefined') { defaultValue = ''; }

        var ret = _GetRaw(data, possibleKeys);
        if (ret == null) { ret = defaultValue; }
        return ret;
    };
    var _ValueExists = function (data, possibleKeys) {
        return _GetRaw(data, possibleKeys) != null;
    }

    var _formatAsDate = function (data) {
        if (data != null && data.length > 0) {
            var parts = data.split("T");
            return parts[0];
        } else {
            return "";
        }
    };

    return {
        validateApiResult: _validateApiResult,
        isWrappedResult: isWrappedResult,
        get: _Get,
        exists: _ValueExists,
        formatAsDate: _formatAsDate
    };
}(jQuery);


//#endregion

app.dataservices._requestJSON = function (method, url, data, _options) {
    var dfd = new $.Deferred();
    if (typeof (data) === 'undefined') { data = {}; }
    var options = $.extend(true, {}, {
        type: method,
        url: url,
        dataType: 'json',
        //contentType: 'application/json',
        data: data,
        cache: false,
        statusCode: {
            404: function () {
                dfd.reject("Unable to locate requested resource: page not found - " + url);
            }
        }
    }, _options);

    $.ajax(options)
        .done(function (result) {
            if (app.dataservices.helper.validateApiResult(result, function (msg) { dfd.reject(msg); })) {
                if (app.dataservices.helper.isWrappedResult(result)) {
                    dfd.resolve(result.Data);
                } else {
                    dfd.resolve(result);
                }
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            dfd.reject(app.dataservices._parseFail(jqXHR, textStatus, errorThrown, dfd));
        });
    return dfd.promise();
};

app.dataservices.getJSON = function (url, data, options) {
    return app.dataservices._requestJSON("GET", url, data, options);
};

app.dataservices.postJSON = function (url, data, options) {
    return app.dataservices._requestJSON("POST", url, data, options);
};

app.dataservices.putJSON = function (url, data, options) {
    return app.dataservices._requestJSON("PUT", url, data, options);
};

app.dataservices.deleteJSON = function (url, data, options) {
    return app.dataservices._requestJSON("DELETE", url, data, options);
};

app.dataservices._parseFail = function (jqXHR, textStatus, errorThrown) {
    console.log("_parseFail", arguments);

    if ($.isPlainObject(jqXHR)) {
        if (typeof (jqXHR.responseText) === "string" && jqXHR.responseText.contains("Log On")) {
            return "Your session has timed out... Please log in!";
        } else if (typeof (jqXHR.status) === 'number' && jqXHR.status == 500) {
            return "The server had a problem processing your request... please let the systems administrator know of this issue!";
        } else {
            return "Error processing your request... " + errorThrown;
        }
    } else {
        return textStatus + ": " + errorThrown;
    }
};

//=============================================================================//
//========== helper to auto-run functions on "ready" =====//
//=============================================================================//
app.init = {

    add: function (name, fn) {
        app.init[name] = fn;
    },
    run: function () {
        for (var property in app.init) {
            if (app.init.hasOwnProperty(property) && $.isFunction(app.init[property]) && property != 'run' && property != 'add') {
                app.init[property].call(app.init);
            }
        }
    }
}

$(function () {
    app.init.run();
});



//=============================================================================//
//========== Misc Functions  =====//
//=============================================================================//
window.parseBool = window.parseBool || function (obj) {
    if (typeof (obj) === 'undefined') { throw "Cannot parse boolean - missing argument."; }
    else if (obj === null) { throw "Cannot parse boolean - null object."; }
    else if (typeof (obj) === 'boolean') { return obj; }
    else if (typeof (obj) === 'string') { return obj.toLowerCase() == 'true'; }
    else if (typeof (obj) === 'number') { return obj > 0; }
    else { throw "Cannot parse boolean - invalid object type: " + typeof (obj); }
};


//=============================================================================//
//========== String Extensions =====//
//=============================================================================//

String.prototype.trim = function () {
    return $.trim(this);    //REQUIRES jQUERY
}

String.prototype.toLower = function () {
    return this.toLowerCase();
}

String.prototype.toUpper = function () {
    return this.toUpperCase();
}

String.prototype.startsWith = function (prefix) {
    if (prefix.length <= this.length && prefix.length > 0) {
        return (this.substr(0, prefix.length) == prefix);
    } else {
        return false;
    }
}
String.prototype.endsWith = function (suffix) {
    if (suffix.length <= this.length && suffix.length > 0) {
        return (this.substr(this.length - suffix.length, suffix.length) == suffix);
    } else {
        return false;
    }
}
String.prototype.contains = function (needle) {
    if (needle.length <= this.length && needle.length > 0) {
        return (this.indexOf(needle) >= 0);
    } else {
        return false;
    }
}

//=============================================================================//
//========== Array Extensions =====//
//=============================================================================//

Array.prototype.contains = function (needle) {
    return this.indexOf(needle) >= 0;
}
Array.prototype.trim = function () {
    for (var i = 0; i < this.length; i++) {
        if (typeof (this[i]) === 'string') { this[i] = this[i].trim(); }
    }
    return this;
}
