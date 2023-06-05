using UnityEngine;
using Random = UnityEngine.Random;

namespace Objects.Enemy.AttackEnemy
{
    public class NautolanScoutProjectileMovement : MonoBehaviour
    {
        [SerializeField] private float amplitude = 1.0f; 
        [SerializeField] private float frequency = 1.0f; 
        [SerializeField] private float speed = 5.0f; 

        private float startTime;
        private Vector3 initialPosition;
        private bool isLeft;

        private void OnEnable()
        {
            startTime = Time.time; 
            initialPosition = transform.parent.position;
            RandomIsLeft();
        }

        private void FixedUpdate()
        {
            float deltaTime = Time.time - startTime; 
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