using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDriver : MonoBehaviour
{
    public OSC osc;

    ParticleSystem myParticleSystem;
    ParticleSystem.EmissionModule emissionModule;

    void Start()
    {
        // Get the system and the emission module.
        myParticleSystem = GetComponent<ParticleSystem>();
        emissionModule = myParticleSystem.emission;

        Debug.Log("Start Particle System Handler");

        osc.SetAddressHandler("/inputs/analogue", OnReceiveBreathData);
        osc.SetAddressHandler("/imu", VoidHandler);
        osc.SetAddressHandler("/battery", VoidHandler);

    }

    void OnReceiveBreathData(OscMessage message)
    {
        float x = message.GetFloat(0);
        float x1 = message.GetFloat(1);
        float x2 = message.GetFloat(2);

        x = Mathf.Max(x - .04f, 0f);
        float currentRate = x * 200;
        emissionModule.rateOverTime = currentRate;
    }

    void VoidHandler(OscMessage message)
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
