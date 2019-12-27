using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Sayda.Core.TenantProvider.DependencyInjection
{
	public class TenantBuilder
	{
		internal readonly IServiceCollection services;

		public TenantBuilder (IServiceCollection services)
		{
			this.services = services;
		}

		public TenantBuilder UseStrategy<T> (Action<TenantOptions> setupAction) where T : ITenantIdentificationSterategy
		{
			services.AddSingleton<ITenantProvider>(sp =>
			{
				var logger = sp.GetRequiredService<ILogger<TenantProvider>>();
				var tenantProvider = new TenantProvider(logger);
				var sterategy = ActivatorUtilities.CreateInstance<T>(sp, new object [] { setupAction });
				tenantProvider.SetSterategy(sterategy);
				return tenantProvider;
			});

			return this;
		}



	}
}
