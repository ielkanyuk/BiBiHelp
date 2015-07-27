// класс управлени поведением в списке
function ListAction(obj) {
    this.params = {
        containerID: null, 
        actionUrl: null,
        EntityInterface: null
    };
    $.extend(this.params, obj);

    this.container = $("#" + this.params.containerID);
};

// метод добавления новых атрибутов
ListAction.prototype.extend = function (obj) {
    $.extend(this.params, obj);
};

ListAction.prototype._getMessageList = function () {
    var self = this;

    AjaxHelper.LoadContent({
        method: "POST",
        loadUrl: self.params.actionUrl,
        traditional: true,
        postedData: self.params.EntityInterface.GetParams(),
        successFunc: function (dataFromServer) {
            //if (dataFromServer.ReturnViewType != Enums.ReturnViewType.OperationFailed) {
                
                TemplateManager.Rebuild(dataFromServer);
            //}
        }
    });
};

// Событие загрузки списка сообщений
ListAction.prototype.InitList = function () {
    var self = this;
    self._getMessageList();
};

ListAction.prototype._static = {
    deleteButton: "a.iDelete", // идентификатор кнопки "удалить"
    deleteMultipleButton: "a.iDeleteMultiple", // идентификатор кнопки "удалить" для множественного удаления элементов из списка
    resetFilterButton: "a.iResetFilter", // идентификатор кнопки "очистить" фильтр
    filterButton: "a.iFilter", // идентификатор кнопки "фильтр"
    pageSizeSelect: "#PageSize", // идентификатор выбора количества отображаемых элементов списка на странице
    pagerButton: ".paginator > a" // элементы пейджинга
};

