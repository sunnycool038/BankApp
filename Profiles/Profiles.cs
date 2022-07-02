using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ProjectTemplate.Models;
using AutoMapper;

namespace ProjectTemplate.Profiles
{
    
    public class AutomapperProfiles: Profile
    {
        public AutomapperProfiles(){
            CreateMap<RegisterNewAccountModel, Account>();
            CreateMap<UpdateAccountModel, Account>();
            CreateMap<Account, GetAccountModel>();
            CreateMap<RequestTransactionDTO,Transaction>();
        }
    }   
}