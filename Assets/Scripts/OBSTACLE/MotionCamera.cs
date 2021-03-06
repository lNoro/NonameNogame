using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MotionCamera : Obstacle
{
    [SerializeField]
    public float RotateSpeed = 1;
    public float RotateDirection = 1;
    public float TotalSpeed;
    public float MaximumRotateDegree = 30f;
    public GameObject ice;


    void Start()
    {       
        TotalSpeed = RotateSpeed * RotateDirection * Time.deltaTime;

        this.transform.DORotate(new Vector3(0, 0, -60), 3).SetLoops(-1,LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    { 

    }

    IEnumerator FreezeEnemy(GameObject Enemy)
    {
        Instantiate(ice, Enemy.transform);
        Enemy.GetComponent<ChasingRobotController>().frozen=true;
        yield return new WaitForSeconds(2);  
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("dead !!!");
        if(collision.CompareTag("Player")&& !overriden)
        {
            GameObject.Find("Player").GetComponent<CharacterController>().Die();
        }
        if(collision.CompareTag("ChasingRobot") && overriden)
        {
            StartCoroutine(FreezeEnemy(collision.gameObject));

        }
    }


}
