using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZoomCamera : SimpleSingleton<PlayerZoomCamera> {

    private Cinemachine.CinemachineVirtualCamera vCamera;

    private bool isCameraBusy = false;

    public override void Awake()
    {
        base.Awake();

        vCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        vCamera.enabled = false;
    }

    public IEnumerator ZoomToEvent(Transform t, float seconds)
    {
        if (!isCameraBusy)
        {
            isCameraBusy = true;
            ZoomToTransform(t);
            yield return new WaitForSeconds(seconds);
            ZoomOut();
        }
    }
    public void ZoomToTransform(Transform t)
    {
        isCameraBusy = true;
        Vector3 pos = t.position;
        pos.z = -10f;
        transform.position = pos;
        vCamera.Follow = t;
        vCamera.enabled = true;
    }

    public void ZoomOut()
    {
        isCameraBusy = false;
        vCamera.enabled = false;
    }
}
