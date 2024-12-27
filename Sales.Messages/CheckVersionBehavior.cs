namespace Messages;

public class CheckVersionBehavior :
    Behavior<IIncomingLogicalMessageContext>
{
    private readonly Version maxiumVersion;

    public CheckVersionBehavior(Version maxiumVersion)
    {
        this.maxiumVersion = maxiumVersion;
    }

    public override async Task Invoke(IIncomingLogicalMessageContext context, Func<Task> next)
    {
        if (context.MessageHeaders.ContainsKey("ApplicationVersion"))
        {
            var version = Version.Parse(context.MessageHeaders["ApplicationVersion"]);
            Console.WriteLine($"XXXX: Version found: {version}");

            if (version > maxiumVersion)
            {
                Console.WriteLine($"XXXX: Skip message, higher version");

                throw new VersionHigherException();
            }
        }

        // custom logic before calling the next step in the pipeline.

        await next();

        // custom logic after all inner steps in the pipeline completed.
    }
}

