

// #region Función para actualizar la hora dentro del div
function startClock() {
    const clockDiv = document.getElementById('clockDiv');

    setInterval(() => {
        const now = new Date();
        const hours = String(now.getHours()).padStart(2, '0');
        const minutes = String(now.getMinutes()).padStart(2, '0');
        const seconds = String(now.getSeconds()).padStart(2, '0');
        clockDiv.textContent = `${hours}:${minutes}:${seconds}`;
    }, 1000); // Actualiza cada segundo
}

// Iniciar la función del reloj
startClock();

// #endregion

// #region TTS
function hablarTexto(texto) {

    const utterance = new SpeechSynthesisUtterance(texto); // Crear una nueva instancia de SpeechSynthesisUtterance
    window.speechSynthesis.speak(utterance); // Hablar el texto
}

/* Cargar voces disponibles

let voces = [];
function cargarVoces() {
    voces = window.speechSynthesis.getVoices();
}

window.speechSynthesis.onvoiceschanged = cargarVoces;

function hablarTexto(texto) {
    const utterance = new SpeechSynthesisUtterance(texto); // Crear una nueva instancia de SpeechSynthesisUtterance
    //Microsoft Sabina - Spanish (Mexico)
    //const vozSeleccionada = voces.find(voz => voz.name === "Microsoft Raul - Spanish (Mexico)");
    const vozSeleccionada = voces.find(voz => voz.name === "Microsoft Sabina - Spanish (Mexico)");
    if (vozSeleccionada) {
        utterance.voice = vozSeleccionada; // Asignar la voz seleccionada
    } else {
        console.warn("La voz no está disponible.");
    }

    window.speechSynthesis.speak(utterance); // Hablar el texto
}

*/

// #endregion

// #region Ajustar Camara

// #endregion


// #region otros



function setOcultar(entrada, activo) {
    let outInput = document.getElementById(entrada);
    if (activo) {
        outInput.setAttribute("hidden", true);
    } else {
        outInput.removeAttribute("hidden");
    }
}

function esVisible(entrada) {
    let outInput = document.getElementById(entrada);
    if (outInput.hasAttribute("hidden")) {
        return false;
        console.log("El div está oculto.");
    } else {
        console.log("El div está visible.");
        return true;
    }
}

// #endregion


// #region Obtener Recetas

// #endregion