using UnityEngine;

public class Attack : MonoBehaviour {
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public float attackRangeX;
    public float attackRangeY;
    public LayerMask whatisEnemy;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Debug.Log("bam");
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position,new Vector2(attackRangeX,attackRangeY),0,whatisEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    //Bam
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(5);
                    
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
	}
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(attackPos.position, new Vector2(attackRangeX, attackRangeY));
    }
}
