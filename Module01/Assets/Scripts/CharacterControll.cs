using UnityEngine;

public class CharacterControll : MonoBehaviour
{
	private Rigidbody rb;
	public float speed = 5f;
	public float jumpForce = 5f;
	public float jumpCooldown = 0.2f;
	public float maxSpeed = 10f;
	public bool isActive = false;
	private bool canJump = true;

	public bool CanJump
	{
		get { return canJump; }
		set { canJump = value; }
	}
	
	private bool isGrounded = false;
	private bool isTouchingWall = false;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		if (isActive)
		{
			float moveHorizontal = Input.GetAxis("Horizontal");

			if (!isTouchingWall)
			{
				Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
				rb.AddForce(movement * speed, ForceMode.Force);
			}
			LimitMaxSpeed();
		}
	}

	void Update()
	{
		if (isActive)
		{
			float raycastDistance = GetComponent<Collider>().bounds.extents.y + 0.1f;
			RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.down, raycastDistance);

			RaycastHit? closestHit = null;

			foreach (var hit in hits)
			{
				if (closestHit == null || hit.distance > closestHit.Value.distance)
				{
					closestHit = hit;
				}
			}

			isGrounded = closestHit.HasValue;

			if (isGrounded)
			{
				var hit = closestHit.Value;
				int hitLayer = hit.collider.gameObject.layer;
				if (hit.collider.gameObject.CompareTag("Exit"))
					return;

				string layer = LayerMask.LayerToName(hitLayer);
				if (layer.Contains("Color"))
				{
					if (gameObject.name + "Color" != layer)
						return;
				}
			}

			if (Input.GetKey(KeyCode.Space) && isGrounded && canJump)
			{
				rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
				canJump = false;
				Invoke(nameof(EnableJump), jumpCooldown);
			}
		}
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Water") && gameObject.name != "Claire")
		{
			Debug.Log(gameObject.name + " Die");
			Destroy(gameObject);
		}
	}

	void EnableJump()
	{
		canJump = true;
	}

	private void LimitMaxSpeed()
	{
		Vector3 velocity = rb.linearVelocity;
		velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
		velocity.z = Mathf.Clamp(velocity.z, -maxSpeed, maxSpeed);
		rb.linearVelocity = velocity;
	}
}
