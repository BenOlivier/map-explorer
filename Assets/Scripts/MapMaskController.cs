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
        Shader.SetGlobalFloat("_MaskScale", MaskScale);
        Shader.SetGlobalFloat("_MaskLength", MaskLength / MaskScale);
        Shader.SetGlobalFloat("_MaskWidth", MaskWidth / MaskScale);
        Shader.SetGlobalFloat("_MaskRadius", MaskRadius / MaskScale);
        Shader.SetGlobalFloat("_FadeWidth", FadeWidth);
    }

    private void OnDrawGizmos()
    {
        var v1 = transform.position + new Vector3(-MaskWidth / 2 - FadeWidth * MaskScale, 0, -MaskLength / 2 - FadeWidth * MaskScale);
        var v2 = transform.position + new Vector3(-MaskWidth / 2 - FadeWidth * MaskScale, 0, +MaskLength / 2 + FadeWidth * MaskScale);
        var v3 = transform.position + new Vector3(+MaskWidth / 2 + FadeWidth * MaskScale, 0, +MaskLength / 2 + FadeWidth * MaskScale);
        var v4 = transform.position + new Vector3(+MaskWidth / 2 + FadeWidth * MaskScale, 0, -MaskLength / 2 - FadeWidth * MaskScale);

        Gizmos.DrawLine(v1, v2);
        Gizmos.DrawLine(v2, v3);
        Gizmos.DrawLine(v3, v4);
        Gizmos.DrawLine(v4, v1);
    }
}