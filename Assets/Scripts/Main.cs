using UnityEngine;

public class Main : MonoBehaviour {
    int backgroundIndex = 0;
    bool isSwitchingBackground;
    void Update() {
        isSwitchingBackground = false;

        // Render new data image
        if (Input.GetKeyDown(KeyCode.R)) {
            RenderData();
        }

        // Save current scene data
        if (Input.GetKeyDown(KeyCode.T)) {
            SaveData();
        }

        // Build dataset for current background
        if (Input.GetKeyDown(KeyCode.S)) {
            BuildDataSet();
        }

        // Build dataset for each background
        else if (Input.GetKeyDown(KeyCode.A)) {
            for (int i = 0; i < GlobalData.backgroundCount; i++) {
                BackgroundSwitcher.SwitchBackground(i);
                BuildDataSet();
            }
        }

        else if (isSwitchingBackground = Input.GetKeyDown(KeyCode.RightArrow)) {
            backgroundIndex = (backgroundIndex+1) % GlobalData.backgroundCount;
        }
        else if (isSwitchingBackground = Input.GetKeyDown(KeyCode.LeftArrow)) {
            backgroundIndex = backgroundIndex == 0 ? GlobalData.backgroundCount - 1 : (backgroundIndex-1) % GlobalData.backgroundCount;
        }
        else if (Input.inputString != "") {
            char input = Input.inputString.ToCharArray()[0];
            if (isSwitchingBackground = input >= '0' && input <= '9' && input - '0' < GlobalData.backgroundCount) {
                backgroundIndex = input - '0';
            }
        }

        if (isSwitchingBackground) {
            ObjectManager.DestroyObjects();
            BackgroundSwitcher.SwitchBackground(backgroundIndex);
        }
    }
    void RenderData() {
        ObjectManager.DestroyObjects();
        ObjectManager.GenerateObjects();
    }
    void SaveData() {
        ObjectDataWriter.Write();
        ScreenCapturer.Capture();
        GlobalData.dataCount++;
    }
    void BuildDataSet() {
        for (int i = 0; i < Settings.dataSize; i++) {
            RenderData();
            SaveData();
        }
    }
}
