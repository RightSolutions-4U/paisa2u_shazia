using paisa2u.common.Resources;

namespace paisa2u.common.Services
{
    public interface ISubscriptionService
    {
        Task<SubscriptionResource> AddSubsPercent(SubscriptionResource resource, CancellationToken cancellationToken);
        Task<List<RegUserResource>> GetSubsPercentAll(CancellationToken cancellationToken);
        Task<List<SubscriptionResource>> GetSubsPercentAllSubs(CancellationToken cancellationToken);
        Task<SubscriptionResource> GetSubsPercentByRegId(int RegId, CancellationToken cancellationToken);
        Task<SubscriptionResource> UpdateSubsPercent(int RecId, SubscriptionResource resource, CancellationToken cancellationToken);
        Task<SubscriptionResource> DeleteSubsPercent(int Recid, CancellationToken cancellationToken);
    }
}