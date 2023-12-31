﻿using Microsoft.EntityFrameworkCore;
using paisa2u.common.Models;
using paisa2u.common.Resources;

namespace paisa2u.common.Services
{
    public class SubscriptionSetupService : ISubscriptionSetupService
    {
        private readonly PaisaDbContext _context;

        public SubscriptionSetupService(PaisaDbContext context)
        {
            _context = context;
        }

        Task<SubscriptionSetupResource> ISubscriptionSetupService.AddSubscription(SubscriptionSetupResource resource, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<SubscriptionSetupResource> ISubscriptionSetupService.DeleteSubscription(int Subsid, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<List<SubscriptionSetupResource>> ISubscriptionSetupService.GetSubscriptionAll(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SubscriptionSetupResource> GetSubscriptionBySubType(string Subtype, CancellationToken cancellationToken)
        {
            var subscriptionsetup = await _context.Subscriptions.Where(x => x.Subtype == Subtype).OrderByDescending(x => x.Endate).
               FirstOrDefaultAsync();
            if (subscriptionsetup == null)
                throw new Exception("Subscription record does not exist!");
            return new SubscriptionSetupResource(
                subscriptionsetup.Subsid,
                subscriptionsetup.Subtype,
                (int)subscriptionsetup.Subfee,
                (float)subscriptionsetup.Appowner,
                (float)subscriptionsetup.Vendor,
                (float)subscriptionsetup.Subvendor,
                (float)subscriptionsetup.Customer,
                (DateTime)subscriptionsetup.Endate,
                subscriptionsetup.Enuser
                );
        }

        Task<SubscriptionSetupResource> ISubscriptionSetupService.UpdateSubscription(int Subsid, SubscriptionSetupResource resource, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
