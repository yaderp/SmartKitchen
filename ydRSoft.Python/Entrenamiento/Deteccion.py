import cv2
from ultralytics import YOLO
import math

# Cargar el modelo YOLO
modelo = YOLO('Modelo/ydRSoft_SKv2.pt')  # Ajusta la ruta si es necesario
print("Clases detectables por el modelo:", modelo.names)

cap = cv2.VideoCapture(0)

if not cap.isOpened():
    print("Error: No se pudo acceder a la cámara.")
    exit()

cap.set(cv2.CAP_PROP_FRAME_WIDTH, 1280)
cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 720)

while True:
    ret, frame = cap.read()  # Leer un frame de la cámara
    if not ret:
        break

    # Realizar la detección con YOLO
    resultados = modelo(frame)

    for resultado in resultados:
        for deteccion in resultado.boxes.data.tolist():
            x1, y1, x2, y2, confianza, clase = deteccion
            px = int((x1 + x2) / 2)
            py = int((y1 + y2) / 2)
            radio = int(math.sqrt((x2 - x1) ** 2 + (y2 - y1) ** 2) / 2)
            print("ID: " + str(int(clase)) + str(" |  " + modelo.names[int(clase)]) + " (" + str(px) + "," + str(py) + ")")
    # Dibujar los resultados en la imagen
    annotated_frame = resultados[0].plot()  # `plot` dibuja las cajas y etiquetas detectadas

    # Mostrar el frame anotado en la ventana
    cv2.imshow("Detección en Tiempo Real", annotated_frame)

    # Presiona 'q' para salir del bucle
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

# Liberar la cámara y cerrar ventanas
cap.release()
cv2.destroyAllWindows()
