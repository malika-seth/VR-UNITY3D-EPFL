using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anemometer : MonoBehaviour
{
    public OSC osc;

    private float velocity;
    public float Velocity
    {
        get
        {
            return velocity;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // register handlers
        osc.SetAddressHandler("/inputs/analogue", OnReceiveBreathData);
        osc.SetAddressHandler("/imu", VoidHandler); // did not see option to turn these off in OSC board. 
        osc.SetAddressHandler("/battery", VoidHandler); // should add low battery warning to game
    }

    void OnReceiveBreathData(OscMessage message)
    {
        float x = message.GetFloat(0);
        float x1 = message.GetFloat(1);
        float x2 = message.GetFloat(2);

        x = Mathf.Max(x - .04f, 0f);
        velocity = x * 800;
    }

    void VoidHandler(OscMessage message)
    {
    }

    // Update is called once per frame
    void Update()
    {        
    }
}
