function ListEvent(obj) { // события для списков

    var listAction = new ListAction(obj);

    this.init = function () {
        listAction.InitList();
    };
}