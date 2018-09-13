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

    static public void Save(int hiScore)
    {
        FileStream file; // the file
        string fileDestination = Application.persistentDataPath + "/save.dat";

        if (!File.Exists(fileDestination))
            file = File.Create(fileDestination);
        else
            file = File.OpenWrite(fileDestination);

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(file, hiScore);
        file.Close();
    }

    static public int Load()
    {
        FileStream file;
        string fileDestination = Application.persistentDataPath + "/save.dat";

        if (File.Exists(fileDestination))
            file = File.OpenRead(fileDestination);
        else
            return 0;

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        int hiScore = (int)binaryFormatter.Deserialize(file);
        file.Close();

        return hiScore;
    }
}
