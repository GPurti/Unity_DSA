using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject
{
    public int playerDamage;

    private Animator animator;
    private Transform target;
    private bool skipMove;
    public int life;

    [HideInInspector] public RoomGameManager roomGameManager;

    // Start is called before the first frame update
    protected override void Start()
    {
        SetLife();
        Invoke("SetGameManager", 1);
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
    }

    private void SetLife()
    {
        this.life = 4;
        Player p1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (p1.GetFire())
        {
            this.life = 2;
        }
    }

    private void SetGameManager()
    {
        roomGameManager.AddEnemyToList(this);
    }

    public void damagedByPlayer()
    {
        life--;
        if(life <= 0)
        {
            roomGameManager.RemoveEnemyFromList(this.gameObject);
            roomGameManager.CheckIfGameOver();
            animator.SetTrigger("enemyDie");
            Destroy(this.gameObject);
        }
    }


    protected override void AttemptMove <T> (int xDir, int yDir)
    {
        /*if (skipMove)
        {
            skipMove = false;
            return;
        }*/

        base.AttemptMove<T>(xDir, yDir);

        //skipMove = true;
    }

    public void MoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;

        if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
            yDir = target.position.y > transform.position.y ? 1 : -1;
        else
            xDir = target.position.x > transform.position.x ? 1 : -1;

        AttemptMove<Player>(xDir, yDir);
    }

    protected override void OnCantMove <T> (T component)
    {
        Player hitPlayer = component as Player;

        hitPlayer.LoseHp(playerDamage);

        animator.SetTrigger("enemyAttack");
    }
}
