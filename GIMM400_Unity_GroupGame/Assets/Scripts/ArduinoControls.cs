using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArduinoControls : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody rb;
    private SerialPort sp = new SerialPort("COM4", 57600);
    private string[] splitLine;
    private float x;
    private float y;
    private float button;

    // Start is called before the first frame update
    void Start()
    {
        sp = new SerialPort("COM4", 57600);
        sp.Open();
        sp.ReadTimeout = 1;
        sp.DtrEnable = true;
        sp.RtsEnable = true;
    }

    void FixedUpdate()
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
            Debug.Log(splitLine);
        }
    }

    void Move()
    {
        Debug.Log(splitLine);
        /*if (CMD == "")//right
        {
            rb.AddForce(moveSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (CMD == "")//left
        {
            rb.AddForce(-moveSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (CMD == "")//up
        {
            rb.AddForce(0, 0, moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }

        if (CMD == "")//down
        {
            rb.AddForce(0, 0, -moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }*/
    }

    void ReadCom()
    {
        splitLine = sp.ReadLine().Split();
        if (!float.TryParse(splitLine[0], out x)) print("Failed to parse x");
        if (!float.TryParse(splitLine[1], out y)) print("Failed to parse y");
        if (!float.TryParse(splitLine[2], out button)) print("Failed to parse button");
    }
}