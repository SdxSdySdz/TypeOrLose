public class ReadyTransition : Transition
{
    private void Update()
    {
        if (Race.AreAllRunnersReady)
        {
            IsPossible = true;
        }
    }
}
