using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Endpoints;

public static class ProblemDetailsExtensions
{
    public static Dictionary<string, string[]> ConvertToProblemDetails(this IReadOnlyCollection<Notification> notifications)
        => notifications.GroupBy(not => not.Key).ToDictionary(not => not.Key, not => not.Select(mes => mes.Message).ToArray());

    public static Dictionary<string, string[]> ConvertToProblemDetails(this IEnumerable<IdentityError> error)
        => new() { { "Error", error.Select(err => err.Description).ToArray() } };
}
