/* 
 * Autores: AFbaquero; JaramirezT : Fanalca
 */

var mostrar = true;

$(window).on('beforeunload', function () {
    if (mostrar) {
        LoadingPanel.Show();
    } else {
        LoadingPanel.Hide();
    }
	
});

$(function () {
    hidePanel();

    $("#ctl00_ctl00_LoadingPanel_LD").click(function () {
        hidePanel();
    });

    $(".download").click(function () {
        mostrar = false;
        console.log('que muera maduro');
        hidePanel();
        LoadingPanel.Hide();	
    });
});
 
function loadPanel(){
	LoadingPanel.Show();
}

function hidePanel()
{
	LoadingPanel.Hide();	
}

function alerta(mensaje) {
	alert(mensaje);	
}

function mensaje(mensaje) {
    swal(mensaje);
}

function informativo(titulo, mensaje){
	swal(titulo, mensaje, "info");
}

function errado(titulo, detalle) {
    swal(titulo, detalle, "error");	
}

function tituloDetalle(titulo, detalle) {
    swal(titulo, detalle);
}

function exitoso(titulo, detalle) {
	swal(titulo, detalle, "success");
}

function advertencia(titulo, detalle) {
    swal(titulo, detalle, "warning");
}

function confirmarAccion(titulo, detalle) {
    swal({
        title: titulo,
        text: detalle,
        type: "success",
        showCancelButton: true,
        confirmButtonColor: "#cc0001",
        confirmButtonText: "Aceptar",
        cancelButtonText: "Cancelar",
        closeOnConfirm: false
    },
	function () {
		$('.redirect').click();
	});
}

function confirmarDetalleAccion(titulo, detalle) {
    swal({
        title: titulo,
        text: detalle,
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#cc0001",
        confirmButtonText: "Aceptar",
        cancelButtonText: "Cancelar",
        closeOnConfirm: false,
        closeOnCancel: false
    },
            function (isConfirm) {
                if (isConfirm) {
                    swal("Realizado", "Ejecutado con éxito", "success");
                } else {
                    swal("Cancelado", "Ejecución cancelada con éxito", "error");
                }
            });
}

function tituloDetalleIcono(titulo, detalle, rutaIcono) {
    swal({
        title: titulo,
        text: detalle,
        imageUrl: rutaIcono
    });
}

function tituloDetalleHtml(titulo, detalle) {
	swal({
        title: titulo,
        text: detalle,
        html: true
    });
}

function tituloDetalleTimer(titulo, detalle, tiempo) {
	swal({
        title: titulo,
        text: detalle,
        timer: tiempo,
        showConfirmButton: false
    });
}

function entradaTexto(titulo, detalle) {
	swal({
        title: titulo,
        text: detalle,
        type: "input",
        cancelButtonText: "Cancelar",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-top",
        inputPlaceholder: "Ingrese Texto"
    },
            function (inputValue) {
                if (inputValue === false)
                    return false;

                if (inputValue === "") {
                    swal.showInputError("Debe Ingresar un Texto !");
                    return false;
                }

                swal("Bien", "Texto Ingresado: " + inputValue, "success");
            });
}

function tituloDetalleAjax(titulo, detalle) {
    swal({
        title: titulo,
        text: detalle,
        type: "info",
        cancelButtonText: "Cancelar",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
    },
            function () {
                setTimeout(function () {
                    swal("Ejecutado con éxito");
                }, 2000);
            });
}