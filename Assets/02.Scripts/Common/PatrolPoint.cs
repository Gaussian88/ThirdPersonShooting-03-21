using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    public Color lineColor = Color.black;
    [SerializeField] private List<Transform> patrolPointList;
    void Start()
    {
        var patrolobj = this.gameObject;
        if (patrolobj != null )
            patrolobj.GetComponentsInChildren<Transform>(patrolPointList);
        patrolPointList.RemoveAt(0);
    }
    private void OnDrawGizmos()
    {
       
        for(int i =0; i< patrolPointList.Count;i++)
        {
            Vector3 currentPos = patrolPointList[i].position;
            Vector3 preveousPos = Vector3.zero;
            if(i > 0)
            {
                preveousPos = patrolPointList[i-1].position;
            }
            else if( i==0 &&patrolPointList.Count >= 1)
            {
                preveousPos = patrolPointList[patrolPointList.Count-1].position;
            }
            Gizmos.color = lineColor;
            Gizmos.DrawSphere(currentPos, 1f);
            Gizmos.DrawLine(preveousPos, currentPos);
        }



    }
}
