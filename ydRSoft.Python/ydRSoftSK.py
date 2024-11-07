import numpy as np
import cv2
import math
from ultralytics import YOLO

def procesar_imagen_yolo(image_data):
    print("Detectando Productos ... ")

    modelo = YOLO('modelo/tucto3.pt')

    nparr = np.frombuffer(image_data, np.uint8)
    imagen = cv2.imdecode(nparr, cv2.IMREAD_COLOR)

    resultados = modelo(imagen)

    items = []

    ids_nombres = {
        0: 'AJÍ AMARILLO',
        1: 'BERENGENA',
        2: 'CAIGUA',
        3: 'CEBOLLA',
        4: 'CHOCLO',
        5: 'LIMÓN',
        6: 'MANZANA',
        7: 'NABO',
        8: 'NARANJA',
        9: 'PAPA',
        10: 'PEPINILLO',
        11: 'PLÁTANO',
        12: 'TOMATE',
        13: 'ZANAHORIA',
        14: 'ZAPALLO'
    }

    lprod = ""
    for resultado in resultados:
        for deteccion in resultado.boxes.data.tolist():
            x1, y1, x2, y2, confianza, clase = deteccion
            px = int((x1 + x2) / 2)
            py = int((y1 + y2) / 2)
            radio = int(math.sqrt((x2 - x1) ** 2 + (y2 - y1) ** 2) / 2)
            print("ID: "+str(int(clase)) + str(" |  "+modelo.names[int(clase)]) +" ("+str(px)+","+str(py)+ ","+str(radio)+")")
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

    #print("Productos : " + lprod)
    #Consultar.guardarProductoCompleto(lprod,image_data)
    return items