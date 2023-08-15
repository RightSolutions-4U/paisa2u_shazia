using Microsoft.EntityFrameworkCore;
using paisa2u.common.Models;
using paisa2u.common.Resources;
using System;

namespace paisa2u.common.Services
{
    public class SubscriptionService : ISubscriptionService
    {

        private readonly PaisaDbContext _context;
        public SubscriptionService(PaisaDbContext context)
        {
            _context = context;
        }

        public async Task<SubscriptionResource> AddSubsPercent(SubscriptionResource resource, CancellationToken cancellationToken)
        {
            var subscriptionperc = new Subscriptionperc
            {
                RecId = resource.Recid,
                RegId = resource.RegId,
                Appowner = resource.Appowner,
                Vendor = resource.Vendor,
                Subvendor = resource.Subvendor,
                Customer = resource.Customer,
                Endate = resource.Endate,
                Enuser = resource.Enuser
            };
            await _context.Subscriptionpercs.AddAsync(subscriptionperc, cancellationToken);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                throw new Exception("Subscription% record cannot be added!");
            }

            return new SubscriptionResource
                (
                resource.Recid,
                resource.RegId,
                resource.Appowner,
                resource.Vendor,
                resource.Subvendor,
                resource.Customer,
                resource.Endate,
                resource.Enuser

                );
        }

        public async Task<SubscriptionResource> DeleteSubsPercent(int Recid, CancellationToken cancellationToken)
        {
            var delsubscriptionperc = await _context.Subscriptionpercs.FirstOrDefaultAsync(
                x => x.RecId == Recid);
            if (delsubscriptionperc.RecId != Recid)
            {
                throw new Exception("Record to remove does not exist");
            }
            var subscriptionperc = new SubscriptionResource
            (
                Recid,
                delsubscriptionperc.RegId,
                (float)delsubscriptionperc.Appowner,
                (float)delsubscriptionperc.Vendor,
                (float)delsubscriptionperc.Subvendor,
                (float)delsubscriptionperc.Customer,
                delsubscriptionperc.Endate,
                delsubscriptionperc.Enuser
            );
            _context.Entry(delsubscriptionperc).State = EntityState.Deleted;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Subscription% record cannot be deleted!");
            }
            return subscriptionperc;

        }

        public async Task<List<SubscriptionResource>> GetSubsPercentAllSubs(CancellationToken cancellationToken)
        {
            List<SubscriptionResource> SubsList = new List<SubscriptionResource>();
            var subscriptionperc = await _context.Subscriptionpercs.OrderByDescending(x => x.Endate)
            .ToListAsync(cancellationToken);
            if (subscriptionperc == null)
                throw new Exception("Subscription% record does not exist!");
            foreach (Subscriptionperc Subs in subscriptionperc)
            {
                SubsList.Add
                (
                    new SubscriptionResource(
                    Subs.RecId,
                    Subs.RegId,
                    (float)Subs.Appowner,
                    (float)Subs.Vendor,
                    (float)Subs.Subvendor,
                    (float)Subs.Customer,
                    Subs.Endate,
                    Subs.Enuser
                    )

                );;
            }
            return SubsList;
        }
        public async Task<List<RegUserResource>> GetSubsPercentAll(CancellationToken cancellationToken)
        {
            List<RegUserResource> RegList = new List<RegUserResource>();
            var subscriptionperc1 = _context.Subscriptionpercs;
            var users = await _context.Users
            .Where(m => !subscriptionperc1.Any(d => d.RegId == m.RegId))
.           ToListAsync(cancellationToken);
            if (users == null)
                throw new Exception("Subscription% record does not exist!");
            foreach (Users User in users)
            {
                RegList.Add
                (
                    new RegUserResource(
                    User.RegId,
                    User.Firstname,
                    User.Middlename,
                    User.Lastname,
                    User.Email,
                    User.Username,
                    "",
                    User.Referredby,
                    User.Regtype,
                    User.Vendortype,
                    User.Phonenumber,
                    User.Endate,
                    User.Enuser,
                    User.Substype,
                    User.Regstatus,
                    User.Autorenewal,
                    User.Qrpicture,
                    User.PasswordSalt,
                    User.PasswordHash
                    )
                   
                );
            }
            return RegList;
        }
        public async Task<SubscriptionResource> GetSubsPercentByRegId(int RegId, CancellationToken cancellationToken)
        {
            var subscriptionperc = await _context.Subscriptionpercs.Where(x => x.RegId == RegId).OrderByDescending(x => x.Endate).
                FirstOrDefaultAsync(
               );
            if (subscriptionperc == null)
                throw new Exception("Subscription% record does not exist!");
            return new SubscriptionResource(
                subscriptionperc.RecId,
                subscriptionperc.RegId,
                (float)subscriptionperc.Appowner,
                (float)subscriptionperc.Vendor,
                (float)subscriptionperc.Subvendor,
                (float)subscriptionperc.Customer,
                subscriptionperc.Endate,
                subscriptionperc.Enuser
                );
        }
        public async Task<SubscriptionResource> UpdateSubsPercent(int RecId, SubscriptionResource resource, CancellationToken cancellationToken)
        {
            var upsubspercent = await _context.Subscriptionpercs.FirstOrDefaultAsync(
                x => x.RecId == RecId && x.RegId == resource.RegId);
            if (upsubspercent.RecId != RecId)
                throw new Exception("Subscription% record does not exist!");
            upsubspercent.RecId = resource.Recid;
            upsubspercent.RegId = resource.RegId;
            upsubspercent.Appowner = resource.Appowner;
            upsubspercent.Vendor = resource.Vendor;
            upsubspercent.Subvendor = resource.Subvendor;
            upsubspercent.Customer = resource.Customer;
            upsubspercent.Endate = resource.Endate;
            upsubspercent.Enuser = resource.Enuser;

            _context.Entry(upsubspercent).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return new SubscriptionResource(
                upsubspercent.RecId,
                upsubspercent.RegId,
                (float)upsubspercent.Appowner,
                (float)upsubspercent.Vendor,
                (float)upsubspercent.Subvendor,
                (float)upsubspercent.Customer,
                upsubspercent.Endate,
                upsubspercent.Enuser
                );


        }

    }
}
