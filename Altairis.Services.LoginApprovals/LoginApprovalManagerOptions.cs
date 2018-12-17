using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altairis.Services.LoginApprovals {
    public class LoginApprovalManagerOptions {

        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(5);

    }
}
