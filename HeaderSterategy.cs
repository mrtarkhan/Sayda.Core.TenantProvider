using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;

namespace Sayda.Core.TenantProvider
{
	public class HeaderSterategy : ITenantIdentificationSterategy
	{

		private readonly string _tenantKey;
		private readonly int _tenantMaxKeySize;

		public HeaderSterategy (Action<TenantOptions> setupAction)
		{
			var item = new TenantOptions();
			setupAction(item);
			_tenantMaxKeySize = item.MaxLength;
			_tenantKey = item.Key;
		}

		public string IdentifyTenant (object context)
		{
			if (context == null)
				throw new TenantException(null, new ArgumentException($"\"{nameof(context)}\" can not be null", nameof(context)));

			if (!(context is HttpContext))
				throw new TenantException(null,
					new ArgumentException($"\"{nameof(context)}\" type must be of type HttpContext", nameof(context)));


			(context as HttpContext).Request.Headers.TryGetValue(_tenantKey, out StringValues host);

			if (host.Count == 0)
				return null;

			if (host.Count > 1)
				throw new TenantException(null,
					new ArgumentException("incorrect data, tenantId should be unique"));

			var tenantId =  host.FirstOrDefault()
				.Replace("\"", "")
				.Replace("'", "")
				.Trim();

			if (tenantId.Length > _tenantMaxKeySize) 
				throw new TenantException(null,
					new ArgumentException("tenantId is too long"));

			return tenantId;

		}

	}
}