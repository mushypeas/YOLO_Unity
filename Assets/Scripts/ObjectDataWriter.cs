using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDataWriter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        
    }

    static public void Write() {
        List<string> dataString = new List<string>();
        foreach (ObjectData objectData in GlobalData.objectDict.Values) {
            dataString.Add(objectData.ToYOLO());
        }
        File.WriteAllLines(Settings.savePath + GlobalData.dataCount + ".txt", dataString);
    }
}
