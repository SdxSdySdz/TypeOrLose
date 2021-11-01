public class AllReadyTransition : Transition
{
    private void Update()
    {
        if (Race.AreAllRunnersReady)
        {
            IsPossible = true;
        }
    }
}