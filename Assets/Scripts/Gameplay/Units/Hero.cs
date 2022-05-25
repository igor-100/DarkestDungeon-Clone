using UnityEngine;

public class Hero : Unit, IHero
{
    private const string FILLPHASE_SHADER_PARAM = "_FillPhase";
    private const float FILLPHASE_SHADER_VALUE = 0.2f;

    private MeshRenderer renderer;
    private MaterialPropertyBlock propBlock;


    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        propBlock = new MaterialPropertyBlock();
    }

    public void Choose()
    {
        Material[] materials = renderer.materials;

        for (int i = 0; i < materials.Length; i++)
        {
            SetShaderFloatProperty(i, FILLPHASE_SHADER_PARAM, FILLPHASE_SHADER_VALUE);
        }
    }

    public void UnChoose()
    {
        Material[] materials = renderer.materials;

        for (int i = 0; i < materials.Length; i++)
        {
            SetShaderFloatProperty(i, FILLPHASE_SHADER_PARAM, 0);
        }
    }

    private void SetShaderFloatProperty(int materialId, string shaderParam, float paramValue)
    {
        renderer.GetPropertyBlock(propBlock, materialId);
        propBlock.SetFloat(shaderParam, paramValue);
        renderer.SetPropertyBlock(propBlock);
    }
}
