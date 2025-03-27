using CRM_API.Models;

namespace CRM_API.Data.Services.Interfaces
{
    public interface IClientDbServices
    {
        Task DeleteClientById(Guid ClientID);
        Client GetClientByIdFromStoredProcedure(Guid ID);
        Task<List<Client>> GetClientsFromStoredProcedure();
        Task SaveChangesAsync();
        Task<List<Client>> GetSoftDeleteStoredProcedure();
        Task CreateClient(int TitleID, string ClientName, string ClientSurname, string ClientEmail, string ClientContactNumber, string ClientAddress, byte[] ClientProfilePicture, int TypeID, string LoginUsername, string LoginPassword);
        Task UpdateClient(Guid ClientID, int TitleID, string ClientName, string ClientSurname, string ClientEmail, string ClientContactNumber, string ClientAddress, byte[] ClientProfilePicture, int TypeID);
    }
}