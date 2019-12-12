#if !WINDOWS_UWP
using HoloForge.Holo;

public class LongGazeDistanceBehaviour : HoloBehaviour, IGazeComponent
{
    [GazeComponent]
    public GazeComponent gazeComponent;

    [AnimatorComponent]
    public AnimatorComponent animatorComponent;

    public float maxDistance = 0.0f;

    string action;

    public void OnGazeEnded(HoloGameObject _target, HoloGameObject _collider)
    {
        Engine.Async.CancelInvoke(action);
        animatorComponent.SetBoolParameter("Gaze", false);
    }

    public void OnGazeStarted(HoloGameObject _target, HoloGameObject _collider)
    {
        animatorComponent.SetBoolParameter("Gaze", true);
    }

    public void OnGazeTapped(HoloGameObject _target, HoloGameObject _collider)
    {
    }

    public void OnLongGazeEnded(HoloGameObject _target, HoloGameObject _collider)
    {
        action = Engine.Async.InvokeOnCondition(CheckDistance, OnLongGaze);
    }

    private bool CheckDistance()
    {
        float distance = Engine.GetUserDistanceFrom(gazeComponent.Target.Transform.Position);
        return distance < maxDistance;
    }

    private void OnLongGaze()
    {
        animatorComponent.SetBoolParameter("LongGaze", true);
    }
}
#endif