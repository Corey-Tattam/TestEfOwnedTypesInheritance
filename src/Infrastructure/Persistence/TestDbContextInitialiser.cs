using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence;

public class TestDbContextInitialiser
{
    private readonly ILogger<TestDbContextInitialiser> _logger;
    private readonly TestDbContext _testDbContext;

    public TestDbContextInitialiser(ILogger<TestDbContextInitialiser> logger, TestDbContext testDbContext)
    {
        _logger = logger;
        _testDbContext = testDbContext;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_testDbContext.Database.IsSqlServer())
            {
                // To delete the database and then re-create, uncomment this line.
                //await _testDbContext.Database.EnsureDeletedAsync();
                await _testDbContext.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        if (_testDbContext.SettlementOrders.Any()) return;

        // Seed
        const int MaxOrdersToCreate = 10000;

        static List<Document> CreateDocuments(int orderIndex)
        {
            const int MaxNumberOfDocumentsToCreatePerOrder = 5;
            var documents = new List<Document>();
            for (int i = 1; i <= MaxNumberOfDocumentsToCreatePerOrder; i++)
            {
                documents.Add(new Document
                {
                    DocumentDateFieldOne = DateTime.Now,
                    DocumentDateFieldTwo = DateTime.Now.AddDays(20),
                    DocumentStringFieldOne = $"{nameof(Document.DocumentStringFieldOne)}_Order-{orderIndex}_Document-{i}",
                    DocumentStringFieldTwo = $"{nameof(Document.DocumentStringFieldTwo)}_Order-{orderIndex}_Document-{i}",
                    DocumentStringFieldThree = $"{nameof(Document.DocumentStringFieldThree)}_Order-{orderIndex}_Document-{i}",
                    FilePath = $"{nameof(Document.FilePath)}_Order-{orderIndex}_Document-{i}",
                    IsDeleted = false,
                });
            }
            return documents;
        }

        static List<IndividualConsumer> CreateIndividualConsumers(int orderIndex)
        {
            const int MaxNumberOfConsumersToCreatePerOrder = 2;
            var consumers = new List<IndividualConsumer>();
            for (int i = 1; i <= MaxNumberOfConsumersToCreatePerOrder; i++)
            {
                consumers.Add(new IndividualConsumer
                {
                    Address = new CommonAddress($"{nameof(CommonAddress.Street)}_Order-{orderIndex}_Consumer-{i}", $"{nameof(CommonAddress.Suburb)}_Order-{orderIndex}_Consumer-{i}", $"{nameof(CommonAddress.State)}_Order-{orderIndex}_Consumer-{i}"),
                    ConsumerDateFieldOne = DateTime.Now,
                    ConsumerDateFieldTwo = DateTime.Now.AddDays(30),
                    ConsumerStringFieldOne = $"{nameof(IndividualConsumer.ConsumerStringFieldOne)}_Order-{orderIndex}_Consumer-{i}",
                    ConsumerStringFieldTwo = $"{nameof(IndividualConsumer.ConsumerStringFieldTwo)}_Order-{orderIndex}_Consumer-{i}",
                    ConsumerStringFieldThree = $"{nameof(IndividualConsumer.ConsumerStringFieldThree)}_Order-{orderIndex}_Consumer-{i}",
                    IndividualName = $"{nameof(IndividualConsumer.IndividualName)}_Order-{orderIndex}_Consumer-{i}",
                });
            }
            return consumers;
        }

        static List<OrganisationalConsumer> CreateOrganisationalConsumers(int orderIndex)
        {
            const int MaxNumberOfConsumersToCreatePerOrder = 2;
            var consumers = new List<OrganisationalConsumer>();
            for (int i = 1; i <= MaxNumberOfConsumersToCreatePerOrder; i++)
            {
                consumers.Add(new OrganisationalConsumer
                {
                    Address = new CommonAddress($"{nameof(CommonAddress.Street)}_Order-{orderIndex}_Consumer-{i}", $"{nameof(CommonAddress.Suburb)}_Order-{orderIndex}_Consumer-{i}", $"{nameof(CommonAddress.State)}_Order-{orderIndex}_Consumer-{i}"),
                    ConsumerDateFieldOne = DateTime.Now,
                    ConsumerDateFieldTwo = DateTime.Now.AddDays(30),
                    ConsumerStringFieldOne = $"{nameof(OrganisationalConsumer.ConsumerStringFieldOne)}_Order-{orderIndex}_Consumer-{i}",
                    ConsumerStringFieldTwo = $"{nameof(OrganisationalConsumer.ConsumerStringFieldTwo)}_Order-{orderIndex}_Consumer-{i}",
                    ConsumerStringFieldThree = $"{nameof(OrganisationalConsumer.ConsumerStringFieldThree)}_Order-{orderIndex}_Consumer-{i}",
                    OrganisationName = $"{nameof(OrganisationalConsumer.OrganisationName)}_Order-{orderIndex}_Consumer-{i}",
                });
            }
            return consumers;
        }

        for (int i = 1; i <= MaxOrdersToCreate; i++)
        {
            _testDbContext.SettlementOrders.Add(new SettlementOrder
            {
                Identifier = $"Identifier_Seed_{i}",
                ForwardingAddress = new ForwardingAddress($"{nameof(ForwardingAddress.Street)}_{i}", $"{nameof(ForwardingAddress.Suburb)}_{i}", $"{nameof(ForwardingAddress.State)}_{i}", $"{nameof(ForwardingAddress.Country)}_{i}"),
                OrderType = OrderType.SettlementOrder,
                SettlementStringFieldOne    = $"{nameof(SettlementOrder.SettlementStringFieldOne)}_{i}",
                SettlementStringFieldTwo    = $"{nameof(SettlementOrder.SettlementStringFieldTwo)}_{i}",
                SettlementStringFieldThree  = $"{nameof(SettlementOrder.SettlementStringFieldThree)}_{i}",
                SettlementDateFieldOne = DateTime.Now,
                SettlementDateFieldTwo = DateTime.Now.AddDays(10),
                PropertyAddress = new CommonAddress($"{nameof(CommonAddress.Street)}_{i}", $"{nameof(CommonAddress.Suburb)}_{i}", $"{nameof(CommonAddress.State)}_{i}"),
                Documents = CreateDocuments(i),   
                IndividualConsumers = CreateIndividualConsumers(i),
                OrganisationalConsumers = CreateOrganisationalConsumers(i),
            });

        }

        await _testDbContext.SaveChangesAsync();
    }

}
