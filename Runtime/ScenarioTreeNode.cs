using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScenarioTreeNode : MonoBehaviour
{
    public void MoveNext() => GetComponentInParent<ScenarioTree>().MoveNext(this);
    public void MoveHere() => GetComponentInParent<ScenarioTree>().MoveTo(this);
}
