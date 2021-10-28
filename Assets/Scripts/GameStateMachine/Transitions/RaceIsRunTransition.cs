public class RaceIsRunTransition : Transition
{
    private void Update()
    {
        if (Race.IsRun)
            IsPossible = true;
    }
}
