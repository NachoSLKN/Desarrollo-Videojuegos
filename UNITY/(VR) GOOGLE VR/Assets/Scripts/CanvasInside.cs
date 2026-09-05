using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CanvasGroup))]
public class CanvasInside : MonoBehaviour
{
	[Range (0, 5)]
	public float fadeDuration = 1f;
	float alpha = 0;
	Camera m_Camera;
	CanvasGroup m_CanvasGroup;

	private void Awake ()
	{
		m_Camera = Camera.main;
		m_CanvasGroup = GetComponent<CanvasGroup>();
	}

	void LateUpdate()
{
    LookToPlayer();
    FadeWhenLook();
}

	private void LookToPlayer ()
	{
		Vector3 lookUpDir = (m_Camera.transform.up + m_Camera.transform.forward) / 2;
		Vector3 forwardDir = new Vector3 (lookUpDir.x, 0, lookUpDir.z);
		forwardDir.Normalize ();

		transform.position = m_Camera.transform.position - 1.5f * Vector3.up + forwardDir;

		Vector3 toCameraVec = m_Camera.transform.position - transform.position;
		float camDist = toCameraVec.magnitude;
		if (camDist > m_Camera.nearClipPlane) transform.rotation = Quaternion.LookRotation (m_Camera.transform.forward, m_Camera.transform.up);
	}

	private void FadeWhenLook()
{
    float lookDown = Vector3.Dot(m_Camera.transform.forward, Vector3.down);

    if (lookDown > 0.5f)
    {
        if (alpha < 1)
            alpha += Time.deltaTime / fadeDuration;
    }
    else
    {
        if (alpha > 0)
            alpha -= Time.deltaTime / fadeDuration;
    }

    alpha = Mathf.Clamp01(alpha);
    m_CanvasGroup.alpha = alpha;
}

public void ExitGame()
{
    StartCoroutine(QuitWithDelay());
}

private IEnumerator QuitWithDelay()
{
    yield return new WaitForSeconds(0.8f);

#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
}

}