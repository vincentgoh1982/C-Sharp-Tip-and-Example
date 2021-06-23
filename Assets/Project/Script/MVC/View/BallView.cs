
// Describes the Ball view and its features.
public class BallView : BounceElement
{
    // Only this is necessary. Physics is doing the rest of work.
    // Callback called upon collision.
    void OnCollisionEnter() { app.controller.OnBallGroundHit(); }
}
