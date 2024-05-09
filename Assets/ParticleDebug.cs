using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDebug : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            _particleSystem.Play();
        }
    }
}
