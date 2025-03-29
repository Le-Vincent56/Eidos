using UnityEngine;

namespace Eidos.Scenes
{
    [CreateAssetMenu(fileName = "Scene Group Data", menuName = "Data/Scene Group")]
    public class SceneGroupData : ScriptableObject
    {
        [HideInInspector] public int InitialScene;
        public SceneGroup[] SceneGroups;
    }
}
