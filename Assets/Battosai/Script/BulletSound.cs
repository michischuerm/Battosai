using UnityEngine;
using System.Collections;

public class BulletSound : MonoBehaviour
{

    private AudioClip[] shootSoundsCurrent;

    [Range(0, 36f)]//Value below
    public float pitchRange = -36f;
    public float minVolumeRange = 0.8f;
    public float maxVolumeRange = 1f;


    private AudioSource audioSource;
    private int randomValueFromSoundArray;
    private static int previousRandomValue = -1;
    private float pitchOctaveValue = 1.05946f;


    void Start()
    {

        audioSource = gameObject.GetComponent<AudioSource>();


        if (shootSoundsShort.Length > 0)
        {
            disableRandomAudioSuccessively(shootSoundsCurrent);
            playRandomSoundOnStart(shootSoundsCurrent);
        }
        else
        {
            Debug.Log("No SoundClips in Array!");
        }
    }


    void Awake()
    {
    }


    private void disableRandomAudioSuccessively(AudioClip[] shootArrayDependingOnShotPower)
    {
        do
        {
            randomValueFromSoundArray = Random.Range(0, shootArrayDependingOnShotPower.Length);
        }
        while (previousRandomValue == randomValueFromSoundArray);

        if (shootArrayDependingOnShotPower.Length == 1)
        {
            previousRandomValue = -1;
        }
        else
        {
            previousRandomValue = randomValueFromSoundArray;
        }
    }
    void playRandomSoundOnStart(AudioClip[] shootArrayDependingOnShotPower)
    {
        audioSource.clip = shootArrayDependingOnShotPower[randomValueFromSoundArray];
        audioSource.pitch = Mathf.Pow(pitchOctaveValue, (1 + (Random.Range(-pitchRange, +pitchRange))));
        audioSource.volume = Random.Range(minVolumeRange, maxVolumeRange);
        audioSource.Play();
        //Debug.Log("you played: " + shootArrayDependingOnShotPower[randomValueFromSoundArray]);
    }

}