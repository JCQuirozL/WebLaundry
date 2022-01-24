var ServiceTypes = [];

//fetch service types from database
function LoadService(element){
    if (ServiceTypes.length == 0) {
        //ajax function to fetch data
        $.ajax({
            type: "GET",
            url: '/home/getServiceTypes',
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