namespace mediaPlayerPrj;

/// <summary>One group member shown on the splash screen.</summary>
public class TeamMember
{
    /// <summary>Full name.</summary>
    public string Name { get; }

    /// <summary>Role from the role matrix.</summary>
    public string Role { get; }

    /// <summary>Photo path relative to the Assets folder, for example Team\joey.jpg.</summary>
    public string PhotoFile { get; }

    /// <summary>Creates a member.</summary>
    /// <param name="name">Full name.</param>
    /// <param name="role">Role from the role matrix.</param>
    /// <param name="photoFile">Photo path relative to the Assets folder.</param>
    public TeamMember(string name, string role, string photoFile)
    {
        Name = name;
        Role = role;
        PhotoFile = photoFile;
    }
}

/// <summary>Group details used by the splash screen, the title bar and the About box.</summary>
public static class TeamInfo
{
    /// <summary>Name of the application.</summary>
    public const string AppTitle = "Tapedeck";

    /// <summary>Group name shown on the splash screen and title bar.</summary>
    public const string GroupName = "OOP Group 3";

    /// <summary>Class section.</summary>
    public const string Section = "BSCS 2-A";

    /// <summary>Logo file in the Assets folder.</summary>
    public const string LogoFile = "logo.png";

    /// <summary>Every member, in the order shown on the splash screen.</summary>
    public static readonly TeamMember[] Members =
    {
        new TeamMember("Joey Cuison", "Team lead / player engine, UI and integration", @"Team\joey.jpg"),
        new TeamMember("Heaven", "Library saving, loading and deployment", @"Team\heaven.jpg"),
        new TeamMember("Ammaar Azmy", "Sorting and searching", @"Team\ammaar.jpg"),
        new TeamMember("Christian Maddox Rivera", "Skin design and splash screen", @"Team\maddox.jpg"),
        new TeamMember("Valencia Genevieve", "Favorites, testing and user manual", @"Team\genevieve.jpg"),
    };
}
