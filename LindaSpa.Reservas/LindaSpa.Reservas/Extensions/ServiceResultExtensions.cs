using LindaSpa.Application.Core;
using Microsoft.AspNetCore.Mvc;

namespace LindaSpa.Reservas.Extensions;

public static class ServiceResultExtensions
{
	public static ActionResult ToError<T>(this ControllerBase controller, ServiceResult<T> result)
	{
		return controller.StatusCode(result.StatusCode, result.Message);
	}
}
