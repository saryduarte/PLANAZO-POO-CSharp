using System;

namespace Planazo.Modelos
{
    public class Cine : Evento
    {
        public Cine(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
            : base(id, nombre, fecha, precioBase, capacidadMaxima) { }

        public override decimal CalcularPrecioFinal()
        {
            return PrecioBase;
        }
    }
}