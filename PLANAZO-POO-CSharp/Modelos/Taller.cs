using System;

namespace Planazo.Modelos
{
    public class Taller : Evento
    {
        public Taller(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
            : base(id, nombre, fecha, precioBase, capacidadMaxima) { }

        public override decimal CalcularPrecioFinal()
        {
            return PrecioBase * 0.90m;
        }
    }
}