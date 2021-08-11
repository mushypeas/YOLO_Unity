using UnityEngine;
using System;

public class Settings : MonoBehaviour{

    // variables with loaders
    public UnityEngine.Object[] _objectList;
    public static UnityEngine.Object[] objectList;
    public string _savePath = "~";
    public static string savePath;
    public int _dataImageResolution = 1080;
    public static int dataImageResolution;
    public int _dataSize = 0;
    public static int dataSize;

    // variables without loaders
    // public int _maxBGResolution = 4096;
    public static int maxBGResolution = 4096;
    // public int _maxObjResolution = 512;
    public static int maxObjResolution = 512;

    void Awake() {
        if (_savePath[0] == '~') {
            string __savePath = "/home/" + Environment.UserName;
            for (int i = 1; i < _savePath.Length; i++) {
                __savePath += _savePath[i];
            }
            savePath = __savePath;
        }
        else {
            savePath = _savePath;
        }
        if (savePath[savePath.Length-1] != '/') {
            savePath += '/';
        }

        objectList = _objectList;
        dataImageResolution = _dataImageResolution;
        dataSize = _dataSize;
        // maxBGResolution = _maxBGResolution;
        // maxObjResolution = _maxObjResolution;
    }
}
