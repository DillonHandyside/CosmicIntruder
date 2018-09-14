using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class FileIO : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    /// <summary>
    /// Save function which takes in an integer hiScore parameter and writes 
    /// it to a binary save file
    /// </summary>
    /// <param name="hiScore">the new hiScore to save</param>
    static public void Save(int hiScore)
    {
        FileStream file;
        string fileDestination = Application.persistentDataPath + "/save.dat";

        // safety check if file exists
        if (!File.Exists(fileDestination))
            file = File.Create(fileDestination); // file doesn't exist, create it
        else
            file = File.OpenWrite(fileDestination); // file exists, open it for writing

        // write hiScore to file in binary format
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(file, hiScore);

        // done with file
        file.Close();
    }

    /// <summary>
    /// Load function which opens a binary save file and reads it. Returns the
    /// integer value of the hiScore stored inside said file
    /// </summary>
    /// <returns>the saved hiScore to load</returns>
    static public int Load()
    {
        FileStream file;
        string fileDestination = Application.persistentDataPath + "/save.dat";

        // safety check if file exists
        if (File.Exists(fileDestination))
            file = File.OpenRead(fileDestination); // file exists, open it for reading
        else
            return 0; // no file exists, therefore the current hiScore is zero

        // reformat and read the binary file
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        int hiScore = (int)binaryFormatter.Deserialize(file);

        // done with file
        file.Close();
        return hiScore;
    }
}
