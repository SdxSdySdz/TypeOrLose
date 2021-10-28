using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class TypingSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _wordFilledSound;
    [SerializeField] private List<AudioClip> _sounds;

    private AudioSource _audioSource;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomSound()
    {
        int index = Random.Range(0, _sounds.Count);
        _audioSource.clip = _sounds[index];
        _audioSource.Play();
    }

    public void PlayWordFilledSound()
    {
        _audioSource.clip = _wordFilledSound;
        _audioSource.Play();
    }
}
