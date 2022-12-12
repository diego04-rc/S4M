using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private AudioClip musicaNormal;
    [SerializeField]
    private AudioClip musicaCombate;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(musicaNormal);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activarMusicaCombate()
    {
        audioSource.Stop();
        audioSource.loop = true;
        audioSource.PlayOneShot(musicaCombate);
    }

    public void desactivarMusicaCombate()
    {
        audioSource.Stop();
        audioSource.loop = true;
        audioSource.PlayOneShot(musicaNormal);
    }
}
