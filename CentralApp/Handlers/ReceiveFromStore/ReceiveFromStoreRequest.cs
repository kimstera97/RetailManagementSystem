using MediatR;
using RetailManagement.Shared.Models;

namespace CentralApp.Handlers.ReceiveFromStore;

public class ReceiveFromStoreRequest(SyncPayload payload) : IRequest
{
    public SyncPayload Payload { get; set; } = payload;
}
