using Microsoft.AspNetCore.Mvc;
using API_Investigacion.Models;

namespace API_Investigacion.Controllers
{
    [ApiController]
    [Route("api/Clientes")]
    public class ClientesController : Controller
    {
        private DbContextInvestigacion dbContext = null;

        public ClientesController(DbContextInvestigacion context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        [Route("List")]
        public List<Cliente> List()
        {
            return this.dbContext.Clientes.ToList();
        }

        [HttpGet]
        [Route("Search")]
        public Cliente Search(int id)
        {
            Cliente temp = new Cliente() { id = id, Nombre = "No existe..." };

            try
            {
                var aux = this.dbContext.Clientes.FirstOrDefault(x => x.id == id);

                if (aux != null)
                {
                    temp = aux;
                }
            }
            catch (Exception ex)
            {
                temp.Nombre = $"Error, {ex.InnerException.Message}";
            }
            return temp;
        }

        [HttpPost]
        [Route("Create")]
        public String Create(Cliente temp)
        {
            String msj = "";

            try
            {
                if (temp == null)
                {
                    msj = "No se permiten datos vacios...";
                }
                else if (string.IsNullOrEmpty(temp.Nombre))
                {
                    msj = "Debe indicar el nombre del cliente...";
                }
                else
                {
                    this.dbContext.Clientes.Add(temp);
                    this.dbContext.SaveChanges();
                    msj = $"Cliente {temp.Nombre} almacenado correctamente...";
                }
            }
            catch (Exception ex)
            {
                msj = $"Error al guardar, {ex.InnerException.Message}";
            }
            return msj;
        }

        [HttpPut]
        [Route("Update")]
        public String Update(Cliente temp)
        {
            String msj = "";

            try
            {
                if (temp == null)
                {
                    msj = "No se permiten datos vacios...";
                }
                else if (string.IsNullOrEmpty(temp.Nombre))
                {
                    msj = "Debe indicar el nombre del cliente...";
                }
                else
                {
                    var clienteActual = this.dbContext.Clientes.FirstOrDefault(x => x.id == temp.id);

                    if (clienteActual != null)
                    {
                        clienteActual.Nombre = temp.Nombre;

                        this.dbContext.Clientes.Update(clienteActual);
                        this.dbContext.SaveChanges();
                        msj = $"Cliente {temp.Nombre} actualizado correctamente...";
                    }
                    else
                    {
                        msj = $"No existe un cliente con el codigo {temp.id}";
                    }
                }
            }
            catch (Exception ex)
            {
                msj = $"Error al modificar, {ex.InnerException.Message}";
            }
            return msj;
        }

        [HttpDelete]
        [Route("Delete")]
        public String Delete(int id)
        {
            String msj = "";

            try
            {
                var temp = this.dbContext.Clientes.FirstOrDefault(x => x.id == id);

                if (temp != null)
                {
                    this.dbContext.Clientes.Remove(temp);
                    this.dbContext.SaveChanges();
                    msj = "Cliente eliminado correctamente...";
                }
                else
                {
                    msj = $"No existe un cliente con el codigo {id}";
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
