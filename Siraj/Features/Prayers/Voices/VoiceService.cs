using System;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace Siraj.Features.Prayers.Voices;

internal sealed class VoiceService
{
    private readonly MediaPlayer player = new()
    {
        AudioCategory = MediaPlayerAudioCategory.Alerts
    };

    public void Play(Voice voice, bool isFajr)
    {
        if (player.CurrentState is MediaPlayerState.Playing)
        {
            return;
        }

        var file = $"{voice}{(isFajr ? "Fajr" : string.Empty)}";
        
        player.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets/Voices/{file}.mp3"));

        player.Play();
    }
}