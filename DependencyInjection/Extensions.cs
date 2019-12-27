using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Sayda.Core.TenantProvider.Middleware;
using System;

namespace Sayda.Core.TenantProvider.DependencyInjection
{
	public static class Extensions
	{

		public static TenantBuilder AddMultiTenant (this IServiceCollection services)
		{
			return new TenantBuilder(services);
		}

		public static IApplicationBuilder UseMultiTenant (this IApplicationBuilder app, Func<HttpContext, bool> invariant = null)
		{
			if (invariant == null)
				return app.UseMiddleware<TenantMiddleware>();

			else
				return app.UseMiddleware<TenantMiddleware>(invariant);
		}

	}
}
