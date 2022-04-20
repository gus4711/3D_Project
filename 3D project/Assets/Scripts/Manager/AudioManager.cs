using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager: Singleton<AudioManager>
{
    [SerializeField] AudioClip[] _puddleWalking;
    [SerializeField] AudioClip[] _grassWalking;
    AudioClip[] _playingWalking = null;
    AudioClip[] _lastPlaying = null;
    int _playIndexL = 0;
    int _playIndexH = 0;

    [SerializeField] string[] _audioKey;
    [SerializeField] AudioClip[] _audioValue;

    Dictionary<string, AudioClip> _audioList = new Dictionary<string, AudioClip>();


    AudioSource _audioSource;

    private void Awake()
    {
        for (int i = 0; i < _audioKey.Length; i++)
        {
            _audioList.Add(_audioKey[i], _audioValue[i]);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public void OnShotPlay(string audioKey)
    {
        _audioSource.PlayOneShot(_audioList[audioKey]);
    }

    public void OnShotPlay(string audioKey, float volume)
    {
        _audioSource.PlayOneShot(_audioList[audioKey],volume);
    }

    //index:0 - puddle, 1 - grass
    public void WalkingPlay(int index)
    {
        if (index == 4) _playingWalking = _puddleWalking;
        else/*if (index == 1)*/ _playingWalking = _grassWalking;

        if (_playingWalking != _lastPlaying)
        {
            _playIndexL = 0;
            _playIndexH = _playingWalking.Length - 1;
        }

        _audioSource.PlayOneShot(_playingWalking[Random.Range(_playIndexL, _playIndexH)]);
        _lastPlaying = _puddleWalking;
    }
}
