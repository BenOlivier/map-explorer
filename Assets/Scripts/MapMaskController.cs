using UnityEngine;

public class MapMaskController : MonoBehaviour
{
    public float MaskScale;
    public float MaskLength;
    public float MaskWidth;
    public float MaskRadius;
    public float MaskRotation;

    private void Update()
    {
        SetValues();
    }

    private void OnValidate()
    {
        SetValues();
    }

    public void SetValues()
    {
        Shader.SetGlobalFloat("_MaskScale", MaskScale);
        Shader.SetGlobalFloat("_MaskLength", MaskLength / MaskScale);
        Shader.SetGlobalFloat("_MaskWidth", MaskWidth / MaskScale);
        Shader.SetGlobalFloat("_MaskRadius", MaskRadius / MaskScale);
        Shader.SetGlobalFloat("_MaskRotation", MaskRotation);
        Shader.SetGlobalVector("_MaskPosition", transform.position);
    }

    private void OnDrawGizmos()
    {
        var v1 = transform.position + Quaternion.Euler(0, MaskRotation, 0) * new Vector3(-MaskWidth / 2, 0, -MaskLength / 2);
        var v2 = transform.position + Quaternion.Euler(0, MaskRotation, 0) * new Vector3(-MaskWidth / 2, 0, +MaskLength / 2);
        var v3 = transform.position + Quaternion.Euler(0, MaskRotation, 0) * new Vector3(+MaskWidth / 2, 0, +MaskLength / 2);
        var v4 = transform.position + Quaternion.Euler(0, MaskRotation, 0) * new Vector3(+MaskWidth / 2, 0, -MaskLength / 2);

        Gizmos.DrawLine(v1, v2);
        Gizmos.DrawLine(v2, v3);
        Gizmos.DrawLine(v3, v4);
        Gizmos.DrawLine(v4, v1);
    }
}