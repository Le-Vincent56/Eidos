using Eidos.Eidras;
using UnityEngine;

namespace Eidos.Battles
{
    [CreateAssetMenu(fileName = "Mosaic", menuName = "Eidra/Mosaic")]
    public class Mosaic : ScriptableObject
    {
        public string CatalystName;
        public EidraData[] Eidras = new EidraData[4];
    }
}
