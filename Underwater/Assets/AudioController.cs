using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

    public static AudioSource currentAudioSource;
    public static AudioSource targetAudioSource;
    public static float targetVolume;

    public static float audioAdjustRate = 0.01f;

    void Start()
    {
        currentAudioSource = GameObject.Find("submarine bgm").GetComponent<AudioSource>();
        targetAudioSource = currentAudioSource;
    }

	// Update is called once per frame
	void Update () {
        if (targetAudioSource != currentAudioSource)
        {
            currentAudioSource.volume -= audioAdjustRate;
            if(currentAudioSource.volume == 0)
            {
                currentAudioSource.gameObject.SetActive(false);
                currentAudioSource = targetAudioSource;
            }
            return;
        }

        if(currentAudioSource.volume < targetVolume)
            currentAudioSource.volume += audioAdjustRate;
	}

    public static void changeAudioSource(AudioSource newAudio, float newVolume)
    {
        targetAudioSource = newAudio;
        targetVolume = newVolume;

        targetAudioSource.gameObject.SetActive(true);
        targetAudioSource.volume = 0;
    }
}
