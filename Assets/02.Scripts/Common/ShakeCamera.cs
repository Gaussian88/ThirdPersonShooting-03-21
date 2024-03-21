using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public Transform camPivotTr;
    public bool isShake = false;
    private Vector3 originPos;
    private Quaternion originRot;
    private float shakeTime = 0f;
    private float duration = 0.4f;
    void Start()
    {
        camPivotTr = transform.parent.GetComponent<Transform>();
        
    }
    void Update()
    {
        if(isShake)
        {
            Vector3 shakePos = Random.insideUnitSphere; //원 안에서 불규칙한 값은 산출 한다.
            camPivotTr.position = shakePos * 0.1f;
            //펄린노이스 함수는 잔디나 파도가 칠때 불규칙한 산출하는함수
            Vector3 shakeRot = new Vector3(0f, 0f, Mathf.PerlinNoise(Time.time * 0.1f, 0.0f));
            //
            //camTr.rotation = Quaternion.Euler(shakeRot);
            //Vector3 ShakePos = new Vector3(Random.Range(-0.001f, 0.001f), Random.Range(-0.001f, 0.001f), 0f);
            // Vector3 shakeRot = new Vector3(Random.Range(-0.000001f, 0.0000001f), Random.Range(-0.000001f, 0.000001f), 0f);
            //camTr.position = ShakePos;
            //camTr.rotation = Quaternion.Euler(shakeRot);
            //float x = Random.Range(-0.1f, 0.1f);
            //float y = Random.Range(-0.1f, 0.1f);
            //camPivotTr.position += new Vector3(x, y, 0f);
            Quaternion rot = Quaternion.LookRotation(shakeRot);
            camPivotTr.rotation = rot;
            if (Time.time - shakeTime > duration)
            {
                isShake = false;
                camPivotTr.position = originPos;
                camPivotTr.rotation = originRot; 

            }
        }
        


    }
    public void TurnOn()
    {
        if (isShake == false) isShake = true;
        originPos = camPivotTr.position;
        originRot = camPivotTr.rotation;
        shakeTime = Time.time;
    }
}
