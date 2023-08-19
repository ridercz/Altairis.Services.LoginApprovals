using Altairis.Services.LoginApprovals;

namespace Microsoft.Extensions.DependencyInjection;

public static class AltairisServicesLoginApprovalsRegistrationExtensions {

    public static IServiceCollection AddLoginApprovals(this IServiceCollection services) {
        services.AddHttpContextAccessor();
        services.AddSingleton<ILoginApprovalSessionStore>(new InMemoryLoginApprovalSessionStore());
        services.AddSingleton<LoginApprovalManager>();
        return services;
    }

    public static IServiceCollection AddLoginApprovals(this IServiceCollection services, ILoginApprovalSessionStore store) {
        services.AddHttpContextAccessor();
        services.AddSingleton(store);
        services.AddSingleton<LoginApprovalManager>();
        return services;
    }

    public static IServiceCollection AddLoginApprovals<TStore>(this IServiceCollection services) where TStore : class, ILoginApprovalSessionStore {
        services.AddHttpContextAccessor();
        services.AddSingleton<ILoginApprovalSessionStore, TStore>();
        services.AddSingleton<LoginApprovalManager>();
        return services;
    }

}
