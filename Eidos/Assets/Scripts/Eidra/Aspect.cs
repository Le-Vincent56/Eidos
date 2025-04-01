using System.Collections.Generic;
using UnityEngine;

namespace Eidos.Eidras
{
    [CreateAssetMenu(fileName = "Aspect", menuName = "Eidra/Aspect")]
    public class Aspect : ScriptableObject
    {
        public string Name;
        public List<Aspect> Resistances;
        public List<Aspect> Vulnerabilities;
    }
}
