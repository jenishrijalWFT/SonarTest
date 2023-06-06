
// namespace Boss.Gateway.IntegrationTests.Persistence;

// public class CompanyRepositoryIntegrationTests : IClassFixture<DatabaseFixture> { }

// namespace Boss.Gateway.IntegrationTests.Repositories
// {
//     public class CompanyRepositoryIntegrationTests : IClassFixture<DatabaseFixture>
//     {
//         private readonly IDbConnection _connection;
//         private readonly CompanyRepository _repository;

//         public CompanyRepositoryIntegrationTests(DatabaseFixture fixture)
//         {
//             _connection = fixture.Connection;
//             _repository = new CompanyRepository(_connection);
//         }

//         [Fact]
//         public async Task AddCompanyAsync_ShouldAddNewCompanyToDatabase()
//         {
//             // Arrange
//             var company = new Company
//             {
//                 Id = Guid.NewGuid(),
//                 Name = "Example Company",
//                 Symbol = "EXC",
//                 Email = "example@example.com",
//                 Website = "http://www.example.com",
//                 InstrumentType = "Stock"
//             };

//             // Act
//             await _repository.AddCompanyAsync(company);
//             var insertedCompany = await _connection.QuerySingleOrDefaultAsync<Company>(
//                 "SELECT * FROM companies WHERE id = @Id", new { company.Id });

//             // Assert
//             Assert.NotNull(insertedCompany);
//             Assert.Equal(company.Id, insertedCompany.Id);
//             Assert.Equal(company.Name, insertedCompany.Name);
//             Assert.Equal(company.Symbol, insertedCompany.Symbol);
//             Assert.Equal(company.Email, insertedCompany.Email);
//             Assert.Equal(company.Website, insertedCompany.Website);
//             Assert.Equal(company.InstrumentType, insertedCompany.InstrumentType);
//         }

//         [Fact]
//         public async Task GetCompanyList_ShouldReturnListOfCompanies()
//         {
//             // Arrange
//             var company1 = new Company
//             {
//                 Id = Guid.NewGuid(),
//                 Name = "Example Company 1",
//                 Symbol = "EXC1",
//                 Email = "example1@example.com",
//                 Website = "http://www.example1.com",
//                 InstrumentType = "Stock"
//             };
//             var company2 = new Company
//             {
//                 Id = Guid.NewGuid(),
//                 Name = "Example Company 2",
//                 Symbol = "EXC2",
//                 Email = "example2@example.com",
//                 Website = "http://www.example2.com",
//                 InstrumentType = "Stock"
//             };
//             await _connection.ExecuteAsync(
//                 "INSERT INTO companies (id, name, symbol, email, website, instrument_type) " +
//                 "VALUES (@Id, @Name, @Symbol, @Email, @Website, @InstrumentType)",
//                 new[] { company1, company2 });

//             // Act
//             var companies = await _repository.GetCompanyList();

//             // Assert
//             Assert.NotNull(companies);
//             Assert.Equal(2, companies.Count);
//             Assert.Equal(company1.Id, companies[0].Id);
//             Assert.Equal(company2.Id, companies[1].Id);
//         }
//     }

//     public class DatabaseFixture : IDisposable
//     {
//         public IDbConnection Connection { get; private set; }

//         public DatabaseFixture()
//         {
//             var configuration = new ConfigurationBuilder()
//                 .AddJsonFile("appsettings.json")
//                 .Build();

//             var connectionString = configuration.GetConnectionString("TestDatabase");
//             Connection = new NpgsqlConnection(connectionString);
//             Connection.Open();

//             // Set up test database tables
//             Connection.Execute(@"
//                 CREATE TABLE IF NOT EXISTS companies (
//                     id UUID PRIMARY KEY,
//                     name VARCHAR(255) NOT NULL,
//                     symbol VARCHAR(20) NOT NULL,
//                     email VARCHAR(255) NOT NULL,
//                     website VARCHAR(255) NOT NULL,
//                     instrument_type VARCHAR(20) NOT NULL
//             );
//         ");
//         }

//         public void Dispose()
//         {
//             // Clean up test database tables
//             Connection.Execute("DROP TABLE IF EXISTS companies");
//             Connection.Dispose();
//         }
//     }
