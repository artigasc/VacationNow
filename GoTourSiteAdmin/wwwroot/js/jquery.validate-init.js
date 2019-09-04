$.validator.addMethod('filesize', function (value, element, params) {
    return this.optional(element) || (element.files[0].size <= params[0] * params[1])
}, 'File size must be less than {0}'); 

$("#FormAddUser").validate({
	rules: {
		UserName: {
			required: true,
			minlength: 4,
			email: true
		},
		Password: {
			required: true,
			minlength: 9
		},
		FirstName: {
			required: true,
			minlength: 3
		},
		FirstLastName: 'required',
		Email: {
			email: true
		},
		Phone: {
			digits: true,
			required: true,
			minlength: 12

		}

	},
	messages: {
		UserName: {
			required: "Ingrese un correo válido",
			minlength: jQuery.validator.format("Ingrese por lo menos {0} caracteres!"),
			email: "Ingrese un correo electrónico válido"
		},
		Password: {
			required: "Ingrese una contraseña correcta",
			minlength: jQuery.validator.format("Ingrese por lo menos {0} caracteres!")
		},
		FirstName: {
			minlength: jQuery.validator.format("Ingrese por lo menos {0} caracteres!")
		},
		FirstLastName: 'Ingrese un apellido válido',
		Email: {
			email: "Ingrese un correo electrónico válido"
		},
		Phone: {
			required: "Ingrese un número telefónico válido",
			minlength: jQuery.validator.format("Ingrese por lo menos {0} caracteres!"),
			digits: "Ingrese sólo dígitos"
		}
	}
});

var v = $("#FormCity").validate({
    errorPlacement: function errorPlacement(error, element) { element.before(error); },
    rules: {
        Name: {
            required: true,
            minlength: 9
        },

        Description: {
            required: true
        },
        UrlPhoto: {
            required: true,
            extension: "jpg,jpeg,png",
            filesize: [560, 1000]
        },
        Icono: {
            required: true
        },
        Temperature: {
            required: true,
            digits: true
        },
        Altitude: {
            required: true,
            digits: true
        },
        Population: {
            required: true,
            digits: true
        },
        
        //"Slogan[0]": "required",
        //"Slogan[1]": "required",
        //"Slogan[2]": "required",
        //"Description[]": {
        //    required: true
        //},
        //"Location[]": {
        //    required: true
        //},
        //"FarmingProduction[]": {
        //    required: true
        //},
        //"DescriptionDistricts[]": {
        //    required: true
        //},
      
    },
    messages: {

        Name: {
            required: "Este campo es requerido",
            minlength: jQuery.validator.format("Ingrese por lo menos {0} caracteres!"),
        },
        UrlPhoto: {
            required: "Este campo es requerido",
            filesize: jQuery.validator.format("El tamaño máximo es de {0}KB"),
            extension: "Sólo se permiten imágenes en formato {0}"
        },
        Icono: {
            required: "Seleccione un elemento"
        },
        Temperature: {
            required: "Complete este campo",
            digits: "Sólo dígitos"
        },
        Altitude: {
            required: "Complete este campo",
            digits: "Sólo dígitos"
        },
        Population: {
            required: "Complete este campo",
            digits: "Sólo dígitos"
        }
        
    }
});
var arrCityLanguage = ["Slogan", "Description", "Location", "FarmingProduction", "DescriptionDistricts"];

for (var i = 0; i <= arrCityLanguage.length; i++) {
    for (var e = 0; e <= 2; e++) {
        console.log($("[name='" + arrCityLanguage[i] + "[" + e + "]']"));

        $("[name='" + arrCityLanguage[i] + "[" + e + "]']").rules('add', {
                required: true
        });     
    }
}



