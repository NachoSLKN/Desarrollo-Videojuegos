using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [Header("Timeline")]
    public int actualStep = 0;
    public int[] steps;
    public TimelineAsset[] timelineAssets;

    [Header("Complete")]
    public PlayableDirector playableDirector;
    public CinemachineDollyCart cart;
    public CinemachineSmoothPath path;

    private void Start()
    {
        // Note: PLAY
        Play();
    }

    public void Play()
    {
        // Note: CORRUTINA
        StartCoroutine(PlayTimeline());
    }

    IEnumerator PlayTimeline()
    {
        Debug.Log("Start");

        while (actualStep < steps.Length)
        {
            if (cart.m_Position > steps[actualStep])
            {
                Debug.Log("Step: " + actualStep.ToString());
                playableDirector.playableAsset = timelineAssets[actualStep];
                playableDirector.Play();
                actualStep++;
            }

            yield return null;
        }

        Debug.Log("WAITING");

        while (cart.m_Position < path.PathLength)
        {
            yield return null;
        }

        Debug.Log("END");
    }
}
