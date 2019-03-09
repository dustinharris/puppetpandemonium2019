using UnityEngine;
//using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class HandleTextFile : MonoBehaviour {

    public string fileName;

    void WriteString()
    {
#if UNITY_EDITOR
        string path = "Assets/Resources/" + fileName;
#else
        string path = "puppetpandemonium_Data/Resources/Levels/" + fileName;
#endif


        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("Test");
        writer.Close();

        //Re-import the file to update the reference in the editor
        //AssetDatabase.ImportAsset(path);
    }
    
    public void ReadString()
    {
#if UNITY_EDITOR
        StreamReader reader = new StreamReader("Assets/Resources/" + fileName);
#else
        StreamReader reader = new StreamReader("puppetpandemonium_Data/Resources/Levels/" + fileName);
#endif

        //Read the text from directly from the test.txt file
        //StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

    public List<string> ConvertLevelLayoutToList()
    {
#if UNITY_EDITOR
        string path = "Assets/Resources/" + fileName;
#else
        string path = "Resources/Levels/" + fileName;
#endif
        var platformStateList = new List<string>();

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        //Debug.Log(reader.ReadToEnd());
        char ch;
        int Tchar = 0;
        do
        {
            ch = (char)reader.Read();
            //Debug.Log(ch.ToString());
            platformStateList.Add(ch.ToString());
            Tchar++;
        } while (!reader.EndOfStream);
        reader.Close();
        return platformStateList;
    }


}