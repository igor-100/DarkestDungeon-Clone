using Assets.Scripts.Configurations;
using Spine.Unity;
using System;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour, IUnit
{
    private const string BASE_POSITION_LAYER = "BasePosition";
    private const string FRONT_POSITION_LAYER = "FrontPosition";

    private new MeshRenderer renderer;
    private MaterialPropertyBlock propBlock;

    private SkeletonAnimation skeletonAnimation;
    private Vector2 target;

    private MinerProperties minerProperties;

    public int Id { get; set; }
    public ECharacters CharacterType { get; set; }
    public Vector2 InitialPosition { get; set; }
    public bool IsClickable { get; set; }
    public bool IsMovingToFightScene { get; private set; }
    public bool IsMovingToBasePlace { get; private set; }

    public event Action<IUnit> Clicked = (unit) => { };
    public event Action FinishedMoving = () => { };
    public event Action Attacked = () => { };
    public event Action ReceivedDamage = () => { };

    private void Awake()
    {
        minerProperties = CompositionRoot.GetConfiguration().GetMinerProperties();

        skeletonAnimation = GetComponent<SkeletonAnimation>();
        renderer = GetComponent<MeshRenderer>();
        propBlock = new MaterialPropertyBlock();
    }

    private void Update()
    {
        if (IsMovingToFightScene)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, minerProperties.FightLocalScale,
                minerProperties.ScaleChangingSpeed * Time.deltaTime);
            transform.position = Vector2.MoveTowards(transform.position, target, minerProperties.MoveSpeed * Time.deltaTime);
            if (transform.position.Equals(target))
            {
                IsMovingToFightScene = false;
                FinishedMoving();
            }
        }
        else if (IsMovingToBasePlace)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, minerProperties.BaseLocalScale,
                minerProperties.ScaleChangingSpeed * Time.deltaTime);
            transform.position = Vector2.MoveTowards(transform.position, target, minerProperties.MoveSpeed * Time.deltaTime);
            if (transform.position.Equals(target))
            {
                GetToTheBaseLayer();
                IsMovingToBasePlace = false;
                FinishedMoving();
            }
        }
    }

    private void GetToTheBaseLayer()
    {
        renderer.sortingLayerName = BASE_POSITION_LAYER;
    }

    private void OnMouseDown()
    {
        if (IsClickable)
        {
            Clicked(this);
        }
    }

    public void Highlight(float highlightValue)
    {
        Material[] materials = renderer.materials;

        for (int i = 0; i < materials.Length; i++)
        {
            SetShaderFloatProperty(i, minerProperties.FillPhaseShaderParam, highlightValue);
        }
    }

    public void StopHighlighting()
    {
        Material[] materials = renderer.materials;

        for (int i = 0; i < materials.Length; i++)
        {
            SetShaderFloatProperty(i, minerProperties.FillPhaseShaderParam, 0);
        }
    }

    private void SetShaderFloatProperty(int materialId, string shaderParam, float paramValue)
    {
        renderer.GetPropertyBlock(propBlock, materialId);
        propBlock.SetFloat(shaderParam, paramValue);
        renderer.SetPropertyBlock(propBlock);
    }

    public void MoveToFightScene(Vector2 target)
    {
        this.target = target;
        IsMovingToFightScene = true;
        GetInFrontToFight();
    }

    private void GetInFrontToFight()
    {
        renderer.sortingLayerName = FRONT_POSITION_LAYER;
    }

    public void MoveToBasePlace(Vector2 target)
    {
        this.target = target;
        IsMovingToBasePlace = true;
    }

    public void Hit()
    {
        skeletonAnimation.loop = false;
        skeletonAnimation.AnimationName = minerProperties.DamageAnim;
        StartCoroutine(CompleteAnimation());
    }

    private IEnumerator CompleteAnimation()
    {
        yield return new WaitForSeconds(skeletonAnimation.state.GetCurrent(0).Animation.duration);
        ReceivedDamage();
        Wait();
    }

    public void Attack()
    {
        skeletonAnimation.loop = false;
        skeletonAnimation.AnimationName = minerProperties.AttackAnim;
        StartCoroutine(ReadyToHit());
    }

    private IEnumerator ReadyToHit()
    {
        yield return new WaitForSeconds(skeletonAnimation.state.GetCurrent(0).Animation.duration / 2f);
        Attacked();
        yield return new WaitForSeconds(skeletonAnimation.state.GetCurrent(0).Animation.duration / 2f);
        Wait();
    }

    private void Wait()
    {
        skeletonAnimation.loop = true;
        skeletonAnimation.AnimationName = minerProperties.IdleAnim;
    }
}
