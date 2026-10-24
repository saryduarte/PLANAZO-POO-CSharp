using System;
using System.Collections.Generic;
using System.Linq;
using Planazo.Modelos;

namespace Planazo.Servicios
{
    public class GestorReservas
    {
        private readonly List<Evento> _eventos = new();
        private readonly List<Reserva> _reservas = new();
        private int _contadorReserva = 1;

        public GestorReservas()
        {
            InicializarEventosPorDefecto();
        }

        private void InicializarEventosPorDefecto()
        {
            // Mínimo 2 películas, 2 conciertos y 2 talleres + festival de prueba
            _eventos.Add(new Cine(1, "Inception (Cine)", DateTime.Now.AddDays(5), 20000m, 50));
            _eventos.Add(new Cine(2, "Interstellar (Cine)", DateTime.Now.AddDays(7), 22000m, 40));
            _eventos.Add(new Concierto(3, "Rock en la Ciudad (Concierto)", DateTime.Now.AddDays(10), 50000m, 100));
            _eventos.Add(new Concierto(4, "Jazz Sunset (Concierto)", DateTime.Now.AddDays(12), 40000m, 60));
            _eventos.Add(new Taller(5, "Taller de Robótica", DateTime.Now.AddDays(3), 30000m, 25));
            _eventos.Add(new Taller(6, "Taller de Pintura y Óleo", DateTime.Now.AddDays(8), 25000m, 15));
            _eventos.Add(new Festival(7, "Festival Estéreo Beat", DateTime.Now.AddDays(20), 80000m, 200));
        }

        public IReadOnlyList<Evento> ObtenerEventos() => _eventos.AsReadOnly();
        public IReadOnlyList<Reserva> ObtenerReservas() => _reservas.AsReadOnly();

        public Evento ConsultarEvento(int id)
        {
            var evento = _eventos.FirstOrDefault(e => e.Id == id);
            if (evento == null)
                throw new KeyNotFoundException($"No se encontró ningún evento con el ID {id}.");
            return evento;
        }

        public Reserva CrearReserva(int idEvento, string idUsuario, string nombreUsuario, int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("No se permiten reservas con cantidades negativas o en cero.");

            var evento = ConsultarEvento(idEvento);

            if (evento.CuposDisponibles < cantidad)
                throw new InvalidOperationException($"Cupos insuficientes. Solicitados: {cantidad}, Disponibles: {evento.CuposDisponibles}.");

            if (!evento.ReducirCupos(cantidad))
                throw new InvalidOperationException("No fue posible descontar los cupos.");

            var usuario = new Usuario(idUsuario, nombreUsuario);
            var nuevaReserva = new Reserva(_contadorReserva++, usuario, evento, cantidad);
            _reservas.Add(nuevaReserva);

            return nuevaReserva;
        }

        public void CancelarReserva(int idReserva)
        {
            var reserva = _reservas.FirstOrDefault(r => r.Id == idReserva);
            if (reserva == null)
                throw new KeyNotFoundException($"No existe ninguna reserva con el ID {idReserva}.");

            reserva.Cancelar();
        }

        public (int totalReservasActivas, int totalEntradas, decimal valorTotal) ObtenerResumen()
        {
            var reservasActivas = _reservas.Where(r => r.Estado == EstadoReserva.Activa).ToList();
            int totalActivas = reservasActivas.Count;
            int totalEntradas = reservasActivas.Sum(r => r.CantidadEntradas);
            decimal valorTotal = reservasActivas.Sum(r => r.ValorTotal);

            return (totalActivas, totalEntradas, valorTotal);
        }

        public List<Reserva> ConsultarReservasPorUsuario(string idUsuario)
        {
            return _reservas.Where(r => r.Cliente.Identificacion.Equals(idUsuario, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}