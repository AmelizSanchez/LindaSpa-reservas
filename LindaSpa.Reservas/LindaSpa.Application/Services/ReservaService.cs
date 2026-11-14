using LindaSpa.Application.Core;
using LindaSpa.Application.Dtos;
using LindaSpa.Domain.Entities;
using LindaSpa.Infrastructure.Core;
using LindaSpa.Infrastructure.Repositories;

namespace LindaSpa.Application.Services;

public class ReservaService
{
	private readonly ReservaRepository _reservaRepository;
	private readonly ClienteRepository _clienteRepository;
	private readonly BaseRepository<Servicio> _servicioRepository;
	private readonly BaseRepository<Especialista> _especialistaRepository;

	public ReservaService(ReservaRepository reservaRepository,
		ClienteRepository clienteRepository,
		BaseRepository<Servicio> servicioRepository,
		BaseRepository<Especialista> especialistaRepository)
	{
		_reservaRepository = reservaRepository;
		_clienteRepository = clienteRepository;
		_servicioRepository = servicioRepository;
		_especialistaRepository = especialistaRepository;
	}

	public ServiceResult<List<ReservaDto>> GetAll()
	{
		var lista = _reservaRepository.GetAll().Select(ToDto).ToList();
		return ServiceResult<List<ReservaDto>>.Ok(lista);
	}

	public ServiceResult<ReservaDto> GetById(int id)
	{
		var reserva = _reservaRepository.GetById(id);
		if (reserva is null)
		{
			return ServiceResult<ReservaDto>.Fail("La reserva no existe.", 404);
		}

		return ServiceResult<ReservaDto>.Ok(ToDto(reserva));
	}

	public ServiceResult<ReservaDto> Add(ReservaDto dto)
	{
		var errores = Validar(dto.ClienteId, dto.ServicioId, dto.EspecialistaId, dto.FechaHora, 0);
		if (errores.Count > 0)
		{
			return ServiceResult<ReservaDto>.Fail(string.Join(" ", errores));
		}

		var nuevo = new Reserva
		{
			ClienteId = dto.ClienteId,
			ServicioId = dto.ServicioId,
			EspecialistaId = dto.EspecialistaId,
			FechaHora = dto.FechaHora
		};

		_reservaRepository.Add(nuevo);

		return ServiceResult<ReservaDto>.Ok(ToDto(nuevo), "La reserva se creó correctamente.", 201);
	}

	public ServiceResult<ReservaDto> Update(int id, ReservaDto dto)
	{
		var reserva = _reservaRepository.GetById(id);
		if (reserva is null)
		{
			return ServiceResult<ReservaDto>.Fail("La reserva no existe.", 404);
		}

		var errores = Validar(dto.ClienteId, dto.ServicioId, dto.EspecialistaId, dto.FechaHora, id);
		if (errores.Count > 0)
		{
			return ServiceResult<ReservaDto>.Fail(string.Join(" ", errores));
		}

		reserva.ClienteId = dto.ClienteId;
		reserva.ServicioId = dto.ServicioId;
		reserva.EspecialistaId = dto.EspecialistaId;
		reserva.FechaHora = dto.FechaHora;

		_reservaRepository.Update(reserva);

		return ServiceResult<ReservaDto>.Ok(ToDto(reserva), "La reserva se actualizó correctamente.");
	}

	public ServiceResult<bool> Remove(int id)
	{
		var reserva = _reservaRepository.GetById(id);
		if (reserva is null)
		{
			return ServiceResult<bool>.Fail("La reserva no existe.", 404);
		}

		_reservaRepository.Remove(reserva);

		return ServiceResult<bool>.Ok(true, "La reserva se eliminó correctamente.");
	}

	private List<string> Validar(int clienteId, int servicioId, int especialistaId, DateTime fechaHora, int idActual)
	{
		var errores = new List<string>();

		if (fechaHora == default)
		{
			errores.Add("La fecha y hora de la reserva son obligatorias.");
			return errores;
		}
		if (fechaHora < DateTime.Now)
		{
			errores.Add("La fecha y hora no pueden estar en el pasado.");
		}

		if (clienteId <= 0 || !_clienteRepository.Exists(clienteId))
		{
			errores.Add("El cliente no existe.");
		}

		var servicio = servicioId > 0 ? _servicioRepository.GetById(servicioId) : null;
		if (servicio is null)
		{
			errores.Add("El servicio no existe.");
		}

		if (especialistaId <= 0 || !_especialistaRepository.Exists(especialistaId))
		{
			errores.Add("El especialista no existe.");
		}

		if (errores.Count == 0
			&& !_reservaRepository.EstaDisponible(especialistaId, fechaHora, servicio!.DuracionMinutos, idActual))
		{
			errores.Add("El horario no está disponible para este especialista.");
		}

		return errores;
	}

	private static ReservaDto ToDto(Reserva reserva) => new()
	{
		Id = reserva.Id,
		ClienteId = reserva.ClienteId,
		ServicioId = reserva.ServicioId,
		EspecialistaId = reserva.EspecialistaId,
		FechaHora = reserva.FechaHora
	};
}
