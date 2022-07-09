using UnityEngine;

namespace PlayerController
{
    public class PlayerController : MonoBehaviour, IDamageable
    {
        IControl currentState;
        public Idle idle = new Idle();
        public Run run = new Run();
        public Jump jump = new Jump();
        public Crouch crouch = new Crouch();

        [HideInInspector]
        public Animator animator;
        [HideInInspector]
        public Rigidbody2D rb;
        [HideInInspector]
        public CapsuleCollider2D capsuleCollider;
        [HideInInspector]
        public BoxCollider2D groundCheck;

        public float jump_force = 5f;
        [HideInInspector]
        public float movement_speed;
        [HideInInspector]
        public bool isJumping = false;
        public float speed = 5f;
        public bool facing_right = true;
        private float health = 100;

        // Start is called before the first frame update
        void Start()
        {
            //getting all neccessary components
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            capsuleCollider = GetComponent<CapsuleCollider2D>();

            currentState = idle;
            currentState.enterState(this);

        }

        // Update is called once per frame
        void Update()
        {
            anyToJump();
            currentState.updateState(this);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            currentState.collisionState(this, collision);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                animator.SetBool("jump", false);
                isJumping = false;
            }
            currentState.triggerState(this,collision);
        }
        public void getDamage(float damage)
        {
            if (damage >= health)
            {
                //animator.SetTrigger("damage");
                Debug.Log("Player got Hit!");
            }
        }
        public void movement()
        {
            movement_speed = Input.GetAxis("Horizontal");
            Vector2 position = transform.position;
            position.x += movement_speed * speed * Time.deltaTime;
            transform.position = position;

            //animating and flippping ninja according to value.
            if (facing_right && movement_speed < 0)
                flip();
            else if (!facing_right && movement_speed > 0)
                flip();
        }
        public void switchState(IControl state)
        {
            currentState = state;
            currentState.enterState(this);
        }
        public void idleOrRun()
        {
            if (movement_speed == 0)
            {
                switchState(idle);
            }
            else
            {
                switchState(run);
            }
        }
        public void anyToJump()
        {
            //if player jumps
            if (Input.GetButtonDown("Jump"))
            {
                if(!isJumping)
                    switchState(jump);
            }
                
        }
        public void flip()
        {
            transform.Rotate(0f, 180, 0f);
            facing_right = !facing_right;
        }
        public void Attack()
        { 
            if(Input.GetMouseButtonDown(0))
                animator.SetBool("Attack", true);  
        }
        public void NinjaAttecked()
        {
            animator.SetBool("Attack", false);
        }
    }
}
