using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;  // Kamera hangi objeyi takip edecek
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);  // Kamera ile hedef arasýndaki mesafe
    [SerializeField] private float smoothSpeed = 0.125f;  // Kameranýn yumuþak hareket hýzý
    [SerializeField] private float dashSmoothSpeed = 0.05f;  // Dash sýrasýnda kamera hýzý

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
        currentSmoothSpeed = dashSmoothSpeed;  // Dash sýrasýnda kamera hýzý artýrýlýr
    }

    public void ResetSpeed()
    {
        currentSmoothSpeed = smoothSpeed;  // Dash sona erdiðinde kamera hýzý normale döner
    }
}
