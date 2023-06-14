using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Objects.UI
{
    public class BossHealthBar : MonoBehaviour
    {
        [SerializeField] private float healthBarSpacing = 3.5f;
        [SerializeField] private RectTransform healthBarContainer;
        [SerializeField] private GameObject healthBarPrefab;
    
        private int maxHealth;
        private float healthBarWidth;
        private float healthBarHeight;
        private float barHeight;
        private void Start()
        {
            maxHealth = MiniBossKla_edCtrl.Instance.MinibossKlaEdDamageReciever.HpMax;
            healthBarWidth = healthBarContainer.rect.width;
            healthBarHeight = healthBarContainer.rect.height;
            barHeight = (healthBarHeight - (healthBarSpacing * (maxHealth - 1))) / maxHealth;
            SetMaxHealth(maxHealth);
        }

        public void SetMaxHealth(int maxHealth)
        {
            this.maxHealth = maxHealth;
            CreateHealthBars();
        }

        private void FixedUpdate()
        {
            UpdateHealthBars();
        }

        private void CreateHealthBars()
        {
            ClearHealthBars();

            RectTransform healthBarRectTransform = healthBarPrefab.GetComponent<RectTransform>(); // Retrieve the RectTransform component once

            for (int i = 0; i < maxHealth; i++)
            {
                Vector2 position = new Vector2(0f, i * (barHeight + healthBarSpacing) - ((healthBarHeight - barHeight) / 2)); // Set the position of the health bar to be centered within healthBarContainer
                GameObject healthBar = Instantiate(healthBarPrefab, healthBarContainer);
                healthBarRectTransform.sizeDelta = new Vector2(healthBarWidth, barHeight);
                healthBarRectTransform.anchoredPosition = position;
                healthBar.GetComponent<Image>().enabled = true; // Enable the Image component
                healthBarRectTransform.anchorMin = new Vector2(0.5f, 0.5f); // Set the anchorMin and anchorMax values to center the health bar within healthBarContainer
                healthBarRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                healthBarRectTransform.pivot = new Vector2(0.5f, 0.5f); // Set the pivot to rotate the health bar around the center
            }
        }




        private void UpdateHealthBars()
        {
            int visibleHealthBars = Mathf.Clamp(MiniBossKla_edCtrl.Instance.MinibossKlaEdDamageReciever.HpCurrent, 0, maxHealth);

            for (int i = 0; i < maxHealth; i++)
            {
                GameObject healthBar = healthBarContainer.GetChild(i).gameObject;
                healthBar.SetActive(i < visibleHealthBars);
            }
        }

        private void ClearHealthBars()
        {
            foreach (Transform child in healthBarContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}