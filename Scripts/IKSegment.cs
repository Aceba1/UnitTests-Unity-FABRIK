using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class IKSegment : MonoBehaviour
{
    public Transform next;
    public float length;
    Vector3 instDir;

    public enum RotType : byte
    {
        All,
        X,
        Y,
        Z
    }

    public RotType rotType;

    public void SetEndPos(Vector3 pos)
    {
        transform.rotation = Quaternion.FromToRotation(next.position - transform.position, pos - transform.position);
    }

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
