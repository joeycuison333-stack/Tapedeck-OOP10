namespace Tapedeck.UI   
{
    public class TeamMember
    {
        public string Name { get; }
        public string Role { get; }
        public string PhotoFile { get; }

        public TeamMember(string name, string role, string photoFile)
        {
            Name = name;
            Role = role;
            PhotoFile = photoFile;
        }
    }

    public static class TeamInfo
    {
        public const string AppTitle = "Tapedeck";
        public const string GroupName = "OOP Group 2";
        public const string Section = "BSCS 2-A";
        public const string LogoFile = "logo.png";

        public static readonly TeamMember[] Members =
        {
            new TeamMember("Joey Cuison", "Team lead / player engine, UI and integration", @"Team\joey.jpg"),
            new TeamMember("Heaven", "Library saving, loading and deployment", @"Team\heaven.jpg"),
            new TeamMember("Ammaar Azmy", "Sorting and searching", @"Team\ammaar.jpg"),
            new TeamMember("Christian Maddox Rivera", "Skin design and splash screen", @"Team\maddox.jpg"),
            new TeamMember("Valencia Genevieve", "Favorites, testing and user manual", @"Team\genevieve.jpg"),
        };
    }
}