using Elysium.UI.ProgressBar;
using Elysium.Utils;
using Elysium.Utils.Timers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class manages the scenes loading and unloading
/// </summary>
public class LocationLoader : MonoBehaviour
{
    [Header("Initialization Scene")]
    [SerializeField] private SceneReference _initializationScene = default;
    [Header("Load on start")]
    [SerializeField] private SceneReference[] _mainMenuScenes = default;
    [Header("Loading Screen")]
    [SerializeField] private GameObject _loadingInterface = default;
    [SerializeField] private UI_ProgressBar _loadingProgressBar = default;

    [Header("Load Event")]
    //The load event we are listening to
    [SerializeField] private LoadSceneEvent _loadEventChannel = default;

    private event Action OnLoadProgressChanged;

    //List of the scenes to load and track progress
    private List<AsyncOperation> _scenesToLoadAsyncOperations = new List<AsyncOperation>();
    //List of scenes to unload
    private List<Scene> _ScenesToUnload = new List<Scene>();
    //Keep track of the scene we want to set as active (for lighting/skybox)
    private string _activeScene;

    private void OnEnable()
    {
        _loadEventChannel.OnLoadingRequested += LoadScenes;
    }

    private void OnDisable()
    {
        _loadEventChannel.OnLoadingRequested -= LoadScenes;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().path == _initializationScene.ScenePath)
        {            
            LoadMainMenu();
        }
    }

    private void LoadMainMenu()
    {
        var menuScenePaths = new string[_mainMenuScenes.Length];

        for (int i = 0; i < _mainMenuScenes.Length; i++)
        {
            menuScenePaths[i] = _mainMenuScenes[i].ScenePath;
        }

        Debug.LogError(new List<string>(menuScenePaths));
        LoadScenes(menuScenePaths, true);
    }

    /// <summary> This function loads the scenes passed as array parameter </summary>
    private void LoadScenes(string[] locationsToLoad, bool showLoadingScreen)
    {
        //Add all current open scenes to unload list
        AddScenesToUnload();

        _activeScene = locationsToLoad[0];

        for (int i = 0; i < locationsToLoad.Length; ++i)
        {
            String currentSceneName = locationsToLoad[i];
            if (!CheckLoadState(currentSceneName))
            {
                //Add the scene to the list of scenes to load asynchronously in the background
                _scenesToLoadAsyncOperations.Add(SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive));
            }
        }
        _scenesToLoadAsyncOperations[0].completed += SetActiveScene;
        if (showLoadingScreen)
        {
            //Show the progress bar and track progress if loadScreen is true
            _loadingInterface.SetActive(true);
            StartCoroutine(TrackLoadingProgress());
        }
        else
        {
            //Clear the scenes to load
            _scenesToLoadAsyncOperations.Clear();
        }

        //Unload the scenes
        UnloadScenes();
    }

    private void SetActiveScene(AsyncOperation asyncOp)
    {
        var scene = SceneManager.GetSceneByPath(_activeScene);
        SceneManager.SetActiveScene(scene);
    }

    private void AddScenesToUnload()
    {
        for (int i = 0; i < SceneManager.sceneCount; ++i)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.path != _initializationScene.ScenePath)
            {
                Debug.Log("Added scene to unload = " + scene.name);
                //Add the scene to the list of the scenes to unload
                _ScenesToUnload.Add(scene);
            }
        }
    }

    private void UnloadScenes()
    {
        if (_ScenesToUnload != null)
        {
            for (int i = 0; i < _ScenesToUnload.Count; ++i)
            {
                //Unload the scene asynchronously in the background
                SceneManager.UnloadSceneAsync(_ScenesToUnload[i]);
            }
        }
        _ScenesToUnload.Clear();
    }

    /// <summary> This function checks if a scene is already loaded </summary>
    private bool CheckLoadState(String sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; ++i)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary> This function updates the loading progress once per frame until loading is complete </summary>
    private IEnumerator TrackLoadingProgress(float _minLoadingTime = 0f)
    {
        float totalProgress = 0;

        //The fillAmount for all scenes
        _loadingProgressBar.BindCustomValue(() => totalProgress, () => _scenesToLoadAsyncOperations.Count, ref OnLoadProgressChanged);
        
        bool minLoadingTimeComplete = false;
        Timer.CreateTimer(_minLoadingTime + 0.01f, () => !this, true).OnTimerEnd += () => minLoadingTimeComplete = true;

        //When the scene reaches 0.9f, it means that it is loaded
        //The remaining 0.1f are for the integration
        while (totalProgress <= 0.9f || !minLoadingTimeComplete)
        {
            totalProgress = 0;

            //Iterate through all the scenes to load
            for (int i = 0; i < _scenesToLoadAsyncOperations.Count; ++i)
            {
                // Debug.Log("Scene" + i + " :" + _scenesToLoadAsyncOperations[i].isDone + " | progress = " + _scenesToLoadAsyncOperations[i].progress);
                //Adding the scene progress to the total progress
                totalProgress += _scenesToLoadAsyncOperations[i].progress;
            }

            OnLoadProgressChanged?.Invoke();
            yield return null;
        }

        //Clear the scenes to load
        _scenesToLoadAsyncOperations.Clear();

        //Hide progress bar when loading is done
        _loadingInterface.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit!");
    }

}
