using System.IO;
using UnityEngine;

public class ScreenCapturer : MonoBehaviour {
    private static int resolution = 1080;
    private static string filePath;
    private static Transform backgroundTransform;
    private static SpriteRenderer backgroundRenderer;
    private static Texture2D backgroundTexture;
    private static Camera screenCamera;
    void Start() {
        resolution = Settings.imageResolution;
        filePath = Settings.savePath;
        backgroundRenderer = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        backgroundTransform = GameObject.Find("Background").GetComponent<Transform>();
        screenCamera = GameObject.Find("Capture Camera").GetComponent<Camera>();
    }
    public static void Capture() {
        RenderTexture currentRT;
        Texture2D screenImage;
        float screenRatio;
        byte[] imgData;

        backgroundTexture = backgroundRenderer.sprite.texture;
        screenRatio = (float)backgroundTexture.width / (float)backgroundTexture.height;
        currentRT = RenderTexture.active;
        screenCamera.targetTexture = new RenderTexture(((int)(resolution * screenRatio)), resolution, 0);
        RenderTexture.active = screenCamera.targetTexture;
 
        screenCamera.Render();
 
        screenImage = new Texture2D(screenCamera.targetTexture.width, screenCamera.targetTexture.height);
        screenImage.ReadPixels(new Rect(0, 0, screenCamera.targetTexture.width, screenCamera.targetTexture.height), 0, 0);
        screenImage.Apply();
        RenderTexture.active = currentRT;
 
        imgData = screenImage.EncodeToPNG();
        Destroy(screenImage);
 
        File.WriteAllBytes(filePath + GlobalData.dataCount + ".png", imgData);
    }
}
