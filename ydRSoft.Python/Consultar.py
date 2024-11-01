
def guardarProductoTxt(item):
    with open('D:/productos.txt', 'a') as file:  # 'a' es para añadir al archivo sin sobreescribir
        file.write(f"Id: {item['Id']}, Nombre: {item['Nombre']}, PosX: {item['PosX']}, PosY: {item['PosY']}, Radio: {item['Radio']}\n")



def guardarImg(image_data):
    image_path = "D:/Imagenes/imagen_copia4.png"
    with open(image_path, "wb") as f:
        f.write(image_data)

