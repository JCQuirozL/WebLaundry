var datatable;

$(document).ready(function () {

    loadDatatable();
    var id = document.getElementById("customerid");
    if (id.value > 0) {
        $('#myModal').modal('show');
    }
});

function limpiar() {
    var id= document.getElementById("customerid");
    var name = document.getElementById("nameid");
    var lastname = document.getElementById("lastnameid");
    var address = document.getElementById("addressid");
    var email = document.getElementById("emailid");
   

    id.value = 0;
    name.value = "";
    lastname.value = "";
    address.value = "";
    email.value = "";
    

}

function loadDatatable() {

    datatable = $('#tblData').DataTable({

        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        },

        "ajax": { "url": "/Customers/Listado" },

        "columns": [


            { "data": "name", "width": "20%" },
            { "data": "lastname", "width": "20%" },

            { "data": "address", "width": "10%" },
            { "data": "phone", "width": "10%" },
            { "data": "email", "width": "15%" },
            

            {
                "data": "customerid",
                "render": function (data) {
                    return `
                      <div>
                        <a href="/Customers/Create/${data}" class="btn btn-warning" style="cursor:pointer;">
                            Editar
                        </a>
                        <a onclick=Delete("/Customers/Delete/${data}") class="btn btn-outline-danger " style="cursor:pointer;">
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
        title: "¿Está seguro de querer borrar éste Oficio?",
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



