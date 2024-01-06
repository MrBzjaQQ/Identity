using Identity.Users.Infrastructure.Database;

namespace Identity.Users.Web.Settings;

public class WebAppSettings
{
    public DatabaseSettings? DatabaseSettings { get; set; }
}
