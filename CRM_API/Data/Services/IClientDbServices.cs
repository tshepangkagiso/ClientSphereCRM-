using CRM_API.Models;

namespace CRM_API.Data.Services
{
    public interface IClientDbServices
    {
        Task DeleteClientById(Guid ClientID);
        Task<Client> GetClientByIdFromStoredProcedure(Guid ID);
        Task<List<Client>> GetClientsFromStoredProcedure();
        Task UpdateClient(Guid ClientID, int TitleID, string ClientName, string ClientSurname, string ClientEmail, string ClientContactNumber, string ClientAddress, byte[] ClientProfilePicture, int TypeID);
        Task UpdateClientsType(Guid ID, int TypeID);
        Task UpdateMultipleClientsBasedOnInput(string Input, object Condition, object Output);
    }
}