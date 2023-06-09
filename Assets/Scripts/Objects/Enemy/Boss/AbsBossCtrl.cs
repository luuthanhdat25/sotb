using UnityEngine;

namespace Enemy.Boss
{
    public abstract class AbsBossCtrl : RepeatMonoBehaviour
    {
        protected Vector3 defaultPosition;
        [SerializeField] protected Transform mainCam;
        [SerializeField] protected Animator bossSMBAnimator;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadCamera();
            this.LoadAnimator();
        }

        protected virtual void LoadCamera()
        {
            if (this.mainCam != null) return;
            this.mainCam = GameObject.FindObjectOfType<Camera>().transform;
            Debug.Log(transform.name + " Load: bossSMBAnimator" );
        }

        protected virtual void LoadAnimator()
        {
            if (this.bossSMBAnimator != null) return;
            this.bossSMBAnimator = GetComponent<Animator>();
            Debug.Log(transform.name + " Load: bossSMBAnimator" );
        }
        
        public abstract void SetDeadAnimation();
        public virtual Vector3 GetCameraPosition() => this.mainCam.position;
        public virtual Vector3 GetDefaultPosition() => this.defaultPosition;
    }
}