(function () {
    window.TemplateManager = {
        //добавляем данные о задаче в контейнер
        AppendDataToContainer: function (options, generateEvent, undefined) {
            if (generateEvent == undefined)
                generateEvent = true;

            var templateFunction = options.tmplCacheKey,
                jsonData = options.jsonData,
                placeholder = options.placeholder,
                isClear = options.isClear,
                toEnd = options.ToContainerEnd,
                insertAfter = options.insertAfter; // jquery селектор
            //если потребовали очистки то очищаем контейнер
            if (isClear)
                $(placeholder).empty();

            var tmplOptions = {
                tmplOptionsConstructor: function (array) {
                    return {
                        dataArrayIndex: function (item) {
                            return $.inArray(item, array);
                        },

                        arrayLength: array.length
                    };
                },
                generateOptions: function (array) {
                    return $.extend(tmplOptions, tmplOptions.tmplOptionsConstructor(array));
                }
            };

            var tmplHtml;
            try {
                tmplHtml = TemplateManager.generateViewByTemplate(templateFunction, jsonData);
            } catch (ex) {
                console.dir(ex);
                console.dir(jsonData);
                console.dir(placeholder);
                console.dir(templateFunction);
            }

            $(placeholder).each(function () {
                var $this = $(this);
                if (toEnd && $this.children().length > 0) {
                    $this.children(":last").after(tmplHtml);
                } else {
                    if (insertAfter == undefined || insertAfter == "")
                        $this.prepend(tmplHtml);
                    else {
                        $this.find(insertAfter).after(tmplHtml);
                    }
                }

                //когда вставляет данные в контейнер то перерисовываем скролл
                //var api = $this.closest(".scroll-pane").data("jsp");
                //if (api) {
                //    api.reinitialise();
                //}
            });

            // сообщаем всему миру что список перестроен
            //if (generateEvent)
            //    $(placeholder).trigger("ready");
        },

        //парсим данные которые пришли отдали в getGenericTemplateManager
        ParseDataFromServer: function (dataForTemplate) {
            //если отдали уже распарсенные данные
            if ($.isPlainObject(dataForTemplate) && dataForTemplate.TmplMgrInfo) {
                return dataForTemplate;
            } else { //если напрямую отдали то что пришло с сервера
                var tempJson = util.GetValueFromAjaxResult(dataForTemplate);
                if (!tempJson || !tempJson.TmplMgrInfo) {
                    throw "Не удалось распарсить dataForTemplate переданный в getGenericTemplateManager";
                }

                return tempJson;
            }
        },

        //кеш для jquery шаблонов
        storage: {
            jqTemplateCache: {},
            localStorageProxy: false,
            getLocalStorage: function () {
                if (TemplateManager.storage.localStorageProxy === false) {
                    TemplateManager.storage.localStorageProxy = new Storage();
                }
                return TemplateManager.storage.localStorageProxy;
            },
            get: function (key) {
                var value = TemplateManager.storage.jqTemplateCache[key];
                if (value) {
                    return value;
                }
                else {
                    value = TemplateManager.storage.getLocalStorage().get(key);
                    if (value) {
                        value = Function("return " + value)(); // в localstorage хранятся строки, создаем функцию
                        TemplateManager.storage.jqTemplateCache[key] = value;
                        return value;
                    }
                    else {
                        return undefined;
                    }
                }
            },
            set: function (key, value) {
                TemplateManager.storage.jqTemplateCache[key] = value;
                TemplateManager.storage.getLocalStorage().set(key, value.toString());
            }
        },

        //валидация данных пришедших с сервера. Надо стобы был хотя бы пустой массив.
        ValidateData: function (data) {
            if (!data)
                throw "Не присланы данные. Если данных нет то должен отдаваться пустой массив";
            if (!$.isArray(data))
                throw "В качестве данных может быть только массив.";
        },

        //получение шаблона из кеша или загрузка с сервера //ToDo: тут нужно реализовать забор шаблона из кэша
        GetTemplate: function (templateUrl) {
            if (!templateUrl)
                throw "Не задан урл шаблона. Урл шаблона должен быть обязательно задан.";
            templateUrl = templateUrl.toLowerCase();
            //если шаблон есть в кеше
            if (!TemplateManager.storage.get(templateUrl)) {
                $.ajax({
                    url: templateUrl,
                    async: false,
                    cache: true,
                    success: function (jQtmpl) {
                        TemplateManager.storage.set(templateUrl, TemplateManager.renderTemplate(jQtmpl));
                    }
                });
            }
            return TemplateManager.storage.get(templateUrl);
        },

        // Обновить
        Rebuild: function (dataFromServer) {
            var data = $.parseJSON(dataFromServer.JsonForTemplate);
            var tmplMgrInfo = data.TmplMgrInfo;
            TemplateManager.AppendDataToContainer({
                tmplCacheKey: TemplateManager.GetTemplate(tmplMgrInfo.TemplateUrl),
                jsonData: data.Data,
                placeholder: data.TmplMgrInfo.Placeholder,
                isClear: true,
                toEnd: false
            });
        },

        renderTemplate: function (templatePattern) {
            return doT.template(templatePattern, TemplateManager.templateSettings);
        },

        // получить jquery элемент по шаблону
        generateElementByTemplate: function (templateFunction, data) {
            return $(TemplateManager.generateViewByTemplate(templateFunction, data));
        },

        // получить строку html по шаблону
        generateViewByTemplate: function (templateFunction, data) {
            var view = "";
            //if ($.isArray(data)) {
            var length = data.length;
            for (var i = 0; i < length; ++i) {
                view += templateFunction(data[i], { array: data });
            }
            //}
            //else {
            //    view = templateFunction(data, { array: data });
            //}
            return view;
        },

        //подходит ли то что прислано с сервера для использоватния TemplateManager'ом
        //IsDataForGenericTemplateManager: function (dataFromServer) {
        //    if ($.isPlainObject(dataFromServer) &&
        //        dataFromServer.ReturnViewType &&
        //            (
        //                (dataFromServer.ReturnViewType & Enums.ReturnViewType.JsonForTemplate) == Enums.ReturnViewType.JsonForTemplate ||
        //                    (dataFromServer.ReturnViewType & Enums.ReturnViewType.JsonForTemplateAndNotificationMessage) == Enums.ReturnViewType.JsonForTemplateAndNotificationMessage ||
        //                        (dataFromServer.ReturnViewType & Enums.ReturnViewType.Layer) == Enums.ReturnViewType.Layer
        //            ) && util.isJSON(dataFromServer.JsonForTemplate)) {
        //        var jsonForTemplate = util.GetValueFromAjaxResult(dataFromServer);
        //        if (jsonForTemplate && jsonForTemplate.TmplMgrInfo && jsonForTemplate.TmplMgrInfo.TmplMgrId) {
        //            return true;
        //        }
        //    }
        //    return false;
        //},

        //задан ли идентификатор родительского элемента
        //IsParentIdDefined: function (parentIds) {
        //    parentIds = util.convertOneValueToArrayIfNotArray(parentIds);
        //    return !((parentIds && parentIds.length && parentIds.length == 1 && $.Guid.IsEmpty(parentIds[0])) || (!parentIds));
        //},

        templateSettings: {
            evaluate: /\{\{([\s\S]+?\}?)\}\}/g,
            interpolate: /\{\{=([\s\S]+?)\}\}/g,
            encode: /\{\{!([\s\S]+?)\}\}/g,
            use: /\{\{#([\s\S]+?)\}\}/g,
            useParams: /(^|[^\w$])def(?:\.|\[[\'\"])([\w$\.]+)(?:[\'\"]\])?\s*\:\s*([\w$\.]+|\"[^\"]+\"|\'[^\']+\'|\{[^\}]+\})/g,
            define: /\{\{##\s*([\w\.$]+)\s*(\:|=)([\s\S]+?)#\}\}/g,
            defineParams: /^\s*([\w$]+):([\s\S]+)/,
            conditional: /\{\{\?(\?)?\s*([\s\S]*?)\s*\}\}/g,
            iterate: /\{\{~\s*(?:\}\}|([\s\S]+?)\s*\:\s*([\w$]+)\s*(?:\:\s*([\w$]+))?\s*\}\})/g,
            varname: 'it',
            optionsName: 'options',
            strip: true,
            append: true,
            selfcontained: false
        }
    };
})();