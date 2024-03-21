using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Transform camPivotTr;
    public Transform camTr;
    public float height = 5f;
    public float distance = 10f;
    public float rotDamping = 10f;
    public float moveDamping = 15f;
    public  float targetOffset = 1.0f;
    public float heightmax = 15f;
    private float origin;
    [SerializeField] private Transform playerTr;
    void Start()
    {
        origin = height;
        playerTr = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
        if(target == null&& camPivotTr==null) return;
        //RaycastHit hit;
        //if (Physics.Raycast(camPivotTr.position,  camTr.position- camPivotTr.position, out hit,
        // 20f, ~(1 << LayerMask.NameToLayer("Player"))))
        //{
        //    camTr.localPosition = Vector3.back * hit.distance;
        //}
        //else
        //    camTr.localPosition = Vector3.back * distance;

        RaycastHit hit;
        Vector3 castDir = (playerTr.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, castDir, out hit, 20f))
        {
            if(!hit.collider.CompareTag("Player"))
            height = Mathf.Lerp(height, heightmax, Time.deltaTime * 1.0f);
            else
              height = Mathf.Lerp(height, origin, Time.deltaTime * 1.0f);
            
        }
       
      

    }
    void LateUpdate()
    {
        var camPos = target.position - (target.forward * distance) + (target.up * height);
        camPivotTr.position = Vector3.Slerp(camPivotTr.position, camPos, Time.deltaTime * moveDamping);
        camPivotTr.rotation = Quaternion.Slerp(camPivotTr.rotation, target.rotation, Time.deltaTime * rotDamping);
        camPivotTr.LookAt(target.transform.position +(target.up * targetOffset));
    }
    private void OnDrawGizmos() //씬화면에 라인(선)이나 색깔을 넣어주는 함수 콜백 함수 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(target.position + (target.up * targetOffset), 0.1f);

        Gizmos.DrawLine(target.position + (target.up * targetOffset), camPivotTr.position);
    }
    void OnValidate()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        camPivotTr = GetComponent<Transform>();
        camTr = transform.GetChild(0).transform;

    }
}
