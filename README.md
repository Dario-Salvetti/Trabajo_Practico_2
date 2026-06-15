Guia basica del usuario 
En esta guia podra ver los endpoints de la Api Rest y que opciones tiene para usar

1. Ver todas las categorias
   GET  http://localhost:5047/Catalogo/
   Alli podra ver todas las categorias creadas
2. Crear una categoria
   POST http://localhost:5047/Catalogo/
   Ejemplo
   {
    "Tipo":"Lacteos"
   }
3. Ver todos los productos
    GET  http://localhost:5047/Productos/
4. Crear un producto
   POST http://localhost:5047/Productos/
   Ejemplo
   {
    "Nombre":"nombreDelProducto",
    "Marca": "marcaDelProducto",
    "Presentacion": "envase1litro",
    "Stock": cantidadIngresada,
    "Precio": valor;
   }
  5. Actualizar un producto
     PUT http://localhost:5047/Productos/

 6. Eliminar un producto
   DELETE http://localhost:5047/Productos/
      
    
