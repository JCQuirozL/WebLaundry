var datatable;

$(document).ready(function () {

    loadDatatable();
    var id = document.getElementById("statusid");
    if (id.value > 0) {
        $('#myModal').modal('show');
    }
});

function limpiar() {
    var id = document.getElementById("statusid");
    var name = document.getElementById("nameid");
   


    id.value = 0;
    name.value = "";
  

}

function loadDatatable() {

    datatable = $('#tblData').DataTable({

        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        },

        "ajax": { "url": "/OrderStatus/Listado" },

        "columns": [


            { "data": "name", "width": "60%" },
           

            {
                "data": "statusid",
                "render": function (data) {
                    return `
                      <div>
                        <a href="/OrderStatus/Create/${data}" class="btn btn-warning" style="cursor:pointer;">
                            Editar
                        </a>
                        <a onclick=Delete("/OrderStatus/Delete/${data}") class="btn btn-outline-danger " style="cursor:pointer;">
                            Borrar
                        </a>
                        
                      </div>
                    `;
                }, "width": "20%"

            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "¿Está seguro de querer borrar éste registro?",
        text: "Éste registro NO se podrá recuperar",
        icon: "warning",
        buttons: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        alert(data.message);
                        datatable.ajax.reload();
                    }
                    else {
                        alert(data.message);
                    }
                }
            });
        }
    });
}



