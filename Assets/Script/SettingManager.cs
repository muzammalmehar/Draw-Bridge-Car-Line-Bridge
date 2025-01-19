using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public Slider soundFx;
    public Slider bg;

    public AudioSource soundFxAudio;
    public AudioSource bgMusicAudio;

    void Start()
    {
        // Initialize the sliders with the current volume levels from MusicManager
        soundFxAudio = MusicManager.instance.soundFxAudio;
        bgMusicAudio = MusicManager.instance.bgMusicAudio;

        soundFx.value = soundFxAudio.volume;
        bg.value = bgMusicAudio.volume;

        // Add listeners to detect slider value changes
        soundFx.onValueChanged.AddListener(delegate { SoundFxSlider(); });
        bg.onValueChanged.AddListener(delegate { BgMusicSlider(); });
    }

    // Method to update Sound FX volume based on slider value
    public void SoundFxSlider()
    {
        if (soundFxAudio != null)
        {
            soundFxAudio.volume = soundFx.value;
        }
    }

    // Method to update Background Music volume based on slider value
    public void BgMusicSlider()
    {
        if (bgMusicAudio != null)
        {
            bgMusicAudio.volume = bg.value;
        }
    }
}
