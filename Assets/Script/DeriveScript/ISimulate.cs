using UnityEngine;

interface ISimulate
{
    /// <summary>
    /// シミュレーション速度を変化させる関数
    /// </summary>
    /// <param name="speed"> シミュレーション速度（デフォルトは１）</param>
    public void SimulateChange(float speed = 1);
}
