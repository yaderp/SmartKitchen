import Consultar
import base64
import numpy as np
import cv2
import math
from ultralytics import YOLO
from flask import Flask, jsonify, request

app = Flask(__name__)

# Ruta de bienvenida
@app.route('/')
def home():
    return "¡Bienvenido a la API!"

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

        return procesar_imagen_yolo(image_data)

    except Exception as e:
        #traceback.print_exc()  # Imprimir el stack trace completo
        return jsonify({"error": str(e)}), 500

def guardarImg(image_data):
    image_path = "D:/Imagenes/imagen_copia4.png"
    with open(image_path, "wb") as f:
        f.write(image_data)


def procesar_imagen_yolo(image_data):
    print("Detectando Productos ... ")

    modelo = YOLO('modelo/yolov8m.pt')

    nparr = np.frombuffer(image_data, np.uint8)
    imagen = cv2.imdecode(nparr, cv2.IMREAD_COLOR)

    resultados = modelo(imagen)

    items = []

    ids_nombres = {
        47: "MANZANA",
        46: "PLATANO",
        51: "ZANAHORIA",
        49: "NARANJA",
        28: "Nombre_Personalizado_60"
    }

    lprod = ""
    for resultado in resultados:
        for deteccion in resultado.boxes.data.tolist():
            x1, y1, x2, y2, confianza, clase = deteccion
            px = int((x1 + x2) / 2)
            py = int((y1 + y2) / 2)
            radio = int(math.sqrt((x2 - x1) ** 2 + (y2 - y1) ** 2) / 2)
            print(str(modelo.names[int(clase)])+" -> "+str(int(px)))
            if int(clase) in ids_nombres:
                nombre = ids_nombres[int(clase)]
                item = {
                    "Id": int(clase),
                    "Nombre": nombre,
                    "PosX": px,
                    "PosY": py,
                    "Radio": radio,
                }
                #Consultar.guardarProductoTxt(item)
                lprod = lprod + " - "+nombre+"("+str(px)+","+str(py)+ ",R"+str(radio) +") "
                items.append(item)

    print("Productos : " + lprod)
    return jsonify(items)



if __name__ == '__main__':
    app.run(debug=True)
