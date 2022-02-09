$('#ServiceId,#ClothingId,#subtotal,#total').val('0');
$('#quantity,#price').val('');
$('orderItemError').empty();

//Retrieve Clothing Types
$('#ServiceId').change(function () {
    $('#ClothingId').empty();
    $('#quantity').empty();
    $.ajax({
        type: 'POST',
        url: '/Orders/GetClothingTypesAsync',
        dataType: 'json',
        data: { serviceTypeId: $('#ServiceId').val() },
        success: function (clothingTypes) {
            $('#price').val('');
            $('#ClothingId').append('<option value="0">Selecciona el tipo de prenda</option>');
            $.each(clothingTypes, function (i, clothingType) {
                $('#ClothingId').append('<option value="'
                    + clothingType.clothingTypeId + '">'
                    + clothingType.name + '</option>');
            });
        },
        error: function (ex) {
            alert('Error al recuperar los tipos de prenda' + ex);
        }
    });
    return false;
})

//Retrieve Price acoording to the Clothing Type
$('#ClothingId').change(function () {
    $('#price').empty();
    $.ajax({
        type: 'POST',
        url: '/Orders/GetPrice',
        dataType: 'json',
        data: { clothingType: $('#ClothingId option:selected').val() },
        success: function (data) {
            //render Prices according Clothin Type
            if (data != null) {
                var precio = data;
            }
            if (precio != null) {
                $('#price').val(precio);
                subtotal = ($('#price').val() * $('#quantity').val()) / $('#iva').val();
                $('#subtotal').val(parseFloat(subtotal.toFixed(2)));
                var total = ($('#subtotal').val() * $('#iva').val());
                $('#total').val(parseFloat(total.toFixed(1)));
                console.log(total);
            }
        },
        error: function (ex) {
            alert('Error al recuperar precio' + ex);
        }
    });

})


function calculateTotals() {
    subtotal = ($('#price').val() * $('#quantity').val()) / $('#iva').val();
    $('#subtotal').val(parseFloat(subtotal.toFixed(2)));
    console.log(subtotal);

    var total = ($('#subtotal').val() * $('#iva').val());
    $('#total').val(parseFloat(total.toFixed(1)));
    console.log(total);

}


var boton = document.getElementById('add');
var guardar = document.getElementById('submit');
var list = document.getElementById('lista');
var data = [];
var cant = 0;

boton.addEventListener("click", agregar);
guardar.addEventListener("click", save);

function agregar() {
    var clothingtype = $("#ClothingId option:selected").text();
    var quantity = document.getElementById('quantity').value;
    var price = document.getElementById('price').value;
    var subtotal = document.getElementById('subtotal').value;
    var total = ( parseFloat(quantity) * parseFloat(price) );

    console.log(total);

   

    if ($("#ServiceId").val() == 0 || $("#ClothingId").val() == 0 || $("#quantity").val() == 0)
    {
        alert("Campos requeridos, favor de verificar");
        return;
    }

    //agregar detalle al array
    data.push({
        "id":cant,
        "clothingtype": clothingtype,
        "quantity": parseFloat(quantity),
        "total": parseFloat(total)
    });
    console.log(data);
    var row = 'row' + cant;
    var fila = '<tr id=' + row + '><td>' + clothingtype + '</td><td>' + quantity + '</td><td>' + price + '</td><td>' + subtotal + '</td><td>' + parseFloat(total) + '</td><td></td><td> <a class="btn btn-danger" onclick="eliminar('+cant+')">Eliminar</a></td></tr >';
    //agregar a la table
    $("#lista").append(fila);
    $("#ClothingId").val('');
    $("#quantity").val('');
    $("#price").val('');
    cant++;
    sumar();
}

function sumar() {
    var tot = 0;
    for (x of data) {
        tot = tot + x.total;
    }
   
    document.getElementById('topay').innerHTML = "Cobrar: $" + parseFloat(tot);
}

function eliminar(row) {
    //eliminar la fila actual
    $("#row" + row).remove();
    var i = 0;
    var pos = 0;
    for (x of data) {
        if (x.id == row) {
            pos = i;
        }
        i++;
    }
    data.splice(pos, 1);
    sumar();
}

function save() {

}