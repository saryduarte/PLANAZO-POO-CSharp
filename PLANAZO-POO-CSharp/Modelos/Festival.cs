using System;

namespace Planazo.Modelos
{
    public class Festival : Evento
    {
        public Festival(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
            : base(id, nombre, fecha, precioBase, capacidadMaxima) { }

        // Regla propia: recargo del 20% por servicios e impuestos de festival
        public override decimal CalcularPrecioFinal()
        {
            return PrecioBase * 1.20m;
        }
    }
}