public class AnyRunnersTransition : Transition
{
    private void Update()
    {
        if (Race.RunnersCount > 0)
            IsPossible = true;
    }
}
