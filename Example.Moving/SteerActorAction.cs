using System;
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
    public class SteerActorAction : Byui.Games.Scripting.Action
    {
        private IKeyboardService _keyboardService;

        public SteerActorAction(IServiceFactory serviceFactory)
        {
            _keyboardService = serviceFactory.GetKeyboardService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
            {
                Actor actor = scene.GetFirstActor("actors");
                // declare direction variables
                int directionX = 0;
                int directionY = 0;
                float rotation = actor.GetRotation();
                

                rotation = (rotation % 360);

                if(rotation < 0){
                    rotation = 360 + rotation;
                }
                
                rotation = rotation *(float) (3.14159/180);

                // determine vertical or y-axis direction
                if (_keyboardService.IsKeyDown(KeyboardKey.W))
                {
                    directionX = (int)Math.Round(Math.Cos(rotation) * 5);
                    directionY = (int)Math.Round(Math.Sin(rotation) * 5);
                    
                    
                }
                else if (_keyboardService.IsKeyDown(KeyboardKey.S))
                {
                    directionY = 5;
                }

                // determine horizontal or x-axis direction
                // if (_keyboardService.IsKeyDown(KeyboardKey.A))
                // {
                //     directionX = -5;
                // }
                // else if (_keyboardService.IsKeyDown(KeyboardKey.D))
                // {
                //     directionX = 5;
                // }

                // steer the actor in the desired direction
                actor.Steer(directionX, directionY);
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't steer actor.", exception);
            }
        }
    }
}