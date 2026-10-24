using System;

namespace Planazo.Modelos
{
    public abstract class Evento
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public DateTime Fecha { get; private set; }
        public decimal PrecioBase { get; private set; }
        public int CapacidadMaxima { get; private set; }
        public int CuposDisponibles { get; private set; }

        protected Evento(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
        {
            if (precioBase < 0)
                throw new ArgumentException("El precio base no puede ser negativo.");
            if (capacidadMaxima <= 0)
                throw new ArgumentException("La capacidad debe ser mayor que cero.");

            Id = id;
            Nombre = nombre;
            Fecha = fecha;
            PrecioBase = precioBase;
            CapacidadMaxima = capacidadMaxima;
            CuposDisponibles = capacidadMaxima;
        }

        public abstract decimal CalcularPrecioFinal();

        public bool ReducirCupos(int cantidad)
        {
            if (cantidad <= 0 || cantidad > CuposDisponibles)
                return false;

            CuposDisponibles -= cantidad;
            return true;
        }

        public void DevolverCupos(int cantidad)
        {
            if (cantidad > 0)
            {
                CuposDisponibles = Math.Min(CapacidadMaxima, CuposDisponibles + cantidad);
            }
        }
    }
}