using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] songs;
    private AudioSource audioSource;
    private int currentClipIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            if (songs.Length > 0)
            {
                audioSource.clip = songs[currentClipIndex];
                audioSource.Play();
                currentClipIndex = (currentClipIndex + 1) % songs.Length;
            }
        }
    }
}

