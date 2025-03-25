using CRM_API.Data.Services.Interfaces;
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

        //Query to get all clients in Backup table for soft delete stored procedure
        public async Task<List<Client>> GetSoftDeleteStoredProcedure()
        {
            return await this.dbContext.Clients.FromSqlRaw("EXEC spSoftDelete").ToListAsync();
        }


        // Query to get client by ID from the stored procedure
        public Client GetClientByIdFromStoredProcedure(Guid ID)
        {
            var clientList = this.dbContext.Clients.FromSqlRaw("EXEC spGetClientByID {0}", ID).AsEnumerable().FirstOrDefault();

            return clientList ?? new Client();
        }

        // Create a new client by executing the stored procedure
        public async Task CreateClient(int TitleID, string ClientName, string ClientSurname,
                                       string ClientEmail, string ClientContactNumber, string ClientAddress,
                                       byte[] ClientProfilePicture, int TypeID, string LoginUsername, string LoginPassword)
        {


            await this.dbContext.Database.ExecuteSqlRawAsync(
                "EXEC spCreateClient @TitleID = {0}, @ClientName = {1}, @ClientSurname = {2}, @ClientEmail = {3}, " +
                "@ClientContactNumber = {4}, @ClientAddress = {5}, @ClientProfilePicture = {6}, @TypeID = {7} , @LoginUsername = {8},@LoginPassword ={9}",
                TitleID, ClientName, ClientSurname, ClientEmail, ClientContactNumber, ClientAddress, ClientProfilePicture, TypeID,LoginUsername,LoginPassword);
        }


        // Update clients type from the stored procedure
        public async Task UpdateClientsType(int OldTypeID, int NewTypeID)
        {
            await this.dbContext.Database.ExecuteSqlRawAsync("EXEC spUpdateClientsType @OldTypeID = {0}, @NewTypeID = {1}",OldTypeID, NewTypeID);
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


        // Delete client by ID from the stored procedure
        public async Task DeleteClientById(Guid ClientID)
        {
            await this.dbContext.Database.ExecuteSqlRawAsync("EXEC spDeleteClientById @ClientID = {0}", ClientID);
        }

        public async Task SaveChangesAsync()
        {
            await this.dbContext.SaveChangesAsync();
        }
    }

}
