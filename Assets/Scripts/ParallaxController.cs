using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;  // Ana kamera referansı
    [SerializeField] private Vector2 parallaxEffectMultiplier;  // Parallax hızı için çarpan

    private Vector3 lastCameraPosition;

    private void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y, 0);
        lastCameraPosition = cameraTransform.position;
    }
}