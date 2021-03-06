using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace EMService
{
    /// <summary>
    /// AccountRole 异常对象
    /// </summary>
    public class AccountRoleCodeAlreadyExistsException : BusinessException
    {
        public AccountRoleCodeAlreadyExistsException(string name)
           : base("PM:000001", $"A Organization with code {name} has already exists!")
        {

        }
    }
}
