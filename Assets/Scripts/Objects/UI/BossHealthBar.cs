using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Objects.UI
{
    public class BossHealthBar : MonoBehaviour
    {
        public int healthTest = 80;
        public float healthBarSpacing = 3.5f;
        public GameObject BossGameobject;
        public RectTransform healthBarContainer;
        public GameObject healthBarPrefab;
    
        private int maxHealth;
        private float healthBarWidth;
        private float healthBarHeight;
        private float barHeight;
        private void Start()
        {
            healthBarWidth = healthBarContainer.rect.width;
            healthBarHeight = healthBarContainer.rect.height;
            barHeight = (healthBarHeight - (healthBarSpacing * (maxHealth - 1))) / maxHealth;
            SetMaxHealth(healthTest);
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

            for (int i = 0; i < maxHealth; i++)
            {
                Vector2 position = new Vector2(0f, i * (barHeight + healthBarSpacing) - ((healthBarHeight - barHeight) / 2)); // Đặt vị trí của thanh máu để nằm ở giữa healthBarContainer
                GameObject healthBar = Instantiate(healthBarPrefab, healthBarContainer);
                RectTransform healthBarRectTransform = healthBar.GetComponent<RectTransform>();
                healthBarRectTransform.sizeDelta = new Vector2(healthBarWidth, barHeight);
                healthBarRectTransform.anchoredPosition = position;
                healthBar.GetComponent<Image>().enabled = true; // Bật thành phần Image
                healthBarRectTransform.anchorMin = new Vector2(0.5f, 0.5f); // Đặt giá trị anchorMin và anchorMax để thanh máu nằm ở giữa healthBarContainer
                healthBarRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                healthBarRectTransform.pivot = new Vector2(0.5f, 0.5f); // Đặt pivot để thanh máu xoay quanh trung tâm
            }
        }



        private void UpdateHealthBars()
        {
            int visibleHealthBars = Mathf.Clamp(healthTest, 0, maxHealth);

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