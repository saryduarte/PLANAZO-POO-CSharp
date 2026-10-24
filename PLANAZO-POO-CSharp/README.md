# PLANAZO - Sistema de Gestión de Eventos en C# y .NET

Aplicación de consola desarrollada en **C# y .NET** para la gestión integral de eventos, consulta de disponibilidad de cupos, reserva de entradas y cancelaciones, aplicando los principios fundamentales de la **Programación Orientada a Objetos (POO)**[span_1](start_span)[span_1](end_span).

---

## 1. Descripción de la Aplicación
PLANAZO es una plataforma de entretenimiento que administra diferentes tipos de actividades: funciones de cine, conciertos y talleres[span_2](start_span)[span_2](end_span). El sistema permite a los usuarios:
* Visualizar eventos disponibles con cálculo dinámico de tarifas según la categoría[span_3](start_span)[span_3](end_span).
* Consultar información detallada de cada evento mediante su identificador[span_4](start_span)[span_4](end_span).
* Realizar reservas controlando la disponibilidad en tiempo real[span_5](start_span)[span_5](end_span).
* Cancelar reservas registradas y restaurar automáticamente los cupos al inventario[span_6](start_span)[span_6](end_span).
* Visualizar métricas consolidadas sobre recaudación y ocupación[span_7](start_span)[span_7](end_span).
* Gestionar reservas por usuario mediante su documento de identidad (Reto Adicional 2)[span_8](start_span)[span_8](end_span).

---

## 2. Instrucciones de Ejecución

Para clonar y ejecutar este proyecto localmente o a través de **GitHub Codespaces**:

1. Abrir la terminal en la raíz del proyecto donde se encuentra el archivo de solución o proyecto `.csproj`.
2. Restaurar y compilar la solución:
   ```bash
   dotnet build
   ```
3. Ejecutar la aplicación:
   ```bash
   dotnet run
   ```

---

## 3. Estructura del Proyecto y Relación de Clases

El proyecto está organizado siguiendo el principio de separación de responsabilidades[span_9](start_span)[span_9](end_span):

```text
PLANAZO-POO-CSharp/
│
├── Program.cs                  # Interfaz de consola, menús y captura de datos[span_10](start_span)[span_10](end_span)
│
├── Modelos/                    # Entidades del dominio del negocio[span_11](start_span)[span_11](end_span)
│   ├── Evento.cs               # Clase abstracta base[span_12](start_span)[span_12](end_span)
│   ├── Cine.cs                 # Derivada: mantiene precio base[span_13](start_span)[span_13](end_span)
│   ├── Concierto.cs            # Derivada: recargo del 15%[span_14](start_span)[span_14](end_span)
│   ├── Taller.cs               # Derivada: descuento del 10%[span_15](start_span)[span_15](end_span)
│   ├── Festival.cs             # Derivada (Reto 1): recargo del 20%[span_16](start_span)[span_16](end_span)
│   ├── Usuario.cs              # Entidad para gestión de clientes (Reto 2)[span_17](start_span)[span_17](end_span)
│   └── Reserva.cs              # Asociación entre Usuario y Evento[span_18](start_span)[span_18](end_span)
│
├── Servicios/
│   └── GestorReservas.cs       # Lógica de negocio, colecciones y validaciones[span_19](start_span)[span_19](end_span)
│
├── Capturas/                   # Evidencias de pruebas de ejecución[span_20](start_span)[span_20](end_span)
└── README.md                   # Documentación técnica del repositorio[span_21](start_span)[span_21](end_span)
```

### Relaciones entre clases:
* **Herencia (`is-a`):** `Cine`, `Concierto`, `Taller` y `Festival` heredan de la clase base abstracta `Evento`[span_22](start_span)[span_22](end_span).
* **Asociación / Composición (`has-a`):** Cada `Reserva` contiene una referencia a un objeto `Usuario` (el comprador) y a un objeto `Evento` (el evento reservado)[span_23](start_span)[span_23](end_span).
* **Agregación:** La clase de servicio `GestorReservas` administra colecciones (`List<Evento>` y `List<Reserva>`) para coordinar las operaciones del sistema[span_24](start_span)[span_24](end_span).

---

## 4. Conceptos de POO Implementados

| Concepto POO | Implementación en el Código |
| :--- | :--- |
| **Abstracción** | Se implementó la clase abstracta `Evento`[span_25](start_span)[span_25](end_span). Modela los atributos esenciales (`Id`, `Nombre`, `Fecha`, `PrecioBase`, `CapacidadMaxima`, `CuposDisponibles`) y define el contrato obligatorio del método `CalcularPrecioFinal()` sin imponer una implementación rígida[span_26](start_span)[span_26](end_span). |
| **Encapsulamiento** | Las propiedades de los modelos cuentan con accesores controlados (`get; private set;`). Los estados internos como `CuposDisponibles` solo se modifican a través de métodos seguros como `ReducirCupos(int)` y `DevolverCupos(int)`, previniendo inconsistencias[span_27](start_span)[span_27](end_span). |
| **Herencia** | Se extienden las características de `Evento` hacia clases especializadas (`Cine`, `Concierto`, `Taller`, `Festival`) mediante la reutilización de constructores base (`base(...)`) y código común[span_28](start_span)[span_28](end_span). |
| **Polimorfismo** | Cada subclase sobrescribe el método abstracto `CalcularPrecioFinal()` (`override`) aplicando su propia regla financiera[span_29](start_span)[span_29](end_span):<br>• `Cine`: Precio base[span_30](start_span)[span_30](end_span).<br>• `Concierto`: Precio base * 1.15[span_31](start_span)[span_31](end_span).<br>• `Taller`: Precio base * 0.90[span_32](start_span)[span_32](end_span).<br>• `Festival`: Precio base * 1.20[span_33](start_span)[span_33](end_span). |
| **Colecciones** | Se utilizó `List<T>` dentro de `GestorReservas` para almacenar y consultar dinámicamente eventos y reservas en memoria[span_34](start_span)[span_34](end_span). |
| **Manejo de Excepciones** | Implementación de bloques `try-catch` y lanzamiento de excepciones (`ArgumentException`, `InvalidOperationException`, `KeyNotFoundException`) para garantizar que datos inválidos o desbordes no cierren inesperadamente el programa[span_35](start_span)[span_35](end_span). |

---

## 5. Retos Adicionales Implementados (Calificación 5,0)

1. **Reto 1 (+0,2) — Nuevo Tipo de Evento:** Se creó la clase `Festival.cs` que hereda de `Evento`, implementando un recargo del 20% sobre el precio base sin alterar ninguna clase preexistente[span_36](start_span)[span_36](end_span).
2. **Reto 2 (+0,2) — Gestión de Usuarios:** Se integró la entidad `Usuario.cs` para vincular cada reserva a una persona por documento, habilitando la opción en el menú para consultar todas las reservas de un usuario específico[span_37](start_span)[span_37](end_span).
3. **Reto 3 (+0,1) — Diagrama de Clases UML:** Se diseñó la estructura completa del sistema en notación UML[span_38](start_span)[span_38](end_span).

---

## 6. Diagrama de Clases UML (Reto 3)

```mermaid
classDiagram
    class Evento {
        <<abstract>>
        -int _id
        -string _nombre
        -DateTime _fecha
        -decimal _precioBase
        -int _capacidadMaxima
        -int _cuposDisponibles
        +Id: int
        +Nombre: string
        +Fecha: DateTime
        +PrecioBase: decimal
        +CapacidadMaxima: int
        +CuposDisponibles: int
        +CalcularPrecioFinal()* decimal
        +ReducirCupos(cantidad: int) bool
        +DevolverCupos(cantidad: int) void
    }

    class Cine {
        +CalcularPrecioFinal() decimal
    }
    class Concierto {
        +CalcularPrecioFinal() decimal
    }
    class Taller {
        +CalcularPrecioFinal() decimal
    }
    class Festival {
        +CalcularPrecioFinal() decimal
    }

    Evento <|-- Cine
    Evento <|-- Concierto
    Evento <|-- Taller
    Evento <|-- Festival

    class Usuario {
        +Identificacion: string
        +Nombre: string
    }

    class EstadoReserva {
        <<enumeration>>
        Activa
        Cancelada
    }

    class Reserva {
        +Id: int
        +Cliente: Usuario
        +EventoReservado: Evento
        +CantidadEntradas: int
        +ValorTotal: decimal
        +Estado: EstadoReserva
        +Cancelar() void
    }

    class GestorReservas {
        -List~Evento~ _eventos
        -List~Reserva~ _reservas
        -int _contadorReserva
        +ObtenerEventos() IReadOnlyList~Evento~
        +ObtenerReservas() IReadOnlyList~Reserva~
        +ConsultarEvento(id: int) Evento
        +CrearReserva(idEvento: int, idUsuario: string, nombre: string, cantidad: int) Reserva
        +CancelarReserva(idReserva: int) void
        +ObtenerResumen() Tuple
        +ConsultarReservasPorUsuario(idUsuario: string) List~Reserva~
    }

    Reserva --> Usuario
    Reserva --> Evento
    Reserva --> EstadoReserva
    GestorReservas o-- Evento
    GestorReservas o-- Reserva
```

---

## 7. Pregunta de Sustentación

**Pregunta:**  
*¿Por qué utilizaste una clase abstracta `Evento` en lugar de crear clases completamente independientes para Cine, Concierto y Taller? ¿Qué ventajas tiene esta decisión cuando necesitamos agregar un nuevo tipo de evento?*[span_39](start_span)[span_39](end_span)

**Respuesta Técnica:**  
El uso de la clase abstracta `Evento` responde a dos pilares esenciales de la POO: **reutilización de código** y **diseño desacoplado**[span_40](start_span)[span_40](end_span). 

1. **Evita la redundancia:** Si `Cine`, `Concierto` y `Taller` fueran independientes, tendríamos que duplicar en cada una de ellas los mismos campos (`Id`, `Nombre`, `Fecha`, `PrecioBase`, `CuposDisponibles`), así como los métodos de control de aforo (`ReducirCupos` y `DevolverCupos`)[span_41](start_span)[span_41](end_span). Centralizar esto en `Evento` permite mantener el código ordenado y en un único punto de verdad[span_42](start_span)[span_42](end_span).
2. **Habilita el Polimorfismo:** Al tener un tipo base común, el sistema de almacenamiento en `GestorReservas` puede manejar una única lista genérica `List<Evento>`[span_43](start_span)[span_43](end_span). Cuando se procesa una reserva o se calcula el total, el gestor solo llama a `evento.CalcularPrecioFinal()` sin importar el tipo específico del objeto en tiempo de compilación; en tiempo de ejecución, el entorno invoca la implementación correcta[span_44](start_span)[span_44](end_span).
3. **Facilidad de Extensión (Principio Open/Closed):** Cuando se solicitó el nuevo tipo de evento (`Festival`), **no fue necesario modificar ninguna clase existente ni alterar la lógica del gestor ni del menú**[span_45](start_span)[span_45](end_span). Simplemente se creó la nueva clase derivada `Festival : Evento` y se añadió a la lista, garantizando escalabilidad y mantenimiento sin efectos colaterales[span_46](start_span)[span_46](end_span).

---

## 8. Evidencias de Casos de Prueba

Las capturas de pantalla ubicadas en la carpeta `Capturas/` evidencian el funcionamiento y validación de los escenarios requeridos[span_47](start_span)[span_47](end_span):

### Caso 1: Registrar una reserva válida
*Se reserva una cantidad permitida dentro de los cupos disponibles, calculando el valor total correctamente.*[span_48](start_span)[span_48](end_span)
![Caso 1](Capturas/caso1_reserva_valida.png)

### Caso 2: Intentar reservar más entradas que los cupos disponibles
*El sistema intercepta la petición, muestra el mensaje de error controlado y no altera los cupos existentes.*[span_49](start_span)[span_49](end_span)
![Caso 2](Capturas/caso2_exceso_cupos.png)

### Caso 3: Intentar realizar una reserva con cantidad negativa o cero
*Validación de entrada que impide cantidades menores o iguales a cero.*[span_50](start_span)[span_50](end_span)
![Caso 3](Capturas/caso3_cantidad_negativa.png)

### Caso 4: Cancelar una reserva y comprobar devolución de cupos
*Se cancela una reserva activa y se evidencia en la consulta del evento que los cupos fueron restituidos.*[span_51](start_span)[span_51](end_span)
![Caso 4](Capturas/caso4_cancelar_devolver_cupos.png)

### Caso 5: Intentar cancelar dos veces la misma reserva
*El sistema valida el estado de la reserva e impide que una reserva ya cancelada vuelva a procesarse.*[span_52](start_span)[span_52](end_span)
![Caso 5](Capturas/caso5_doble_cancelacion.png)

### Caso 6: Consultar un evento inexistente
*Control de excepción por ID no encontrado sin interrumpir la ejecución de la consola.*[span_53](start_span)[span_53](end_span)
![Caso 6](Capturas/caso6_evento_inexistente.png)

### Caso 7: Comprobación de precios finales por tipo de evento
*Demostración del polimorfismo en el cálculo: Cine (base), Concierto (+15%), Taller (-10%) y Festival (+20%).*[span_54](start_span)[span_54](end_span)
![Caso 7](Capturas/caso7_precios_polimorfismo.png)