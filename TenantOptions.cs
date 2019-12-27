using System;
using System.Collections.Generic;
using System.Text;

namespace Sayda.Core.TenantProvider
{
	public class TenantOptions
	{
		public string Key { get; set; }
		public int MaxLength { get; set; } = 32;
	}
}
