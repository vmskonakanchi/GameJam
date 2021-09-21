using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    AudioManager instance;
    private void Awake()
    {
        if (instance == null) instance = this; 
        else 
        {
            Destroy(gameObject); 
            return;
        }      
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.playOnAwake = s.playOnAwake;
            s.source.loop = s.loop;
        }
    }
    private void Start()
    {
        PlaySound("theme");
    }
    public void PlaySound(string name) 
    {
        foreach(Sound sd in sounds)
        {
            if (sd == null) return;
            if (sd.name == name)
            {              
                sd.source.Play();
            }
        }
    }
}