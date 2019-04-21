using UnityEngine.Audio;
using System;
using UnityEngine;


public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    private Sound pesma;
    private int i;

    [HideInInspector]
    public bool kontrola_pustanja = true;

    public static AudioManager instance;
   

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            if(s.name != "Background") s.source.clip = s.clip[0];
            else Random_Music("Background");
            s.source.volume = s.volume;
            s.source.priority = s.priority;
            s.source.loop = s.loop;
        }
    
    }

    private void Update()
    {
        if(kontrola_pustanja) Random_Music("Background");

    }

    private void Random_Music(string name)
    {
        if (pesma == null)
        {
            pesma = Array.Find(sounds, sound => sound.name == name);
            i = UnityEngine.Random.Range(0, pesma.clip.Length);
            //Debug.LogWarning(i);       
        }
 
        if (!pesma.source.isPlaying) 
        {
           // Debug.LogWarning(pesma.clip.Length);
            if (i >= pesma.clip.Length-1) i = 0;
            else i++;
            pesma.source.clip = pesma.clip[i];
            pesma.source.Play();
           // Debug.LogWarning("pusteno");
        }
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Play();
    }

    



}
