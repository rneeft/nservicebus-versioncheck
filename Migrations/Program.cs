using DbUp;
using System.Reflection;

var connectionString = @"Data Source=.\DEV01;Initial Catalog=nservicebustest;Integrated Security = SSPI;TrustServerCertificate = True";

EnsureDatabase.For
    .SqlDatabase(connectionString);

var upgradeEngine = DeployChanges.To
    .SqlDatabase(connectionString)
    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
    .WithTransactionPerScript()
    .LogToConsole()
    .Build() ?? throw new InvalidOperationException($"upgradeEngine is null");

var result = upgradeEngine.PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error.ToString());
    Console.ResetColor();
    return -1;
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Success!!");
Console.ResetColor();

return 0;

