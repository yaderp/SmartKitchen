from ultralytics import YOLO

def main():
    # cargar el modelo
    model = YOLO("Modelo/yolov8m.pt")

    # entrenar el modelos
    results = model.train(data="Dataset/data.yaml", epochs=100, imgsz=640)

if __name__ == "__main__":
    main()
