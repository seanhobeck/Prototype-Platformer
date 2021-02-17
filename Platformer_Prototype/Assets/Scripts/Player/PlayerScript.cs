using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerScript : MonoBehaviour
{
    /* Variables. */
    public Rigidbody2D rigidbody;

    public Vector2 currentvelo = Vector2.zero;
    public float dashing;
    public float cheight;
    public bool onground;

    [Range(0.5f, 2.5f)] public float walkingspeed = 0.85f;
    [Range(0.5f, 1f)] public float jumpingamount = 0.75f;
    [Range(0.1f, 1f)] public float frictionspeed = 0.3f;

    /* Unity Methods. */
    #region UnityMethods

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        /* Initialize some things like the inventory. */
    }

    private void Update()
    {
        this.UpdateMovement();
    }

    private void FixedUpdate()
    {
        onground = IsOnGround(ref cheight);
    }

    #endregion

    /* Player Methods. */
    #region PlayerMethods

    public IEnumerator ShiftTimer() 
    {
        yield return new WaitForSeconds(.25f); 
        dashing = 1f;                              // <----- @owengretzinger,   fixed by setting it equal to 1,  example:  8 * 1 = 8.
        yield return new WaitForSeconds(5);
        dashing = 0f;
    }

    public void UpdateMovement()
    {
        /* Dash Keycode check. */
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashing == 0f) 
        {
            dashing = 3.85f;
            StartCoroutine(ShiftTimer());
        }

        /* Horizontal Direction. */
        float horiz = Input.GetAxisRaw("Horizontal");

        /* Slight smoothing so the player movement isn't too snappy. */
        Vector2 movedir = Vector2.zero;
        movedir = Vector2.SmoothDamp(movedir, new Vector2(horiz, 0), ref currentvelo, frictionspeed);     // <----- @Chris Q,   this friction method is already implentated and is changeable.

        Vector3 velo = (movedir * transform.right) * walkingspeed;

        /* If dashing, multiply the velo by the dash speed. */
        if (dashing != 0f)
            velo *= dashing;

        float airfactor = (onground ? 1f : 0.65f);
        transform.Translate(velo * airfactor);
         
        if (Input.GetKeyDown(KeyCode.Space))
        {
            /* Added method with ref for those who need it. */
            if(IsOnGround(ref cheight))
                rigidbody.AddForce(transform.up * 400f * jumpingamount);
        }
    }

    public bool IsOnGround(ref float reff) 
    {
        /* Ignore everything but the stuff in layer 3. */
        int mask = 1 << 3;
        mask = ~mask;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, mask);

        /* Get the point magnitude. */
        Vector2 delta = (Vector2)transform.position - hit.point;
        float dist = Mathf.Abs(delta.magnitude);

        reff = dist;
        
        /* Distance is less than the height plus a little. */
        return dist <= transform.localScale.y + 0.1f;
    }

    #endregion
}
