using System;
using System.Collections.Generic;
using Byui.Games.Casting;
using Byui.Games.Scripting;
using Byui.Games.Services;


namespace Example.Scaling
{
    /// <summary>
    /// Moves the actors and wraps them around the screen boundaries. Note, this is different from
    /// steering them which only changes their direction. The call to actor.Move() is what updates
    /// their position on the screen.
    /// </summary>
    public class MoveActorAction : Byui.Games.Scripting.Action
    {
        private IKeyboardService _keyboardService;

        public MoveActorAction(IServiceFactory serviceFactory)
        {
            _keyboardService = serviceFactory.GetKeyboardService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
            {
                // get the actors from the scene
                Actor actor = scene.GetFirstActor("actors");
                List<Actor> bullets = scene.GetAllActors("bullets");
                List<Actor> meteors = scene.GetAllActors("meteors");
                Actor screen = scene.GetFirstActor("screen");
                
                
                // move the actor and wrap it around the screen boundaries, bullets do not wrap
                foreach (Actor bullet in bullets)
                {  
                    bullet.Move();
                }
                foreach (Actor meteor in meteors)
                {
                    meteor.Move();
                    meteor.WrapIn(screen);
                }
                actor.Move();
                actor.WrapIn(screen);

                

            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't move actor.", exception);
            }
        }
    }
}