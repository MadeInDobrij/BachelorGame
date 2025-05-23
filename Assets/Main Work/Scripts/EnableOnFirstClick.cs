using UnityEngine;

public class EnableOnFirstClick : MonoBehaviour
{
    public GameObject objectToEnable;
    private bool hasActivated = false;

    void Update()
    {
        if (hasActivated) return;

        // Detect mouse click (PC) or first touch (Mobile)
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            objectToEnable.SetActive(true);
            hasActivated = true;
        }
    }
}