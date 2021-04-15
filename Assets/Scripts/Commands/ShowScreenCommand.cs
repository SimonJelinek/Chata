public class ShowScreenCommand<T> : CommandBase
{
    public override void Execute()
    {
        App.screenManager.Show<T>();
    }
}
