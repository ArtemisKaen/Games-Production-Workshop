using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	public float speed = 3.0f;
    public float maxSpeed = 5.0f;
	public float maxSteering = 2.0f;
    public float slowingRadius = 10.0f;
    public float attackRadius = 3.0f;

    public GameObject target;
    public GameObject player;
    public Rigidbody2D rigidBody2D;

    private float directionScaler;
    private Vector2 playerPos;
    private Vector2 targetPosition;


	// Use this for initialization
	public void Start()
	{
		rigidBody2D = GetComponent<Rigidbody2D>();

        target = GameObject.Find("DefencePoint");
        targetPosition = target.transform.position;

        player = GameObject.Find("Player");

        directionScaler = transform.localScale.x;
    }

	// FixedUpdate is called once per physics frame
	public void FixedUpdate()
	{
        CooperativeArbitration();
    }

	// Update is called once per game frame
	public void Update()
	{
		SetDirection();
        playerPos = player.transform.position;


    }
	
	#region Helper functions
	/// <summary>
	/// Returns the mouse position in 2d space
	/// </summary>
	/// <returns>the mouse position in 2d space</returns>
	public Vector2 GetMousePosition()
	{
		Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		return new Vector2(temp.x, temp.y);
	}

	/// <summary>
	/// Sets the direction of the triangle to the direction it is moving in to give the illusion it is turning. Trying taking out the function
	/// call in Update() to see what happens
	/// </summary>
	public void SetDirection()
	{
		// Don't set the direction if no direction
		/*if (rigidBody2D.velocity.sqrMagnitude > 0.0f)
		{
			transform.right = new Vector3(rigidBody2D.velocity.x, rigidBody2D.velocity.y, 0.0f);
		}*/

        if (rigidBody2D.velocity.x >= 0)
        {
            transform.localScale = new Vector2(directionScaler, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(-directionScaler, transform.localScale.y);
        }

	}


	public Vector2 LimitSteering(Vector2 steeringVelocity)
	{
		// This limits the steering velocity to maxSteering. sqrMagnitude is used rather than magnitude as in magnitude a square root must be computed which is a slow operation.
		// By using sqrMagnitude and comparing with maxSteering squared we can around using the expensive square root operation.
		if (steeringVelocity.sqrMagnitude > maxSteering * maxSteering)
		{
			steeringVelocity.Normalize();
			steeringVelocity *= maxSteering *2;
		}
		return steeringVelocity;
	}

	public Vector2 LimitVelocity(Vector2 velocity)
	{
		// This limits the velocity to max speed. sqrMagnitude is used rather than magnitude as in magnitude a square root must be computed which is a slow operation.
		// By using sqrMagnitude and comparing with maxSpeed squared we can around using the expensive square root operation.
		if (velocity.sqrMagnitude > speed * speed)
		{
			velocity.Normalize();
			velocity *= speed;
		}
		return velocity;
	}
	#endregion

	public Vector2 Seek(Vector2 currentVelocity, Vector2 targetPosition)
	{
        // These 3 lines are equivalent to: desiredVelocity = Normalise(targetPosition - currentPoisition) * MaxSpeed
        Vector2 desiredVelocity = targetPosition - new Vector2(transform.position.x, transform.position.y);
		desiredVelocity.Normalize();
		desiredVelocity *= speed;

		// Useful for showing directions in scene view to visualise what the AI is doing
		Debug.DrawRay(transform.position, desiredVelocity, Color.red);
		Debug.DrawRay(transform.position, currentVelocity);

        // Calculate the desired velocity
        float distanceToTarget = Vector2.Distance(targetPosition, transform.position);
        float distanceToPlayer = Vector2.Distance(playerPos, transform.position);
 
        // Check the distance to detect whether the character
        // is inside the slowing area

        if (distanceToTarget < slowingRadius) {
        // Inside the slowing area
            desiredVelocity = desiredVelocity * speed * (distanceToTarget / slowingRadius);
        }
        else
        {
            // Outside the slowing area.
            desiredVelocity = desiredVelocity * speed;
        }

        // Calculate steering velocity
        Vector2 steeringVelocity = desiredVelocity - currentVelocity;

        return steeringVelocity;
	}

	#region BehaviorManagement
	/// <summary>
	/// This is responsible for how to deal with multiple behaviours and selecting which ones to use. Please see this link for some decent descriptions of below:
	/// https://alastaira.wordpress.com/2013/03/13/methods-for-combining-autonomous-steering-behaviours/
	/// Remember some options for choosing are:
	/// 1 Finite state machines which can be part of the steering behaviours or not (Not the best approach but quick)
	/// 2 Weighted Truncated Sum
	/// 3 Prioritised Weighted Truncated Sum
	/// 4 Prioritised Dithering
	/// 5 Context Behaviours: https://andrewfray.wordpress.com/2013/03/26/context-behaviours-know-how-to-share/
	/// 6 Any other approach you come up with
	/// </summary>
	public void CooperativeArbitration()
	{
        float distanceToPlayer = Vector2.Distance(playerPos, transform.position);
        if (distanceToPlayer < attackRadius)
        {
            targetPosition = playerPos;
        }
        else
        {
            targetPosition = target.transform.position;
        }


        // Currently just choosing seeking so no arbitration is happening
        Vector2 currentVelocity = rigidBody2D.velocity;
		currentVelocity += Seek(currentVelocity, targetPosition);

		// Uncomment the next line to activate flee. See what happens when you do both behaviours and observe why Cooperative Arbitration is needed
		//currentVelocity += Flee(currentVelocity, targetPosition);

		currentVelocity = LimitVelocity(currentVelocity);
		rigidBody2D.velocity = currentVelocity;
	}
	#endregion
}