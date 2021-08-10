using UnityEngine;
using UnityEditor;
public class SpritePreprocessor : AssetPostprocessor {
    void OnPreprocessTexture() {
        TextureImporter textureImporter = (TextureImporter)assetImporter;

        Debug.Log("Hy");
        textureImporter.textureType = TextureImporterType.Sprite;
        textureImporter.maxTextureSize = 512;
        textureImporter.textureCompression = TextureImporterCompression.Compressed;
    }
}
