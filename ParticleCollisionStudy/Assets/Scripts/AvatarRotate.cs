using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarRotate : MonoBehaviour
{
    private float angle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("left"))
        {
            Debug.Log("left arrow");
            if (angle > -90f)
            {
                angle -= 5f;
                angle = Mathf.Max(angle, -90f);
                transform.eulerAngles = new Vector3(0f, angle, 0f);
            }
        }

        if (Input.GetKeyDown("right"))
        {
            Debug.Log("right arrow");
            if (angle < 90f)
            {
                angle += 5f;
                angle = Mathf.Min(angle, 90f);
                transform.eulerAngles = new Vector3(0f, angle, 0f);
            }
        }
    }
}
