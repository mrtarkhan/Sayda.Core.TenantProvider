using System;

namespace Sayda.Core.TenantProvider
{
	public class TenantException : Exception
	{
		public TenantException ()
		{
		}

		public TenantException (string message)
			: base(message)
		{
		}

		public TenantException (string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
