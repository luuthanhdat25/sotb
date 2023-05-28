using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public abstract class ResourcePlayerBar : RepeatMonoBehaviour
    {
        [SerializeField] protected List<Transform> images;
        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadListImage();
        }

        protected void LoadListImage()
        {
            if (this.images.Count > 0) return;
            foreach (Transform image in transform)
            {
                this.images.Add(image);
            }
        }

        private void FixedUpdate()
        {
            this.UpdateBarStatus();
        }

        protected abstract void UpdateBarStatus();
    }
}