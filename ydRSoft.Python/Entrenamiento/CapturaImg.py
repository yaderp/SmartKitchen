import cv2
import os
import time
# Iniciar la captura de video desde la cámara web
cap = cv2.VideoCapture(0)

# Configurar la resolución deseada (ajusta según tu cámara)
cap.set(cv2.CAP_PROP_FRAME_WIDTH, 1280)
cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 720)

# Ruta donde se guardarán las imágenes
save_path = "D:/Imagenes/Otros"
if not os.path.exists(save_path):
    os.makedirs(save_path)

# Contador para los nombres de archivo
image_counter = 1
# Tiempo inicial para el temporizador
start_time = time.time()
capture_enabled = False

while True:
    ret, frame = cap.read()
    if not ret:
        break

    # Mostrar el frame en tiempo real en la ventana
    cv2.imshow("Captura de Imagenes", frame)

    # Capturar la imagen cada 5 segundos si la captura está habilitada
    if capture_enabled and (time.time() - start_time >= 4):
        # Guardar la imagen en la carpeta especificada
        image_name = f"{save_path}/Otros_{image_counter}.jpg"
        cv2.imwrite(image_name, frame)
        print(f"Img_{image_counter}")

        # Incrementar el contador y reiniciar el temporizador
        image_counter += 1
        start_time = time.time()  # Reinicia el tiempo para el próximo intervalo

    # Capturar la tecla presionada
    key = cv2.waitKey(1)

    # Si presionas 'Enter' (código ASCII 13), alterna el estado de captura
    if key == 13:
        capture_enabled = not capture_enabled  # Cambia entre activar y desactivar la captura
        state = "activada" if capture_enabled else "detenida"
        print(f"Captura  {state}")
        start_time = time.time()  # Reinicia el tiempo al alternar

    # Presiona 'q' para salir del bucle
    if key == ord('q'):
        break

# Liberar la cámara y cerrar ventanas
cap.release()
cv2.destroyAllWindows()
