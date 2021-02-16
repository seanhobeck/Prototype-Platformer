using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerScript : MonoBehaviour
{
    /* Variables. */
    public Rigidbody2D rigidbody;

    private Vector2 currentvelo = Vector2.zero;
    public float dashing;

    [Range(1f, 2.5f)] public float walkingspeed = 1.2f;
    [Range(1f, 3.5f)] public float jumpingamount = 2.2f;

    /* Unity Methods. */
    #region UnityMethods

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        /* Initialize some things like the inventory. */
    }

    void Update()
    {
        this.UpdateMovement();
    }

    #endregion

    /* Player Methods. */
    #region PlayerMethods

    public IEnumerator ShiftTimer() 
    {
        yield return new WaitForSeconds(.25f);
        dashing = 0f;
        yield return new WaitForSeconds(5);
    }

    public void UpdateMovement()
    {
        /* Dash Keycode check. */
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            dashing = 3.85f;
            StartCoroutine(ShiftTimer());
        }

        /* Horizontal Direction. */
        float horiz = Input.GetAxisRaw("Horizontal");

        /* Slight smoothing so the player movement isn't too snappy. */
        Vector2 movedir = Vector2.zero;
        movedir = Vector2.SmoothDamp(movedir, new Vector2(horiz, 0), ref currentvelo, 0.1f);

        Vector3 velo = (movedir * transform.right) * walkingspeed;

        /* If dashing, multiply the velo by the dash speed. */
        if (dashing != 0f)
            velo *= dashing;

        transform.Translate(velo, Space.Self);
         
        if (Input.GetKeyDown(KeyCode.Space))
        {
            /* Ignore everything but the stuff in layer 3. */
            int mask = 1 << 3;
            mask = ~mask;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, mask);
            
            /* Get the point magnitude. */
            Vector2 delta = (Vector2)transform.position - hit.point;
            float dist = Mathf.Abs(delta.magnitude);


            /* Distance is less than the height plus a little. */
            if (dist <= transform.localScale.y + 0.1f)
                rigidbody.AddForce(transform.up * 400f);
        }
    }

    #endregion
}
