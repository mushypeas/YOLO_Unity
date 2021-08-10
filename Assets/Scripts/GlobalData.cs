using System.Collections.Generic;

public static class GlobalData {
    public static Dictionary<string, int> objectClass = new Dictionary<string, int>();
    public static Dictionary<int, ObjectData> objectDict = new Dictionary<int, ObjectData>();
    public static int pixelsPerUnit = 100;
    public static int dataCount = 0; // Total number of generated data
    public static int objectCount = 0; // Total number of generated objects
    public static int objectClassCount = 0; // Total number of generated YOLO object classes
    public static int backgroundCount = 0; // Total number of background images in Resources/Backgrounds
}
