using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Member : MonoBehaviour
{
    // Start is called before the first frame update
    public int memberId;
    public bool touch = false;
    public string targetName = "Player";
    Transform target;
    private float speed = 3f;

    private void Update()
    {
        if (touch == true)
            Followtarget();
    }

    public void TargetFind()
    {
        target = GameObject.FindGameObjectWithTag(targetName).GetComponent<Transform>();
    }

    void Followtarget()
    {
        var heading = target.position - this.transform.position;

        if (heading.sqrMagnitude > 0.5f)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        }

        //transform.LookAt(target);
    }

}
