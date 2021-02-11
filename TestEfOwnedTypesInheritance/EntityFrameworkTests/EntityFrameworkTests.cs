using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestEfOwnedTypesInheritance.Entities;
using TestEfOwnedTypesInheritance.Exceptions;
using TestEfOwnedTypesInheritance.Extensions;
using TestEfOwnedTypesInheritance.Helpers;

namespace TestEfOwnedTypesInheritance.EntityFrameworkTests
{
    public static class EntityFrameworkTests
    {
        public static async Task FirstOrDefaultAsync_EntityDoesNotExist_ReturnDefault(TestDbContext dbContext)
        {
            try
            {
                var order = await dbContext.SettlementOrders
                    .Include(o => o.IndividualConsumers)
                    .Include(o => o.OrganisationalConsumers)
                    .FirstOrDefaultAsync(o => o.Id == 999999);

                if (order == default)
                {
                    ConsoleLoggingHelper.WriteTestResult(nameof(FirstOrDefaultAsync_EntityDoesNotExist_ReturnDefault), true);
                }
            }
            catch (Exception)
            {
                ConsoleLoggingHelper.WriteTestResult(nameof(FirstOrNotFoundAsync_EntityDoesNotExist_ThrowNotFoundException), false);
            }
        }

        public static async Task FirstOrDefaultAsync_EntityExists_ReturnEntity(TestDbContext dbContext)
        {
            try
            {
                var order = await dbContext.SettlementOrders
                    .Include(o => o.IndividualConsumers)
                    .Include(o => o.OrganisationalConsumers)
                    .FirstOrDefaultAsync(o => o.Id == 1);

                if (order == default)
                {
                    ConsoleLoggingHelper.WriteTestResult(nameof(FirstOrDefaultAsync_EntityDoesNotExist_ReturnDefault), false);
                }
                else
                {
                    ConsoleLoggingHelper.WriteTestResult(nameof(FirstOrDefaultAsync_EntityDoesNotExist_ReturnDefault), true);
                }
            }
            catch (Exception)
            {
                ConsoleLoggingHelper.WriteTestResult(nameof(FirstOrNotFoundAsync_EntityDoesNotExist_ThrowNotFoundException), false);
            }
        }

        public static async Task FirstOrNotFoundAsync_EntityDoesNotExist_ThrowNotFoundException(TestDbContext dbContext)
        {
            try
            {
                var order = await dbContext.SettlementOrders
                    .Include(o => o.IndividualConsumers)
                    .Include(o => o.OrganisationalConsumers)
                    .FirstOrNotFoundAsync(o => o.Id == 999999, nameof(SettlementOrder), 999999, CancellationToken.None);
            }
            catch (NotFoundException)
            {
                ConsoleLoggingHelper.WriteTestResult(nameof(FirstOrNotFoundAsync_EntityDoesNotExist_ThrowNotFoundException), true);
            }
            catch (Exception)
            {
                ConsoleLoggingHelper.WriteTestResult(nameof(FirstOrNotFoundAsync_EntityDoesNotExist_ThrowNotFoundException), false);
            }
        }

        public static async Task FirstOrNotFoundAsync_EntityExists_ReturnEntity(TestDbContext dbContext)
        {
            try
            {
                var order = await dbContext.SettlementOrders
                    .Include(o => o.IndividualConsumers)
                    .Include(o => o.OrganisationalConsumers)
                    .FirstOrNotFoundAsync(o => o.Id == 1, nameof(SettlementOrder), 1, CancellationToken.None);

                if (order != null)
                {
                    ConsoleLoggingHelper.WriteTestResult(nameof(FirstOrNotFoundAsync_EntityExists_ReturnEntity), true);
                }
                else
                {
                    ConsoleLoggingHelper.WriteTestResult(nameof(FirstOrNotFoundAsync_EntityExists_ReturnEntity), false);
                }
            }
            catch (Exception)
            {
                ConsoleLoggingHelper.WriteTestResult(nameof(FirstOrNotFoundAsync_EntityExists_ReturnEntity), false);
            }
        }

        public static async Task NavigationProperty_EntityNotIncluded_ThrowException(TestDbContext dbContext)
        {
            try
            {
                var order = await dbContext.SettlementOrders
                    .FirstOrNotFoundAsync(o => o.Id == 1, nameof(SettlementOrder), 1, CancellationToken.None);

                var consumersCount = order.IndividualConsumers.Count;

                if (consumersCount >= 0)
                {
                    ConsoleLoggingHelper.WriteTestResult(nameof(NavigationProperty_EntityNotIncluded_ThrowException), false);
                }
            }
            catch (Exception)
            {
                ConsoleLoggingHelper.WriteTestResult(nameof(NavigationProperty_EntityNotIncluded_ThrowException), true);
            }
        }

        public static async Task OwnedEntity_OwningEntityExists_NoExceptionThrown(TestDbContext dbContext)
        {
            try
            {
                var order = await dbContext.SettlementOrders
                    .FirstOrNotFoundAsync(o => o.Id == 1, nameof(SettlementOrder), 1, CancellationToken.None);

                var address = order.ForwardingAddress;

                if (address.State == null)
                {
                    ConsoleLoggingHelper.WriteTestResult(nameof(OwnedEntity_OwningEntityExists_NoExceptionThrown), false);
                }
                else
                {
                    ConsoleLoggingHelper.WriteTestResult(nameof(OwnedEntity_OwningEntityExists_NoExceptionThrown), true);
                }
            }
            catch (Exception)
            {
                ConsoleLoggingHelper.WriteTestResult(nameof(OwnedEntity_OwningEntityExists_NoExceptionThrown), false);
            }
        }

    }
}
