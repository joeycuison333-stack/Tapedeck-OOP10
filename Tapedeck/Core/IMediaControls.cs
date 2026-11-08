namespace mediaPlayerPrj;

/// <summary>The playback controls every player in this project must provide.</summary>
public interface IMediaControls
{
    /// <summary>Starts or resumes the current item.</summary>
    void Play();

    /// <summary>Pauses the current item.</summary>
    void Pause();

    /// <summary>Stops playback.</summary>
    void Stop();

    /// <summary>Moves to the next item (respects shuffle).</summary>
    void Next();

    /// <summary>Moves to the previous item in list order.</summary>
    void Previous();
}
