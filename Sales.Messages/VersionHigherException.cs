namespace Messages;

public class VersionHigherException : Exception 
{
    public override string Message => "Skipped message because of lower version";
}

