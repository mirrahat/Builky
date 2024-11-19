var DataTable; // Ensure this matches the variable used in the loadDataTable function

$(document).ready(function () {
   
    var url = window.location.search;
    if (url.includes("inprocess")) {
        loadDataTable("inprocess")
    } else {
        if (url.includes("completed")) {
            loadDataTable("completed");
        } else {
            if (url.includes("pending")) {
                loadDataTable("pending");
            } else {
                if (url.includes("approved")) {
                    loadDataTable("approved");
                } else {
                    loadDataTable("all");
                }

            }


        }


    }


});

function loadDataTable(status) {
    return $('#tblTable').DataTable({
        "ajax": {
            "url": "/admin/order/GetAll?status=" + status,
        },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'name', "width": "15%" },
            { data: 'phoneNumber', "width": "20%" },
            { data: 'applicationUser.email', "width": "15%" },
            { data: 'orderStatus', "width": "15%" },
            { data: 'orderTotal', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group"> 
                    <a href="/admin/order/details?orderId=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square">Edit</i></a>
                    </div>`;
                },
                "width": "15%"
            }
        ]
    });
}

