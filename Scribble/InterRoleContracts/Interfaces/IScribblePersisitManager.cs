using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterRoleContracts.CommonObjects;

namespace InterRoleContracts.Interfaces
{
    public interface IScribblePersisitManager
    {
        Task PersistWorkTask(CloudTable table, WorkTaskModel data);        
    }
}
