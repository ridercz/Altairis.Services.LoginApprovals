using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.Services.LoginApprovals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoApp.Areas.Identity.Pages.Account.Manage {
    public class ApproveLoginModel : PageModel {
        private readonly LoginApprovalManager loginApprovalManager;

        public ApproveLoginModel(LoginApprovalManager loginApprovalManager) {
            this.loginApprovalManager = loginApprovalManager;
        }

        public string DisplaySessionId { get; set; }

        public string RequesterIpAddress { get; set; }

        public string RequesterUserAgent { get; set; }

        public IActionResult OnGet(string lasid) {
            var las = this.loginApprovalManager.GetLoginApprovalInfo(lasid);
            if (las == null) return this.NotFound();

            this.DisplaySessionId = this.loginApprovalManager.FormatIdForDisplay(las.SessionId);
            this.RequesterIpAddress = las.RequesterIpAddress;
            this.RequesterUserAgent = las.RequesterUserAgent;

            return this.Page();
        }

        public IActionResult OnPostApprove(string lasid) {
            this.loginApprovalManager.ApproveLogin(lasid);
            return this.RedirectToPage("ApproveLoginDone");
        }

        public IActionResult OnPostDecline(string lasid) {
            this.loginApprovalManager.DeclineLogin(lasid);
            return this.RedirectToPage("ApproveLoginDeclined");
        }
    }
}