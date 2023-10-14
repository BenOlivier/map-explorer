using UnityEngine;

public class MapMaskController : MonoBehaviour
{
    public Texture2D MaskTexture;
    public float MaskScale;
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
        Shader.SetGlobalTexture("_MaskTexture", MaskTexture);
        Shader.SetGlobalFloat("_MaskScale", MaskScale);
        Shader.SetGlobalFloat("_MaskRotation", MaskRotation);
        Shader.SetGlobalVector("_MaskPosition", transform.position);
    }

    private void OnDrawGizmos()
    {
        var v1 = transform.position + Quaternion.Euler(0, MaskRotation, 0) * new Vector3(-MaskScale / 2, 0, -MaskScale / 2);
        var v2 = transform.position + Quaternion.Euler(0, MaskRotation, 0) * new Vector3(-MaskScale / 2, 0, +MaskScale / 2);
        var v3 = transform.position + Quaternion.Euler(0, MaskRotation, 0) * new Vector3(+MaskScale / 2, 0, +MaskScale / 2);
        var v4 = transform.position + Quaternion.Euler(0, MaskRotation, 0) * new Vector3(+MaskScale / 2, 0, -MaskScale / 2);

        Gizmos.DrawLine(v1, v2);
        Gizmos.DrawLine(v2, v3);
        Gizmos.DrawLine(v3, v4);
        Gizmos.DrawLine(v4, v1);
    }
}