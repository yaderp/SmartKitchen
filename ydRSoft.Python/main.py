import Consultar
import ydRSoftSK
import base64
from flask import Flask, jsonify, request

app = Flask(__name__)

# Ruta de bienvenida
@app.route('/')
def home():
    return " <h2>¡Bienvenido a la API!</h2> <hr /> <h3>SmartKitchen P20241089</h3>"

# Ruta para obtener todos los elementos
@app.route('/productos', methods=['GET'])
def get_items():
    items = [
        {"Id": 1, "Nombre": "MANZANA", "PosX": 220, "PosY": 240,"Radio":200},
        {"Id": 2, "Nombre": "ZANAHORIA", "PosX": 750, "PosY": 450, "Radio": 150}
    ]
    return jsonify(items)


@app.route('/items', methods=['POST'])
def set_item():
    data = request.json
    image_base64 = data.get("imagen")
    #nombre = data.get("nombre")

    if not image_base64:
        return jsonify({"error": "No se proporcionó una imagen"}), 400

    try:
        image_data = base64.b64decode(image_base64)
        #guardarImg(image_data)

        items =  ydRSoftSK.procesar_imagen_yolo(image_data)
        return jsonify(items)

    except Exception as e:
        #traceback.print_exc()  # Imprimir el stack trace completo
        return jsonify({"error": str(e)}), 500

if __name__ == '__main__':
    app.run(debug=True)
