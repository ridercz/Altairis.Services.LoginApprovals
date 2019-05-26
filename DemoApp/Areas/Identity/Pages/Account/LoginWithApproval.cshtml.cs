using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.Services.LoginApprovals;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoApp.Areas.Identity.Pages.Account {
    public class LoginWithApprovalModel : PageModel {
        private readonly LoginApprovalManager loginApprovalManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public LoginWithApprovalModel(LoginApprovalManager loginApprovalManager, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager) {
            this.loginApprovalManager = loginApprovalManager;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public string ApprovalUrl { get; set; }

        public string DisplaySessionId { get; set; }

        public async Task<IActionResult> OnGetAsync(string lasid = null, string returnUrl = "/") {
            if (string.IsNullOrEmpty(lasid)) {
                // Request new login approval session and redirect to page
                lasid = this.loginApprovalManager.RequestLoginApproval();
                return this.RedirectToPage(new { lasid });
            } else {
                // Check status of session
                var status = this.loginApprovalManager.CheckLoginApprovalStatus(lasid, out var userName);
                switch (status) {
                    case LoginApprovalSessionStatus.Approved:
                        // Approved - sign in
                        var user = await this.userManager.FindByNameAsync(userName);
                        await this.signInManager.SignInAsync(user, isPersistent: false);
                        return this.LocalRedirect(returnUrl);
                    case LoginApprovalSessionStatus.DeclinedOrExpired:
                        // Declined or expired
                        return this.RedirectToPage("LoginWithApprovalFailed");
                    case LoginApprovalSessionStatus.Waiting:
                    default:
                        // Waiting - display URL and refresh
                        this.DisplaySessionId = this.loginApprovalManager.FormatIdForDisplay(lasid);
                        this.ApprovalUrl = this.Url.Page("Manage/ApproveLogin",
                            pageHandler: null,
                            values: new { lasid },
                            protocol: this.Request.Scheme);
                        this.Response.Headers.Add("Refresh", "5");
                        return this.Page();
                }
            }
        }
    }
}