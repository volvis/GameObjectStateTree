using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Aijai.Utils.ScenarioTrees
{
    public class ScenarioTreeNode : MonoBehaviour
    {
        public UnityEvent OnEnabled;
        public UnityEvent OnDisabled;

        private void OnEnable() => OnEnabled.Invoke();
        private void OnDisable() => OnDisabled.Invoke();

        public void MoveNext() => GetComponentInParent<ScenarioTree>().MoveNext(this);

        [ContextMenu("Move NodeTree Here")]
        public void MoveHere() => GetComponentInParent<ScenarioTree>().MoveTo(this);
    }
}