using Microsoft.AspNetCore.Mvc;
using API_Investigacion.Models;
using API_Investigacion.Interfaces;

namespace API_Investigacion.Controllers
{
    [ApiController]
    [Route("api/Facturas")]
    public class FacturasController : Controller
    {
        private DbContextInvestigacion dbContext = null;
        private IMensajeriaService mensajeriaService = null;

        public FacturasController(DbContextInvestigacion context, IMensajeriaService mensajeriaService)
        {
            this.dbContext = context;
            this.mensajeriaService = mensajeriaService;
        }

        [HttpGet]
        [Route("List")]
        public List<Factura> List()
        {
            return this.dbContext.Facturas.ToList();
        }

        [HttpGet]
        [Route("Search")]
        public Factura Search(int id)
        {
            Factura temp = new Factura() { Id = id, Total = 0, FechaEmision = DateTime.Now };

            try
            {
                var aux = this.dbContext.Facturas.FirstOrDefault(x => x.Id == id);

                if (aux != null)
                {
                    temp = aux;
                }
            }
            catch (Exception ex)
            {
                temp.Total = -1;
                var _ = ex.Message;
            }
            return temp;
        }

        [HttpPost]
        [Route("Create")]
        public String Create(Factura temp)
        {
            String msj = "";

            try
            {
                if (temp == null)
                {
                    msj = "No se permiten datos vacios...";
                }
                else if (temp.ClienteId <= 0)
                {
                    msj = "Debe indicar el cliente de la factura...";
                }
                else if (temp.Total <= 0)
                {
                    msj = "El total debe ser mayor que cero...";
                }
                else
                {
                    this.dbContext.Facturas.Add(temp);
                    this.dbContext.SaveChanges();
                    msj = $"Factura {temp.Id} almacenada correctamente...";
                    this.mensajeriaService.EnviarMensaje($"Mensaje cliente: Factura {temp.Id} almacenada correctamente...");
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
        public String Update(Factura temp)
        {
            String msj = "";

            try
            {
                if (temp == null)
                {
                    msj = "No se permiten datos vacios...";
                }
                else if (temp.ClienteId <= 0)
                {
                    msj = "Debe indicar el cliente de la factura...";
                }
                else if (temp.Total <= 0)
                {
                    msj = "El total debe ser mayor que cero...";
                }
                else
                {
                    var facturaActual = this.dbContext.Facturas.FirstOrDefault(x => x.Id == temp.Id);

                    if (facturaActual != null)
                    {
                        facturaActual.ClienteId = temp.ClienteId;
                        facturaActual.Total = temp.Total;
                        facturaActual.FechaEmision = temp.FechaEmision;

                        this.dbContext.Facturas.Update(facturaActual);
                        this.dbContext.SaveChanges();
                        msj = $"Factura {temp.Id} actualizada correctamente...";
                    }
                    else
                    {
                        msj = $"No existe una factura con el codigo {temp.Id}";
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
                var temp = this.dbContext.Facturas.FirstOrDefault(x => x.Id == id);

                if (temp != null)
                {
                    this.dbContext.Facturas.Remove(temp);
                    this.dbContext.SaveChanges();
                    msj = "Factura eliminada correctamente...";
                }
                else
                {
                    msj = $"No existe una factura con el codigo {id}";
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
