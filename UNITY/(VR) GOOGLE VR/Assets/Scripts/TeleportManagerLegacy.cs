using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportManagerLegacy : MonoBehaviour{
    // Con este Script vamos a gestionar todo lo que tenga que ver
    // con la teletransportación.

    #region singleton

    private static TeleportManagerLegacy instance;

    public static TeleportManagerLegacy Instance
    {
        get
        {
            return instance;
        }
    }

    #endregion

    [Header("Teleport")]
    public Image imgFade;
    [Range(0f, 1f)] public float timeTeleport = 0.5f;
    public Transform player;

    float playerGroundPos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerGroundPos = player.position.y;
        //Fade(true);
    }

    public void Fade(bool isFadeIn)
    {
        StartCoroutine(FadeCoroutine(isFadeIn));
    }

    IEnumerator FadeCoroutine(bool isFadeIn)
    {
        Color c = imgFade.color;

        float startAlpha = c.a;
        float targetAlpha = isFadeIn ? 0f : 1f;
        float time = 0f;

        while (time < timeTeleport)
        {
            time += Time.deltaTime;

            c.a = Mathf.Lerp(startAlpha, targetAlpha, time / timeTeleport);
            imgFade.color = c;

            yield return null;
        }

        c.a = targetAlpha;
        imgFade.color = c;
    }

    public void Teleport(Vector3 _newPos)
    {
        StartCoroutine("MovePosition", _newPos);
    }

    IEnumerator MovePosition(Vector3 newPos)
    {
        Fade(false);

        yield return new WaitForSeconds(timeTeleport);

        player.position = new Vector3(
            newPos.x,
            newPos.y + playerGroundPos,
            newPos.z
        );

        yield return new WaitForSeconds(timeTeleport);

        Fade(true);
    }
}