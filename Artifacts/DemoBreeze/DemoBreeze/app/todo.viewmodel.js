﻿/* Defines the Todo application ViewModel */
window.todoApp.todoListViewModel = (function (ko, datacontext) {

    var todoLists = ko.observableArray(),
        error = ko.observable(),

        viewmodel = {
            todoLists: todoLists,
            error: error,
            addTodoList: addTodoList,
            deleteTodoList: deleteTodoList,
            clearErrorMessage: clearErrorMessage
        };

    // load todoLists immediately
    datacontext.getTodoLists(todoLists, error);

    return viewmodel;

    //#region private members
    function addTodoList() {
        var todoList = datacontext.createTodoList();
        todoList.isEditingListTitle(true);
        datacontext.saveNewTodoList(todoList)
            .then(addSucceeded)
            .fail(addFailed);

        function addSucceeded() {
            showTodoList(todoList);
        }
        function addFailed() {
            error("Save of new todoList failed");
        }
    }
    function showTodoList(todoList) {
        todoLists.unshift(todoList); // Insert new todoList at the front
    }
    function deleteTodoList(todoList) {
        todoLists.remove(todoList);
        datacontext.deleteTodoList(todoList)
            .fail(deleteFailed);

        function deleteFailed() {
            showTodoList(todoList); // re-show the restored list
        }
    }
    function clearErrorMessage(obj) {
        obj && obj.errorMessage && obj.errorMessage(null);
    }
    //#endregion

})(ko, todoApp.datacontext);

// Initiate the Knockout bindings
ko.applyBindings(window.todoApp.todoListViewModel);