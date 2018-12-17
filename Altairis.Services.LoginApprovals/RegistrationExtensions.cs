using System;
using System.Collections.Generic;
using System.Text;
using Altairis.Services.LoginApprovals;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection {
    public static class AltairisServicesLoginApprovalsRegistrationExtensions {

        public static IServiceCollection AddLoginApprovals(this IServiceCollection services) {
            services.AddHttpContextAccessor();
            services.AddSingleton<ILoginApprovalSessionStore>(new InMemoryLoginApprovalSessionStore());
            services.AddSingleton<LoginApprovalManager>();
            return services;
        }

        public static IServiceCollection AddLoginApprovals(this IServiceCollection services, ILoginApprovalSessionStore store) {
            services.AddHttpContextAccessor();
            services.AddSingleton<ILoginApprovalSessionStore>(store);
            services.AddSingleton<LoginApprovalManager>();
            return services;
        }

    }
}
