using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Interactions;
using UnityEngine;

public class SceneTransitioner : MonoBehaviour
{

    private InteractionEventChannel _evtChannel;

    public static SceneTransitioner Instance;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);

        Instance = this;
    }

    private void Start()
    {
        _evtChannel = InteractionEventChannel.Instance;
    }


    public void loadNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
