using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altairis.Services.LoginApprovals {
    public class InMemoryLoginApprovalSessionStore : LoginApprovalSessionStoreBase {
        private readonly object saveLock = new object();
        private readonly List<LoginApprovalSession> store = new List<LoginApprovalSession>();

        /// <summary>
        /// Saves the specified session to store.
        /// </summary>
        /// <param name="las">The session to be stored.</param>
        /// <exception cref="ArgumentNullException">las</exception>
        /// <exception cref="ArgumentException">Duplicate session ID</exception>
        protected override void Save(LoginApprovalSession las) {
            if (las == null) throw new ArgumentNullException(nameof(las));

            lock (this.saveLock) {
                if (this.Find(las.SessionId) != null) throw new ArgumentException("Duplicate session ID");
                this.store.Add(las);
            }
        }

        /// <summary>
        /// Finds the specified session.
        /// </summary>
        /// <param name="lasid">The session identifier.</param>
        /// <returns>
        /// The session found or <c>null</c> if no such session is found.
        /// </returns>
        /// <exception cref="ArgumentNullException">lasid</exception>
        /// <exception cref="ArgumentException">Value cannot be empty or whitespace only string. - lasid</exception>
        public override LoginApprovalSession Find(string lasid) {
            if (lasid == null) throw new ArgumentNullException(nameof(lasid));
            if (string.IsNullOrWhiteSpace(lasid)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(lasid));

            // Delete expired sessions
            this.store.RemoveAll(x => x.Expiration <= DateTime.Now);

            // Find session
            return this.store.Find(x => x.SessionId.Equals(lasid, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Deletes the specified session.
        /// </summary>
        /// <param name="lasid">The session identifier.</param>
        /// <exception cref="ArgumentNullException">lasid</exception>
        /// <exception cref="ArgumentException">Value cannot be empty or whitespace only string. - lasid</exception>
        public override void Delete(string lasid) {
            if (lasid == null) throw new ArgumentNullException(nameof(lasid));
            if (string.IsNullOrWhiteSpace(lasid)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(lasid));

            this.store.RemoveAll(x => x.SessionId.Equals(lasid, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Approves the specified session.
        /// </summary>
        /// <param name="lasid">The session identifier.</param>
        /// <param name="userName">Name of the user who approved the session.</param>
        /// <exception cref="ArgumentNullException">
        /// lasid
        /// or
        /// userName
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Value cannot be empty or whitespace only string. - lasid
        /// or
        /// Value cannot be empty or whitespace only string. - userName
        /// </exception>
        public override void Approve(string lasid, string userName) {
            if (lasid == null) throw new ArgumentNullException(nameof(lasid));
            if (string.IsNullOrWhiteSpace(lasid)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(lasid));
            if (userName == null) throw new ArgumentNullException(nameof(userName));
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(userName));

            var las = this.store.Find(x => x.SessionId.Equals(lasid, StringComparison.OrdinalIgnoreCase));
            if (las != null) {
                las.Approved = true;
                las.UserName = userName;
            }
        }
    }
}

