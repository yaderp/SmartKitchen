
var urlWebAcces = "";


// #region utilitarios
function CambioTitulo(texto) {
    var titulo = document.querySelector('title');
    titulo.innerText = texto;
}

function setOcultar(entrada, activo) {
    let outInput = document.getElementById(entrada);
    if (activo) {
        outInput.setAttribute("hidden", true);
    } else {
        outInput.removeAttribute("hidden");
    }
}

function setMaxLenght(entrada, dato) {
    let maxlengh = parseInt(dato);

    if (!isNaN(maxlengh) && maxlengh > 0) {
        let outInput = document.getElementById(entrada);
        outInput.setAttribute('maxlength', maxlengh);
    }
}

function setDataIn(entrada, data) {
    let outInput = document.getElementById(entrada);
    outInput.value = data;
}

function setTextIn(entrada, texto) {
    let outInput = document.getElementById(entrada);
    outInput.innerHTML = texto;
}

function setColor(entrada, color) {
    let outInput = document.getElementById(entrada);
    if (outInput) {
        outInput.style.borderColor = color;
    }
}

function setFocus(entrada) {
    let outInput = document.getElementById(entrada);
    if (outInput && typeof outInput.focus === 'function') {
        outInput.focus();
    }
}

function esVacio(entrada) {
    let outInput = document.getElementById(entrada).value;
    if (outInput != null) {
        if (outInput.length > 0) {
            return false;
        }
    }
    return true;
}

// #endregion

// #region leer archivo Imagen y PDF

function leerArchivo(entrada, salida) {
    var extension = entrada.files[0].name.split(".").pop().toUpperCase();
    if (extension == "JPG" || extension == "PNG") {
        filePreviewImg(entrada, salida);
    } else {
        if (extension == "PDF") {
            filePreviewPdf(entrada, salida);
        } else {
            $(entrada).val("");
            $(salida).html("");
            ErrorReg("Formatos Admitidos : \n .PNG | .JPG | .PDF ");
        }
    }
}

function filePreviewImg(entrada, salida) {
    if (entrada.files && entrada.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(salida).html("<div class='panel panel-success'> <div class='panel-body'> <img class='img-fluid' src= '" + e.target.result + "' /> </div> </div>");
        }
        reader.readAsDataURL(entrada.files[0]);
    }
};

function filePreviewPdf(entrada, salida) {
    if (entrada.files && entrada.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(salida).html("<div class='panel panel-success'> <div class='panel-body'> <iframe  class='ydRFrame' src= '" + e.target.result + "' frameborder='0'> </iframe> </div> </div>");
        }
        reader.readAsDataURL(entrada.files[0]);
    }
};



// #endregion

//#region validacion Numeros
function soloDecimales(event) {
    var inputValue = event.target.value;
    var key = event.key;
    var isDigit = /\d/.test(key);
    var isDecimalPoint = key === '.';
    var isDeleteKey = key === 'Delete' || key === 'Backspace';
    var isNegativeSign = key === '-' && !inputValue.includes('-');

    var hasFourDecimals = inputValue.includes('.') && inputValue.split('.')[1].length >= 4;
    
    keytap = event.keyCode || event.which;
    var tecla_especial = false
    if (keytap == 9 || keytap == 13) {
        tecla_especial = true;
    }

    if (isDigit || isDecimalPoint || isDeleteKey || isNegativeSign || tecla_especial) {
        if (tecla_especial) {
            return;
        }
        if (isDeleteKey) {
            return;
        }

        if (isNegativeSign) {
            if (inputValue.length > 0) {                
                event.preventDefault();
                return;
            }
        }

        if (isDecimalPoint) {
            if (inputValue.includes('.')) {
                event.preventDefault();
                return;
            }
        }

        if (hasFourDecimals) {
            event.preventDefault();
            return;
        }
    } else {
       
        event.preventDefault();
    }
}

// Solo decimales Positivos
function soloDecimalesPos(event) {
    var inputValue = event.target.value;
    var key = event.key;

    // Verificar si la tecla presionada es un dígito o un punto decimal
    var isDigit = /\d/.test(key);
    var isDecimalPoint = key === '.';

    var isDeleteKey = key === 'Backspace' || key === 'Delete';
    keytap = event.keyCode || event.which;
    var tecla_especial = false
    if (keytap == 9 || keytap == 13) {
        tecla_especial = true;
    }
    // Verificar si el valor actual tiene cuatro decimales
    var hasFourDecimals = inputValue.includes('.') && inputValue.split('.')[1].length >= 4;

    if (isDigit || isDecimalPoint || isDeleteKey || tecla_especial) {
        if (tecla_especial) {
            return;
        }
        if (isDeleteKey) {
            return;
        }

        if (isDecimalPoint) {
            if (inputValue.includes('.')) {
                event.preventDefault();
                return;
            }
        }

        if (hasFourDecimals) {
            event.preventDefault();
            return;
        }
    } else {
        
        event.preventDefault();
    }
}


function soloNumeros(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = "0123456789";
    especiales = "9";

    tecla_especial = false
    if (key == 9) {
        tecla_especial = true;
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
}

//#endregion

//#region Mensajes
function ExitoMensaje(mensaje, entrada) {
    swal({
        title: "!!! Felicitaciones !!!",
        text: mensaje,
        type: "success"
    }, function () {
        let outInput = document.getElementById(entrada);
        if (outInput && typeof outInput.focus === 'function') {
            outInput.focus();
        }
    });
};

function ErrorMensaje(mensaje, entrada) {
    swal({
        title: "!!! Advertencia!!!",
        text: mensaje,
        type: "error"
    }, function () {
        let outInput = document.getElementById(entrada);
        if (outInput && typeof outInput.focus === 'function') {
            outInput.focus();
        }
    });
};

function UrlMensaje(mensaje, urlPag) {
    swal({
        title: "!!! Felicitaciones!!!",
        text: mensaje,
        type: "success"
    }, function () {
        window.location.href = urlPag;
    });
};
function ExitoReg(mensaje) {
    swal({
        title: "!!! Felicitaciones !!!",
        text: mensaje,
        type: "success"
    });
};

function ErrorReg(mensaje) {
    swal({
        title: "!!! Advertencia !!!",
        text: mensaje,
        type: "error"
    });
};

//#endregion
