using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PauseParticleOnPause : MonoBehaviour {

    private void Start()
    {
        PauseScript.OnPauseEvent.AddListener(ToggleParticle);
    }

    public void ToggleParticle()
    {
        ParticleSystem system = GetComponent<ParticleSystem>();

        if (system.isPaused)
        {
            system.Play();
        }
        else
        {
            system.Pause();
        }
        
    }
}
