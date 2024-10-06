using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    Vector3 cameraInitialPos;
    public float shakeMag = 0.05f;
    public float shakeTime = 0.5f;
    public Camera mainCamera;

    public void Shake() {
        cameraInitialPos = mainCamera.transform.position;
        InvokeRepeating("StartShake", 0f, 0.05f);
        Invoke("StopShake", shakeTime);
    }

    void StartShake() {
        float cameraShakingOffsetX = Random.value * shakeMag * 2 - shakeMag;
        float cameraShakingOffsetY = Random.value * shakeMag * 2 - shakeMag;
        Vector3 cameraIntermediatePos = mainCamera.transform.position;
        cameraIntermediatePos.x += cameraShakingOffsetX;
        cameraIntermediatePos.y += cameraShakingOffsetY;
        mainCamera.transform.position = cameraIntermediatePos;
    }

    void StopShake() {
        CancelInvoke("StartShake");
        mainCamera.transform.position = cameraInitialPos;
    }
}
