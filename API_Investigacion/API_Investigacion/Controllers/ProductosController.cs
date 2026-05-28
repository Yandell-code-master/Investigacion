using Microsoft.AspNetCore.Mvc;
using API_Investigacion.Models;

namespace API_Investigacion.Controllers
{
    [ApiController]
    [Route("api/Productos")]
    public class ProductosController : Controller
    {
        //Variable para manejar la referencia del ORM
        private DbContextInvestigacion dbContext = null;

        public ProductosController(DbContextInvestigacion context)
        {
            //se asigna la referencia de dbContext
            this.dbContext = context;
        }

        //Metodo encargado de obtener la lista de productos
        [HttpGet]
        [Route("List")]

        public List<Producto> List()
        {
            return this.dbContext.Productos.ToList();
        }

        //EndPoint encargado de buscar un producto por su codigo 
        [HttpGet]
        [Route("Search")]

        public Producto Search(string id)
        {
            Producto temp = new Producto() { CodigoInterno = id, Descripcion = "No existe..." };

            try
            {
                //Se busca el producto
                var aux = this.dbContext.Productos.FirstOrDefault(x => x.CodigoInterno == id);

                //Se valida si existe el producto
                if (aux != null)
                {
                    temp = aux;
                }

            }
            catch (Exception ex)
            {
                temp.Descripcion = $"Error, {ex.InnerException.Message}";
            }
            return temp;
        }

        //Metodo encargado de almacenar un producto
        [HttpPost]
        [Route("Create")]

        public String Create(Producto temp)
        {
            String msj = "";

            try
            {
                //Se valida si existen datos
                if (temp == null)
                {
                    msj = "No se permiten datos vacios...";
                }
                else if (temp.CodigoInterno == "")
                {
                    msj = "Debe indicar el codigo interno del producto...";
                }
                else if (temp.Descripcion == "")
                {
                    msj = "Debe indicar la descripcion del producto...";
                }
                else if (temp.PrecioVenta <= 0)
                {
                    msj = "El precio de venta debe ser mayor que cero...";
                }
                else if (temp.Existencia < 0)
                {
                    msj = "La existencia no puede ser negativa...";
                }
                else
                {
                    //Se agrega el producto
                    this.dbContext.Productos.Add(temp);

                    //Se aplican los cambios
                    this.dbContext.SaveChanges();

                    msj = $"Producto {temp.Descripcion} almacenado correctamente...";
                }

            }
            catch (Exception ex)
            {
                msj = $"Error al guardar, {ex.InnerException.Message}";
            }
            return msj;
        }

        //Metodo encargado de actualizar un producto
        [HttpPut]
        [Route("Update")]

        public String Update(Producto temp)
        {
            String msj = "";

            try
            {
                //Se valida si existen datos
                if (temp == null)
                {
                    msj = "No se permiten datos vacios...";
                }
                else if (temp.CodigoInterno == "")
                {
                    msj = "Debe indicar el codigo interno del producto...";
                }
                else if (temp.Descripcion == "")
                {
                    msj = "Debe indicar la descripcion del producto...";
                }
                else if (temp.PrecioVenta <= 0)
                {
                    msj = "El precio de venta debe ser mayor que cero...";
                }
                else if (temp.Existencia < 0)
                {
                    msj = "La existencia no puede ser negativa...";
                }
                else
                {
                    //Se busca el producto actual
                    var productoActual = this.dbContext.Productos.FirstOrDefault(x => x.CodigoInterno == temp.CodigoInterno);

                    //Se valida si existe el producto
                    if (productoActual != null)
                    {
                        //Se actualizan los datos
                        productoActual.CodigoBarra = temp.CodigoBarra;
                        productoActual.Descripcion = temp.Descripcion;
                        productoActual.PrecioVenta = temp.PrecioVenta;
                        productoActual.Existencia = temp.Existencia;

                        //Se actualiza el registro
                        this.dbContext.Productos.Update(productoActual);

                        //Se aplican los cambios
                        this.dbContext.SaveChanges();

                        msj = $"Producto {temp.Descripcion} actualizado correctamente...";
                    }
                    else
                    {
                        msj = $"No existe un producto con el codigo {temp.CodigoInterno}";
                    }
                }

            }
            catch (Exception ex)
            {
                msj = $"Error al modificar, {ex.InnerException.Message}";
            }
            return msj;
        }

        //Metodo encargado de eliminar un producto por su codigo 
        [HttpDelete]
        [Route("Delete")]

        public String Delete(string id)
        {
            String msj = "";

            try
            {
                //Se busca el producto por su codigo
                var temp = this.dbContext.Productos.FirstOrDefault(x => x.CodigoInterno == id);

                //Se valida si existe el producto
                if (temp != null)
                {
                    //Se elimina el producto
                    this.dbContext.Productos.Remove(temp);

                    //Se aplican los cambios
                    this.dbContext.SaveChanges();

                    msj = "Producto eliminado correctamente...";
                }
                else
                {
                    msj = $"No existe un producto con el codigo {id}";
                }

            }
            catch (Exception ex)
            {
                msj = $"Error al eliminar, {ex.InnerException.Message}";
            }
            return msj;
        }

    }
}