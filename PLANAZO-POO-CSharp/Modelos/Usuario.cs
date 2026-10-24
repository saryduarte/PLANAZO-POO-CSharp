namespace Planazo.Modelos
{
    public class Usuario
    {
        public string Identificacion { get; private set; }
        public string Nombre { get; private set; }

        public Usuario(string identificacion, string nombre)
        {
            Identificacion = identificacion;
            Nombre = nombre;
        }
    }
}