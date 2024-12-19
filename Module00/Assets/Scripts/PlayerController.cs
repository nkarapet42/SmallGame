using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;        // Скорость передвижения
    public float jumpForce = 5f;    // Сила прыжка
    public float jumpCooldown = 0.2f;  // Задержка между прыжками
    private Rigidbody rb;
    private bool canJump = true;     // Флаг для разрешения прыжка
    private bool isGrounded = false; // Проверка на землю

    private Vector3 initialPosition; // Начальная позиция игрока

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;  // Сохраняем начальную позицию
    }

    void FixedUpdate()
    {
        // Движение по горизонтали
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveVertical, 0.0f, -moveHorizontal);
        rb.AddForce(movement * speed);

        // Ограничение максимальной скорости
        if (rb.linearVelocity.magnitude > speed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
        }
    }

    void Update()
    {
        // Проверка на соприкосновение с землей с помощью Raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        // Прыжок при удержании пробела и нахождении на земле
        if (Input.GetKey(KeyCode.Space) && isGrounded && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;  // Отключаем возможность прыгать
            Invoke(nameof(EnableJump), jumpCooldown);  // Включаем прыжок через задержку
        }

        // Возврат к начальной позиции при нажатии R
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPosition();
        }
    }

    // Включение возможности прыжка после задержки
    void EnableJump()
    {
        canJump = true;
    }

    // Метод для возврата к начальной позиции
    void ResetPosition()
    {
        transform.position = initialPosition;   // Возвращаем позицию
        rb.linearVelocity = Vector3.zero;             // Сбрасываем скорость
		rb.angularVelocity = Vector3.zero;
	}

	private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            Debug.Log("Game Over");  // Выводим сообщение в консоль
            Destroy(gameObject);     // Уничтожаем объект игрока
        }
    }
}
