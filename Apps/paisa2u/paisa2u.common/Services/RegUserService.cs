using Microsoft.EntityFrameworkCore;
using paisa2u.common.Resources;
using paisa2u.common.Models;
using Microsoft.AspNetCore.Mvc;

namespace paisa2u.common.Services
{
    public sealed class RegUserService : IRegUserService
    {
        private readonly PaisaDbContext _context;
        
        private readonly string _pepper;
        private readonly int _iteration = 3;
        private IConfiguration configuration;

        public RegUserService(PaisaDbContext context, IConfiguration iConfig)
        {
            _context = context;
            _pepper = Environment.GetEnvironmentVariable("PasswordHashExamplePepper");
            /*this.configuration = configuration;*/
            configuration = iConfig;
        }
        //Added by Shazia Aug 4, 2023 for registration
        public async Task<RegUserResource> Registration(RegUserRegisterResource resource, CancellationToken cancellationToken)
        {
            var user = new Users
            {
                Firstname = resource.Firstname,
                Middlename = resource.Middlename,
                Lastname = resource.Lastname,
                Email = resource.Email,
                Username = resource.Username,
                //Pwd = resource.Pwd, don't save password
                Pwd = "",
                Referredby = resource.Referredby,
                Regtype = resource.Regtype,
                Vendortype = resource.Vendortype,
                Phonenumber = resource.Phonenumber,
                Endate = resource.Endate,
                Enuser = resource.Enuser,
                Substype = resource.Substype,
                Regstatus = resource.Regstatus,
                Autorenewal = resource.Autorenewal,
                Qrpicture = resource.Qrpicture,
                PasswordSalt = PasswordHasher.GenerateSalt(),
                PasswordHash = resource.PasswordHash,
                vendorfilename = resource.vendorfilename

            };
            user.PasswordHash = PasswordHasher.ComputeHash(resource.Pwd, user.PasswordSalt, _pepper, _iteration);
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

             
            
            return new RegUserResource(
                user.RegId,
                user.Firstname,
                user.Middlename,
                user.Lastname,
                user.Email,
                user.Username,
                "",
                user.Referredby,
                user.Regtype,
                user.Vendortype,
                user.Phonenumber,
                user.Endate,
                user.Enuser,
                user.Substype,
                user.Regstatus,
                user.Autorenewal,
                user.Qrpicture,
                user.PasswordSalt,
                user.PasswordHash,
                user.vendorfilename
                
                );
        }

        public async Task<RegUserResource> Login(UserLoginResource resource, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == resource.Email, cancellationToken);

            if (user == null)
                throw new Exception("Email or password did not match.");

            var passwordHash = PasswordHasher.ComputeHash(resource.Pwd, user.PasswordSalt, _pepper, _iteration);
            if (user.PasswordHash != passwordHash)
                throw new Exception("Email or password did not match.");

            
            return new RegUserResource(
                user.RegId,
                user.Firstname,
                user.Middlename,
                user.Lastname,
                user.Email,
                user.Username,
                "",
                user.Referredby,
                user.Regtype,
                user.Vendortype,
                user.Phonenumber,
                user.Endate,
                user.Enuser,
                user.Substype,
                user.Regstatus,
                user.Autorenewal,
                user.Qrpicture,
                user.PasswordHash,
                user.PasswordSalt,
                user.vendorfilename
                );
        }
        public async Task<RegUserResource> Login_check_email(UserLoginResource resource, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == resource.Email, cancellationToken);

            if (user == null)
                throw new Exception("Email or password did not match.");

            return new RegUserResource(
                user.RegId,
                user.Firstname,
                user.Middlename,
                user.Lastname,
                user.Email,
                user.Username,
                "",
                user.Referredby,
                user.Regtype,
                user.Vendortype,
                user.Phonenumber,
                user.Endate,
                user.Enuser,
                user.Substype,
                user.Regstatus,
                user.Autorenewal,
                user.Qrpicture,
                user.PasswordHash,
                user.PasswordSalt,
                user.vendorfilename
                );
        }

        public async Task<List<RegUserResource>> GetRegUsers(CancellationToken cancellationToken)
        {
            List<RegUserResource> userslist = new List<RegUserResource>();
            var users1 = await _context.Users.ToListAsync(cancellationToken);
            foreach(Users User in users1)
            {
                userslist.Add
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
                "",
                "",
                User.vendorfilename
                )
               );
                    
            }
            return userslist;
        }
        public async Task<RegUserResource> GetRegUser(int regid, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.RegId == regid, cancellationToken);

            if (user == null)
                throw new Exception("Registered User Id does not exist!");

           return new RegUserResource(
                user.RegId,
                user.Firstname,
                user.Middlename,
                user.Lastname,
                user.Email,
                user.Username,
                "",
                user.Referredby,
                user.Regtype,
                user.Vendortype,
                user.Phonenumber,
                user.Endate,
                user.Enuser,
                user.Substype,
                user.Regstatus,
                user.Autorenewal,
                user.Qrpicture,
                "",
                "",
                user.vendorfilename
                );

        }
        public async Task<RegUserResource> GetRegType(int regid, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.RegId == regid, cancellationToken);

            if (user == null)
                throw new Exception("Registered User Id does not exist!");

            return new RegUserResource(
                 user.RegId,
                 user.Firstname,
                 user.Middlename,
                 user.Lastname,
                 user.Email,
                 user.Username,
                 "",
                 user.Referredby,
                 user.Regtype,
                 user.Vendortype,
                 user.Phonenumber,
                 user.Endate,
                 user.Enuser,
                 user.Substype,
                 user.Regstatus,
                 user.Autorenewal,
                 user.Qrpicture,
                 "",
                 "",
                 user.vendorfilename
                 );

        }
        public async Task<RegUserResource> UpdateRegUser(int id, RegUserResource user, CancellationToken cancellationToken)
        {
            var eduser = await _context.Users
                .FirstOrDefaultAsync(x => x.RegId == id, cancellationToken);
            if (eduser.RegId != id)
            {
                throw new Exception("User to update does not exist");
            }
            //All fields to be added here
            eduser.RegId = user.Regid;
            eduser.Firstname = user.Firstname;
            eduser.Middlename = user.Middlename;
            eduser.Email = user.Email;
            eduser.Lastname = user.Lastname;
            eduser.Username = user.Username;
            eduser.Referredby = user.Referredby;
            eduser.Regstatus = user.Regstatus;
            eduser.Phonenumber = user.Phonenumber;
            eduser.Endate = user.Endate;
            eduser.Enuser = user.Enuser;
            eduser.Substype = user.Substype;
            eduser.Autorenewal = user.Autorenewal;
            eduser.PasswordHash = user.PasswordHash;
            eduser.PasswordSalt = user.PasswordSalt;
            eduser.Regtype = user.Regtype;
            eduser.Vendortype = user.Vendortype;
            eduser.vendorfilename = user.vendorfilename;
            _context.Entry(eduser).State = EntityState.Modified;

            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
                
            }
            return new RegUserResource(
                user.Regid,
                user.Firstname,
                user.Middlename,
                user.Lastname,
                user.Email,
                user.Username,
                "",
                user.Referredby,
                user.Regtype,
                user.Vendortype,
                user.Phonenumber,
                user.Endate,
                user.Enuser,
                user.Substype,
                user.Regstatus,
                user.Autorenewal,
                user.Qrpicture,
                user.PasswordHash,
                user.PasswordSalt,
                user.vendorfilename
                );
        }

        public async Task<RegUserResource> DeleteRegUser(RegUserResource resource, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.RegId == resource.Regid, cancellationToken);
            if (user.RegId != resource.Regid)
            {
                throw new Exception("User to update does not exist");
            }
            var RegUser = new RegUserResource(
                user.RegId,
                user.Firstname,
                user.Middlename,
                user.Lastname,
                user.Email,
                user.Username,
                "",
                user.Referredby,
                user.Regtype,
                user.Vendortype,
                user.Phonenumber,
                user.Endate,
                user.Enuser,
                user.Substype,
                user.Regstatus,
                user.Autorenewal,
                user.Qrpicture,
                user.PasswordHash,
                "",
                user.vendorfilename
                );
            _context.Entry(user).State = EntityState.Deleted;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("User can not be deleted!");

            }
            return RegUser;
        }

        public async Task<List<RegUserResource>> GetAllReferrals(CancellationToken cancellationToken)
        {
            List<RegUserResource> userslist = new List<RegUserResource>();
            var users1 = await _context.Users
                .Where(x => x.Referredby != null)
                .ToListAsync(cancellationToken);
            if (users1 == null)
                throw new Exception("No referrals found!");

            foreach (Users User in users1)
            {
                userslist.Add
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
                "",
                "", User.vendorfilename
                )
               );
            }
            return userslist;
        }
        public async Task<List<RegUserResource>> GetAllReferralsByRegid(int Regid, CancellationToken cancellationToken)
        {
            List<RegUserResource> userslist = new List<RegUserResource>();
            var users1 = await _context.Users
                .Where(x => x.Referredby==Regid.ToString())
                .ToListAsync(cancellationToken);
            if (users1 == null)
                throw new Exception("No referrals found with the given User Id!");
            foreach (Users User in users1)
            {
                userslist.Add
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
                "",
                "",
                User.vendorfilename
                )
               );
            }
            return userslist;

        }
    }

}
