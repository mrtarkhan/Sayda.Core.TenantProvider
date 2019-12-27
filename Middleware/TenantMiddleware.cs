using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Sayda.Core.TenantProvider.Middleware
{
	public class TenantMiddleware
	{

		private readonly RequestDelegate _next;
		private readonly Func<HttpContext, bool> _invariant;

		public TenantMiddleware (RequestDelegate next)
		{
			_next = next;
		}

		public TenantMiddleware (RequestDelegate next, Func<HttpContext, bool> invariant)
		{
			_next = next;
			_invariant = invariant;
		}


		public async Task Invoke (HttpContext context)
		{

			if (_invariant == null || (_invariant != null && _invariant.Invoke(context)))
			{
				var strategy = context.RequestServices.GetRequiredService<ITenantProvider>();

				var tenantId = strategy.GetTenant(context);

				var tenantService = context.RequestServices.GetRequiredService<ITenantHolder>();

				tenantService.SetTenant(tenantId);
			}

			if (_next != null)
			{
				await _next(context);
			}
		}

	}


}
