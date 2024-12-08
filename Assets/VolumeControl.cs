using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip popSound;
    [SerializeField] private float soundCooldown = 0.3f;

    private float lastSoundTime;

    void Start()
    {
        volumeSlider.value = AudioListener.volume;

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;

        if (audioSource != null && popSound != null && Time.time >= lastSoundTime + soundCooldown)
        {
            audioSource.PlayOneShot(popSound);
            lastSoundTime = Time.time;
        }
    }
}
