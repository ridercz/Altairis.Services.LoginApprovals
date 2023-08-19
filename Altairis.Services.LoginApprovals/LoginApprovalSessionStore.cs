using System.Security.Cryptography;

namespace Altairis.Services.LoginApprovals;

public abstract class LoginApprovalSessionStore : ILoginApprovalSessionStore {
    private const int SessionIdLength = 32; // 32 bytes = 256 bits

    /// <summary>Creates the specified session.</summary>
    /// <param name="las">The session.</param>
    /// <returns>ID of newly created session</returns>
    /// <exception cref="ArgumentNullException">session</exception>
    public virtual string Create(LoginApprovalSession las) {
        // Create new session ID
        var lasidRaw = RandomNumberGenerator.GetBytes(SessionIdLength);
        las.SessionId = string.Join(string.Empty, lasidRaw.Select(x => x.ToString("X2")));

        // Save to store
        this.Save(las);

        // Return generated session ID
        return las.SessionId;
    }

    /// <summary>Approves the specified session.</summary>
    /// <param name="lasid">The session identifier.</param>
    /// <param name="userName">Name of the user who approved the session.</param>
    public abstract void Approve(string lasid, string userName);

    /// <summary>
    /// Deletes the specified session.
    /// </summary>
    /// <param name="lasid">The session identifier.</param>
    public abstract void Delete(string lasid);

    /// <summary>
    /// Finds the specified session.
    /// </summary>
    /// <param name="lasid">The session identifier.</param>
    /// <returns>
    /// The session found or <c>null</c> if no such session is found.
    /// </returns>
    public abstract LoginApprovalSession? Find(string lasid);

    /// <summary>
    /// Saves the specified session to store.
    /// </summary>
    /// <param name="las">The session to be stored.</param>
    protected abstract void Save(LoginApprovalSession las);

}