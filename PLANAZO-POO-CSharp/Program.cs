using System;
using Planazo.Modelos;
using Planazo.Servicios;

namespace Planazo
{
    internal class Program
    {
        private static readonly GestorReservas gestor = new();

        static void Main(string[] args)
        {
            bool salir = false;
            while (!salir)
            {
                try
                {
                    MostrarMenu();
                    string opcion = Console.ReadLine()?.Trim() ?? "";
                    Console.Clear();

                    switch (opcion)
                    {
                        case "1":
                            VerEventos();
                            break;
                        case "2":
                            ConsultarEvento();
                            break;
                        case "3":
                            ReservarEntradas();
                            break;
                        case "4":
                            VerReservas();
                            break;
                        case "5":
                            CancelarReserva();
                            break;
                        case "6":
                            VerResumen();
                            break;
                        case "7":
                            ConsultarPorUsuario();
                            break;
                        case "0":
                            salir = true;
                            Console.WriteLine("Gracias por utilizar PLANAZO. ¡Hasta pronto!");
                            break;
                        default:
                            Console.WriteLine("Opción no válida. Por favor intente nuevamente.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERROR]: {ex.Message}");
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private static void MostrarMenu()
        {
            Console.WriteLine("================================");
            Console.WriteLine("        PLANAZO - EVENTOS       ");
            Console.WriteLine("================================");
            Console.WriteLine("1. Ver eventos disponibles");
            Console.WriteLine("2. Consultar evento");
            Console.WriteLine("3. Reservar entradas");
            Console.WriteLine("4. Ver reservas realizadas");
            Console.WriteLine("5. Cancelar una reserva");
            Console.WriteLine("6. Ver resumen de reservas");
            Console.WriteLine("7. Consultar reservas por usuario (Reto 2)");
            Console.WriteLine("0. Salir");
            Console.Write("\nSeleccione una opción: ");
        }

        private static void VerEventos()
        {
            Console.WriteLine("--- LISTADO DE EVENTOS ---");
            foreach (var ev in gestor.ObtenerEventos())
            {
                Console.WriteLine($"[{ev.Id}] {ev.Nombre} | Fecha: {ev.Fecha:yyyy-MM-dd} | Base: ${ev.PrecioBase:N0} | Precio Final: ${ev.CalcularPrecioFinal():N0} | Cupos: {ev.CuposDisponibles}/{ev.CapacidadMaxima}");
            }
        }

        private static void ConsultarEvento()
        {
            Console.Write("Ingrese el ID del evento a consultar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var ev = gestor.ConsultarEvento(id);
                Console.WriteLine("\n--- DETALLES DEL EVENTO ---");
                Console.WriteLine($"ID: {ev.Id}");
                Console.WriteLine($"Nombre: {ev.Nombre}");
                Console.WriteLine($"Tipo: {ev.GetType().Name}");
                Console.WriteLine($"Fecha: {ev.Fecha:yyyy-MM-dd}");
                Console.WriteLine($"Precio Base: ${ev.PrecioBase:N0}");
                Console.WriteLine($"Precio Final: ${ev.CalcularPrecioFinal():N0}");
                Console.WriteLine($"Cupos Disponibles: {ev.CuposDisponibles} de {ev.CapacidadMaxima}");
            }
            else
            {
                Console.WriteLine("El ID ingresado no es válido.");
            }
        }

        private static void ReservarEntradas()
        {
            Console.WriteLine("--- REALIZAR RESERVA ---");
            Console.Write("Ingrese su documento / identificación: ");
            string idDoc = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Ingrese su nombre: ");
            string nombre = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Ingrese el ID del evento: ");
            if (!int.TryParse(Console.ReadLine(), out int idEvento))
            {
                Console.WriteLine("ID de evento inválido.");
                return;
            }

            Console.Write("Ingrese la cantidad de entradas a reservar: ");
            if (!int.TryParse(Console.ReadLine(), out int cantidad))
            {
                Console.WriteLine("Cantidad numérica inválida.");
                return;
            }

            var reserva = gestor.CrearReserva(idEvento, idDoc, nombre, cantidad);
            Console.WriteLine($"\n¡Reserva exitosa! ID Reserva: #{reserva.Id}");
            Console.WriteLine($"Total a pagar: ${reserva.ValorTotal:N0} por {reserva.CantidadEntradas} entrada(s).");
        }

        private static void VerReservas()
        {
            Console.WriteLine("--- RESERVAS REGISTRADAS ---");
            var lista = gestor.ObtenerReservas();
            if (!lista.Any())
            {
                Console.WriteLine("No hay reservas registradas en el sistema.");
                return;
            }

            foreach (var r in lista)
            {
                Console.WriteLine($"Reserva #{r.Id} | Cliente: {r.Cliente.Nombre} ({r.Cliente.Identificacion}) | Evento: {r.EventoReservado.Nombre} | Cantidad: {r.CantidadEntradas} | Total: ${r.ValorTotal:N0} | Estado: {r.Estado}");
            }
        }

        private static void CancelarReserva()
        {
            Console.Write("Ingrese el ID de la reserva a cancelar: ");
            if (int.TryParse(Console.ReadLine(), out int idReserva))
            {
                gestor.CancelarReserva(idReserva);
                Console.WriteLine($"\nLa reserva #{idReserva} ha sido cancelada exitosamente y sus cupos fueron devueltos.");
            }
            else
            {
                Console.WriteLine("ID de reserva inválido.");
            }
        }

        private static void VerResumen()
        {
            var (activas, entradas, total) = gestor.ObtenerResumen();
            Console.WriteLine("--- RESUMEN DE RESERVAS ---");
            Console.WriteLine($"Reservas Activas: {activas}");
            Console.WriteLine($"Total de Entradas Reservadas: {entradas}");
            Console.WriteLine($"Valor Total Recaudado: ${total:N0}");
        }

        private static void ConsultarPorUsuario()
        {
            Console.Write("Ingrese el documento del usuario: ");
            string idDoc = Console.ReadLine()?.Trim() ?? "";

            var reservas = gestor.ConsultarReservasPorUsuario(idDoc);
            Console.WriteLine($"\n--- RESERVAS ASOCIADAS AL USUARIO ({idDoc}) ---");
            if (!reservas.Any())
            {
                Console.WriteLine("No se encontraron reservas para este usuario.");
                return;
            }

            foreach (var r in reservas)
            {
                Console.WriteLine($"Reserva #{r.Id} | Evento: {r.EventoReservado.Nombre} | Cantidad: {r.CantidadEntradas} | Total: ${r.ValorTotal:N0} | Estado: {r.Estado}");
            }
        }
    }
}