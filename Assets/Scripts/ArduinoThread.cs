using UnityEngine;
using System;
using System.Threading;
using System.Collections;
using System.IO.Ports;

public class ArduinoThread : MonoBehaviour
{
    /* The serial port where the Arduino is connected. */
    [Tooltip("The serial port where the Arduino is connected")]
    public string port = "COM5";
    /* The baudrate of the serial port. */
    [Tooltip("The baudrate of the serial port")]
    public int baudrate = 9600;
    public int timeout = 1000;

    private SerialPort stream;
    private Thread thread;
    private Queue outputQueue; // From Unity to Arduino
    private Queue inputQueue; // From Arduino to Unity

    public bool looping = true;

    public void StartThread()
    {
        outputQueue = Queue.Synchronized(new Queue());
        inputQueue = Queue.Synchronized(new Queue());

        thread = new Thread(ThreadLoop);
        thread.Start();
    }

    public void ThreadLoop()
    {
        // Opens the connection on the serial port
        stream = new SerialPort(port, baudrate);
        stream.ReadTimeout = 50;
        stream.Open();

        // Looping
        while (IsLooping())
        {
            // Send to Arduino
            if (outputQueue.Count != 0)
            {
                //Debug.Log("something in output queue;");
                string command = (string)outputQueue.Dequeue();
                WriteToArduino(command);
            }

            // Read from Arduino
            //Debug.Log("Checking arduino queue:");
            string result = ReadFromStream();
            if (result != null)
            {
                //Debug.Log("Found something in the queue");
                inputQueue.Enqueue(result);
            }
        }
        stream.Close();
    }

    public void SendToArduino(string command)
    {
        outputQueue.Enqueue(command);
    }

    public void StopThread()
    {
        lock (this)
        {
            looping = false;
        }
    }

    public bool IsLooping()
    {
        lock (this)
        {
            return looping;
        }
    }

    public string ReadFromStream(int timeout = 50)
    {
        stream.ReadTimeout = timeout;
        try
        {
            return stream.ReadLine();
        }
        catch (TimeoutException)
        {
            return null;
        }
    }

    public string ReadFromArduino()
    {
        if (inputQueue.Count != 0)
            Debug.Log("Input NOT 0");
        if (inputQueue.Count == 0)
            return null;

        return (string)inputQueue.Dequeue();
    }

    public string[] ReadAllFromArduino()
    {
        if (inputQueue.Count == 0)
            return null;

        string[] newStrings = new string[inputQueue.Count];

        for (int i = 0; i < inputQueue.Count; i++)
        {
            newStrings[i] = (string)inputQueue.Dequeue();
        }

        return newStrings;
    }

    public void WriteToArduino(string message)
    {
        // Send the request
        //Debug.Log("Writing request out to Arduino");
        stream.WriteLine(message);
        stream.BaseStream.Flush();
    }

    public void CloseThread()
    {
        thread.Abort();
    }
}