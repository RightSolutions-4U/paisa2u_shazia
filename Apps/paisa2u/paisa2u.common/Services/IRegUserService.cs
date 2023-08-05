using paisa2u.common.Models;
using paisa2u.common.Resources;

namespace paisa2u.common.Services
{
    public interface IRegUserService
    {
        Task<RegUserResource> Registration(RegUserRegisterResource resource, CancellationToken cancellationToken);
        Task<RegUserResource> Login(UserLoginResource resource, CancellationToken cancellationToken);

        Task<List<RegUserResource>> GetRegUsers(CancellationToken cancellationToken);

        //Added by Mohtashim on 05-July-2023
        Task<List<RegUserResource>> GetAllReferrals(CancellationToken cancellationToken);

        Task<List<RegUserResource>> GetAllReferralsByRegid(int Regid, CancellationToken cancellationToken);

        Task<RegUserResource> GetRegUser(int regid, CancellationToken cancellationToken);
        
        Task<RegUserResource> UpdateRegUser(int id, RegUserResource resource, CancellationToken cancellationToken);

        Task<RegUserResource> DeleteRegUser(RegUserResource resource, CancellationToken cancellationToken);

    }
}
