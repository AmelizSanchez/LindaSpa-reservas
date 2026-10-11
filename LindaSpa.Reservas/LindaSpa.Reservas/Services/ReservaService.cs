using LindaSpa.Reservas.DTOs;
using LindaSpa.Reservas.Models.Entities;

namespace LindaSpa.Reservas.Services;

public class ReservaService : IReservaService
{
    private readonly List<Reserva> _reservas = new();
    private int _siguienteId = 1;

    private readonly IClienteService _clientes;
    private readonly IServicioService _servicios;
    private readonly IEspecialistaService _especialistas;

    public ReservaService(
        IClienteService clientes,
        IServicioService servicios,
        IEspecialistaService especialistas)
    {
        _clientes = clientes;
        _servicios = servicios;
        _especialistas = especialistas;
    }

    public (ReservaResponseDto? Reserva, string? Error) Crear(ReservaCreateDto dto)
    {
        if (_clientes.ObtenerPorId(dto.ClienteId) is null)
            return (null, "El cliente no existe.");

        var servicio = _servicios.ObtenerPorId(dto.ServicioId);
        if (servicio is null)
            return (null, "El servicio no existe.");

        if (_especialistas.ObtenerPorId(dto.EspecialistaId) is null)
            return (null, "El especialista no existe.");

        if (!EstaDisponible(dto.EspecialistaId, dto.FechaHora, servicio.DuracionMinutos))
            return (null, "El horario no está disponible para este especialista.");

        var reserva = new Reserva
        {
            Id = _siguienteId++,
            ClienteId = dto.ClienteId,
            ServicioId = dto.ServicioId,
            EspecialistaId = dto.EspecialistaId,
            FechaHora = dto.FechaHora
        };

        _reservas.Add(reserva);
        return (ToDto(reserva), null);
    }

    public IEnumerable<ReservaResponseDto> Listar() =>
        _reservas.Select(ToDto);

    public ReservaResponseDto? ObtenerPorId(int id)
    {
        var reserva = _reservas.FirstOrDefault(r => r.Id == id);
        return reserva is null ? null : ToDto(reserva);
    }

    private bool EstaDisponible(int especialistaId, DateTime inicio, int duracionMinutos)
    {
        var fin = inicio.AddMinutes(duracionMinutos);

        foreach (var existente in _reservas.Where(r => r.EspecialistaId == especialistaId))
        {
            var duracionExistente = _servicios.ObtenerPorId(existente.ServicioId)?.DuracionMinutos ?? 0;
            var inicioExistente = existente.FechaHora;
            var finExistente = inicioExistente.AddMinutes(duracionExistente);

            if (inicio < finExistente && fin > inicioExistente)
                return false;
        }

        return true;
    }

    private static ReservaResponseDto ToDto(Reserva r) => new()
    {
        Id = r.Id,
        ClienteId = r.ClienteId,
        ServicioId = r.ServicioId,
        EspecialistaId = r.EspecialistaId,
        FechaHora = r.FechaHora
    };
}