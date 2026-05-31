using API_Investigacion.Interfaces;

namespace API_Investigacion.util
{
    public class EmailMessage : IMensajeriaService
    {
        public void EnviarMensaje(string mensaje)
        {
            Console.WriteLine($"Enviando email con el mensaje: {mensaje}");
        }
    }
}
