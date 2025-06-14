using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace EIDOS.Scene_Management
{
    public class SceneGroupManager
    {
        public event Action<string> OnSceneLoaded = delegate { };
        public event Action<string> OnSceneUnloaded = delegate { };
        public event Action<int> OnSceneGroupLoaded = delegate { };

        private readonly AsyncOperationHandleGroup handleGroup = new AsyncOperationHandleGroup(10);

        private SceneGroup activeSceneGroup;
        public int CurrentIndex;

        /// <summary>
        /// Load scenes for a SceneGroup
        /// </summary>
        public async UniTask LoadScenes(int groupIndex, SceneGroup group, IProgress<float> progress, bool reloadDupScenes = false)
        {
            // Set the active scene group
            activeSceneGroup = group;

            // Create a container for the loaded scenes
            List<string> loadedScenes = new List<string>();

            // Wait for scenes to unload
            await UnloadScenes();

            // Get the number of scenes
            int sceneCount = SceneManager.sceneCount;

            // Iterate through each scene
            for(int i = 0; i < sceneCount; i++)
            {
                // Add the scene's name to the loaded scenes List
                loadedScenes.Add(SceneManager.GetSceneAt(i).name);
            }

            // Get the total number of scenes to load
            int totalScenesToLoad = activeSceneGroup.scenes.Count;

            // Create an operation group for the total number of scenes
            AsyncOperationGroup operationGroup = new AsyncOperationGroup(totalScenesToLoad);

            // Iterate through each scene to load
            for(int i = 0; i < totalScenesToLoad; i++)
            {
                // Get the scene within the group
                SceneData sceneData = group.scenes[i];

                // Skip if not supposed to reload scenes and the scene is already loaded
                if (reloadDupScenes == false && loadedScenes.Contains(sceneData.Name)) continue;

                switch (sceneData.reference.State)
                {
                    // Check if the Scene Reference State is Regular (within the build settings)
                    case SceneReferenceState.Regular:
                    {
                        // Store the loading operation
                        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneData.reference.Path, LoadSceneMode.Additive);

                        // Add the operation to the operation group
                        operationGroup.Operations.Add(operation);
                        break;
                    }
                    // Otherwise check if the Scene Reference State is Addressable (within an Addressable group)
                    case SceneReferenceState.Addressable:
                    {
                        // Store the loading operation handle
                        AsyncOperationHandle<SceneInstance> sceneHandle = Addressables.LoadSceneAsync(
                            sceneData.reference.Path, 
                            LoadSceneMode.Additive
                        );

                        // Add the scene handle to the handles group
                        handleGroup.Handles.Add(sceneHandle);
                        break;
                    }
                    
                    case SceneReferenceState.Unsafe:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                // Invoke the OnSceneLoaded event
                OnSceneLoaded.Invoke(sceneData.Name);
            }

            // Wait until all AsyncOperations and AsyncOperationHadles in the groups are done
            while (!operationGroup.IsDone || !handleGroup.IsDone)
            {
                // Report the progress
                progress?.Report((operationGroup.Progress + handleGroup.Progress) / 2);

                // Add some delay
                await UniTask.Delay(100, true);
            }

            // Get the Active Scene
            Scene activeScene = SceneManager.GetSceneByName(activeSceneGroup.FindSceneNameByType(SceneType.ActiveScene));

            // Check if the active scene is valid
            if(activeScene.IsValid())
                // Set the active scene
                SceneManager.SetActiveScene(activeScene);

            // Invoke the OnSceneGroupLoaded event
            OnSceneGroupLoaded.Invoke(groupIndex);

            // Set the current index
            CurrentIndex = groupIndex;
        }

        /// <summary>
        /// Unload scenes for the active Scene Group
        /// </summary>
        private async UniTask UnloadScenes()
        {
            // Create a container for the unloaded scenes
            List<string> unloadedScenes = new List<string>();

            // Get the active scene by name
            string activeScene = SceneManager.GetActiveScene().name;

            // Get the number of scenes
            int sceneCount = SceneManager.sceneCount;

            // Iterate backwards through the scenes
            for (int i = sceneCount - 1; i > 0; i--)
            {
                // Get the scene
                Scene sceneAt = SceneManager.GetSceneAt(i);

                // Skip if already unloaded
                if (!sceneAt.isLoaded) continue;

                // Get the scene name
                string sceneName = sceneAt.name;

                // Skip if the bootstrapper scene
                if (sceneName == "Bootstrapper") continue;

                // Skip if the scene exists as a handle
                if (handleGroup.Handles.Any(h => h.IsValid() && h.Result.Scene.name == sceneName)) continue;

                // Add the scene to be unloaded
                unloadedScenes.Add(sceneName);
            }

            // Create an operation group for unloaded scenes
            AsyncOperationGroup operationGroup = new AsyncOperationGroup(unloadedScenes.Count);

            // Iterate through each unloaded scene
            foreach(string scene in unloadedScenes)
            {
                // Store the unloading as an operation
                AsyncOperation operation = SceneManager.UnloadSceneAsync(scene);
                
                // Skip if the operation is null
                if (operation == null) continue;

                // Add the operation to the operation group
                operationGroup.Operations.Add(operation);

                // Invoke the OnSceneUnloaded event
                OnSceneUnloaded.Invoke(scene);
            }

            // Iterate through each handle within the handles group
            foreach(AsyncOperationHandle<SceneInstance> handle in handleGroup.Handles)
            {
                // Check if the handle is valid
                if (handle.IsValid())
                    // If so, unload it asynchronously
                    await Addressables.UnloadSceneAsync(handle).ToUniTask();
            }

            // Clear the handles group
            handleGroup.Handles.Clear();

            // Wait until all AsyncOperations in the operation group are done
            while(!operationGroup.IsDone)
            {
                // Delay to avoid a tight loop
                await UniTask.Delay(100, true);
            }
        }
    }
}
