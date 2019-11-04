using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Enemy : MonoBehaviour, IActor
{
    public float health = 10f;
    private float currentHealth;
    [SerializeField]
    protected Animator animator;

    protected bool isChangeColor;

    private Collider2D collider2D;

    private Rigidbody2D rigidbody;


    [SerializeField]
    private SpriteRenderer colorHurt;
    [SerializeField]
    private float timeHurt = 1f;
    private Color color = Color.white;
    private float tempTime;
    private float timeCount;

    //Spine
    public bool isSpine;
    [SerializeField]
    private MeshRenderer meshRenderer;
    private MaterialPropertyBlock materialPropertyBlock;
    private Color hitColor = Color.white;
    private Color normalColorSpine = Color.black;

    //enum Action
    protected EnemyAction currentAction;


  

    protected virtual void OnEnable()
    {
        currentHealth = health;
        animator.SetBool("IsDie", false);

        if (!isSpine)
        {
            normalColor = Color.white;
            try
            {
                normalColor.a = 1;
                normalColor.r = 1;
                normalColor.b = 1;
                normalColor.g = 1;
                colorHurt.color = normalColor;
            }
            catch
            {
                Debug.LogError("!!!!!!");
            }
        }
        else if(isSpine)
        {
            hitColor = Color.white;
            normalColor = Color.black;
            try
            {
                hitColor.a = 1f;
                normalColor.a = 1f;
                materialPropertyBlock.SetColor("_Color", Color.white);
                materialPropertyBlock.SetColor("_Black", normalColor);
                meshRenderer.SetPropertyBlock(materialPropertyBlock);
            }
            catch
            {
                Debug.LogError("!!!!!!!");
            }
        }




    }

    [ContextMenu("___takedame")]
    protected virtual void Test()
    {
        isChangeColor = true;
    }

    public void TakeDame(float damage)
    {
        animator.SetTrigger("IsHurt");
        currentHealth -= damage;
        if (currentHealth <= 0) currentHealth = 0;
        isChangeColor = true;
    }


    protected virtual void Start()
    {
        currentHealth = health;
        collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        colorHurt = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        materialPropertyBlock = new MaterialPropertyBlock();
    }


    public void OnDeath()
    {
        if(currentHealth <= 0)
        {
            animator.SetBool("IsDie", true);
        }
    }

    private Color normalColor = Color.white;
   



    //void HurtColor()
    //{
    //    tempTime += Time.deltaTime;
    //    timeCount += Time.deltaTime;
    //    if(tempTime <= timeHurt/2)
    //    {



    //        color.g = 1 - tempTime / (timeHurt / 2);
    //        color.b = 1 - tempTime / (timeHurt / 2);
    //        colorHurt.color = color;
    //    }



    //    if(tempTime > timeHurt/2 )
    //    {
    //        color.a = 1;
    //        color.g = tempTime / (timeHurt / 2) - 1;
    //        color.b = tempTime / (timeHurt / 2) - 1;
    //        colorHurt.color = color;
    //    }
    //    if (tempTime > timeHurt)
    //    {
    //        tempTime = 0;
    //        isGetDame = !isGetDame;
    //    }
    //}

        
    [Header("SetColorHurt")]
    public int channelR = 255;
    public int channelG = 160;
    public int channelB = 128;
    //public int channelA = 90;
    protected virtual void HurtColor()
    {

        if (!isSpine)
        {

            timeCount += Time.deltaTime;
            if (timeCount <= timeHurt / 2)
            {
                tempTime += Time.deltaTime;
                //color.a = 1 - tempTime / mauSoX(channelA);
                color.r = 1 - tempTime / mauSoX(channelR);
                color.g = 1 - tempTime / mauSoX(channelG);
                color.b = 1 - tempTime / mauSoX(channelB);
                colorHurt.color = color;
            }



            if (timeCount > timeHurt / 2)
            {
                tempTime -= Time.deltaTime;
                //color.a = 1 - tempTime / mauSoX(channelA);
                color.r = 1 - tempTime / mauSoX(channelR);
                color.g = 1 - tempTime / mauSoX(channelG);
                color.b = 1 - tempTime / mauSoX(channelB);
                colorHurt.color = color;
            }



            if (timeCount > timeHurt)
            {
                tempTime = 0;
                timeCount = 0;
                isChangeColor = !isChangeColor;
            }

            Debug.Log(timeCount);
            Debug.Log("IsChangeColor" + isChangeColor);
        }
        else if(isSpine)
        {
            timeCount += Time.deltaTime;
            if(timeCount <= timeHurt/2)
            {
                hitColor.a = timeCount / (timeHurt / 2);
                materialPropertyBlock.SetColor("_Black", hitColor);
                meshRenderer.SetPropertyBlock(materialPropertyBlock);
            }
            if(timeCount > timeHurt)
            {
                tempTime += Time.deltaTime;
                normalColorSpine.a = (tempTime*2) / timeHurt;
                materialPropertyBlock.SetColor("_Black", normalColorSpine);
                meshRenderer.SetPropertyBlock(materialPropertyBlock);
            }


            if(timeCount > timeHurt)
            {
                tempTime = 0;
                timeCount = 0;
                isChangeColor = !isChangeColor;
                currentAction = EnemyAction.Idle;
            }
        }
      
    }















    private float mauSoX(float A)
    {
        float x = ((timeHurt / 2) * 255) / (255 - A);
        return x;
        
    }
   
   

    protected virtual void Update()
    {
        
        if (isChangeColor) HurtColor();
    }

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}



public enum EnemyAction
{
    OnSpawning,
    Idle,
    Hurt,
    Run,
    Hit,
    Jump,
    Die
}