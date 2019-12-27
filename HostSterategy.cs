using System;

namespace Sayda.Core.TenantProvider
{
	public class HostSterategy : ITenantIdentificationSterategy
	{

		public HostSterategy ()
		{

		}

		public string IdentifyTenant (object context)
		{
			throw new NotImplementedException();
		}
	}
}
