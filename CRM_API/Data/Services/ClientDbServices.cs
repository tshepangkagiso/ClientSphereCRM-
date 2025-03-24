using CRM_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM_API.Data.Services
{
    public class ClientDbServices : IClientDbServices
    {
        private readonly ApplicationDbContext dbContext;

        public ClientDbServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Query to get all clients from the stored procedure
        public async Task<List<Client>> GetClientsFromStoredProcedure()
        {
            return await this.dbContext.Clients.FromSqlRaw("EXEC spGetAllClients").ToListAsync();
        }

        // Query to get client by ID from the stored procedure
        public async Task<Client> GetClientByIdFromStoredProcedure(Guid ID)
        {
            return await this.dbContext.Clients.FromSqlRaw("EXEC spGetClientByID {0}", ID).FirstOrDefaultAsync() ?? new Client();
        }

        // Update client type from the stored procedure
        public async Task UpdateClientsType(Guid ID, int TypeID)
        {
            await this.dbContext.Database.ExecuteSqlRawAsync("EXEC spUpdateClientsType @ClientID = {0}, @TypeID = {1}", ID, TypeID);
        }

        // Update client information from the stored procedure
        public async Task UpdateClient(Guid ClientID, int TitleID, string ClientName, string ClientSurname,
                                        string ClientEmail, string ClientContactNumber, string ClientAddress,
                                        byte[] ClientProfilePicture, int TypeID)
        {
            await this.dbContext.Database.ExecuteSqlRawAsync(
                "EXEC spUpdateClient @ClientID = {0}, @TitleID = {1}, @ClientName = {2}, @ClientSurname = {3}, " +
                "@ClientEmail = {4}, @ClientContactNumber = {5}, @ClientAddress = {6}, @ClientProfilePicture = {7}, @TypeID = {8}",
                ClientID, TitleID, ClientName, ClientSurname, ClientEmail, ClientContactNumber, ClientAddress, ClientProfilePicture, TypeID);
        }

        // Update multiple clients' information from the stored procedure
        public async Task UpdateMultipleClientsBasedOnInput(string Input, object Condition, object Output)
        {
            await this.dbContext.Database.ExecuteSqlRawAsync(
                "EXEC spUpdateMultipleClientsBasedOnInputAndOutput @Input = {0}, @Condition = {1}, @Output = {2}",
                Input, Condition, Output);
        }

        // Delete client by ID from the stored procedure
        public async Task DeleteClientById(Guid ClientID)
        {
            await this.dbContext.Database.ExecuteSqlRawAsync("EXEC spDeleteClientById @ClientID = {0}", ClientID);
        }
    }

}
