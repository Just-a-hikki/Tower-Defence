using UnityEngine;
using SpaceShooter;

public class TimeLevelCondition : MonoBehaviour, ILevelCondition
{
    [SerializeField] private float timeLimit = 25f;

    private void Start()
    {
        timeLimit += Time.time;
    }
    public bool IsCompleted => Time.time > timeLimit;
}
