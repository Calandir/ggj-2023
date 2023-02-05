using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Cinemachine.CinemachineTargetGroup;

public class TrackRootEnds : MonoBehaviour
{
    [SerializeField]
    private CinemachineTargetGroup m_targetGroup;

    private void OnEnable()
    {
        RootsController.RootCreatedAction += RootSpawned;
        RootsController.RootFinishedAction += RootFinished;
    }
    private void OnDisable()
    {
        RootsController.RootCreatedAction -= RootSpawned;
        RootsController.RootFinishedAction -= RootFinished;
    }

    private void RootSpawned(Root root)
    {
        List<Target> newTargets = new List<Target>(m_targetGroup.m_Targets);
        Target target = new Target();
        target.target = root.transform;
        target.weight = 1;
        target.radius = 1;
        newTargets.Add(target);
        m_targetGroup.m_Targets = newTargets.ToArray();
    }

    private void RootFinished(Root root)
    {
        List<Target> newTargets = new List<Target>();
        foreach (var target in m_targetGroup.m_Targets)
        {
            if (target.target != root.transform)
            {
                newTargets.Add(target);
            }
        }
        m_targetGroup.m_Targets = newTargets.ToArray();
    }
}
