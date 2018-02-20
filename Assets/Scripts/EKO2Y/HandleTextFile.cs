using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class HandleTextFile : MonoBehaviour {

    public string fileName;

    void WriteString()
    {
        string path = "Assets/Resources/" + fileName;

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("Test");
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
        TextAsset asset = (TextAsset)Resources.Load(fileName);

        //Print the text from the file
        //Debug.Log(asset.text);
    }
    
    public void ReadString()
    {
        string path = "Assets/Resources/" + fileName;

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

    public List<string> ConvertLevelLayoutToList()
    {
        string path = "Assets/Resources/" + fileName;
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