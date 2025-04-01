using System.Collections.Generic;
using UnityEngine;

namespace Eidos.Eidras
{
    [CreateAssetMenu(fileName = "EidraData", menuName = "Eidra/Data")]
    public class EidraData : ScriptableObject
    {
        public string Name;

        public Aspect Primary;
        public Aspect Secondary;

        public List<AscensionData> Ascensions;
    }
}
