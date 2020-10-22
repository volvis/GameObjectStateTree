using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Aijai
{
    public class ScenarioTree : MonoBehaviour
    {
        List<ScenarioTreeNode> Nodes;
        public ScenarioTreeNode Current;

        [SerializeField]
        bool UseLogging;

        private void Awake()
        {
            Nodes = GetComponentsInChildren<ScenarioTreeNode>(true).ToList();
            foreach (var n in Nodes)
            {
                foreach (var tree in FindRoute(n))
                {
                    tree.gameObject.SetActive(false);
                }
            }

            if (Current == null && Nodes.Count > 0)
            {
                Current = Nodes[0];
            }

            MoveTo(Current);
        }

        public void MoveNext(ScenarioTreeNode reference)
        {
            int indexOf = Nodes.IndexOf(reference) + 1;
            if (indexOf < Nodes.Count)
            {
                MoveTo(Nodes[indexOf]);
            }
        }

        public void MoveTo(ScenarioTreeNode node)
        {
            if (node != null && Nodes.Contains(node))
            {
                var CurrentRoute = FindRoute(Current);
                var TargetRoute = FindRoute(node);
                TargetRoute.Reverse();

                for (var i = 0; i < CurrentRoute.Count; i++)
                {
                    if (TargetRoute.Contains(CurrentRoute[i]) == false)
                    {
                        if (UseLogging)
                            Debug.Log($"Disabling Node {CurrentRoute[i].gameObject.name}", CurrentRoute[i].gameObject);

                        CurrentRoute[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        break;
                    }
                }

                for (var i = 0; i < TargetRoute.Count; i++)
                {
                    if (UseLogging)
                        Debug.Log($"Enabling Node {TargetRoute[i].gameObject.name}", TargetRoute[i].gameObject);

                    TargetRoute[i].gameObject.SetActive(true);
                }

                Current = node;
            }
        }



        List<Transform> FindRoute(ScenarioTreeNode From)
        {
            List<Transform> Route = new List<Transform>();
            if (From != null)
            {
                Route.Add(From.transform);
                Transform Next = From.transform.parent;
                while (Next != null & Next.TryGetComponent(out ScenarioTree _) == false)
                {
                    Route.Add(Next.transform);
                    Next = Next.transform.parent;
                }
            }
            return Route;
        }

        bool MoveUp(ScenarioTreeNode Node, out ScenarioTreeNode Parent)
        {
            Parent = null;
            var parentTransform = Node.transform.parent;

            if (parentTransform == null)
                return false;

            return parentTransform.TryGetComponent(out Parent);
        }
    }
}