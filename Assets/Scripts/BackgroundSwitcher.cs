using UnityEngine;

public class BackgroundSwitcher : MonoBehaviour {
    static GameObject backgroundObject;
    static Transform cameraTransform;
    static SpriteRenderer backgroundRenderer;
    static Texture2D[] backgroundList;
    void Start() {
        backgroundList = Resources.LoadAll<Texture2D>("Backgrounds/");
        GlobalData.backgroundCount = backgroundList.Length;

        backgroundObject = GameObject.Find("Background");
        backgroundRenderer = backgroundObject.GetComponent<SpriteRenderer>();
        cameraTransform = GameObject.Find("Camera").GetComponent<Transform>();
    }

    public static void SwitchBackground(int index) {
        Texture2D backgroundTexture = backgroundList[index];
        float width, height;

        width = (float)backgroundTexture.width / GlobalData.pixelsPerUnit;
        height = (float)backgroundTexture.height / GlobalData.pixelsPerUnit;
        backgroundRenderer.sprite = Sprite.Create(
            backgroundTexture,
            new Rect(0f, 0f, backgroundTexture.width, backgroundTexture.height),
            new Vector2(0.5f,0.5f),
            GlobalData.pixelsPerUnit
        );

        cameraTransform.position = new Vector3(0f,0f, -((float)height * Mathf.Pow(3f, 0.5f) / 2));
    }
}
