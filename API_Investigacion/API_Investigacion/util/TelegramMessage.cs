using API_Investigacion.Interfaces;

namespace API_Investigacion.util
{
    public class TelegramMessage : IMensajeriaService
    {
        public void EnviarMensaje(string mensaje)
        {
            Console.WriteLine($"Enviando mensaje a Telegram: {mensaje}");
        }
    }
}
