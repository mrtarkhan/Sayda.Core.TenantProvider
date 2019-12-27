using Microsoft.AspNetCore.Http;

namespace Sayda.Core.TenantProvider
{
	public interface ITenantProvider
	{
		string GetTenant (HttpContext context);
	}
}
