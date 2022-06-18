using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private RectTransform fill;
    [SerializeField] private float timeToOverride = 0.000005f;
    [SerializeField] private Animator animator;


    private bool _notFilled = true;
    private float _origX;
    
    // Start is called before the first frame update
    void Start()
    {
        _origX = fill.offsetMax.x;
    }

    
    private IEnumerator Override(List<GameObject> inrangeObstacles)
    {
        animator.SetTrigger("Override");
        yield return new WaitForSeconds(0.1f);
        while (_notFilled)
        {
            fill.offsetMax = new Vector2(fill.offsetMax.x + 4f, fill.offsetMax.y);
            yield return new WaitForSeconds(timeToOverride);
            if (fill.offsetMax.x >= -1f)
                _notFilled = false;
        }
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("GoOut");
        yield return new WaitForSeconds(0.15f);
        GameObject.Find("Player").GetComponent<Animator>().SetBool("smash", false);
        fill.offsetMax = new Vector2(_origX, fill.offsetMax.y);
        _notFilled = true;
        foreach(GameObject obj in inrangeObstacles)
        {
            if (obj.CompareTag("Laser"))
            {
                obj.GetComponentInChildren<Obstacle>().SetOverride();
            }
            else
            {
                obj.GetComponent<Obstacle>().SetOverride();
            }
        }
        inrangeObstacles.Clear();
    }


    public void StartOverride(List<GameObject> inrangeObstacles)
    {
        StartCoroutine(Override(inrangeObstacles));
    }
}
