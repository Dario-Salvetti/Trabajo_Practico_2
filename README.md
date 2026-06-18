Guia basica del usuario 

En esta guia podra ver los endpoints de la Api

1. Ver todos los catalogos
   GET  http://localhost:5047/Catalogo/
   
2. Ver un catalogo
   GET http://localhost:5047/Catalogo/id*
   
3. Crear un catalogo
   POST http://localhost:5047/Catalogo/
   Ejemplo de JSON:
   {
      "CatalogoNombre":"nombre del nuevo catalogo"
   }
   
4. Ver productos dentro de un catalogo
  GET  http://localhost:5047/Catalogo/id*/Productos/

5. Borrar catalogo
   DELETE http://localhost:5047/Catalogo/id*
   
6. Ver todos los productos
   GET  http://localhost:5047/Catalogo/Productos/
   
7. Crear un producto
   POST http://localhost:5047/Producto/id**
   Ejemplo de JSON:
   {
      "Marca":"nombre de la marca",
      "Nombre":"nombre del producto",
      "Presentacion":"kilos o litros del producto",
      "Stock": cantidad de producto,
      "Precio": costo que tendra el producto
   }
  
8. Cambiar un producto
  PUT http://localhost:5047/Producto
  Ejemplo de JSON:
  {
      "Id": Id del producto,
      "Precio": nuevo precio del producto,
      "Stock": cantidad a restar del producto (para agregar usar numeros negativos),
      "CatalogoNombre": Id del nuevo catalogo en el que estara
  }

9. Eliminar un producto
   DELETE http://localhost:5047/Producto/id*

10. Buscar un producto
    GET http://localhost:5047/Producto/id*


    *id: es el identificador de catalogo o producto segun corresponda en formato numero
    El puerto (localhost: xxxx) puede cambiar segun la Pc donde se ejecute la API
   ** al crear un producto el id en la url debe ser el del catalogo en el que ira.
    
