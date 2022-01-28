using kist_api.Model;
using kist_api.Model.dashboard;
using kist_api.Model.dtcusid;
using kist_api.Model.dtmobile;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kist_api.Services
{
    public interface IContractService
    {
    



        Task<List<Contract>> GetContract(ContractRequest req);
        Task<Contract> UpdateContract(Contract con , String username);




    }
}