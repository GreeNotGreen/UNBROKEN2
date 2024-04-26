using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatManager : MonoBehaviour
{
    [SerializeField] private float _bpm;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Intervals[] _intervals;

    private bool _audioLoaded = false;

    private void Start()
    {
        if (_audioSource.clip != null)
        {
            _audioLoaded = true;
            StartBeatDetection();
        }
        else
        {
            _audioSource.clip.LoadAudioData();
            StartCoroutine(WaitForAudioLoad());
        }
    }

    private void StartBeatDetection()
    {
        foreach (Intervals interval in _intervals)
        {
            interval.Initialize(_bpm, _audioSource.clip.frequency);
        }
    }

    private IEnumerator WaitForAudioLoad()
    {
        while (!_audioLoaded)
        {
            if (_audioSource.clip.loadState == AudioDataLoadState.Loaded)
            {
                _audioLoaded = true;
                StartBeatDetection();
            }
            else
            {
                yield return null;
            }
        }
    }

    private void Update()
    {
        if (_audioLoaded)
        {
            foreach (Intervals interval in _intervals)
            {
                float sampledTime = (_audioSource.timeSamples / (_audioSource.clip.frequency * interval.GetIntervalLength()));
                interval.CheckForNewInterval(sampledTime);
            }
        }
    }
}

[System.Serializable]
public class Intervals
{
    [SerializeField] private float _steps;
    [SerializeField] private UnityEvent _trigger;
    private int _lastInterval;
    private float _intervalLength;

    public void Initialize(float bpm, int sampleRate)
    {
        _intervalLength = 60f / (bpm * _steps);
    }

    public void CheckForNewInterval(float interval)
    {
        if (Mathf.FloorToInt(interval) != _lastInterval)
        {
            _lastInterval = Mathf.FloorToInt(interval);
            _trigger.Invoke();
        }
    }

    public float GetIntervalLength()
    {
        return _intervalLength;
    }
}