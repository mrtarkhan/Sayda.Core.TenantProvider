using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;

namespace Sayda.Core.TenantProvider
{
	public class TenantProvider : ITenantProvider
	{

		ITenantIdentificationSterategy _sterategy;
		ILogger _logger;
		string _sterategyType = string.Empty;

		public TenantProvider (ILogger<TenantProvider> logger)
		{
			_logger = logger;
		}



		public void SetSterategy (ITenantIdentificationSterategy sterategy)
		{
			_sterategy = sterategy;
			_sterategyType = sterategy.GetType().ToString();
			_logger.LogInformation($"tenant sterategy is {_sterategyType}");
		}


		public string GetTenant (HttpContext context)
		{

			string tenantId;
			try
			{
				tenantId = _sterategy.IdentifyTenant(context);
			}
			catch (Exception ex)
			{
				var message = $"error in getting tenant id using {_sterategyType}";
				_logger.LogError(ex, message);
				throw new TenantException(message, ex);
			}

			if (tenantId == null)
				_logger.LogWarning("tenantId is null");

			else
				_logger.LogWarning($"tenantId is {tenantId}");

			return tenantId;

		}


	}
}
