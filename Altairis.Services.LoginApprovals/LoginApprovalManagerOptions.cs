namespace Altairis.Services.LoginApprovals;

public class LoginApprovalManagerOptions {

    public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(5);

}
