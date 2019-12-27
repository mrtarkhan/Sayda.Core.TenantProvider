namespace Sayda.Core.TenantProvider
{
	public interface ITenantHolder
	{
		void SetTenant (string tenantId);
		TenantInfo GetTenant ();
	}
}
