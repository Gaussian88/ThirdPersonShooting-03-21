using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmos : MonoBehaviour
{
    public enum Type { NORMAL=0,IMG}
    public Type type = Type.NORMAL;
    private const string IconName = "Enemy";
    [SerializeField] private Color _color = Color.red;
    [SerializeField] private float _radius = 0.3f;
    void Start()
    {
        
    }
    void OnDrawGizmos() //���̳� ���� �׷��ִ� ����Ƽ ���� �Լ� �ݹ��Լ� 
    {
        if (type == Type.NORMAL)
        {
            Gizmos.color = _color; //�÷� 
            Gizmos.DrawSphere(transform.position, _radius); // ����̳� ����(��ġ, �ݰ�)
        }
        else
        {
            Gizmos.DrawIcon(transform.position + Vector3.up * 0.5f, IconName,false);
        }

    }

}
