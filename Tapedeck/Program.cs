// TEMPORARY Program.cs for Joey's first commit (step J1).
// Copy this file to Tapedeck/Program.cs in the repo. It opens an empty window so `main`
// builds before the other files exist. Joey replaces it with the real Program.cs in step J9.
namespace mediaPlayerPrj;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new Form { Text = "Tapedeck (setup)" });
    }
}
