namespace Express.Scene.Objects;

public interface ISceneUser
{
    IScene Scene { get; set; }

    void AddedToScene(IScene scene);
    void RemovedFromScene(IScene scene);
}