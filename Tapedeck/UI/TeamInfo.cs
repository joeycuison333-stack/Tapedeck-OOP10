namespace mediaPlayerPrj;

/// <summary>One group member shown on the splash screen.</summary>
public class TeamMember
{
    /// <summary>Full name.</summary>
    public string Name { get; }

    /// <summary>Role from the role matrix.</summary>
    public string Role { get; }

    /// <summary>Photo path relative to the Assets folder.</summary>
    public string PhotoFile { get; }

    /// <summary>Creates a member.</summary>
    public TeamMember(string name, string role, string photoFile)
    {
        Name = name;
        Role = role;
        PhotoFile = photoFile;
    }
}

/// <summary>
/// PLACEHOLDER pushed by Joey so the project builds. MADDOX replaces this whole file
/// with all five members, their roles and photos.
/// </summary>
public static class TeamInfo
{
    /// <summary>Application title.</summary>
    public const string AppTitle = "Tapedeck";

    /// <summary>Group number.</summary>
    public const string GroupName = "Group 3";

    /// <summary>Class section.</summary>
    public const string Section = "BSCS 2A";

    /// <summary>Logo inside the Assets folder.</summary>
    public const string LogoFile = "logo.png";

    /// <summary>Placeholder member list.</summary>
    public static readonly TeamMember[] Members =
    {
        new TeamMember("Team member", "Role", @"Team\placeholder.jpg"),
    };
}
