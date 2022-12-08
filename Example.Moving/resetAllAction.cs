using System;
using System.Numerics;
using System.Collections.Generic;
using Byui.Games.Casting;
using Byui.Games.Scripting;
using Byui.Games.Services;


namespace Example.Scaling
{
    /// <summary>
    /// Rotates an actor left or right based on keyboard input.
    /// </summary>
    public class ResetAllAction : Byui.Games.Scripting.Action
    {
        private IKeyboardService _keyboardService;

        private int resetTimer = 0;

        public ResetAllAction(IServiceFactory serviceFactory)
        {
            _keyboardService = serviceFactory.GetKeyboardService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            
            resetTimer += 1;
           
            if (_keyboardService.IsKeyDown(KeyboardKey.R) && resetTimer > 20)
            {
                resetTimer = 0;
                
                
                this.reset(scene, "meteors");
                this.reset(scene, "actors");
                this.reset(scene, "bullets");
                // this.reset(scene, "labels");

                this.add(scene, "actors");
                this.add(scene, "meteors");
                this.add(scene, "meteors");
                this.add(scene, "meteors");
            }

        }

        private void reset(Scene scene, String actorType)
        {
            List<Actor> Actors = scene.GetAllActors(actorType);

            foreach(Actor actor in Actors)
            {
                scene.RemoveActor(actorType, actor);
            }


        }

        private void add(Scene scene, String actorType)
        {

            Actor actor = new Actor();
            

            if(actorType == "meteors"){

                actor.SizeTo(50, 50);
                Random rnd = new Random();

                actor.MoveTo(rnd.Next(50, 1030), rnd.Next(0,500));
                actor.Steer((float) rnd.Next(1,3), (float) rnd.Next(1,3));

                Vector2 velocity = new Vector2();
                while(velocity.X == 0 && velocity.Y == 0)
                {
                    velocity.X = rnd.Next(-4,4);
                    velocity.Y = rnd.Next(-4,4);
                }
            }
            else if(actorType == "actors")
            {
                actor.SizeTo(50, 20);
                actor.MoveTo(0, 275);
                actor.Tint(Color.Blue());
            }

            scene.AddActor(actorType, actor);
        }
    }
}