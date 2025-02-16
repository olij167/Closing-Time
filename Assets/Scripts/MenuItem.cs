using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItem : MonoBehaviour
{
    //rotate & move on z axis
    public Vector3 dir = Vector3.one;
    public float speed = 20f;
    public float minSizeIncrease = 2f;
    public float maxSizeIncrease = 4f;
    //public float zMoveDistance;
    //public float moveSpeed;

    public bool inRandomiserLoop = false;

    public Vector3 minSize;
    public Vector3 maxSize;


    private void Start()
    {
        //dir = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

        transform.localScale *= Random.Range(minSizeIncrease, maxSizeIncrease);

        //minSize = transform.localScale / 2;

        //maxSize = transform.localScale * 2;

    }
    public void Update()
    {
        transform.Rotate(dir.x * speed * Time.deltaTime, dir.y * speed * Time.deltaTime, dir.z * speed * Time.deltaTime, Space.Self) ;

        //transform.localScale = Vector3.Lerp(minSize, maxSize, Mathf.PingPong(Time.deltaTime, speed));

        if (!inRandomiserLoop)
        {
            StartCoroutine(RandomiseDir());
        }
        
    }

    public IEnumerator RandomiseDir()
    {
        inRandomiserLoop = true;
        yield return new WaitForSeconds(Random.value * 10);

        dir = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
       
        yield return null;
        inRandomiserLoop = false ;
    }
}
