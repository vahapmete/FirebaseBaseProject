
using Core.Persistence.Repositories.Firestore;
using Google.Cloud.Firestore;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Persistence;
public static class CorePersistenceServicesRegistration
{
    public static IServiceCollection AddCorePersistenceServices(this IServiceCollection services)
    {

        services.AddSingleton(_ => new FirestoreProvider(
            new FirestoreDbBuilder
            {
                ProjectId = FireConstants.ProjectId,
                JsonCredentials = FireConstants.JsonCredentials // <-- service account json file
            }.Build()
        ));
        return services;
    }
}
