using UnityEngine;
using Random = UnityEngine.Random;

namespace Objects.Enemy.AttackEnemy
{
    public class NautolanScoutProjectileMovement : MonoBehaviour
    {
        public float amplitude = 1.0f; // Biên độ của đồ thị sin/cos
        public float frequency = 1.0f; // Tần số của đồ thị sin/cos
        public float speed = 5.0f; // Tốc độ di chuyển theo trục Y

        private float startTime;
        private Vector3 initialPosition;
        private bool isLeft;

        private void OnEnable()
        {
            startTime = Time.time; // Lưu thời điểm bắt đầu để tính thời gian đã trôi qua
            initialPosition = transform.parent.position;
            RandomIsLeft();
        }

        private void FixedUpdate()
        {
            float deltaTime = Time.time - startTime;  // Thời gian đã trôi qua từ lúc bắt đầu
            float xPos = amplitude * Mathf.Sin(2f * Mathf.PI * frequency * deltaTime);

            Vector3 newPosition = initialPosition;
            if (isLeft) newPosition.x += xPos;
            else newPosition.x -= xPos;
            newPosition.y -= deltaTime * speed;

            transform.parent.position = newPosition;
        }

        private void RandomIsLeft()
        {
            int randomNumber = Random.Range(1, 3);
            if (randomNumber == 1) isLeft = true;
            else isLeft = false;
        }
    }
}