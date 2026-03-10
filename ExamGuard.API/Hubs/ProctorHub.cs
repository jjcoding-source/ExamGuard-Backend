using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ExamGuard.API.Hubs
{
    [Authorize]
    public class ProctorHub : Hub
    {
        public async Task JoinExamSession(string examId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"exam-{examId}");
        }

        public async Task JoinMonitorGroup(string examId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"monitor-{examId}");
        }

        public async Task SendViolationAlert(string examId, object violationData)
        {
            await Clients
                .Group($"monitor-{examId}")
                .SendAsync("ReceiveViolationAlert", violationData);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}