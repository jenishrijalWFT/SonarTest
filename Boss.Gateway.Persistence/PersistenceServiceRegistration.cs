using System.Data;
using Boss.Gateway.Application.Contracts.Persistence;

using Boss.Gateway.Application.Contracts.Persistence.CloseOut;
using Boss.Gateway.Persistence.Repositories;
using Boss.Gateway.Persistence.Repositories.BuyBillCloseOut;

using Boss.Gateway.Persistence.Repositories.Commissions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using StackExchange.Redis;

namespace Boss.Gateway.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString:Postgres"];
            services.AddScoped<IDbConnection>(db =>
            {
                var connection = new NpgsqlConnection(connectionString);
                connection.Open();
                return connection;
            });

            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IFloorsheetRepository, FloorsheetRepository>();
            services.AddScoped<ISectorRepository, SectorRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICM03Repository, cm03Repository>();
            services.AddScoped<ICm31Repository, cm31Repository>();
            services.AddScoped<IBrokerageCommissionRepository, BrokerageCommissionRepository>();
            services.AddScoped<ITransactionCommissionRepository, TransactionCommissionRepository>();
            services.AddScoped<IAccountHeadRepository, AccountHeadRepository>();
            services.AddScoped<IJournalVoucherRepository, JournalVoucherRepository>();
            services.AddScoped<IVoucherTypeRepository, VoucherTypeRepository>();
            services.AddScoped<ISellBillRepository, SellBillRepository>();
            services.AddScoped<IPurchaseBillRepository, PurchaseBillRepository>();
            services.AddScoped<ICM05Repository, CM05Repository>();
            services.AddScoped<IAccountStatementRepository, AccountStatementRepository>();
            services.AddScoped<ICM30Repository, CM30Repository>();
            services.AddScoped<ISellBillPaymentRepository, SellBillPaymentRepository>();
            services.AddScoped<IAdvancePayment, AdvancePaymentRepository>();
            services.AddMemoryCache();
            services.AddScoped<IMemoryCacheRepository, MemoryCacheRepository>();
            services.AddSingleton<IRedisRepository>(sp =>
            {
                var redisConfig = configuration["ConnectionString:Redis"];
                var redis = ConnectionMultiplexer.Connect(redisConfig!);
                return new RedisRepository(redis.GetDatabase());
            });
            return services;
        }
    }
}