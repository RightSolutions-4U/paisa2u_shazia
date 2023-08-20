using paisa2u.common.Resources;

namespace paisa2u.common.Services
{
    public interface ISubscriptionSetupService
    {       
            Task<SubscriptionSetupResource> AddSubscription(SubscriptionSetupResource resource, CancellationToken cancellationToken);
            Task<List<SubscriptionSetupResource>> GetSubscriptionAll(CancellationToken cancellationToken);
            Task<SubscriptionSetupResource> GetSubscriptionBySubType(string Subtype, CancellationToken cancellationToken);
            Task<SubscriptionSetupResource> UpdateSubscription(int Subsid, SubscriptionSetupResource resource, CancellationToken cancellationToken);
            Task<SubscriptionSetupResource> DeleteSubscription(int Subsid, CancellationToken cancellationToken);
       
    }
}
