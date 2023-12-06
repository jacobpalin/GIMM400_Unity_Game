using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArduinoControls : MonoBehaviour
{
    private SerialPort sp;
    [SerializeField] private string[] splitLine;
    private float x;
    private float y;
    private float button;

    // Start is called before the first frame update
    void Start()
    {
        sp = null;
        sp = new SerialPort("COM4", 57600);
        sp.Open();
        sp.ReadTimeout = 10;
        sp.DtrEnable = true;
        sp.RtsEnable = true;
    }

    void Update()
    {
        if (sp.IsOpen)
        {
            try
            {
                ReadCom();
                Move();
            }
            catch (System.Exception)
            {

            }
        }
        else
        {
            Move();
        }
    }

    void Move()
    {
        if (y >= 550)//left
        {
            Debug.Log("left");
        }

        if (y <= 450)//right
        {
            Debug.Log("right");
        }

        if (x <= 450)//up
        {
            Debug.Log("up");
        }

        if (x >= 550)//down
        {
            Debug.Log("down");
        }
        if (button == 0)//attack
        {
            Debug.Log("attack");
        }
    }

    void ReadCom()
    {
        splitLine = sp.ReadLine().Split();
        if (!float.TryParse(splitLine[1], out x)) print("Failed to parse x");
        if (!float.TryParse(splitLine[2], out y)) print("Failed to parse y");
        if (!float.TryParse(splitLine[3], out button)) print("Failed to parse button");
    }
}