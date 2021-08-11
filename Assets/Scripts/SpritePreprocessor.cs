using UnityEditor;
public class SpritePreprocessor : AssetPostprocessor {
    void OnPreprocessTexture() {
        TextureImporter textureImporter = (TextureImporter)assetImporter;

        textureImporter.textureType = TextureImporterType.Sprite;

        // apply resolution for backgrounds
        if (textureImporter.assetPath.Contains("Backgrounds")) {
            textureImporter.maxTextureSize = Settings.maxBGResolution;
        }
        // apply resolution for objects
        else {
            textureImporter.maxTextureSize = Settings.maxObjResolution;
        }
    }
}
