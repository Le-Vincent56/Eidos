using NUnit.Framework;
using UnityEngine;

namespace Eidos.Eidra
{
    public class Eidra : MonoBehaviour
    {
        [SerializeField] private EidraData data;
        private Statistics statistics;
        private (Aspect primary, Aspect secondary) aspects;
    }
}
