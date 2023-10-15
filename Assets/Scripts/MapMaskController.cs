using UnityEngine;

public class MapMaskController : MonoBehaviour
{
    public float MaskScale;
    public float MaskLength;
    public float MaskWidth;
    public float MaskRadius;
    public float FadeWidth;

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
        Shader.SetGlobalVector("_MaskPosition", transform.position);
        Shader.SetGlobalFloat("_MaskRotation", transform.rotation.eulerAngles.y);
        Shader.SetGlobalFloat("_MaskScale", MaskScale);
        Shader.SetGlobalFloat("_MaskLength", MaskLength / MaskScale);
        Shader.SetGlobalFloat("_MaskWidth", MaskWidth / MaskScale);
        Shader.SetGlobalFloat("_MaskRadius", MaskRadius / MaskScale);
        Shader.SetGlobalFloat("_FadeWidth", FadeWidth);
    }

    private void OnDrawGizmos()
    {
        Quaternion rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        float fadeWidth = FadeWidth * MaskScale;

        var v1 = transform.position + rotation * new Vector3(-MaskWidth / 2 - fadeWidth, 0, -MaskLength / 2 - fadeWidth);
        var v2 = transform.position + rotation * new Vector3(-MaskWidth / 2 - fadeWidth, 0, +MaskLength / 2 + fadeWidth);
        var v3 = transform.position + rotation * new Vector3(+MaskWidth / 2 + fadeWidth, 0, +MaskLength / 2 + fadeWidth);
        var v4 = transform.position + rotation * new Vector3(+MaskWidth / 2 + fadeWidth, 0, -MaskLength / 2 - fadeWidth);

        Gizmos.DrawLine(v1, v2);
        Gizmos.DrawLine(v2, v3);
        Gizmos.DrawLine(v3, v4);
        Gizmos.DrawLine(v4, v1);
    }
}