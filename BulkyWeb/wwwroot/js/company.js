var DataTable; // Ensure this matches the variable used in the loadDataTable function

$(document).ready(function () {
    DataTable = loadDataTable(); // Initialize and assign the DataTable instance
});

function loadDataTable() {
    return $('#tblTable').DataTable({
        "ajax": {
            "url": "/admin/company/getall",
        },
        "columns": [
            { data: 'title', "width": "15%" },
            { data: 'description', "width": "15%" },
            { data: 'isbn', "width": "15%" },
            { data: 'author', "width": "15%" },
            { data: 'category.name', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group"> 
                    <a href="/admin/company/upsert?id=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square">Edit</i></a>
                    <a onClick="Delete('/admin/company/delete/${data}')" class="btn btn-danger mx-2"><i class="bi bi-trash-fill">Delete</i></a>
                    </div>`;
                },
                "width": "15%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (Data) {
                    // Make sure DataTable is defined and accessible
                    if (DataTable) {
                        DataTable.ajax.reload(); // Reload the DataTable
                    } else {
                        console.error('DataTable is not initialized.');
                    }

                    // Use consistent variable naming
                    toastr.success(Data.message); // Ensure Data.message is the correct property
                },
                error: function (xhr, status, error) {
                    console.error('Error deleting item:', error);
                    toastr.error("Deletion failed");
                }
            });
        }
    });
}
