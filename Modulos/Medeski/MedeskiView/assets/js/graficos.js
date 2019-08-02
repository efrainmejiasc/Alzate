google.charts.load('current', {'packages':['corechart']});

google.charts.setOnLoadCallback(function(){
	drawPie()
});

// First Card
	var myDataDona;
	function printDona(datos){
		myDataDona = datos;
		google.charts.setOnLoadCallback(function() { 	
			drawDona(JSON.parse(datos));
		}); 
	}
	function drawDona(datos) {
		// var data = google.visualization.arrayToDataTable(datos);
		var data = new google.visualization.DataTable();	
		data.addColumn('string', 'word');
		data.addColumn('number', 'total');
		
		for (var i = 0; i < datos.length; i++) {
				data.addRow([datos[i].servicio, datos[i].total]);
		}
		
		var options = {
			title: 'Categoria de Servicios',
			pieHole: 0.5,
		};

		var chart = new google.visualization.PieChart(document.getElementById('donna_div'));

		chart.draw(data, options);
	}


// Second card
	var myDataBar;
	function printBars(datos){
		myDataBar = datos;
		google.charts.setOnLoadCallback(function() { 	
			drawBars(JSON.parse(datos));
		});	
	}
	function drawBars(datos) {	
		var data = new google.visualization.DataTable();	
		data.addColumn('string', 'word');
		data.addColumn('number', 'total');
		data.addColumn({type:'string', role:'annotation'});
		
		suma = 0;
		porc = 0;
		for (var i = 0; i < datos.length; i++) {
			suma += datos[i].total;
			porc += parseFloat(datos[i].porcentaje.replace("%", "").replace(",", "."))
			porcentaje = parseFloat(datos[i].porcentaje.replace("%", "").replace(",", ".")).toFixed(3)
			data.addRow([datos[i].producto, datos[i].total, porcentaje + "%"]);
		}
		/*
		porcPend = (100 - porc).toFixed(3);
		sumaPend = suma * 100 / porc
		data.addRow(["Otros Productos", sumaPend, porcPend + "%"]);
		*/
		var options = {
			title: 'Aplicaciones Empresariales',
			//chartArea: {width: '50%', height: '80%'},
			bar: {groupWidth: "70%"},
			legend: { position: "none" },
			annotations: {
				alwaysOutside: true,
				textStyle: {
					fontSize: 12,
					auraColor: 'none',
					color: '#555'
				},
				boxStyle: {
					stroke: '#ccc',
					strokeWidth: 1,
					gradient: {
						color1: '#f3e5f5',
						color2: '#f3e5f5',
						x1: '0%', y1: '0%',
						x2: '0%', y2: '100%'
					}
				}
			},
			hAxis: {
				title: 'Nivel de Servicios',
				minValue:0,
			},
			vAxis: {
				title: 'Apps',
				color: '#c10',
				height: 100
			}
		};

		var chart = new google.visualization.BarChart(document.getElementById('bars_div'));
		chart.draw(data, options);
	}

function drawPie() {
	var data = google.visualization.arrayToDataTable([
	
        ['División', 'Distribución Servicio'],
        ['Motos', 52], ['Autos', 15], ['FN Transversal', 14], 
        ['Apps Industriales', 4], ['Capacidades e Inv.', 4], ['Ensamble', 4],
        ['Performance & A.', 4], ['Otras Empresas', 2] , ['Carrocerias', 1], 
        ['Concesiones', 0.3]           
	
    ]);

    var options = {
        title: 'Distribución del Servicio',
        pieSliceText: 'percentage',
        pieStartAngle: 30,
        slices: {
            0: { offset: 0.01, color: '#cc1100' },
            2: {offset: 0.03,color: '#0383f8'},
            1: {offset: 0.05,color: '#3a3a3a'},
            3: {offset: 0.1, color: '#93b706'},
            4: {offset: 0.1, color: '#f44109'},
            5: {offset: 0.1, color: '#04269d'},
            6: {offset: 0.1, color: '#4319e8'},
            7: {offset: 0.1, color: '#f9a502'},
            8: {offset: 0.1, color: '#36ff03'},
            9: {offset: 0.1, color: '#6fd8f9'}
            //    15: {offset: 1.5},
        },
    };

    var chart = new google.visualization.PieChart(document.getElementById('pie_div'));
    chart.draw(data, options);
}

$(window).resize(function(){
	printDona(myDataDona);
    printBars(myDataBar);
    drawPie();  
});