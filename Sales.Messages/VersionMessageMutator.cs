using NServiceBus.MessageMutator;

namespace Messages;

public class VersionMessageMutator : IMutateIncomingTransportMessages
{
    private readonly Version maxiumVersion;

    public VersionMessageMutator(Version maxiumVersion)
    {
        this.maxiumVersion = maxiumVersion;
    }

    public Task MutateIncoming(MutateIncomingTransportMessageContext context)
    {
        if (context.Headers.ContainsKey("ApplicationVersion"))
        {
            var version = Version.Parse(context.Headers["ApplicationVersion"]);
            Console.WriteLine($"MITM: Version found: {version}");

            if (version > maxiumVersion)
            {
                Console.WriteLine($"MITM: Skip message, higher version");
                

                throw new VersionHigherException();
            }
        }

        return Task.CompletedTask;
    }
}

