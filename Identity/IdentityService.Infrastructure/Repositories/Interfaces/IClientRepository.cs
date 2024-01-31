using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.IdentityService.Infrastructure.Repositories.Interfaces
{
    
        public interface IClientRepository
        {
            Task<IEnumerable<Client>> GetClientsAsync();
        }
}


