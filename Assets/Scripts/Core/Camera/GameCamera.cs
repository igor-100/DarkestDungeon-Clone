using Cinemachine;
using UnityEngine;

public class GameCamera : MonoBehaviour, IGameCamera
{
    [SerializeField] private CinemachineVirtualCamera cinemaCamera;

    public void SetTarget(Transform target)
    {
        cinemaCamera.m_Follow = target;
    }
}
