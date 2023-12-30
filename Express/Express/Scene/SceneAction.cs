namespace Express.Scene;



public class SceneAction
{
    public enum SceneOperation
    {
        Add,
        Remove
    }
    
    private SceneOperation _operation;
    private object _item;

    public SceneOperation Operation => _operation;

    public object Item => _item;

    public SceneAction(SceneOperation sceneOperation, object item)
    {
        _operation = sceneOperation;
        _item = item;
    }

}