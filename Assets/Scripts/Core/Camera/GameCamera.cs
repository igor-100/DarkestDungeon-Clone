using Assets.Scripts.Configurations;
using Cinemachine;
using UnityEngine;

public class GameCamera : MonoBehaviour, IGameCamera
{
    private const float FLOAT_EQUALITY_TRESHOLD = 0.1f;

    [SerializeField] private Camera gameCamera;

    private float targetLensSize;

    private Vector3 targetPosition;

    private bool changingLensSize;
    private bool changingTarget;

    private CameraProperties cameraProps;

    private void Awake()
    {
        cameraProps = CompositionRoot.GetConfiguration().GetCameraProperties();
    }

    public void SetLensSize(float size)
    {
        targetLensSize = size;
        changingLensSize = true;
    }

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
        changingTarget = true;
    }

    private void LateUpdate()
    {
        if (changingLensSize)
        {
            if (Mathf.Abs(gameCamera.orthographicSize - targetLensSize) <= FLOAT_EQUALITY_TRESHOLD)
            {
                changingLensSize = false;
            }
            else
            {
                gameCamera.orthographicSize =
                    Mathf.Lerp(gameCamera.orthographicSize, targetLensSize, cameraProps.LensSizeUpdateSpeed * Time.deltaTime);
            }
        }
        if (changingTarget)
        {
            if (gameCamera.transform.position.Equals(targetPosition))
            {
                changingTarget = false;
            }
            else
            {
                gameCamera.transform.position = 
                    Vector3.Lerp(gameCamera.transform.position, targetPosition, cameraProps.PositionUpdateSpeed * Time.deltaTime);
            }
        }
    }
}
