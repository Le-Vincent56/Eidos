using Eflatun.SceneReference;
using System;

namespace Eidos.Scenes
{
    [Serializable]
    public class SceneData
    {
        public SceneReference Reference;
        public string Name => Reference.Name;
        public SceneType SceneType;
    }
}
