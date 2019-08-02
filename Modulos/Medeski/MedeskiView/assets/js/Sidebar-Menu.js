$("#perfil_card").hide();

function MostrarEsconder3() {
    var z = $("#perfil_card");
    if (z.is(":visible")) {
        z.hide(); 
    } else {
        z.show();
    }
}

function abrirMenu () {
	$("#wrapper").toggleClass("toggled");
	$("#ContenidoDer").toggleClass("toggled");
	
	$("#perfil_card").hide();
	   
}

function cerrarMenu () {  
	if (!$("#wrapper").hasClass('toggled')) {
		$("#wrapper").toggleClass("toggled");
		$("#ContenidoDer").toggleClass("toggled");
	}    
	
	$("#perfil_card").hide();
}
