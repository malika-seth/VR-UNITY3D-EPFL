using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;

public class ReceivePosition : MonoBehaviour
{
    public OSC osc;
    public bool logOSCTransmissions = true;
    public string logFileName = @"output.csv";

    private long messageCount = 0;
    private StringBuilder csv = new StringBuilder();
    private long frameNumber = 0;


    // Use this for initialization
    void Start()
    {
        Debug.Log("Start Address handler"   );

        osc.SetAddressHandler("/inputs/analogue", OnReceiveBreathData);
        osc.SetAddressHandler("/imu", VoidHandler);
        osc.SetAddressHandler("/battery", VoidHandler);

    }

    // Update is called once per frame
    void Update()
    {
        frameNumber++;
    }

    void VoidHandler(OscMessage message)
    {
    }

    void OnReceiveBreathData(OscMessage message)
    {
        float x = message.GetFloat(0);
        float x1 = message.GetFloat(1);
        float x2 = message.GetFloat(2);
        DateTime timeStamp = DateTime.Now;

        Vector3 position = transform.position;

        position.x = x*10;
        position.y = x*10;
        transform.position = position;

        if (logOSCTransmissions)
        {
            var newLine = String.Format("{0}, {1}, {2}, {3}, {4}, {5}", messageCount, frameNumber, timeStamp.ToString("s.ffff"), x, x1, x2);
            // Debug.Log(x.ToString());
            csv.AppendLine(newLine);
        }
        messageCount++;
    }

    void OnApplicationQuit()
    {
        if (logOSCTransmissions)
        {
            Debug.Log("Writing Data to File");
            File.WriteAllText(logFileName, csv.ToString());
        }
    }
}