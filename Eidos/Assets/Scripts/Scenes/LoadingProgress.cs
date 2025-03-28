using System;
using UnityEngine;

namespace Eidos.Scenes
{
    public class LoadingProgress : IProgress<float>
    {
        public event Action<float> Progressed;

        const float ratio = 1f;

        /// <summary>
        /// Report the loading progress
        /// </summary>
        public void Report(float value) => Progressed?.Invoke(value / ratio);
    }
}
