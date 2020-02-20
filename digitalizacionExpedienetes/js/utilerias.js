

function diHola() {
    alert("hola");
}



//      OBTIENE EL ARCHIVO DEL INPUTFILE
//function ObtenerArchivo(input) {
//    if (input.files && input.files[0]) {
//        var reader = new FileReader(), nombre = input.value.split('\\').pop().toLowerCase(), b = 0
//            , ext = nombre.split('.').pop(), string = "", id = parseInt(input.id.split('inputFile').pop());
//        reader.onload = function (e) {
//            string = e.target.result;
//            $.each(files, function (key, value) {
//                if (value[0] === id) {
//                    files[key] = [id, nombre, string, input.files[0]];
//                    b++; return false;
//                }
//            });
//            if (b === 0)
//                files[files.length] = [id, nombre, string, input.files[0]];
//            if (ext == 'pdf') {
//                //$('#image_upload_preview' + id).hide();
//                //$('#i_upload_preview' + id).show();
//                $('#i_upload_preview' + id).removeClass("fa-cloud-upload-alt fa-images tadaInfinito").addClass("fa-file-pdf").css("color", "darkred");
//            } else {
//                //$('#image_upload_preview' + id).show();
//                //$('#i_upload_preview' + id).hide();
//                $('#i_upload_preview' + id).removeClass("fa-cloud-upload-alt fa-file-pdf tadaInfinito").addClass("fa-images").css("color", "lightseagreen");
//            }
//            $('#i_upload_preview' + id).show();
//            //$('#image_upload_preview' + id).attr('src', string);
//        }
//        reader.readAsDataURL(input.files[0]);
//    }
//}

//      VALIDA FORMATO DE CORREO
//function ValidaEmail(input) {
//    if (/^[^@]+@[^@]+\.[a-zA-Z]{2,}$/.test(input.value))
//        $("#" + input.id).removeClass("invalid").addClass("valid");
//    else
//        $("#" + input.id).removeClass("valid").addClass("invalid");
//}

////      VALIDA LONGITUD DE NÚM TELEFÓNICO
//function ValidaPhone(input) {
//    var numTel = input.value.replace(/[^\d]+/g, '').toString(), total = numTel.length;
//    switch (total) {
//        case 0:
//            $("#" + input.id).val("");
//            break;
//        case 1: case 2: case 3:
//            $("#" + input.id).val("(" + numTel);
//            break;
//        case 4: case 5: case 6:
//            $("#" + input.id).val("(" + numTel.substring(0, 3) + ") " + numTel.substring(3, total));
//            break;
//        case 7: case 8: case 9: case 10:
//            $("#" + input.id).val("(" + numTel.substring(0, 3) + ") " + numTel.substring(3, 6) + " " + numTel.substring(6, total));
//            break;
//        default: $("#" + input.id).val("(" + numTel.substring(0, 3) + ") " + numTel.substring(3, 6) + " " + numTel.substring(6, 10));
//    }
//    if (total >= 10)
//        $("#" + input.id).removeClass("invalid").addClass("valid");
//    else
//        $("#" + input.id).removeClass("valid").addClass("invalid");
//}

////      CONFIRMA SI HAY COINCIDENCIA EN CAMPOS CONFIRMACION (CORREO Y TELÉFONO)
//function Confirmar(input1, input2) {
//    if (input2.value == input1)
//        $("#" + input2.id).removeClass("invalid").addClass("valid");
//    else
//        $("#" + input2.id).removeClass("valid").addClass("invalid");
//}


//      VALIDA QUÉ TIPO DE AVISO SE MUESTRA EN PANTALLA
//function modalMensaje(estatus, mensaje) {
//    $("#SIGuarda").hide();
//    $("#dialog").removeClass("modal-md modal-info modal-success modal-danger modal-warning");
//    $("#IconMensaje").removeClass("fa-bell fa-check fa-times fa-exclamation-triangle");
//    $("#NOGuarda").removeClass("btn-outline-info btn-outline-success btn-danger btn-outline-warning").html("OK");
//    switch (estatus) {
//        case 1: //MENSAJE NOTIFICACIÓN
//            $("#dialog").addClass("modal-sm modal-info");
//            $("#IconMensaje").addClass("fa-bell");
//            $("#NOGuarda").addClass("btn-outline-info");
//            break;
//        case 2: //MENSAJE SATISFACTORIO
//            $("#dialog").addClass("modal-sm modal-success");
//            $("#IconMensaje").addClass("fa-check");
//            $("#NOGuarda").addClass("btn-outline-success");
//            break;
//        case 3: //MENSAJE ERROR
//            $("#dialog").addClass("modal-sm modal-danger");
//            $("#IconMensaje").addClass("fa-times");
//            $("#NOGuarda").addClass("btn-danger");
//            break;
//        case 4: //MENSAJE ADVERTENCIA
//            $("#dialog").addClass("modal-sm modal-warning");
//            $("#IconMensaje").addClass("fa-exclamation-triangle");
//            $("#NOGuarda").addClass("btn-outline-warning");
//            break;
//    }
//    document.getElementById('mensaje').innerHTML = mensaje;
//    $("#btnMensaje").trigger("click");
//}
//function modalConfirmación(estatus, mensaje) {
//    $("#dialog").removeClass("modal-sm modal-info modal-success modal-danger modal-warning");
//    $("#IconMensaje").removeClass("fa-bell fa-check fa-times fa-exclamation-triangle");
//    $("#SIGuarda").removeClass("btn-info btn-success btn-outline-danger btn-warning").show();
//    $("#NOGuarda").removeClass("btn-outline-info btn-outline-success btn-danger btn-outline-warning").html("NO");
//    switch (estatus) {
//        case 1: //MENSAJE NOTIFICACIÓN
//            $("#dialog").addClass("modal-sm modal-info");
//            $("#IconMensaje").addClass("fa-bell");
//            $("#SIGuarda").addClass("btn-info");
//            $("#NOGuarda").addClass("btn-outline-info");
//            break;
//        case 2: //MENSAJE SATISFACTORIO
//            $("#dialog").addClass("modal-sm modal-success");
//            $("#IconMensaje").addClass("fa-check");
//            $("#SIGuarda").addClass("btn btn-success");
//            $("#NOGuarda").addClass("btn-outline-success");
//            break;
//        case 3: //MENSAJE ERROR
//            $("#dialog").addClass("modal-sm modal-danger");
//            $("#IconMensaje").addClass("fa-times");
//            $("#SIGuarda").addClass("btn-outline-danger");
//            $("#NOGuarda").addClass("btn-danger");
//            break;
//        case 4: //MENSAJE ADVERTENCIA
//            $("#dialog").addClass("modal-sm modal-warning");
//            $("#IconMensaje").addClass("fa-exclamation-triangle");
//            $("#SIGuarda").addClass("btn-warning");
//            $("#NOGuarda").addClass("btn-outline-warning");
//            break;
//    }
//    document.getElementById('mensaje').innerHTML = mensaje;
//    $("#btnMensaje").trigger("click");
//}

//$(document).ready(function () {
//    javascript: window.history.forward(1);
//});

//FUNCION JAVASCRIPT QUE NO DESPLIEGA EL ALERT
//function Validar() {

//    var txtnom = document.getElementById('<%=txtNombres.ClientID%>');
//    var txtapell = document.getElementById('<%=txtApellidos.ClientID%>');
//    var txtrfc = document.getElementById('<%=txtRFC.ClientID%>');
//    var ret = true;
//    if (txtnom.value == "" || txtapell.value == "" || txtrfc.value =="")
//    {
//        alert("ingrese sus datos en cualquiera de los campos");
//        ret = false;  
//    }
//    else {
//        ret = true;
//    }
//    return ret;

//}



function mostraracceso() {
    $('#Acceso').fadeIn();
    $('#divbtnNuevaConsulta').fadeOut();
}

//}

//function ValidarForm() {
//    var ret = true;
//    if (document.getElementById('txtRFC').value == "" && document.getElementById('txtNombres') == "" && document.getElementById('txtApellidos') == "") {
//        document.getElementById('lblRFC').textContent = "Ingresa tu RFC";
//        document.getElementById('lblNombre').textContent = "Ingresa tu Nombre";
//        document.getElementById('lblApellido').textContent = "Ingresa tu Apellido";
//        ret = false;

//    } else {
//        document.getElementById('txtRFC').textContent = "";
//        document.getElementById('txtNombres').textContent = "";
//        document.getElementById('txtApellidos').textContent = "";
//    }
//    return ret;
//}




//var variable = document.getElementByTagName('tabla1').innerHTML;
//variable.Visible = false;

//if (GridView1_SelectedIndexChanged() == true) {
//    variable.Visible = true;
//} else {
//    Variable.Visible = false;
//}




function divGrid() {
    $('#GridMostrar').removeAttr('style');
    $('#GridMostrar').hide();
}


//$(function algo() {


//    //var empleadoinfo = document.getElementById('empleadoInfo');
//    //empleadoinfo.style.display = 'block';
//});




$(function Ocultar() {
    $("<%=btnNuevaConsulta.ClientID%>").on("click", function () {
        $("#Acceso").fadeIn();
        $("#GridMostrar").fadeToggle();
        $("#btnNuevaConsulta").hide();
    });
});

//<% --Mantiene el div acceso visible si no hay registros en los TextBox.-- %>

function hideaccess() {
    ocultaracceso();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        ocultaracceso();
    }
}



