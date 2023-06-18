using Enemy;
using UnityEngine;

public class MiniBossNautolanChasePlayerSMB : AbsChasePlayerSMB
{
    protected override int GetNumberOfAttacks() => MinibossNautolanCtrl.Instance.GetNumberOfAttacksChaseBehaviour();
    
    protected override float GetSpeedChase() => MinibossNautolanCtrl.Instance.GetSpeedChase();

    protected override void SetShipModelAnimationChase(bool isOn) 
        => MinibossNautolanCtrl.Instance.MinibossNautolanModelShipAnimation.SetIsChasePlayer(isOn);

    protected override void UnSetAnimation() => MinibossNautolanCtrl.Instance.SetIsChasePlayer(false);

    protected override Vector3 GetCameraPosition() => MinibossNautolanCtrl.Instance.GetCameraPosition();
    
    protected override void ResetOutVector()
    {
        if (this.outVector != Vector3.zero) return;
        float getRandomX = Random.Range(-8f, 8f);
        float getRandomY = Random.Range(-17f, -13f);
        this.outVector = new Vector3(getRandomX, getRandomY);
    }
}
