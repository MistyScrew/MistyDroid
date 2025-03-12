using NitroBolt.Functional;
using System.Collections.Immutable;

namespace MistyDroid.Server.Services;

//C:\Users\Mist\AppData\Local\Android\Sdk\platform-tools

public class AdbManager
{
    ImmutableDictionary<string, AdbSession> Sessions = ImmutableDictionary<string, AdbSession>.Empty;

    public AdbSession GetOrCreateSession(string name)
    {
        var session = Sessions.Find(name);
        if (session == null)
        {
            session = new AdbSession();
            Sessions = Sessions.Add(name, session);
        }
        return session;
    }
}
