window.dtUtils = {
    init: function (tableId) {
        $(tableId).DataTable({ paging: false, searching: false });
    },
    destroy: function (tableId) {
        $(tableId).DataTable().destroy();
    }
};