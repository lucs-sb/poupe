using Poupe.API.Resources;

namespace Poupe.API.Utils;

public class Util
{
    public static void ValidateGuid(string id)
    {
        if (!Guid.TryParse(id, out _))
            throw new ArgumentException(string.Format(ApiMessage.Invalid_Warning, nameof(id)));
    }
}
