using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZoomCamera : SimpleSingleton<PlayerZoomCamera> {

    private Cinemachine.CinemachineVirtualCamera vCamera;

    public override void Awake()
    {
        base.Awake();

        vCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        vCamera.enabled = false;
    }

    public void ZoomToTransform(Transform t)
    {
        Vector3 pos = t.position;
        pos.z = -10f;
        transform.position = pos;
        vCamera.Follow = t;
        vCamera.enabled = true;
    }

    public void ZoomOut()
    {
        vCamera.enabled = false;
    }
}
