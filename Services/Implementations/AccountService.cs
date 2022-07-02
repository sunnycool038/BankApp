using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ProjectTemplate.Models;
using ProjectTemplate.DAL;
using System.Text;
using ProjectTemplate.Services.Interfaces;

namespace ProjectTemplate.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private YouBankingDbContext _dbContext;
        public AccountService(YouBankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Account Authenticate(string AccountNumber, string Pin)
        {
            var account = _dbContext.Account.Where(x=>x.AccountNumberGenerated==AccountNumber).SingleOrDefault();
            if(account==null) 
            return null!;
            if(!VerifyPinHash(Pin,account.PinHash,account.PinSalt)) return null!;
            return account;

        }

        private static bool VerifyPinHash(string Pin, byte[] PinHash, byte[] PinSalt){
            if(string.IsNullOrWhiteSpace(Pin)) throw new ArgumentNullException("pin");
            using(var hmac=new System.Security.Cryptography.HMACSHA512(PinSalt)){
                var computedPinHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Pin));
                for(int i=0; i < computedPinHash.Length; i++){
                    if(computedPinHash[i]!=PinHash[i]) return false;
                }
                return true;
            }
        }

        public Account Create(Account account, string Pin, string ComfirmPin)
        {
            if(_dbContext.Account.Any(x=>x.Email==account.Email)) throw new ApplicationException("an accuount already exist with this email");
            if(!Pin.Equals(ComfirmPin)) throw new ArgumentException("pins do not match","pin");
            byte[] PinHash, PinSalt;
            CreatePinHash(Pin,out PinHash,out PinSalt);
            account.PinHash = PinHash;
            account.PinSalt = PinSalt;
            _dbContext.Account.Add(account);
            _dbContext.SaveChanges();
            return account;

        }
        private static void CreatePinHash(string Pin, out byte[] PinHash, out byte[] PinSalt){
            using(var hmac= new System.Security.Cryptography.HMACSHA512()){
                PinSalt = hmac.Key;
                PinHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Pin));
            }
        }
        

        public void Delete(int Id)
        {
            var account = _dbContext.Account.Find(Id);
            if(account !=null){
                _dbContext.Account.Remove(account);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<Account> GetAllAcounts()
        {
            return _dbContext.Account.ToList();
        }

        public Account GetByAccountNumber(string AccountNumber)
        {
            var account = _dbContext.Account.Where(x=>x.AccountNumberGenerated==AccountNumber).FirstOrDefault();
            if(account==null) return null!;
            
            return account;
        }

        public Account GetById(int Id)
        {
            var account = _dbContext.Account.Where(x=>x.Id==Id).FirstOrDefault();
            if(account==null) return null!;
            
            return account;
        }

        public void Update(Account account, string Pin)
        {
            var accountToBeUpdated = _dbContext.Account.Where(x=>x.Email==account.Email).SingleOrDefault();
            if(accountToBeUpdated==null) throw new ApplicationException("Account does not exist");
            if(!string.IsNullOrWhiteSpace(account.Email))
            {
                if(_dbContext.Account.Any(x=>x.Email==account.Email)) throw new ApplicationException("This Email "+account.Email+" already exist" + account.AccountName +" already exist");
                accountToBeUpdated.Email = account.Email;
            }
            if(!string.IsNullOrWhiteSpace(account.PhoneNumber))
            {
                if(_dbContext.Account.Any(x=>x.PhoneNumber==account.PhoneNumber)) throw new ApplicationException("This PhoneNumber "+account.PhoneNumber+" already exist" );
                accountToBeUpdated.PhoneNumber = account.PhoneNumber;
            }

            if(!string.IsNullOrWhiteSpace(Pin))
            {
               byte[] pinHash, pinSalt;
               CreatePinHash(Pin,out pinHash,out pinSalt);
               accountToBeUpdated.PinHash = pinHash;
               accountToBeUpdated.PinSalt = pinSalt;
            }
            accountToBeUpdated.DateLastUpdated = DateTime.Now;
            _dbContext.Account.Update(accountToBeUpdated);
            _dbContext.SaveChanges();
        }
    }
}