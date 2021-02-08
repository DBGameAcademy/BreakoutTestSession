using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

class Tools : MonoBehaviour
{
    [MenuItem("Breakout/Play")]
    public static void RunGame()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/Init.unity");
        EditorApplication.isPlaying = true;
    }

    [MenuItem("Breakout/Validate")]
    public static void RunValidation()
    {
        Validate();
    }

    static void Validate()
    {
        Debug.Log("Starting Validation");

        if (Application.isPlaying)
        {
            Debug.LogError("Validation Failed - Do not validate while game is running!");
            return;
        }

        if (EditorSceneManager.GetActiveScene().name != "Init")
        {
            EditorUtility.DisplayDialog("Wrong Scene", "Validation requires the init scene to be open, please open the init scene before running validation", "Ok");
            return;
        }

        int validationErrors = 0;

        // -- Check pool has no duplicates
        List<PoolObjectItem.ePoolItem> items = new List<PoolObjectItem.ePoolItem>();
        PoolManager poolManager = FindObjectOfType<PoolManager>();
        if (poolManager == null)
        {
            validationErrors++;
            Debug.LogError("Pool Manager not found!");
        }
        else
        {
            for (int i = 0; i < poolManager.itemsToPool.Count; i++)
            {
                if (items.Contains(poolManager.itemsToPool[i].PoolItemKey))
                {
                    validationErrors++;
                    Debug.LogError("Pool Manager has duplicate Item Key entries!");
                }
                else
                {
                    items.Add(PoolManager.Instance.itemsToPool[i].PoolItemKey);
                }
            }
        }

        if (validationErrors > 0)
        {
            Debug.LogError("Validation Failed " + validationErrors + " found!");
        }
        else
        {
            Debug.Log("Validation Complete - No Errors Found");
        }
    }
}