namespace Sayda.Core.TenantProvider
{
	public interface ITenantIdentificationSterategy
	{
		string IdentifyTenant (object context);
	}
}
