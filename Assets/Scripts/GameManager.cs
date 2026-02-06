using UnityEngine;
using TMPro;
using UnityEngine.UI; 
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject audioPanel;
    [SerializeField] private Slider musicSlider; 
    [SerializeField] private Slider sfxSlider; 
    [SerializeField] private AudioMixer mixer; 
    [SerializeField] private string exposedMusicParamName;
    [SerializeField] private string exposedSFXParamName;

    public void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void ToggleAudio()
    {
        audioPanel.SetActive(!audioPanel.activeSelf);
    }

    public void SetMusicVolume()
    {
        float volume = Mathf.Log10(Mathf.max(musicSlider.value, 0.00001f)) * 20f;
        mixer.SetFloat(exposedMusicParamName, volume);
    }

    public void SetSFXVolume()
    {
        float volume = Mathf.Log10(Mathf.max(sfxSlider.value, 0.00001f)) * 20f; 
        mixer.SetFloat(exposedSFXParamName, volume); 
        Debug.Log(volume); 
    }
}
