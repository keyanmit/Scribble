using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterRoleContracts.Enums;

namespace AzureHelperUtils.CommonObjects
{
    public class DeploymentContext
    {
        public EnvironmentEnum CurrentEnvironment { get; set; }
    }
}
