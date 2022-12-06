using System;
using System.Numerics;
using Byui.Games.Casting;
using Byui.Games.Scripting;
using Byui.Games.Services;


namespace Example.Scaling
{
    /// <summary>
    /// Steers an actor in a direction corresponding to keyboard input. Note, this does not update 
    /// the actor's position, just steers it in a certain direction. See MoveActorAction to see how
    /// the actor's position is actually updated.
    /// </summary>
    public class ShootAction : Byui.Games.Scripting.Action
    {
        private IKeyboardService _keyboardService;

        public ShootAction(IServiceFactory serviceFactory)
        {
            _keyboardService = serviceFactory.GetKeyboardService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
            {
                Actor player = scene.GetFirstActor("actors");


                
                
                

                if (_keyboardService.IsKeyDown(KeyboardKey.Space))
                {
                    Vector2 playerVelocity = player.GetVelocity();
                    Vector2 playerPosition = player.GetPosition();

                    Actor bullet = new Actor(playerVelocity, playerPosition);
                    bullet.SizeTo(10, 10);
                    scene.AddActor("bullets", bullet);
                }
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't shoot with actor.", exception);
            }
        }
    }
}