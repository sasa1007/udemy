$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {

    dataTable = $('#tblData').DataTable({
        "ajax": {url: '/admin/product/getAll'},
        "columns": [
            {data: 'name'},
            {data: 'author'},
            {data: 'description'},
            {data: 'isbn'},
            {data: 'price'},
            {data: 'price50'},
            {data: 'price100'},
            {data: 'listPrice'},
            {data: 'category.name'},
            {
                data: 'id',
                render: function (data) {
                    return `<div class="w-75 btn-group" >
                                <a href="/admin/product/upsert?id=${data}">Edit</a> <br><br>
                                <a href="/admin/product/delete?id=${data}">Delete</a> 
                            </div>`
                }
            },
        ]
    });
}

