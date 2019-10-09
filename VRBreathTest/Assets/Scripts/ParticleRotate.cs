using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRotate : MonoBehaviour
{
    private GameObject cameraRig;


    // Start is called before the first frame update
    void Start()
    {
        cameraRig = GameObject.FindGameObjectWithTag("mainCamera");

        Debug.Log("STARTING PARTICLE ROTATE - " + cameraRig.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        // get the camera positions
        float cameraX = cameraRig.transform.position.x +  0.3f;
        float cameraY = cameraRig.transform.position.y - 0.2f;
        float cameraZ = cameraRig.transform.position.z;
        float x_rot = cameraRig.transform.rotation.x;
        float y_rot = cameraRig.transform.rotation.y;
        float z_rot = cameraRig.transform.rotation.z;
        float w = cameraRig.transform.rotation.w;
        Vector3 v3 = new Vector3(cameraX, cameraY, cameraZ);

        Quaternion pos_vector = new Quaternion(x_rot, y_rot, z_rot, w);
        this.transform.position = v3;
        this.transform.rotation = pos_vector;
        Debug.Log("x pos: " + cameraX.ToString());

    }
}
