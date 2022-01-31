

$(document).ready(function () {
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
                if (data != null )
                {
                    var precio = data;
                }
                if (precio != null)
                {
                    $('#price').val(precio);
                }
            },
            error: function (ex) {
                alert('Error al recuperar precio' + ex);
            }
        });

    })

    //Calculate Subtotal
    $('#quantity').change(function () {
        subtotal = ($('#price').val() * $('#quantity').val()) / $('#iva').val();
        $('#subtotal').val(parseFloat(subtotal.toFixed(2)));
        console.log(subtotal);
    });

    //Calculate Total
    $('#quantity').change(function () {
        var total = ($('#subtotal').val() * $('#iva').val());
        $('#total').val(parseFloat(total.toFixed(1)));
        console.log(total);
    });


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
            var $newRow = $('#mainrow').clone().removeAttr('id');
            $('.st', $newRow).val($('#ServiceId').val());
            $('.clothingtype', $newRow).val($('#ClothingId').val());

            //Replace add button with remove button
            $('#add', $newRow).addClass('remove').val('Remove').removeClass('btnSuccess').addClass('btn btn-danger');

            //remove id attribute from new clone row
            $('#ServiceId,#ClothingId,#quantity,#price,#add', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();

            //append Clone Row
            $('#orderdetailsItems').append($newRow);
            

            //clear select data
            $('#ServiceId,#ClothingId').val('0');
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
                url: '/Orders/Save',
                data: JSON.stringify(data),
                contentType: 'application/json',
                success: function (data) {
                    if (data.status) {
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

