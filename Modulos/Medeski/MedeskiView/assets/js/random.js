var background =new Array(); 

if (screen.width >= 640) {
	background[1] = "assets/img/Screen_00.jpg"; 
	background[2] = "assets/img/Screen_01.jpg";
	background[3] = "assets/img/Screen_02.jpg";

}
else {

	background[1] = "assets/img/Screen_00_Mobile.jpg"; 
	background[2] = "assets/img/Screen_01_Mobile.jpg";
	background[3] = "assets/img/Screen_02_Mobile.jpg";	
}

//Función Aleatoria para Generar un numero de 1 a 6
var numberGen = Math.floor((Math.random() * 3) + 1);

// Ruta local de imagenes antes de Exprtar el proyecto 
document.getElementById("cuerpo").style.backgroundImage = "url("+background[numberGen]+")";
