using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class IKSegment : MonoBehaviour
{
    public Transform next;
    public float length;

    public enum RotType : byte
    {
        All,
        X,
        Y,
        Z
    }

    public RotType rotType;



    // Start is called before the first frame update
    void Awake()
    {
        if (next == null) next = transform.GetChild(0);
        length = next.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}
