using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoadManager : MonoBehaviour
{
    public static string sceneNameToLoad;
    public static string lastActiveScene;
    public TextMeshProUGUI loadMessage;

    public void restartScene(string sceneName) {
        sceneNameToLoad = sceneName;
        AIWinStatus.restartGame = true;
        loadMessage.text = "Loading " + sceneName + "...";
    }

    public void setSceneToLoad(string name) {
        sceneNameToLoad = name;
    }

    public static void setLastScene() {
        lastActiveScene = SceneManager.GetActiveScene().name;
    }

    public static string getLastActiveScene() {
        return lastActiveScene;
    }
}
