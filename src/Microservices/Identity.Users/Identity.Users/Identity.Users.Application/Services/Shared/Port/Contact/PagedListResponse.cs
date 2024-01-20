namespace Identity.Users.Application.Services.Shared.Port.Contact;

/// <summary>
/// Generic paged list response
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed record PagedListResponse<T>
    where T : new()
{
    /// <summary>
    /// Generic items
    /// </summary>
    public IList<T> Items { get; init; } = Array.Empty<T>();

    /// <summary>
    /// Total count of entities
    /// </summary>
    public long TotalCount { get; init; }
}
