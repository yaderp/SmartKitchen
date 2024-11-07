import time
contador_id = 0
def guardarProductoTxt(item,Idprod):
    with open('D:/productos.txt', 'a') as file:  # 'a' es para añadir al archivo sin sobreescribir
        file.write(f"Id: {Idprod}, Nombre: {item['Nombre']}, PosX: {item['PosX']}, PosY: {item['PosY']}, Radio: {item['Radio']}\n")

def guardartxt(texto,Idprod):
    with open('D:/productos.txt', 'a') as file:
        file.write(f"Id: {Idprod}, Item: {texto}\n")


def guardarImg(image_data,IdProd):
    image_path = "D:/Imagenes/"+str(IdProd)+".png"
    with open(image_path, "wb") as f:
        f.write(image_data)


def guardarProductoCompleto(item, image_data):
    global contador_id
    contador_id += 1  # Incrementa el contador en cada llamada para generar un nuevo IdProd

    # Genera un Id único utilizando el contador
    IdProd = f"Producto_{contador_id}"

    # Llama a las funciones para guardar la información en el archivo de texto y la imagen
    guardartxt(item, IdProd)
    guardarImg(image_data, IdProd)

    print(f"Producto guardado con Id: {IdProd}")
