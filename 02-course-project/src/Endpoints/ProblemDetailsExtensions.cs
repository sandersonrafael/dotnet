using Flunt.Notifications;

namespace FinalProject.Endpoints;

public static class ProblemDetailsExtensions
{
    public static Dictionary<string, string[]> ConvertToProblemDetails(this IReadOnlyCollection<Notification> notifications)
        => notifications.GroupBy(not => not.Key).ToDictionary(not => not.Key, not => not.Select(mes => mes.Message).ToArray());
}
