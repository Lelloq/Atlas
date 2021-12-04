using System;
using UnityEngine;
using UnityEngine.UI;

public class HP_and_Attack : MonoBehaviour
{
    public int iPlayerHealth = 100;
    private int iHealth;

    public float fAttackSpeed;
    private float fAttackWait;
    private bool bAttack = true;

    public int iPlayerBaseAttack;
    private int iChangeAttack;
    public float fAttackRange;
    public Text iHealthText;
    public Transform attackCenter;
    public Transform player;
    public LayerMask enemiesLayer;
    public AudioSource SwordSwing1, SwordSwing2;
    public AudioSource SwordHit1, SwordHit2;
    public AudioSource BlockSound;
    public Animator anim;
    public GameObject Sword;
    public Transform hurt;
    public GameObject GameOver;

    public bool bSword = false;
    public bool bTexAxe = false;
    public bool bBlocking = false;
    public bool Hit = false;

    private KeyCode MLeft; private KeyCode MRight;
    private KeyCode MUp; private KeyCode MDown;
    private KeyCode SwingSword; private KeyCode Block;
    private InGameData Data;

    private System.Random RandNum;

    // Start is called before the first frame update
    private void Start()
    {
        MLeft = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"));
        MRight = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));
        MUp = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W"));
        MDown = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S"));
        SwingSword = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Attack", "Mouse0"));
        Block = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Block", "Mouse1"));

        RandNum = new System.Random();
        iHealth = iPlayerHealth;
        iChangeAttack = iPlayerBaseAttack;

        Data = GameObject.Find("GameData").GetComponent<InGameData>();
    }

    // Update is called once per frame
    //private void Update()
    //{
    //    if (fAttackWait <= 0)
    //    {
    //        fAttackWait = fAttackSpeed;
    //        bAttack = true;
    //    }
    //    else
    //    {
    //        fAttackWait -= Time.deltaTime;
    //    }

    //    Weapons();

    //    iHealthText.text = ("Health: " + iHealth.ToString());

    //    if (Hit)
    //    {
    //        hurt.GetComponent<ParticleSystem>().enableEmission = true;
    //        new WaitForSeconds(.2f);
    //        hurt.GetComponent<ParticleSystem>().enableEmission = false;
    //        Hit = false;
    //    }
    //    else { hurt.GetComponent<ParticleSystem>().enableEmission = false; }
    //}

    public void Weapons()
    {
        if (PlayerState.bSwordCollect == true)
        {
            bSword = true;
        }
        if (PlayerState.bAxeCollect == true)
        {
            bTexAxe = true;
        }

        if (bSword == true)
        {
            iChangeAttack = iPlayerBaseAttack * 3;
        }
        if (bTexAxe == true)
        {
            iChangeAttack = iPlayerBaseAttack * 5;
        }
    }

    private void FixedUpdate()
    {

        if (fAttackWait <= 0)
        {
            fAttackWait = fAttackSpeed;
            bAttack = true;
        }
        else
        {
            fAttackWait -= Time.deltaTime;
        }

        Weapons();

        iHealthText.text = ("Health: " + iHealth.ToString());

        if (iHealth <= 0)
        {
            transform.position = new Vector2(-2, -2);

            iHealth = iPlayerHealth;

            GameOver.SetActive(true);
            Time.timeScale = 0;
        }

        if (bAttack == true && Data.GotSword && bBlocking == false)
        {
            if (Input.GetKey(SwingSword))
            {

                if (attackCenter.localPosition.x < -1)
                {
                    anim.SetTrigger("Swing Left");
                    Sword.GetComponent<SpriteRenderer>().sortingOrder = 999;
                }
                else if (attackCenter.localPosition.x > 1)
                {
                    anim.SetTrigger("Swing Right");
                    Sword.GetComponent<SpriteRenderer>().sortingOrder = 8;
                }
                else if (attackCenter.localPosition.y < -1)
                { 
                    anim.SetTrigger("Swing Down");
                     Sword.GetComponent<SpriteRenderer>().sortingOrder = 8;
                }
                else if (attackCenter.localPosition.y > 1)
                {
                anim.SetTrigger("Swing Up");
                Sword.GetComponent<SpriteRenderer>().sortingOrder = 999;
                }
                
                Collider2D[] enmiesToDamage = Physics2D.OverlapCircleAll(attackCenter.position, fAttackRange, enemiesLayer);
                for (int i = 0; i < enmiesToDamage.Length; i++)
                {
                    enmiesToDamage[i].GetComponent<HP_N_Attack>().TakeDamage(iChangeAttack);
                    if (RandNum.Next(1, 2) == 1)
                    {
                        SwordHit1.Play();
                    }
                    else { SwordHit2.Play(); }
                }
                if (RandNum.Next(1, 2) == 1)
                {
                    SwordSwing1.Play();
                }
                else { SwordSwing2.Play(); }
                bAttack = false;
            }
        }

        if (Input.GetKey(MUp))
        {
            attackCenter.position = new Vector2(player.position.x, player.position.y + 0.5f);
        }
        if (Input.GetKey(MLeft))
        {
            attackCenter.position = new Vector2(player.position.x + -0.5f, player.position.y);
        }
        if (Input.GetKey(MDown))
        {
            attackCenter.position = new Vector2(player.position.x, player.position.y + -0.5f);
        }
        if (Input.GetKey(MRight))
        {
            attackCenter.position = new Vector2(player.position.x + 0.5f, player.position.y);
        }

        if (Input.GetKeyDown(Block) && Data.GotSword)
        {
            bBlocking = true;
        }
        if (Input.GetKeyUp(Block) && Data.GotSword)
        {
            bBlocking = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackCenter.position, fAttackRange);
    }

    public void TakeDamage(int damage)
    {
        if (bBlocking == false)
        {
            iHealth -= damage;
            Debug.Log("Player hit");
            Hit = true;
        }
        else { BlockSound.Play(); }
    }
}