using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;

public class ParticleDriver : MonoBehaviour
{
    public OSC osc;
    public Anemometer meter;

    ParticleSystem myParticleSystem;
    ParticleSystem.EmissionModule emissionModule;

    // for the collision https://answers.unity.com/questions/1197119/how-to-detect-when-and-where-a-particle-hits-a-sur.html
    private List<ParticleCollisionEvent> CollisionEvents;

    // for the hit rate GUI
    public GameObject textGUI;
    private TextMesh mesh;

    // Sphere Object to change color with collisions
    public GameObject colorColliderChangeObject;

    // cube for relight simulation
    public GameObject relightCube;

    // tuning parameter
    private const float HIT_RATE_TUNING_PARAMTER = 400f;

    // for angle of particle emission
    private float angle = -15.3f;

    // delay counter for cube relighting 
    private int cubeRelightDelayCount = -1;
    
    void Start()
    {
        // Get the system and the emission module.
        myParticleSystem = GetComponent<ParticleSystem>();
        emissionModule = myParticleSystem.emission;

        // collision
        CollisionEvents = new List<ParticleCollisionEvent>();

        // get the text mesh for the hit rate display
        mesh = textGUI.GetComponent<TextMesh>();

        // Get the material to change color for the sphere
        colorColliderChangeObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);

    }

    public void OnParticleCollision(GameObject other)
    {
        // the event count should be the number of collisions this frame
        int eventCount = myParticleSystem.GetCollisionEvents(other, CollisionEvents);

        float hitRate = eventCount / Time.deltaTime;

        mesh.text = "Hit Rate: " + Convert.ToInt32(hitRate).ToString();

        // Debug.Log(" $$ Particle Collision with: " + other.name + " Frame: " + Time.frameCount + " Delta Time: " + Time.deltaTime.ToString() + " - eventcount: " + eventCount.ToString());

        // if it is the sphere
        if(other.name == "SphereColorTest")
        {
            // float colorValue = (1f - Math.Min(hitRate / HIT_RATE_TUNING_PARAMTER, 1f))*255f;
            float colorValue = (1f - Math.Min(hitRate / HIT_RATE_TUNING_PARAMTER, 1f));
            // Debug.Log("Color Value: " + colorValue.ToString());
            colorColliderChangeObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(1f, colorValue, colorValue));

            // Destroy(other);
        }
        else if(other.name == "CubeRelight")
        {
            relightCube.SetActive(false);
            cubeRelightDelayCount = 200;
        }

        for (int i = 0; i < eventCount; i++)
        {
            //TODO: Do your collision stuff here. 
            // You can access the CollisionEvent[i] to obtaion point of intersection, normals that kind of thing
            // You can simply use "other" GameObject to access it's rigidbody to apply force, or check if it implements a class that takes damage or whatever
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Hook into the current velocity from anenometer mediator
        emissionModule.rateOverTime = meter.Velocity;

        // Since update is called before collision events
        mesh.text = "Hit Rate: 0";
        colorColliderChangeObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);

        // Check if rotate up/down for particle system
        if (Input.GetKeyDown("up"))
        {
            Debug.Log("up arrow");
            if (angle > -30f)
            {
                angle -= 2.5f;
                angle = Mathf.Max(angle, -30f);
                transform.eulerAngles = new Vector3(angle, transform.eulerAngles.y, transform.eulerAngles.z);
                // transform.eulerAngles = new Vector3(angle, 0f, 0f);
            }
        } else if (Input.GetKeyDown("down"))
        {
            Debug.Log("down arrow");
            if (angle < 30f)
            {
                angle += 2.5f;
                angle = Mathf.Min(angle, 30f);
                // transform.eulerAngles.Set(angle, transform.eulerAngles.y, transform.eulerAngles.z);
                transform.eulerAngles = new Vector3(angle, transform.eulerAngles.y, transform.eulerAngles.z);
            }
        } 

        // set up framecountdown for cube relight
        if(cubeRelightDelayCount > 0)
        {
            cubeRelightDelayCount--;
        }
        else if (cubeRelightDelayCount == 0)
        {
            relightCube.SetActive(true);
            cubeRelightDelayCount = -1;
        }

    }
}
