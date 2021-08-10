using UnityEngine;
using UnityEditor;
using System.IO;
public class ObjectManager : MonoBehaviour
{
    static SpriteRenderer backgroundRenderer;
    static Texture2D backgroundTexture;
    static Transform parentTransform;
    static Object[] objectList;
    void Start() {
        objectList = Settings.objectList;
        backgroundRenderer = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        parentTransform = GameObject.Find("Objects").transform;
    }
    public static void DestroyObjects() {
        int childCount = parentTransform.childCount;
        for (int i = 0; i < childCount; i++) {
            GameObject child = parentTransform.GetChild(0).gameObject;
            ObjectData.Remove(child);
            DestroyImmediate(child);
        }
    }
    public static void GenerateObjects() {
        backgroundTexture = backgroundRenderer.sprite.texture;
        foreach(Object _object in objectList) {
            if (_object is GameObject) {
                GenerateObject(_object as GameObject);
            }
            else if (_object is Texture2D) {
                Generate2DObject(_object as Texture2D);
            }
            else if (_object is DefaultAsset) {
                Generate2DObject(_object.name);
            }
        }
    }
    private static void GenerateObject(GameObject _gameObject) {
        GameObject gameObject = GameObject.Instantiate(_gameObject);
        ObjectData.Pose objectPose = ObjectData.GetObjectPose(gameObject);
        Transform objectTransform = gameObject.GetComponent<Transform>();
        gameObject.transform.SetParent(parentTransform);
        gameObject.name = _gameObject.name;
        
        float objectWidth, objectHeight, xLimit, yLimit;
        objectWidth = Mathf.Abs(objectPose.corners[0][0] - objectPose.corners[3][0]);
        objectHeight = Mathf.Abs(objectPose.corners[0][1] - objectPose.corners[3][1]);

        xLimit = backgroundTexture.width / GlobalData.pixelsPerUnit / 2 - objectWidth;
        yLimit = backgroundTexture.height / GlobalData.pixelsPerUnit / 2 - objectHeight;

        objectTransform.position = new Vector3(Random.Range(-xLimit, xLimit),Random.Range(-yLimit, yLimit), -1f);
        objectTransform.rotation = Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));

        ObjectData.Add(gameObject);
    }
    private static void Generate2DObject(string objectName) {
        Texture2D[] textureList = Resources.LoadAll<Texture2D>("Objects2D/" + objectName);
        if (textureList.Length == 0) {
            Debug.LogError("Invalid object \"" + objectName + "\". Check if there are valid images in the directory, or if the directory name/path is incorrect.");
            return;
        }
        Texture2D objectTexture = textureList[Random.Range(0, textureList.Length)];
        Generate2DObject(objectName, objectTexture);
    }
    private static void Generate2DObject(Texture2D objectTexture) {
        string objectName = Directory.GetParent(AssetDatabase.GetAssetPath(objectTexture)).Name;
        Generate2DObject(objectName, objectTexture);
    }
    private static void Generate2DObject(string objectName, Texture2D objectTexture) {
        GameObject gameObject = new GameObject(objectName);
        SpriteRenderer objectRenderer = gameObject.AddComponent<SpriteRenderer>();
        Transform objectTransform = gameObject.GetComponent<Transform>();
        gameObject.transform.SetParent(parentTransform);

        objectRenderer.flipX = Random.Range(0,2) == 0;
        objectRenderer.flipY = Random.Range(0,2) == 0;
        objectRenderer.sprite = Sprite.Create(
            objectTexture,
            new Rect(0.0f, 0.0f, objectTexture.width, objectTexture.height),
            new Vector2(0.5f, 0.5f),
            GlobalData.pixelsPerUnit
        );

        float xLimit, yLimit;
        xLimit = (backgroundTexture.width - objectRenderer.sprite.rect.width) / GlobalData.pixelsPerUnit / 2;
        yLimit = (backgroundTexture.height - objectRenderer.sprite.rect.height) / GlobalData.pixelsPerUnit / 2;

        objectTransform.position = new Vector3(Random.Range(-xLimit, xLimit), Random.Range(-yLimit, yLimit), 0f);

        ObjectData.Add(gameObject);
    }
}
