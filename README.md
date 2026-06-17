Guia basica del usuario 

En esta guia podra ver los endpoints de la Api

1. Ver todas las categorias
   GET  http://localhost:5047/Catalogo/
   
2. Ver una categoria
   GET http://localhost:5047/Catalogo/id*
   
3. Crear una categoria
   POST http://localhost:5047/Catalogo/
   
4. Ver productos dentro de una categoria
  GET  http://localhost:5047/Catalogo/id*/Productos/

5. Borrar categoria
   DELETE http://localhost:5047/Catalogo/id*
   
6. Ver todos los productos
   GET  http://localhost:5047/Catalogo/Productos/
   
7. Crear un producto
   POST http://localhost:5047/Producto/
  
8. Cambiar un producto (todos sus atributos)
  PUT http://localhost:5047/Producto/id*

9. Eliminar un producto
   DELETE http://localhost:5047/Producto/id*

10. Buscar un producto
    GET http://localhost:5047/Producto/id*


    *id: es el identificador de categoria o producto segun corresponda en formato numero
    El puerto (localhost: xxxx) puede cambiar segun la Pc donde se ejecute la API
      
    
