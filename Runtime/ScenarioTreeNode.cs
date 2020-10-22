using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Aijai
{
    public class ScenarioTreeNode : MonoBehaviour
    {
        public void MoveNext() => GetComponentInParent<ScenarioTree>().MoveNext(this);

        [ContextMenu("Move NodeTree Here")]
        public void MoveHere() => GetComponentInParent<ScenarioTree>().MoveTo(this);
    }
}