using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Web_DonNghiPhep.Hubs
{
    public class NotificationsHub : Hub
    {
        public async Task SendNotification(String message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
        public async Task SendNotificationToGroup(string groupName, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveNotification", message);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }


        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
        public override async Task OnConnectedAsync()
        {
            var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value;

            if (!string.IsNullOrEmpty(userRole))
            {
                if (userRole.Equals("Quản lý", StringComparison.OrdinalIgnoreCase))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, "Manager");
                }
                else if (userRole.Equals("Nhân viên", StringComparison.OrdinalIgnoreCase))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, "Employee");
                }
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value;

            if (!string.IsNullOrEmpty(userRole))
            {
                if (userRole.Equals("Quản lý", StringComparison.OrdinalIgnoreCase))
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Manager");
                }
                else if (userRole.Equals("Nhân viên", StringComparison.OrdinalIgnoreCase))
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Employee");
                }
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
