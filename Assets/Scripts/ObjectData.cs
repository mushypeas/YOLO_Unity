using System.Collections.Generic;
using UnityEngine;

public class ObjectData {
    public class Pose {
        public float[][] corners;
        public float[] center;
        public Pose(float[][] _corners, float[] _center) {
            corners = _corners;
            center = _center;
        }
    };
    public string name;
    public int objectClass;
    public float width, height;
    public Pose pose;
    public ObjectData(GameObject gameObject) {
        name = gameObject.name;
        try {
            objectClass = GlobalData.objectClass[name];
        } catch (KeyNotFoundException) {
            GlobalData.objectClass.Add(name, GlobalData.objectClassCount++);
            objectClass = GlobalData.objectClass[name];
        }
        pose = GetObjectPose(gameObject);
        width = Mathf.Abs(pose.corners[0][0] - pose.corners[3][0]);
        height = Mathf.Abs(pose.corners[0][1] - pose.corners[3][1]);
    }
    public static void Add(GameObject gameObject) {
        ObjectData objectData = new ObjectData(gameObject);
        GlobalData.objectDict.Add(gameObject.GetInstanceID(), objectData);
    }
    public static void Remove(GameObject gameObject) {
        GlobalData.objectDict.Remove(gameObject.GetInstanceID());
    }
    public static Pose GetObjectPose(GameObject gameObject) {
        Bounds objectBound;
        SpriteRenderer renderer = gameObject.GetComponentInChildren<SpriteRenderer>();

        if (renderer != null)   objectBound = renderer.bounds;
        else                    objectBound = gameObject.GetComponentInChildren<MeshRenderer>().bounds;

        float[] objectCenter = {objectBound.center.x, objectBound.center.y};
        float[][] objectCorners = new float[4][] {
            new float[2] {objectBound.min.x, objectBound.min.y},
            new float[2] {objectBound.max.x, objectBound.min.y},
            new float[2] {objectBound.min.x, objectBound.max.y},
            new float[2] {objectBound.max.x, objectBound.max.y}
        };
        return new Pose(objectCorners, objectCenter);
    }
    public string ToYOLO() {
        Texture2D backgroundTexture = GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite.texture;
        float backgroundWidth = (float)backgroundTexture.width / GlobalData.pixelsPerUnit;
        float backgroundHeight = (float)backgroundTexture.height / GlobalData.pixelsPerUnit;
        float center_x = pose.center[0] / backgroundWidth + 0.5f;
        float center_y = pose.center[1] / backgroundHeight + 0.5f;
        return objectClass + " " + center_x + " " + center_y + " " + width / backgroundWidth + " " + height / backgroundHeight;
    }
    public string ToJSON() {
        return    "{\n"
                + "  \"name\": \""+ name + "\",\n" 
                + "  \"objectID\": " + objectClass + ",\n"
                + "  \"width\": " + width + ",\n"
                + "  \"height\": " + height + ",\n"
                + "  \"pose\": {\n"
                + "    \"corners\": [\n"
                + "      [" + pose.corners[0][0] + "," + pose.corners[0][1] + "],\n"
                + "      [" + pose.corners[1][0] + "," + pose.corners[1][1] + "],\n"
                + "      [" + pose.corners[2][0] + "," + pose.corners[2][1] + "],\n"
                + "      [" + pose.corners[3][0] + "," + pose.corners[3][1] + "],\n"
                + "    ],\n"
                + "    \"center\": [" + pose.center[0] + "," + pose.center[1] + "]\n"
                + "  }\n"
                + "}";
    }
}