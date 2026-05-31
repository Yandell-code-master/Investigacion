using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace API_Investigacion.Interfaces
{
    public interface IMensajeriaService
    {
        public void EnviarMensaje(string mensaje);
    }
}
