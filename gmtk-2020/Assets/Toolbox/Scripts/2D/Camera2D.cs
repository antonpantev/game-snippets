using UnityEngine;

namespace Toolbox
{
    /*  Future Improvements
     *    Make a 3D version of this class
     *    
     *    Use Trauma instead of a linear shake amount
     *      trauma ranges from 0 to 1
     *      damage adds trauma (+= 0.2 or 0.5)
     *      trauma decreases over time linearly
     *      cameraShake = trauma ^ 2 (or ^ 3)
     *    
     *    Shake In 2D
     *      angle = maxAngle * shake * GetRandomFloatNegOneToOne();
     *      offsetX = maxOffset * shake * GetRandomFloatNegOneToOne();
     *      offsetY = maxOffset * shake * GetRandomFloatNegOneToOne();
     *  
     *      shakyCamera.angle = camera.angle + angle;
     *      shakyCamera.center = camera.center + Vec2(offsetX, offsetY);
     *    
     *    Shake in 3D (no translational shake!)
     *      yaw = maxYaw * shake * GetRandomFloatNegOneToOne();
     *      pitch = maxPitch * shake * GetRandomFloatNegOneToOne();
     *      roll = maxRoll * shake * GetRandomFloatNegOneToOne();
     *  
     *    Use Perlin Noise instead of GetRandomFloatNegOneToOne (not sure if I agree with this)
     *
     *  Math for Game Programmers: Juicing Your Cameras With Math - https://www.youtube.com/watch?v=tu-Qe66AvtY
     */
    public class Camera2D : CameraFollow
    {
        float shakeAmount;
        float shakeEnd = Mathf.NegativeInfinity;

        public override void LateUpdate()
        {
            Vector3 pos = GetSmoothedPosition();

            if (shakeEnd >= Time.time)
            {
                pos += Random.insideUnitSphere * shakeAmount;
            }

            transform.position = pos;
        }

        public void Shake()
        {
            Shake(0.7f, 0.5f);
        }

        public void Shake(float intensity, float dur)
        {
            shakeAmount = intensity;
            shakeEnd = Time.time + dur;
        }
    }
}