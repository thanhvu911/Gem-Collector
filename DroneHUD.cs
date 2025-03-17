using UnityEngine;
using TMPro;

public class DroneHUD : MonoBehaviour
{
    [SerializeField] private DroneController drone;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text altitudeText;
    
    void Update()
    {
        speedText.text = $"Speed: {drone.GetComponent<Rigidbody>().velocity.magnitude:F1} m/s";
        altitudeText.text = $"Altitude: {drone.transform.position.y:F1} m";
    }
}
