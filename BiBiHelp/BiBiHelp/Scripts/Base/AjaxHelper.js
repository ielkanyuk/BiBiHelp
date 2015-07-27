window.AjaxHelper = (function () {
    return {
        IsLastAjaxSuccess: false,
        //завершился ли последний ajax запрос
        IsLastAjaxComplete: false,
        request: {},
        longQueryTime: 3000,
        timeToWaitBeforeShowLoading: 300, // через сколько показывать крутилку
        longQueryTimeAgain: 4000,
        //Массив, содержащий все ajaxRequest'ы относительно слоя, который являеися в данном случае ключом
        AjaxRequestsInLayer: {},
        arrayOpenedDialogs: [],

        //загружает по урл страницы контент
        LoadContent: function (options) {
            //var $this = options.$this;
            //var $this = null;
            //var proceedAjaxLoadContent = util.proceedAjaxLoadContent(arguments);
            //if (proceedAjaxLoadContent === false)
            //    return false;
            //else if (proceedAjaxLoadContent !== true) {
            //    $this = proceedAjaxLoadContent;
            //}
            //урл по которому будет происходить загрузка данных
            var loadUrl = options.loadUrl;

            //каким способом отправить данные на сервер
            var method = options.method;

            //данные, которые необходимо отослать серверу
            var postedData = options.postedData;

            //если тип ответа от сервера кастомный, то эта функция обработает полученный удачный ответ от сервера
            var successFunc = options.successFunc;

            //отвечает за форму отправки массивов на сервер
            var _traditional = options.traditional;

            // асинхронный ли запрос
            var async = (options.async === false) ? false : true;

            AjaxHelper.IsLastAjaxSuccess = false;
            AjaxHelper.IsLastAjaxComplete = false;

            //тип отправляемого запроса "GET" или "POST"
            var _method = !method ? "GET" : method;

            //AjaxHelper.AbortAjaxRequestByUrl(loadUrl);
            var setTimeOutID = 0,
                timerId,
                ajaxUrl = loadUrl;
            var xhrSettings = {
                url: loadUrl,
                type: _method,
                async: async,
                crossDomain: false,
                data: postedData,
                dataType: "json",
                traditional: _traditional,

                success: function (content) {
                    AjaxHelper.AjaxSuccessFunction(content, successFunc, loadUrl, postedData);
                },
                complete: function (content) {
                    //clearTimeout(timerId); // убираем крутилку текущего запроса
                    //debugger;
                    //AjaxHelper.UnLockContainer();
                    AjaxHelper.IsLastAjaxComplete = true;
                    AjaxHelper.LongQueryNotificateDone(loadUrl);
                    AjaxHelper.request[loadUrl] = null;
                    delete AjaxHelper.request[loadUrl];
                },
            };

            var ajaxRequest = jQuery.ajax(xhrSettings);

            AjaxHelper.request[loadUrl] = ajaxRequest;

            return ajaxRequest;
        },

        lastLoadedAjaxRequest: undefined,

        //функция, которая обрабатывает ответ от сервера с помощью класса AjaxResult
        AjaxSuccessFunction: function (content, successFunc, loadUrl, postedData) {

            if (!content) return;
            
            loadUrl = loadUrl || "";
            postedData = postedData || {};

            AjaxHelper.IsLastAjaxSuccess = true;
            var callbackCalled = false;
            var resultProcessed = true;

            if (Enums.hasFlag(content.ReturnViewType, Enums.ReturnViewType.JsonForTemplate)) {
                if (jQuery.isFunction(successFunc) && !callbackCalled) {
                    callbackCalled = true;
                    successFunc(content);
                }
            }
        },

        //Прерывание аякс запроса по Урлу
        AbortAjaxRequestByUrl: function (loadUrl, fromLongQuery) {
            if (AjaxHelper.CheckIsAbortableUrl(loadUrl.split("?")[0])) {
                if (AjaxHelper.request[loadUrl]) {
                    fromLongQuery = fromLongQuery || false;
                    AjaxHelper.request[loadUrl].abort();
                    if (!fromLongQuery)
                        AjaxHelper.LongQueryNotificateDone(loadUrl);
                }
            }
            if (_.include(AbortAvailableUrls.Lists, loadUrl.split("?")[0]))
                AjaxHelper.AbortAvailableRequests();

        },

        //Ф-ия, которая прерывает все незавершенные аякс - запросы, но разрешенные на прерываение
        AbortAvailableRequests: function () {
            $.map(AjaxHelper.request, function (val, i) {
                if (_.include(AbortAvailableUrls.Lists, i.split("?")[0])) {
                    val.abort();
                    AjaxHelper.request[i] = null;
                    AjaxHelper.LongQueryNotificateDone(i);
                    delete AjaxHelper.request[i];
                }
            });
        },

        //Прерывание аякс запроса по request
        AbortAjaxRequestByRequest: function (request) {
            loadUrl = loadUrl.split("?")[0];
            if (AjaxHelper.request[loadUrl] && AjaxHelper.request[loadUrl].length != 0 && _.include(AbortAvailableUrls.Urls, loadUrl))
                AjaxHelper.request[loadUrl].abort();
        },

        //Убиваем событие если запрос уже выполнен
        LongQueryNotificateDone: function (loadUrl) {
            if (AjaxHelper.request[loadUrl] && AjaxHelper.request[loadUrl].setTimeoutID > 0) {
                clearTimeout(AjaxHelper.request[loadUrl].setTimeoutID);
                AjaxHelper.LongQueryCancelByID(loadUrl);
            }
        },

        //Убиваем запрос, если пользователь отменил все..
        LongQueryCancelByElem: function (loadUrl, $this) {
            $this.closest(".message-wrap").find(".message-close").click();
            AjaxHelper.AbortAjaxRequestByUrl(loadUrl);
        },

        //Убиваем запрос, если пользователь отменил все..
        LongQueryCancelByID: function (loadUrl) {
            if (AjaxHelper.request[loadUrl] && AjaxHelper.request[loadUrl].length != 0) {
                var request = AjaxHelper.request[loadUrl];
                $("#iLongQuery_" + request.setTimeoutID).closest(".message-wrap").find(".message-close").click();
                AjaxHelper.AbortAjaxRequestByUrl(loadUrl, true);
            }
        }
    };
})();
