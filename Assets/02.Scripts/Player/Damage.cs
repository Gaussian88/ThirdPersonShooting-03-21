using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private GameObject BloodEffect;
    private readonly string e_bulletTag = "E_BULLET";
    private readonly string p_bulletTag = "BULLET";
    void Start()
    {
        BloodEffect = Resources.Load<GameObject>("Effects/GoopSpray");
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.CompareTag(e_bulletTag))
        {
            col.gameObject.SetActive(false);

            Vector3 hitpos = col.contacts[0].point; //���� ����
            Vector3 _normal = col.contacts[0].normal;
            Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _normal);
            GameObject blood = Instantiate<GameObject>(BloodEffect, hitpos, rot);
            Destroy(blood, 0.5f);
        }
        else if (col.collider.CompareTag(p_bulletTag))
        {
            col.gameObject.SetActive(false);
        }
    }

}
