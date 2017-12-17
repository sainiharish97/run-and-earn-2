using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lionf : MonoBehaviour {

    [SerializeField]
    float mMoveSpeed;
    [SerializeField]
    float mJumpForce;
    [SerializeField]
    LayerMask mWhatIsGround;
    float kGroundCheckRadius = 0.1f;
    // Invincibility timer
    float kInvincibilityDuration = 1.0f;
    float mInvincibleTimer;
    bool mInvincible;

    //life bar
    public Image lifebar;
    public float life = 1;
    [SerializeField]
    GameObject mDeathParticleEmitter;

    // Damage effects
    float kDamagePushForce = 2.5f;

    // Animator booleans
    bool mRunning;
    bool mGrounded;
    bool mRising;
    bool mFalling;
    // Wall kicking
    bool mAllowWallKick;
    Vector2 mFacingDirection;
    // References to other components and game objects
    Animator mAnimator;
    Rigidbody2D mRigidBody2D;
    List<GroundCheck> mGroundCheckList;



    void Start () {

        // Get references to other components and game objects
        mAnimator = GetComponent<Animator>();
        mRigidBody2D = GetComponent<Rigidbody2D>();

        mFacingDirection = Vector2.right;

        // Obtain ground check components and store in list
        mGroundCheckList = new List<GroundCheck>();
        GroundCheck[] groundChecksArray = transform.GetComponentsInChildren<GroundCheck>();
        foreach (GroundCheck g in groundChecksArray)
        {
            mGroundCheckList.Add(g);
        }

    }

    // Update is called once per frame
    void Update () {
        mRunning = false;
        if (Input.GetButton("Left"))
        {
            transform.Translate(-Vector2.right * mMoveSpeed * Time.deltaTime, Space.World);
            FaceDirection(-Vector2.right);
            mRunning = true;
        }
        if (Input.GetButton("Right"))
        {
            transform.Translate(Vector2.right * mMoveSpeed * Time.deltaTime, Space.World);
            FaceDirection(Vector2.right);
            mRunning = true;
        }

        bool grounded = CheckGrounded();
       
        mGrounded = grounded;

        if (mGrounded && Input.GetButtonDown("Jump"))
        {
            mRigidBody2D.AddForce(Vector2.up * mJumpForce, ForceMode2D.Impulse);
        }
        

        mRising = mRigidBody2D.velocity.y > 0.0f;
        mFalling = mRigidBody2D.velocity.y < 0.0f;
        if (mInvincible)
        {
            mInvincibleTimer += Time.deltaTime;
            if (mInvincibleTimer >= kInvincibilityDuration)
            {
                mInvincible = false;
                mInvincibleTimer = 0.0f;
            }
        }

        UpdateAnimator();
    }

    public Vector2 GetFacingDirection()
    {
        return mFacingDirection;
    }
    private void FaceDirection(Vector2 direction)
    {
        mFacingDirection = direction;
        if (direction == Vector2.right)
        {
            Vector3 newScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
        }
        else
        {
            Vector3 newScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
        }
    }
    private bool CheckGrounded()
    {
        foreach (GroundCheck g in mGroundCheckList)
        {
            if (g.CheckGrounded(kGroundCheckRadius, mWhatIsGround, gameObject))
            {
                return true;
            }
        }
        return false;
    }
     

    private void UpdateAnimator()
    {
        mAnimator.SetBool("isRunning", mRunning);
        mAnimator.SetBool("isGrounded", mGrounded);
        mAnimator.SetBool("isRising", mRising);
        mAnimator.SetBool("isFalling", mFalling);
        mAnimator.SetBool("isHurt", mInvincible);

    }

        public void TakeDamage(float damage)
        {
            if (!mInvincible)
            {
                Vector2 forceDirection = new Vector2(-mFacingDirection.x, 1.0f) * kDamagePushForce;
                mRigidBody2D.velocity = Vector2.zero;
                mRigidBody2D.AddForce(forceDirection, ForceMode2D.Impulse);
                mInvincible = true;
            life = life - damage;
        }
        }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            life = life - (float)(0.10);
            Destroy(col.gameObject);
        }
    }



}
