using paisa2u.common.Resources;

namespace paisa2u.common.Services
{
    public interface ISubscriptionService
    {
        Task<SubscriptionResource> AddSubsPercent(SubscriptionResource resource, CancellationToken cancellationToken);
        Task<SubscriptionResource> GetSubsPercent(int RecId, CancellationToken cancellationToken);
        Task<SubscriptionResource> UpdateSubsPercent(int RecId, SubscriptionResource resource, CancellationToken cancellationToken);
        Task<SubscriptionResource> DeleteSubsPercent(SubscriptionResource resource, CancellationToken cancellationToken);
    }
}