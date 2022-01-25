var ServiceTypes = [];

//fetch service types from database
function LoadService(element){
    if (ServiceTypes.length == 0) {
        //ajax function to fetch data
        $.ajax({
            type: "GET",
            url: '/Orders/getServiceTypes',
            success: function (data) {
                ServiceTypes = data;
                //render servic types
                renderServiceTypes(element);
            }
        })
    }
    else
    { //render service types to the element (the <select> html in this case)
        renderServiceTypes(element);
    }
}

function renderServiceTypes(element) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Seleccione...'));
    $.each(ServiceTypes, function (i, val) {
        $ele.append($('<option/>').val(val.ServiceTypeId).text(val.Name));
    })
}

//fetch Clothing Types
function LoadClothingType(servicetypeDD, priceDD) {
    $.ajax({
        type : "GET",
        url: "/Orders/getClothingTypes",
        data: { 'ServiceTypeId': $(servicetypeDD).val() },
        success: function (data) {
            //render Clothing Types to appropiate Dropdown
            renderClothingType($(servicetypeDD).parents('.mycontainer').find('select.clothingtype'), data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}

function renderClothingType(element, data) {
    var $ele = $(element);
    $ele.append($('<option/>').val('0').text('Seleccione...'));
    $.each(data, function (i, val) {
        $ele.append($('<option/>').val(val.ClothingTypeId).text(val.Name));

    })
}

//fetch Price
function LoadPrice(priceDD) {
    $.ajax({
        type: "GET",
        url: "/Orders/getPrices",
        data: { 'Price': $(priceDD).val() },
        success: function (data) {
            //render Prices according Clothin Type
            var precio = document.getElementById('price');
            $(precio) = data;
        }
    })
}



$(document).ready(function () {
    //Add button click event
    $('#add').click(function () {
        //validation and order items
        var isAllValid = true;
        if ($('#servicetype').val() == "0") {
            isAllValid = false;
            $('#servicetype').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#servicetype').siblings('span.error').css('visibility', 'hidden');

        }

        if ($('#clothingtype').val() == "0") {
            isAllValid = false;
            $('#clothingtype').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#clothingtype').siblings('span.error').css('visibility', 'hidden');

        }

        if (!($('#quantity').val().trim() != '' && (parseFloat($('#quantity').val()) || 0))) {
            isAllValid = false;
            $('#quantity').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#quantity').siblings('span.error').css('visibility', 'hidden');

        }

        if (isAllValid) {
            var $newRow = $('#mainrow').clone().removeAtrr('id');
            $('.st', $newRow).val($('#servicetype').val());
            $('.clothingtype', $newRow).val($('#clothingtype').val());

            //Replace add button with remove button
            $('#add', $newRow).addClass('remove').val('Remove').removeClass('btnSuccess').addClass('btn-danger');

            //remove id attribute from new clone row
            $('#servicetype,#clothingtype,#quantity,#price,#add', $newRow).removeAtrr('id');
            $('span.error', $newRow).remove();

            //append Clone Row
            $('#orderdetailsItems').append($newRow);

            //clear select data
            $('#servicetype,#clothingtype').val('0');
            $('#quantity,#price').val('');
            $('orderItemError').empty();
        }

    })

    $('#orderdetailsItems').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    })

    $('#submit').click(function () {
        var isAllValid = true;

        //validate order items
        $('#orderItemError').text('');
        var list = [];
        var errorItemCount = 0;
        $('#orderdetailsItems tbody tr').each(function (index, ele) {
            if (
                $('select.clothingtype', this).val() == "0" ||
                (parseFloat($('.quantity', this).val()) || 0) == 0
            ) {
                errorItemCount++;
                $(this).addClass('error');
            } else {
                var total = (parseFloat($('.quantity', this).val())) * (parseFloat($('.price', this).val()));

                var orderItem = {
                    ClothingTypeId: $('select.clothingtype', this).val(),
                    Quantity: parseFloat($('.quantity', this).val()),
                    Total: total
                }
                list.push(orderItem);
            }
        })
        if (errorItemCount > 0) {
            $('#orderItemError').text(errorItemCount + "valor no válido en la lista");
            isAllValid = false;
        }

        if (list.length == 0) {
            $('#orderItemError').text('Debe capturar al menos un servicio a añadir');
            isAllValid = false;
        }

        if (isAllValid) {
            var data = {
                CreateDate: $('#orderDate').val().trim(),
                Annotations: $('#description').val().trim(),
                OrderDetails: list
            }

            $(this).val('Please wait ...');
            $.ajax({
                type: 'POST',
                url: '/Orders/save',
                data: JSON.stringify(data),
                contentType: 'application/json',
                success: function (data) {
                    if (d.status) {
                        ('Guardado con éxito');
                        //clear the form
                        list = [];
                        $('description').val('');
                        $('orderdetailsItems').empty();
                    }
                    else {
                        alert('Error');
                    }
                    $('#submit').text('Guardar');
                },
                error: function (error) {
                    console.log(error)
                }
            });
        }
    });
});

LoadService($('#servicetype'));