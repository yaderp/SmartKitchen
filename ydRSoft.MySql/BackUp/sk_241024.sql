CREATE DATABASE  IF NOT EXISTS `dbsk` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `dbsk`;
-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: dbsk
-- ------------------------------------------------------
-- Server version	8.0.19

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `favoritos`
--

DROP TABLE IF EXISTS `favoritos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `favoritos` (
  `id` int NOT NULL AUTO_INCREMENT,
  `iduser` int NOT NULL,
  `idreceta` int NOT NULL,
  `fechareg` datetime DEFAULT NULL,
  `estado` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `favoritos`
--

LOCK TABLES `favoritos` WRITE;
/*!40000 ALTER TABLE `favoritos` DISABLE KEYS */;
INSERT INTO `favoritos` VALUES (1,2,14,'2024-10-21 21:41:59',1),(2,2,2,'2024-10-21 21:42:20',0),(3,2,7,'2024-10-21 21:42:21',0),(4,2,7,'2024-10-22 11:28:41',1);
/*!40000 ALTER TABLE `favoritos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `preferencias`
--

DROP TABLE IF EXISTS `preferencias`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `preferencias` (
  `id` int NOT NULL AUTO_INCREMENT,
  `iduser` int NOT NULL,
  `idprod` int NOT NULL,
  `fechareg` datetime DEFAULT NULL,
  `estado` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `iduser` (`iduser`),
  KEY `idprod` (`idprod`),
  CONSTRAINT `preferencias_ibfk_1` FOREIGN KEY (`iduser`) REFERENCES `usuario` (`id`),
  CONSTRAINT `preferencias_ibfk_2` FOREIGN KEY (`idprod`) REFERENCES `producto` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `preferencias`
--

LOCK TABLES `preferencias` WRITE;
/*!40000 ALTER TABLE `preferencias` DISABLE KEYS */;
INSERT INTO `preferencias` VALUES (8,2,2,'2024-10-20 20:51:49',0),(9,2,5,'2024-10-20 20:51:54',0),(10,2,7,'2024-10-20 20:52:01',0),(11,2,3,'2024-10-20 20:52:03',0),(12,2,1,'2024-10-20 20:52:05',0),(13,2,6,'2024-10-20 20:52:06',0),(14,2,4,'2024-10-20 20:52:07',0),(15,2,7,'2024-10-20 21:07:02',1),(16,2,4,'2024-10-20 21:07:04',2),(17,2,2,'2024-10-20 21:07:06',0),(18,2,3,'2024-10-20 21:07:08',0),(19,2,6,'2024-10-20 21:07:09',2),(20,2,9,'2024-10-22 11:42:55',0),(21,2,11,'2024-10-22 11:42:56',0);
/*!40000 ALTER TABLE `preferencias` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `producto`
--

DROP TABLE IF EXISTS `producto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `producto` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) DEFAULT NULL,
  `calorias` varchar(100) DEFAULT NULL,
  `proteinas` varchar(100) DEFAULT NULL,
  `colesterol` varchar(100) DEFAULT NULL,
  `fibra` varchar(100) DEFAULT NULL,
  `carbohidratos` varchar(100) DEFAULT NULL,
  `azucares` varchar(100) DEFAULT NULL,
  `sodio` varchar(100) DEFAULT NULL,
  `calcio` varchar(100) DEFAULT NULL,
  `grasa` varchar(100) DEFAULT NULL,
  `fechareg` datetime DEFAULT NULL,
  `estado` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `producto`
--

LOCK TABLES `producto` WRITE;
/*!40000 ALTER TABLE `producto` DISABLE KEYS */;
INSERT INTO `producto` VALUES (1,'PLATANO','89 Kcal','1.1 g','0 mg','22.8 g','2.6 g','12.2 g','1 mg','5 mg','0.3 g','2024-10-18 13:55:11',1),(2,'MANZANA','95 Kcal','0.5 g','0 mg','25 g','4 g','19 g','2 mg','11 mg','0.3 g','2024-10-18 13:55:33',1),(3,'NARANJA','62 Kcal','1.2 g','0 mg','15.4 g','3.1 g','12.2 g','0 mg','40 mg','0.2 g','2024-10-18 13:55:37',1),(4,'ZANAHORIA','41 Kcal','0.93 g','0 mg','9.58 g','2.8 g','4.74 g','69 mg','33 mg','0.24 g','2024-10-18 13:55:40',1),(5,'BROCOLI','34 Kcal','2.8 g','0 mg','6.6 g','2.6 g','1.7 g','33 mg','47 mg','0.4 g','2024-10-18 13:55:44',1),(6,'LIMON','29 Kcal','1.1 g','0 mg','9.3 g','2.8 g','2.5 g','2 mg','26 mg','0.3 g','2024-10-18 13:55:47',1),(7,'TOMATE','18 Kcal','0.9 g','0 mg','3.9 g','1.2 g','2.6 g','5 mg','10 mg','0.2 g','2024-10-18 13:55:50',1),(8,'Zapallo','26 Kcal','1 g','0 mg','6.5 g','0.5 g','2.76 g','1 mg','21 mg','0.1 g','2024-10-21 11:47:33',1),(9,'YUCA','160 Kcal','1.4 g','0 mg','38.1 g','1.8 g','1.7 g','14 mg','33 mg','0.3 g','2024-10-21 11:52:52',1),(10,'LECHUGA','15 Kcal','1.4 g','0 mg','2.9 g','1.3 g','1.2 g','28 mg','36 mg','0.2 g','2024-10-21 11:55:39',1),(11,'APIO','16 Kcal','0.7 g','0 mg','3 g','1.6 g','1.8 g','80 mg','40 mg','0.2 g','2024-10-21 12:00:34',1),(12,'CILANTRO','23 Kcal','2.1 g','0 mg','3.7 g','2.8 g','0.9 g','46 mg','67 mg','0.5 g','2024-10-21 12:03:31',0),(13,'LECHE ENTERA','61 Kcal','3.2 g','10 mg','4.8 g','0 g','5.1 g','40 mg','113 mg','3.3 g','2024-10-21 19:53:13',1);
/*!40000 ALTER TABLE `producto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `receta`
--

DROP TABLE IF EXISTS `receta`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `receta` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(750) DEFAULT NULL,
  `ndif` int NOT NULL,
  `tiempo` int NOT NULL,
  `categoria` varchar(100) DEFAULT NULL,
  `ingredientes` text,
  `preparacion` text,
  `fechareg` datetime DEFAULT NULL,
  `estado` int NOT NULL,
  `calificacion` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `receta`
--

LOCK TABLES `receta` WRITE;
/*!40000 ALTER TABLE `receta` DISABLE KEYS */;
INSERT INTO `receta` VALUES (1,'Ensalada de espinacas con manzana y plátano',2,15,'entrada','1 manzana grande, cortada en cubos|1 plátano, rebanado|2 tazas de espinacas frescas|1/4 de taza de nueces picadas|1/4 de taza de queso feta desmenuzado|2 cucharadas de aceite de oliva|1 cucharada de vinagre balsámico|Sal y pimienta al gusto','Lavar y secar las espinacas.|En un tazón grande, combinar las espinacas, manzana, plátano, nueces y queso feta.|En un tazón pequeño, mezclar el aceite de oliva y el vinagre balsámico.|Verter el aderezo sobre la ensalada, mezclar bien.|Sazonar con sal y pimienta al gusto y servir inmediatamente.','2024-10-20 18:55:57',0,0),(2,'Batido de manzana y plátano',1,10,'postre','1 manzana grande, pelada y cortada|1 plátano maduro|1 taza de leche o leche de almendras|1/2 cucharadita de canela|1 cucharada de miel|Hielo al gusto','Colocar la manzana, el plátano, la leche, la canela y la miel en una licuadora.|Agregar hielo al gusto.|Licuar hasta que la mezcla esté suave.|Servir inmediatamente en un vaso grande.','2024-10-20 18:56:08',1,4),(3,'Jugo de Limón Natural',1,10,'entrada','4 limones maduros|1 litro de agua|Azúcar al gusto|Hielo (opcional)','Exprimir los limones para obtener su jugo.|Filtrar el jugo para eliminar semillas y pulpa excesiva.|En una jarra, mezclar el jugo de limón con un litro de agua.|Agregar azúcar al gusto y disolver completamente.|Servir con hielo al gusto.','2024-10-21 12:52:28',0,0),(4,'Jugo de Manzana Natural',1,10,'entrada','4 manzanas rojas|1/2 limón|1 taza de agua|Hielo (opcional)|Azúcar o miel al gusto (opcional)','Lava y corta las manzanas en cuartos, retirando el corazón y las semillas.|Exprime el jugo de medio limón.|Coloca las manzanas y el jugo de limón en una licuadora.|Añade una taza de agua para facilitar el licuado.|Licúa hasta obtener una mezcla homogénea.|Cuela el jugo usando un colador fino para eliminar los sólidos.|Sirve el jugo en un vaso con hielo al gusto.|Añade azúcar o miel si deseas endulzar un poco.','2024-10-21 13:16:00',1,1),(5,'Arroz con Leche',2,45,'postre','1 taza de arroz|4 tazas de leche entera|1 taza de azúcar|1 rama de canela|Cáscara de 1 limón|1/2 cucharadita de esencia de vainilla|Canela en polvo para decorar','Enjuagar el arroz bajo agua fría hasta que el agua salga clara.|En una olla grande, combinar la leche, la rama de canela y la cáscara de limón. Llevar a ebullición.|Añadir el arroz a la olla y reducir el fuego a bajo. Cocinar, removiendo ocasionalmente, hasta que el arroz esté tierno, aproximadamente 20-25 minutos.|Agregar el azúcar y la esencia de vainilla a la mezcla. Revolver bien y continuar cocinando por 10 minutos más.|Retirar del fuego y quitar la rama de canela y la cáscara de limón.|Servir caliente o frío, espolvoreado con canela en polvo.','2024-10-21 13:18:53',1,3),(6,'Arroz con Pollo',3,60,'principal','1 kg de pollo en trozos|2 tazas de arroz|4 tazas de caldo de pollo|1 cucharada de aceite de oliva|1 cebolla grande picada|1 pimiento rojo picado|3 dientes de ajo picados|1 taza de guisantes congelados|1/2 taza de zanahoria cortada en cubos|1/4 cucharadita de cúrcuma|1 cucharadita de paprika|Sal y pimienta al gusto|Cilantro fresco para decorar','Calienta el aceite en una sartén grande a fuego medio-alto.|Añade el pollo troceado y sazona con sal y pimienta. Cocina hasta que esté dorado por todos lados.|Retira el pollo de la sartén y reserva.|En la misma sartén, añade la cebolla, el pimiento rojo, la zanahoria y el ajo. Sofríe hasta que estén blandos.|Incorpora el arroz y mezcla bien con las verduras.|Añade la cúrcuma y la paprika, y cocina por un minuto para liberar los aromas.|Vuelve a colocar el pollo en la sartén. Añade el caldo de pollo y lleva a ebullición.|Reduce el fuego, tapa y cocina a fuego lento durante 20-25 minutos, o hasta que el arroz esté cocido y el líquido se haya absorbido.|Agrega los guisantes cinco minutos antes de terminar la cocción.|Retira del fuego, deja reposar tapado durante 5 minutos.|Sirve decorado con cilantro fresco picado.','2024-10-21 13:26:44',0,0),(7,'Aguadito de Pollo',2,45,'sopa','1 pechuga de pollo grande|1 taza de arroz|1 cucharada de aceite vegetal|1 cebolla grande picada|3 dientes de ajo picados|1 pimiento rojo cortado en tiras|1 zanahoria cortada en rodajas|1/2 taza de choclo desgranado|1/2 taza de arvejas|6 tazas de caldo de pollo|1 manojo de cilantro fresco picado|Sal y pimienta al gusto','Calentar el aceite en una olla grande a fuego medio.|Añadir la cebolla, ajo, pimiento y zanahoria, y sofreír hasta que la cebolla esté transparente.|Agregar la pechuga de pollo y dorar por todos lados.|Incorporar el arroz, choclo y arvejas, y revolver bien.|Verter el caldo de pollo y llevar a ebullición.|Reducir el fuego y cocinar a fuego lento durante 30 minutos.|Retirar la pechuga de pollo, desmenuzarla y volver a ponerla en la olla.|Agregar el cilantro picado, sal y pimienta al gusto, y cocinar por 5 minutos más.|Servir caliente.','2024-10-21 13:27:57',1,3),(8,'Omelete de Huevo Clásico',2,10,'principal','2 huevos|Sal al gusto|Pimienta al gusto|2 cucharadas de leche|1 cucharada de mantequilla o aceite','Batir los huevos en un bol con la leche, sal y pimienta hasta que estén bien mezclados.|Calentar una sartén a fuego medio y añadir la mantequilla o el aceite.|Verter la mezcla de huevos en la sartén caliente, asegurándose de que se extienda uniformemente.|Cocinar durante unos 3-4 minutos o hasta que los bordes comiencen a levantarse del sartén.|Usar una espátula para doblar cuidadosamente una mitad del omelete sobre la otra, formando una media luna.|Servir caliente.','2024-10-21 13:30:09',0,0),(9,'Arroz con Pollo al Estilo Omelete',2,45,'principal','1 taza de arroz|2 tazas de agua|2 pechugas de pollo sin piel y sin hueso|1 cebolla grande picada|1 pimiento rojo picado|2 dientes de ajo picados|1/2 taza de guisantes|3 huevos|1/4 taza de leche|Aceite de oliva|Sal y pimienta al gusto|Perejil picado para decorar','Lavar el arroz hasta que el agua salga clara para eliminar el exceso de almidón.|En una sartén con un poco de aceite de oliva, sofreír la cebolla, el pimiento rojo y el ajo hasta que estén blandos.|Añadir el pollo cortado en cubos pequeños y cocinar hasta que esté dorado.|Incorporar el arroz y los guisantes, mezclar bien.|Agregar las 2 tazas de agua, sal y pimienta al gusto, y llevar a ebullición.|Reducir el fuego, tapar y dejar cocinar por 20 minutos o hasta que el arroz esté cocido.|En un bol, batir los huevos con la leche, sal y pimienta.|Verter la mezcla de huevo sobre el arroz cocido, sin mezclar, y cocinar a fuego lento hasta que los huevos estén cuajados.|Servir caliente, decorado con perejil picado.','2024-10-21 16:10:14',0,0),(10,'Arroz con Pollo y Huevo en Tortilla',2,45,'principal','2 tazas de arroz blanco cocido|2 pechugas de pollo|4 huevos|1 cebolla grande|2 dientes de ajo|1 pimiento rojo|100 g de guisantes|4 cucharadas de aceite de oliva|Sal y pimienta al gusto|1 cucharadita de cúrcuma o colorante alimentario|1 cucharada de salsa de soja','1. Cortar el pollo en cubos pequeños y sazonar con sal, pimienta y un poco de salsa de soja.|2. Picar finamente la cebolla, el ajo y el pimiento.|3. Calentar dos cucharadas de aceite en una sartén grande y sofreír la cebolla, el ajo y el pimiento hasta que estén tiernos.|4. Añadir el pollo al sofrito y cocinar hasta que esté dorado y completamente cocido.|5. Incorporar el arroz y los guisantes al sofrito, mezclar bien. Añadir la cúrcuma o el colorante y ajustar la sal y la pimienta.|6. En otra sartén, calentar el resto del aceite y batir los huevos. Verter los huevos en la sartén para hacer una tortilla grande.|7. Una vez la tortilla esté cocida por un lado, volcar sobre ella el arroz con pollo y doblarla por la mitad, dejando el arroz dentro.|8. Cocinar la tortilla rellena por ambos lados hasta que los huevos estén completamente cocidos.|9. Servir caliente.','2024-10-21 16:14:39',0,0),(11,'Arroz Omurice con Pollo',2,45,'principal','2 tazas de arroz cocido|200 gramos de pechuga de pollo cortada en cubos pequeños|1 cebolla mediana picada|1/2 pimiento verde picado|1/2 pimiento rojo picado|2 dientes de ajo finamente picados|3 huevos|2 cucharadas de salsa de soja|1 cucharada de ketchup|1 cucharadita de azúcar|Aceite vegetal|Sal y pimienta al gusto','Calienta un poco de aceite en una sartén grande a fuego medio-alto. Añade el pollo y saltea hasta que esté completamente cocido y dorado. Reserva.|En la misma sartén, añade un poco más de aceite si es necesario y sofríe la cebolla, los pimientos y el ajo hasta que estén blandos y fragantes.|Incorpora el arroz cocido y mezcla bien. Añade la salsa de soja, el ketchup y el azúcar. Revuelve todo junto y cocina por unos 5 minutos hasta que los sabores se mezclen.|Regresa el pollo a la sartén y mezcla bien con el arroz. Sazona con sal y pimienta al gusto.|En un bol pequeño, bate los huevos. Calienta una sartén pequeña con un poco de aceite a fuego medio. Vierte los huevos batidos y mueve la sartén para que se extiendan uniformemente. Cocina hasta que los huevos estén casi cuajados pero aún ligeramente líquidos en la superficie.|Coloca el arroz frito en un plato y forma un montículo ovalado. Con cuidado, cubre el arroz con la tortilla de huevo.|Sirve caliente, opcionalmente puedes añadir un poco más de ketchup encima antes de servir.','2024-10-21 16:22:23',0,0),(12,'Arroz Envuelto en Omelete con Pollo y Verduras',3,45,'principal','2 tazas de arroz cocido|200 g de pechuga de pollo cocida y desmenuzada|1 zanahoria grande, picada finamente|1 pimiento verde, picado finamente|1 cebolla pequeña, picada finamente|2 dientes de ajo, picados finamente|4 huevos grandes|Sal y pimienta al gusto|Aceite de oliva o vegetal|Salsa de soja (opcional para servir)|Cebollín picado para decorar','En un sartén grande, calienta un poco de aceite a fuego medio y saltea la cebolla y el ajo hasta que estén transparentes.|Añade la zanahoria y el pimiento, y cocina por unos minutos hasta que estén tiernos.|Incorpora el pollo desmenuzado y el arroz cocido. Revuelve bien y cocina hasta que todo esté caliente. Ajusta de sal y pimienta.|En un bol, bate los huevos con un poco de sal y pimienta.|En una sartén aparte, calienta un poco de aceite y vierte la mitad de los huevos batidos, moviendo la sartén para que el huevo cubra toda la superficie.|Cuando el huevo esté casi cocido pero aún ligeramente líquido en la superficie, coloca la mitad del relleno de arroz y pollo en el centro del omelete.|Dobla los lados del omelete sobre el relleno para envolverlo completamente. Deja cocinar por un minuto más.|Repite el proceso con el resto de los huevos y el relleno para hacer otro omelete.|Sirve caliente, adornado con cebollín y opcionalmente con un poco de salsa de soja.','2024-10-21 16:34:30',1,4),(13,'Salchipapas Clásicas',2,30,'principal','4 salchichas grandes|4 papas grandes|Aceite para freír|Sal al gusto|Salsas al gusto (ketchup, mayonesa, mostaza)','Pelar las papas y cortarlas en forma de bastones.|Calentar aceite en una sartén profunda o freidora.|Freír las papas hasta que estén doradas y crujientes. Retirar y escurrir en papel absorbente. Sazonar con sal.|Cortar las salchichas en rodajas y freír en el mismo aceite hasta que estén doradas.|Servir las papas fritas con las rodajas de salchicha encima.|Agregar salsas al gusto.','2024-10-21 16:46:28',0,0),(14,'Arroz con Leche Tradicional',2,45,'postre','1 taza de arroz blanco|4 tazas de leche|1 rama de canela|1/2 taza de azúcar|1 cucharadita de extracto de vainilla|Canela en polvo para decorar|Piel de limón (opcional)','Enjuagar el arroz bajo agua fría hasta que el agua salga clara.|Colocar el arroz, la leche y la rama de canela en una olla grande a fuego medio.|Cocinar, revolviendo ocasionalmente para evitar que se pegue, hasta que el arroz esté tierno y la mezcla haya espesado, aproximadamente 30-35 minutos.|Agregar el azúcar y el extracto de vainilla, y continuar cocinando durante 10 minutos más.|Retirar del fuego y eliminar la rama de canela y la piel de limón si se usó.|Verter el arroz con leche en recipientes y dejar enfriar a temperatura ambiente.|Refrigerar hasta que esté completamente frío.|Servir frío y espolvorear con canela en polvo antes de servir.','2024-10-21 17:33:54',0,5),(15,'Lomo Saltado alla Pizzaiola',3,45,'principal','500 g de lomo de res en tiras|2 cucharadas de aceite de oliva|1 cebolla grande, cortada en julianas|2 tomates grandes, pelados y cortados en tiras|3 dientes de ajo, picados|1/2 taza de vino tinto|1 cucharada de orégano seco|1 cucharada de albahaca fresca picada|Sal y pimienta al gusto|200 g de champiñones en láminas|2 pimientos, uno rojo y otro verde, cortados en tiras|Perejil fresco picado para decorar|200 g de papas fritas para acompañar','Calentar el aceite de oliva en una sartén grande a fuego medio-alto.|Agregar las tiras de lomo y saltear hasta que estén doradas y cocidas.|Retirar la carne de la sartén y reservar.|En la misma sartén, añadir más aceite si es necesario y cocinar la cebolla, el ajo, los pimientos y los champiñones hasta que estén tiernos.|Incorporar los tomates, el vino tinto, el orégano y la albahaca. Cocinar a fuego lento durante 10 minutos.|Volver a añadir la carne a la sartén y mezclar bien. Cocinar todo junto durante 5 minutos más para que los sabores se mezclen.|Sazonar con sal y pimienta al gusto.|Servir el lomo saltado sobre un lecho de papas fritas y espolvorear con perejil fresco picado.','2024-10-21 19:35:01',0,0),(16,'Sopa Minuta Vegana',2,30,'sopa','1 cucharada de aceite de oliva|1 cebolla mediana, picada|2 dientes de ajo, picados|1 zanahoria grande, pelada y cortada en cubos|1 tallo de apio, cortado en cubos|4 tazas de caldo de verduras|1 taza de tomate triturado|1/2 taza de pasta pequeña (ejemplo: macarrones o conchitas)|1 cucharadita de orégano seco|Sal y pimienta al gusto|Perejil fresco picado para decorar','Calentar el aceite en una olla grande a fuego medio.|Agregar la cebolla y el ajo y sofreír hasta que estén transparentes, aproximadamente 5 minutos.|Incorporar la zanahoria y el apio y cocinar durante 5 minutos más, removiendo ocasionalmente.|Verter el caldo de verduras y el tomate triturado. Llevar a ebullición.|Añadir la pasta y el orégano. Reducir el fuego y cocinar hasta que la pasta esté al dente, unos 10 minutos.|Sazonar con sal y pimienta al gusto.|Servir caliente, decorado con perejil fresco picado.','2024-10-21 19:48:34',0,0),(17,'Arroz Saltado con Tofu',2,30,'principal','200 gramos de tofu firme|2 tazas de arroz cocido|1 cebolla roja grande, cortada en julianas|1 pimiento rojo, cortado en tiras|2 tomates, cortados en gajos|3 dientes de ajo, finamente picados|4 cucharadas de salsa de soja|1 cucharada de vinagre|Aceite vegetal|Cilantro fresco picado|Sal y pimienta al gusto','Corta el tofu en cubos pequeños y marínalo con 2 cucharadas de salsa de soja.|Calienta un poco de aceite en un wok o sartén grande a fuego alto y fríe el tofu hasta que esté dorado. Retíralo y reserva.|En el mismo wok, añade un poco más de aceite y sofríe la cebolla, el pimiento y el ajo hasta que estén tiernos.|Agrega los tomates y cocina durante unos minutos hasta que empiecen a suavizarse.|Incorpora el arroz cocido, el tofu, el vinagre y las 2 cucharadas restantes de salsa de soja.|Saltea todo junto durante unos 5 minutos o hasta que el arroz esté bien caliente y los sabores combinados.|Ajusta de sal y pimienta, y justo antes de servir, esparce cilantro fresco picado por encima.','2024-10-22 11:06:40',0,0),(18,'Pachamanca Tradicional',4,240,'principal','1 kg de carne de cordero|1 kg de carne de cerdo|1 kg de pollo|1 kg de papas|500 gr de camote|500 gr de habas|4 choclos|200 gr de ají panca molido|4 hojas de huacatay|Sal y pimienta al gusto|Hojas de plátano para envolver','Limpiar y marinar las carnes con ají panca, sal, pimienta y huacatay durante al menos 2 horas.|Preparar el hoyo en la tierra, calentar piedras al rojo vivo en un fuego.|Colocar una base de piedras calientes en el fondo del hoyo.|Envolver las carnes, papas, camotes, habas y choclos en hojas de plátano.|Colocar los ingredientes envueltos sobre la capa de piedras calientes, intercalando con más piedras calientes.|Cubrir todo con más hojas de plátano y finalmente cubrir el hoyo con tierra para que no escape el calor.|Dejar cocinar durante aproximadamente 3 horas.|Desenterrar cuidadosamente la pachamanca y servir caliente.','2024-10-22 11:29:19',1,0),(19,'Lomo de Tofu Saltado con Hierbas Andinas',3,30,'principal','400 gramos de tofu firme|2 cucharadas de aceite de oliva|1 cebolla roja grande, cortada en julianas|2 tomates, cortados en trozos grandes|1 ají amarillo, sin semillas y cortado en tiras|1 manojo de cilantro fresco, picado|1 manojo de huacatay, picado|Jugo de 1 limón|2 dientes de ajo, finamente picados|Sal y pimienta al gusto|1/4 de taza de salsa de soja|1/2 cucharadita de comino|1 cucharada de vinagre blanco','Cortar el tofu en cubos medianos y secarlos bien con papel toalla.|Calentar el aceite en una sartén grande a fuego medio-alto.|Agregar el tofu en una sola capa y freír hasta que esté dorado y crujiente, aproximadamente 8 minutos. Retirar el tofu y reservar.|En la misma sartén, añadir la cebolla y el ají amarillo, saltear hasta que estén suaves.|Agregar el ajo y cocinar por un minuto más.|Incorporar los tomates, el comino y el vinagre, cocinar por 2 minutos.|Volver el tofu a la sartén, añadir la salsa de soja y el jugo de limón.|Cocinar todo junto por 5 minutos a fuego medio, ajustando la sazón con sal y pimienta.|Apagar el fuego y esparcir las hierbas frescas picadas (cilantro y huacatay) sobre el saltado.|Servir inmediatamente.','2024-10-23 17:53:02',0,0),(20,'Tofu Andino al Ají con Arroz de Quinua',3,45,'principal','200 g de tofu firme|1 taza de quinua|2 tazas de caldo de verduras|1 cebolla roja picada|2 dientes de ajo picados|1 ají amarillo, sin semillas y picado|1 tomate grande picado|2 cucharadas de aceite de oliva|Sal y pimienta al gusto|Cilantro fresco picado para decorar','Corta el tofu en cubos medianos y reserva.|Enjuaga la quinua bajo agua fría hasta que el agua salga clara.|En una olla, calienta una cucharada de aceite de oliva y sofríe la cebolla y el ajo hasta que estén transparentes.|Añade el ají amarillo y el tomate picado, cocina por unos minutos hasta que el tomate se deshaga.|Incorpora el tofu y cocina durante 5 minutos más, removiendo ocasionalmente.|En otra olla, calienta el caldo de verduras y, cuando esté hirviendo, añade la quinua.|Reduce el fuego y cocina la quinua tapada durante 15 minutos o hasta que esté tierna y haya absorbido todo el líquido.|Una vez cocida la quinua, mezcla con el sofrito de tofu.|Ajusta de sal y pimienta y sirve caliente.|Decora con cilantro fresco picado antes de servir.','2024-10-23 17:53:50',0,0),(21,'Tofu Andino al Estilo Pachamanca',3,120,'principal','400 gr de tofu firme|200 gr de papas andinas|1 cebolla roja grande|2 dientes de ajo|1 ramita de romero|1 ramita de tomillo|100 gr de habas|2 choclos cortados en rodajas|50 ml de aceite de oliva|Sal y pimienta al gusto|Hojas de plátano para envolver','Paso 1: Cortar el tofu en cubos grandes y marinar con aceite de oliva, ajo picado, sal y pimienta.|Paso 2: Precalentar el horno a 200°C.|Paso 3: Envolver las papas, habas, choclos, cebolla y tofu marinado en hojas de plátano, agregando romero y tomillo entre las capas.|Paso 4: Colocar los paquetes de hojas de plátano en una bandeja y cubrir con más hojas para simular la técnica de cocción andina de la pachamanca.|Paso 5: Hornear durante aproximadamente 90 minutos o hasta que las verduras estén tiernas y el tofu bien cocido.|Paso 6: Servir caliente, retirando cuidadosamente las hojas de plátano.','2024-10-23 17:55:31',0,0),(22,'Tofu Andino Relleno de Quinua y Hierbas',3,45,'principal','200 g de tofu firme|1 taza de quinua|2 tazas de caldo de verduras|1 cucharada de aceite de oliva|1 cebolla pequeña, finamente picada|2 dientes de ajo, finamente picados|1 zanahoria pequeña, rallada|1/4 taza de perejil fresco, picado|1/4 taza de cilantro fresco, picado|Sal y pimienta al gusto','Cocinar la quinua en el caldo de verduras hasta que esté tierna y haya absorbido todo el líquido, aproximadamente 15 minutos. Dejar enfriar.|En una sartén, calentar el aceite de oliva y sofreír la cebolla y el ajo hasta que estén dorados. Añadir la zanahoria y cocinar por unos minutos más.|Mezclar la quinua cocida con el sofrito de cebolla, zanahoria, ajo, el perejil, el cilantro, sal y pimienta. Corregir el sazón.|Cortar el tofu en bloques de aproximadamente 2x3 pulgadas y con un cuchillo cuidadosamente hacer un corte en el centro para formar un bolsillo.|Rellenar los bolsillos de tofu con la mezcla de quinua y hierbas.|Colocar el tofu relleno en una bandeja para hornear ligeramente aceitada y hornear a 180°C (350°F) durante 20 minutos o hasta que el tofu esté dorado y caliente por dentro.','2024-10-23 18:00:46',0,0),(23,'Tofu Andino a la Parrilla con Salsa de Rocoto y Quinua',3,45,'principal','400 g de tofu firme|1 taza de quinua|2 tazas de agua|1 rocoto|1 diente de ajo|1/4 de taza de aceite vegetal|Jugo de 1 limón|Sal y pimienta al gusto|1 cucharada de perejil picado|Hojas de lechuga para servir','Lavar bien la quinua bajo el grifo y cocinarla en dos tazas de agua con una pizca de sal hasta que esté tierna, aproximadamente 15 minutos. Escurrir y reservar.|Cortar el tofu en cubos grandes y sazonar con sal y pimienta.|Calentar la parrilla a fuego medio-alto y asar los cubos de tofu hasta que estén dorados y crujientes, aproximadamente 10 minutos por lado.|Para la salsa de rocoto, retirar las semillas del rocoto y licuarlo junto con el ajo, aceite vegetal, jugo de limón, sal y pimienta hasta obtener una salsa homogénea.|Mezclar la quinua cocida con el perejil picado y ajustar la sazón.|Servir el tofu a la parrilla sobre un lecho de hojas de lechuga, acompañado de la quinua y bañado con la salsa de rocoto.','2024-10-23 18:04:18',0,0),(24,'Tofu Andino en Salsa de Ají Amarillo con Timbal de Quinua y Hierbas',3,45,'principal','200 gramos de tofu firme|1 taza de quinua|2 tazas de caldo de verduras|1 cucharada de aceite de oliva|1 cebolla roja, finamente picada|2 dientes de ajo, picados|3 ajíes amarillos, sin semillas y picados|200 ml de leche de coco|Sal y pimienta al gusto|1/2 taza de cilantro fresco, picado|1/2 taza de perejil fresco, picado|Jugo de 1 limón','Presionar el tofu con un peso encima durante al menos 30 minutos para extraer el exceso de agua y luego cortarlo en cubos.|Enjuagar la quinua bajo agua fría y luego cocinarla en el caldo de verduras hasta que esté tierna, unos 15 minutos. Dejar enfriar.|Calentar el aceite en una sartén y saltear la cebolla y el ajo hasta que estén transparentes.|Agregar los ajíes amarillos y cocinar por unos minutos más.|Incorporar el tofu y la leche de coco, cocinar a fuego medio hasta que el tofu esté caliente y la salsa se haya reducido un poco, aproximadamente 10 minutos.|Condimentar con sal y pimienta al gusto.|En un bol grande, mezclar la quinua cocida con el cilantro, el perejil y el jugo de limón.|Para servir, usar un molde de timbal o un tazón pequeño para formar los timbales de quinua. Colocar el timbal en el centro del plato y verter alrededor el tofu en salsa de ají amarillo.','2024-10-24 11:08:14',0,0);
/*!40000 ALTER TABLE `receta` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sugerencias`
--

DROP TABLE IF EXISTS `sugerencias`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sugerencias` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(300) DEFAULT NULL,
  `idreceta` int NOT NULL,
  `iduser` int NOT NULL,
  `fechareg` date DEFAULT NULL,
  `estado` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `idreceta` (`idreceta`),
  CONSTRAINT `sugerencias_ibfk_1` FOREIGN KEY (`idreceta`) REFERENCES `receta` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sugerencias`
--

LOCK TABLES `sugerencias` WRITE;
/*!40000 ALTER TABLE `sugerencias` DISABLE KEYS */;
INSERT INTO `sugerencias` VALUES (1,'Arroz Omurice con Pollo',11,1,'2024-10-20',1),(2,'Arroz Envuelto en Omelete con Pollo y Verduras',12,1,'2024-10-21',0),(3,'Arroz Saltado con Tofu',17,1,'2024-10-22',1),(4,'Tofu Andino a la Parrilla con Salsa de Rocoto y Quinua',23,1,'2024-10-23',0),(5,'Tofu Andino en Salsa de Ají Amarillo con Timbal de Quinua y Hierbas',24,1,'2024-10-24',0);
/*!40000 ALTER TABLE `sugerencias` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuario`
--

DROP TABLE IF EXISTS `usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuario` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nombres` varchar(100) NOT NULL,
  `dni` varchar(8) DEFAULT NULL,
  `correo` varchar(120) DEFAULT NULL,
  `clave` varchar(50) NOT NULL,
  `idsexo` int NOT NULL,
  `idcargo` int NOT NULL,
  `fechareg` datetime DEFAULT NULL,
  `estado` int NOT NULL,
  `region` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario`
--

LOCK TABLES `usuario` WRITE;
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
INSERT INTO `usuario` VALUES (1,'INVITADO','12345678','correo@correo','123',1,0,'2024-10-18 12:17:54',1,'PERU'),(2,'YADER','43114343','yaderch@gmail.com','palomita',1,1,'2024-10-18 12:17:54',2,'ITALIA'),(3,'CARLOS','12345678','correo@gmail.com','1234',1,1,'2024-10-18 12:17:54',1,'PERU'),(4,'USUARIO1','12345678','correo@gmail.com','1234',1,1,'2024-10-21 10:31:54',2,'PERU'),(5,'USUARIO2','12345678','correo@gmail.com','1234',1,1,'2024-10-21 10:31:54',2,'PERU'),(6,'USUARIO3','12345678','correo@gmail.com','1234',1,1,'2024-10-21 10:31:54',2,'PERU');
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'dbsk'
--

--
-- Dumping routines for database 'dbsk'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-10-24 13:18:34
