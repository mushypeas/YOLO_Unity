using UnityEngine;
using System;
using UnityEngine.UI;

public class Settings : MonoBehaviour{
    public UnityEngine.Object[] _objectList;
    public static UnityEngine.Object[] objectList;
    public string _savePath = "~";
    public static string savePath;
    public int _imageResolution = 1080;
    public static int imageResolution;
    public int _dataSize = 0;

    public static int dataSize;

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
        imageResolution = _imageResolution;
        dataSize = _dataSize;
    }
}
