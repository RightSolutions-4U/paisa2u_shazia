using paisa2u.common.Resources;
using Microsoft.EntityFrameworkCore;
using paisa2u.common.Models;
using System.Net;

namespace paisa2u.common.Services
{
    public class AddressService : IAddressService
    {

        private readonly PaisaDbContext _context;

        public AddressService(PaisaDbContext context)
        {
            _context = context;
        }
        public async Task<AddressResource> AddAddress(AddressResource resource, CancellationToken cancellationToken)
        {
            var address = new Address
            {
                Addid = resource.addid,
                RegId = resource.regId,
                Countryid = resource.countryid,
                Cityid = resource.cityid,
                Streetaddress = resource.streetaddress,
                Postal = resource.postal,
                District = resource.district,
                Townstehsil = resource.townstehsil,
                Area = resource.area,
                Longitude = resource.longitude,
                Latitude = resource.latitude
            };

            await _context.Addresses.AddAsync(address, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);


            return new AddressResource(
                resource.addid,
                resource.regId,
                resource.countryid,
                resource.cityid,
                resource.streetaddress,
                resource.postal,
                resource.district,
                resource.townstehsil,
                resource.area,
                resource.longitude,
                resource.latitude
                );
        }

        public async Task<AddressResource> DeleteAddress(AddressResource resource, CancellationToken cancellationToken)
        {
            var edaddress = await _context.Addresses
               .FirstOrDefaultAsync(x => x.Addid == resource.addid, cancellationToken);
            //check for cascade
            if (edaddress.Addid != resource.addid)
            {
                throw new Exception("Address to remove does not exist");
            }
            var address = new AddressResource(
                edaddress.Addid,
                    edaddress.RegId,
                    (int)edaddress.Countryid,
                    (int)edaddress.Cityid,
                    edaddress.Streetaddress,
                    edaddress.Postal,
                    edaddress.District,
                    edaddress.Townstehsil,
                    edaddress.Area,
                    edaddress.Longitude,
                    edaddress.Latitude
                );

            _context.Entry(edaddress).State = EntityState.Deleted;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Address can not be deleted!");
            }

            return address;
        }

        public async Task<AddressResource> GetAddress(int Addid, CancellationToken cancellationToken)
        {
            var address = await _context.Addresses
                .FirstOrDefaultAsync(x => x.Addid == Addid, cancellationToken);

            if (address == null)
                throw new Exception("Address does not exist!");
            return new AddressResource(
                address.Addid,
                address.RegId,
                (int)address.Countryid,
                (int)address.Cityid,
                address.Streetaddress,
                address.Postal,
                address.District,
                address.Townstehsil,
                address.Area,
                address.Longitude,
                address.Latitude
                );
        }

        public async Task<IEnumerable<AddressResource>> GetAddresses(CancellationToken cancellationToken)
        {
            List<AddressResource> addresslist = new List<AddressResource>();
            var address1 = await _context.Addresses.ToListAsync(cancellationToken);
            foreach (Address address in address1)
            {
                addresslist.Add
                (
                new AddressResource(
                    address.Addid,
                    address.RegId,
                    (int)address.Countryid,
                    (int)address.Cityid,
                    address.Streetaddress,
                    address.Postal,
                    address.District,
                    address.Townstehsil,
                    address.Area,
                    address.Longitude,
                    address.Latitude

                    )
                );
            }
            return addresslist;
        }

        public async Task<AddressResource> UpdateAddress(int Addid, AddressResource resource, CancellationToken cancellationToken)
        {
            var edaddress = await _context.Addresses
                .FirstOrDefaultAsync(x => x.Addid == Addid, cancellationToken);
            if (edaddress.Addid != Addid)
            {
                throw new Exception("Address to update does not exist");
            }

            edaddress.Addid = resource.addid;
            edaddress.RegId = resource.regId;
            edaddress.Countryid = resource.countryid;
            edaddress.Cityid = resource.cityid;
            edaddress.Streetaddress = resource.streetaddress;
            edaddress.Postal = resource.postal;
            edaddress.District = resource.district;
            edaddress.Townstehsil = resource.townstehsil;
            edaddress.Area = resource.area;
            edaddress.Longitude = resource.longitude;
            edaddress.Latitude = resource.latitude;

            _context.Entry(edaddress).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return new AddressResource(
                    edaddress.Addid,
                    edaddress.RegId,
                    (int)edaddress.Countryid,
                    (int)edaddress.Cityid,
                    edaddress.Streetaddress,
                    edaddress.Postal,
                    edaddress.District,
                    edaddress.Townstehsil,
                    edaddress.Area,
                    edaddress.Longitude,
                    edaddress.Latitude

                    );
        }
    }
}
