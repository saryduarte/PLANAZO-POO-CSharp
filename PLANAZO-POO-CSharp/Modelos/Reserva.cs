using System;

namespace Planazo.Modelos
{
    public enum EstadoReserva
    {
        Activa,
        Cancelada
    }

    public class Reserva
    {
        public int Id { get; private set; }
        public Usuario Cliente { get; private set; }
        public Evento EventoReservado { get; private set; }
        public int CantidadEntradas { get; private set; }
        public decimal ValorTotal { get; private set; }
        public EstadoReserva Estado { get; private set; }

        public Reserva(int id, Usuario cliente, Evento eventoReservado, int cantidadEntradas)
        {
            if (cantidadEntradas <= 0)
                throw new ArgumentException("La cantidad de entradas debe ser mayor a cero.");

            Id = id;
            Cliente = cliente;
            EventoReservado = eventoReservado;
            CantidadEntradas = cantidadEntradas;
            ValorTotal = eventoReservado.CalcularPrecioFinal() * cantidadEntradas;
            Estado = EstadoReserva.Activa;
        }

        public void Cancelar()
        {
            if (Estado == EstadoReserva.Cancelada)
                throw new InvalidOperationException("La reserva ya ha sido cancelada anteriormente.");

            Estado = EstadoReserva.Cancelada;
            EventoReservado.DevolverCupos(CantidadEntradas);
        }
    }
}