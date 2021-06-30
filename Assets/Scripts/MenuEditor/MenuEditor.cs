using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;

public class MenuEditor : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Hai/Play")]
    private static void Play()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Map_0.unity");
        EditorApplication.isPlaying = true;
    }


    [MenuItem("Hai/Loading")]
    private static void KakaoSplash()
    {
        EditorSceneManager.OpenScene("Assets/_Scenes/Map_0.unity");
    }
#endif
}
