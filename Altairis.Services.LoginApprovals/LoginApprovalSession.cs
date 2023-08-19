namespace Altairis.Services.LoginApprovals;

public class LoginApprovalSession {

    public string SessionId { get; set; } = string.Empty;

    public required DateTime Expiration { get; set; }

    public string? RequesterUserAgent { get; set; }

    public required string RequesterIpAddress { get; set; }

    public bool Approved { get; set; }

    public string? UserName { get; set; }

}
