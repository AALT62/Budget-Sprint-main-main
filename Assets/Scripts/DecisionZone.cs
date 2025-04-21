using UnityEngine;

public class DecisionZone : MonoBehaviour
{
    public GameObject decisionPanel; // Drag your UI panel here

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<LaneMover>().isPaused = true;
            decisionPanel.SetActive(true);
        }
    }
}
