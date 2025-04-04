﻿
using CRM_EMPLOYEE_APP.Models;
using CRM_EMPLOYEE_APP.Models.DTOs;

namespace CRM_EMPLOYEE_APP.Http.Interfaces
{
    public interface IClientWebExecutor
    {
        Task CreateClientAsync<T>(CreateClientDTO createClientDto);
        Task DeleteClient(Guid id);
        Task<T?> GetAllClientsAsync<T>();
        Task<T?> GetClientByIdAsync<T>(Guid id);
        Task<Stream> GetClientImage(Guid id);
        Task UpdateClientAsync<T>(UpdateClientDTO updateClientDto);
    }
}