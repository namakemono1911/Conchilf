using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class camera_gang : MonoBehaviour {

    public Transform Target;
    public float zOffset = 0;
    public float Distance = 4.5f;
    public float ZoomStep = 1.0f;
    public float MoveSpeed = 5f;
    public float Pitch = 30f;
    public float yaw = 45f;
    public float TargetMoveSpeed = 3f;
    public float RotateSpeed = 60f;
    private Animator[] animator;

    private Vector3 TargetPos;
    private Vector3 LookPoint;

    private bool firing = false;
    private bool damage = false;
    private float damageTimer;

    void Start()
    {
        transform.position = GetPosition();
        LookPoint = Target.position;
        animator = GameObject.FindObjectsOfType<Animator>();
        Debug.Log("animators = " + animator.Length);
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (damage)
        {

            if (Time.time > damageTimer)
            {
                damage = false;
                foreach (Animator a in animator)
                    a.SetLayerWeight(2, 0);
            }
        }

        if (Input.GetKey("a"))
        {
            yaw -= Time.deltaTime * RotateSpeed;
        }
        if (Input.GetKey("d"))
        {
            yaw += Time.deltaTime * RotateSpeed;
        }
        if (Input.GetKey("w") && Distance > 2f)
        {
            Distance -= Time.deltaTime * ZoomStep;
        }

        if (Input.GetKey("s") && Distance < 14f)
        {
            Distance += Time.deltaTime * ZoomStep;
        }
        if (Input.GetKey("q") && Pitch > 10f)
        {
            Pitch -= Time.deltaTime * RotateSpeed;
        }
        if (Input.GetKey("e") && Pitch < 75f)
        {
            Pitch += Time.deltaTime * RotateSpeed;
        }

        LookPoint = Vector3.MoveTowards(LookPoint, Target.position + Vector3.up * zOffset, TargetMoveSpeed * Time.deltaTime);

        TargetPos = GetPosition();
        transform.position = Vector3.MoveTowards(transform.position, TargetPos, MoveSpeed * Time.deltaTime);
        transform.LookAt(LookPoint);
    }

    Vector3 GetPosition()
    {
        float y = Distance * Mathf.Sin(Pitch * Mathf.Deg2Rad);
        float r = Distance * Mathf.Cos(Pitch * Mathf.Deg2Rad);
        float x = r * Mathf.Cos(yaw * Mathf.Deg2Rad);
        float z = r * Mathf.Sin(yaw * Mathf.Deg2Rad);

        return (Target.position + new Vector3(x, y, z));
    }

    public void InverseAnimBool(string s)
    {
        foreach (Animator a in animator)
            a.SetBool(s, !a.GetBool(s));
    }

    public void SetAnimTrigger(string s)
    {
        foreach (Animator a in animator)
            a.SetTrigger(s);
    }

    public void SetAnimRun()
    {
        foreach (Animator a in animator)
        {
            bool b = !a.GetBool("run");
            a.SetBool("run", b);
            a.SetBool("walk", false);
            a.SetBool("sprint", false);
        }
    }

    public void SetAnimWalk()
    {
        foreach (Animator a in animator)
        {
            bool b = !a.GetBool("walk");
            a.SetBool("walk", b);
            a.SetBool("run", false);
            a.SetBool("sprint", false);
        }
    }

    public void SetAnimSprint()
    {
        foreach (Animator a in animator)
        {
            bool b = !a.GetBool("sprint");
            a.SetBool("sprint", b);
            a.SetBool("walk", false);
            a.SetBool("run", false);
        }
    }

    public void SetVerticalAim(float x)
    {
        foreach (Animator a in animator)
        {
            a.SetFloat("aim_vertical", x);
        }
    }

    public void SetHorizontalAim(float x)
    {
        foreach (Animator a in animator)
        {
            a.SetFloat("aim_horizontal", x);
        }
    }

    public void SetAimWeight(float x)
    {
        foreach (Animator a in animator)
        {
            a.SetLayerWeight(1, x);
        }
    }

    public void SetFireBurst()
    {
        firing = !firing;
        float x = firing ? 1f : 0f;

        foreach (Animator a in animator)
        {
            a.SetLayerWeight(3, x);
        }
    }

    public void SetDeath()
    {

        foreach (Animator a in animator)
        {
            a.SetInteger("death_id", a.GetInteger("death_id") >= 2 ? 0 : (a.GetInteger("death_id") + 1));
            a.SetTrigger("death");
        }
    }

    public void SetDamage()
    {
        damageTimer = Time.time + 0.7f;
        damage = true;
        float x = Random.Range(-1f, 1f);

        foreach (Animator a in animator)
            a.SetFloat("hit_angle", x);

        foreach (Animator a in animator)
        {
            //a.SetLayerWeight(2, 1);
            a.SetTrigger("damage");
        }
    }
}