using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;  // Kamera hangi objeyi takip edecek
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);  // Kamera ile hedef aras�ndaki mesafe
    [SerializeField] private float smoothSpeed = 0.125f;  // Kameran�n yumu�ak hareket h�z�
    [SerializeField] private float dashSmoothSpeed = 0.05f;  // Dash s�ras�nda kamera h�z�

    private float currentSmoothSpeed;

    private void Start()
    {
        currentSmoothSpeed = smoothSpeed;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, currentSmoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    public void SetDashSpeed()
    {
        currentSmoothSpeed = dashSmoothSpeed;  // Dash s�ras�nda kamera h�z� art�r�l�r
    }

    public void ResetSpeed()
    {
        currentSmoothSpeed = smoothSpeed;  // Dash sona erdi�inde kamera h�z� normale d�ner
    }
}
