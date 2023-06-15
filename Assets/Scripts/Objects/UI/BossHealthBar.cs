using System;
using System.Collections.Generic;
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
        private List<GameObject> healthBars = new List<GameObject>();

        private void Start()
        {
            maxHealth = MiniBossKla_edCtrl.Instance.MinibossKlaEdDamageReciever.HpMax;
            
            SetMaxHealth(maxHealth);
        }

        private void SetMaxHealth(int maxHealth)
        {
            this.maxHealth = maxHealth;
            CreateHealthBars();
        }

        private void Update()
        {
            UpdateHealthBars();
        }

        private void CreateHealthBars()
        {
            ClearHealthBars();
            RectTransform healthBarRectTransform = healthBarPrefab.GetComponent<RectTransform>();
            float healthBarWidth = healthBarContainer.rect.width;
            float healthBarHeight = healthBarContainer.rect.height;
            float barHeight = (healthBarHeight - (healthBarSpacing * (maxHealth - 1))) / maxHealth;
            
            for (int i = 0; i < maxHealth; i++)
            {
                Vector2 position = new Vector2(0f, i * (barHeight + healthBarSpacing) - ((healthBarHeight - barHeight) / 2)); // Set the position of the health bar to be centered within healthBarContainer

                GameObject healthBar;

                if (i < healthBars.Count)
                {
                    healthBar = healthBars[i];
                    healthBar.SetActive(true);
                }
                else
                {
                    healthBar = Instantiate(healthBarPrefab, healthBarContainer);
                    healthBars.Add(healthBar);
                }

                RectTransform barRectTransform = healthBar.GetComponent<RectTransform>();
                barRectTransform.sizeDelta = new Vector2(healthBarWidth, barHeight);
                barRectTransform.anchoredPosition = position;
                healthBar.GetComponent<Image>().enabled = true; // Enable the Image component
                barRectTransform.anchorMin = new Vector2(0.5f, 0.5f); // Set the anchorMin and anchorMax values to center the health bar within healthBarContainer
                barRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                barRectTransform.pivot = new Vector2(0.5f, 0.5f); // Set the pivot to rotate the health bar around the center
            }
        }

        private void UpdateHealthBars()
        {
            int visibleHealthBars = Mathf.Clamp(MiniBossKla_edCtrl.Instance.MinibossKlaEdDamageReciever.HpCurrent, 0, maxHealth);

            for (int i = 0; i < healthBars.Count; i++)
            {
                GameObject healthBar = healthBars[i];
                healthBar.SetActive(i < visibleHealthBars);
            }
        }

        private void ClearHealthBars()
        {
            foreach (GameObject healthBar in healthBars)
            {
                healthBar.SetActive(false);
            }
        }
    }
}