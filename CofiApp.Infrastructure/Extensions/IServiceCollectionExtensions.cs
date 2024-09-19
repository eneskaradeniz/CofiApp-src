using Amazon.S3;
using Azure.Storage.Blobs;
using CofiApp.Application.Abstractions.Storage;
using CofiApp.Domain.Enums;
using CofiApp.Infrastructure.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CofiApp.Infrastructure.Extensions
{
    internal static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddStorage(this IServiceCollection services, StorageType storageType, IConfiguration configuration)
        {
            switch (storageType)
            {
                case StorageType.Blob:
                    services.AddSingleton<IStorage, BlobStorage>();
                    services.AddSingleton(_ => new BlobServiceClient(configuration.GetConnectionString("BlobStorage")));
                    break;

                case StorageType.S3:
                    services.AddSingleton<IStorage, S3Storage>();
                    services.AddSingleton<IAmazonS3>(_ => new AmazonS3Client(
                        "accessKey",
                        "secretKey",
                        new AmazonS3Config
                        {
                            ServiceURL = "http://CofiApp.Storage:4566",
                            ForcePathStyle = true,
                            UseHttp = true
                        }));
                    break;

                default:
                    throw new NotImplementedException($"Storage type {storageType} is not implemented.");
            }

            services.AddSingleton<IStorageService, StorageService>();

            return services;
        }
    }
}
