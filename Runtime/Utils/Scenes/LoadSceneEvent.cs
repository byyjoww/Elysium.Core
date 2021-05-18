using Elysium.Utils;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is a used for scene loading events.
/// Takes an array of the scenes we want to load and a bool to specify if we want to show a loading screen.
/// </summary>
[CreateAssetMenu(menuName = "Scriptable Objects/Scriptable Events/Load Scene Channel")]
public class LoadSceneEvent : ScriptableObject
{
	public UnityAction<string[], bool> OnLoadingRequested;

	public void RaiseEvent(SceneReference[] locationsToLoad, bool showLoadingScreen)
	{
		var stringLocations = new string[locationsToLoad.Length];

        for (int i = 0; i < locationsToLoad.Length; i++)
        {
			stringLocations[i] = locationsToLoad[i].ScenePath;
		}

		RaiseEvent(stringLocations, showLoadingScreen);
	}

	public void RaiseEvent(string[] locationsToLoad, bool showLoadingScreen)
	{
		if (OnLoadingRequested != null)
		{
			OnLoadingRequested.Invoke(locationsToLoad, showLoadingScreen);
		}
		else
		{
			Debug.LogWarning("A Scene loading was requested, but nobody picked it up." +
				"Check why there is no SceneLoader already present, " +
				"and make sure it's listening on this Load Event channel.");
		}
	}
}
