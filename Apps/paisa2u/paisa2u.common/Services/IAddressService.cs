using paisa2u.common.Resources;
namespace paisa2u.common.Services
{
    public interface IAddressService
    {
        Task<AddressResource> AddAddress(AddressResource resource, CancellationToken cancellationToken);

        Task<IEnumerable<AddressResource>> GetAddresses(CancellationToken cancellationToken);

        Task<AddressResource> GetAddress(int Addid, CancellationToken cancellationToken);

        Task<AddressResource> UpdateAddress(int Addid, AddressResource resource, CancellationToken cancellationToken);

        Task<AddressResource> DeleteAddress(AddressResource resource, CancellationToken cancellationToken);
    }
}
