using System;
using System.Numerics;
using System.Collections.Generic;
using Byui.Games.Casting;
using Byui.Games.Scripting;
using Byui.Games.Services;


namespace Example.Scaling
{
    /// <summary>
    /// Detects and resolves collisions between actors.
    /// </summary>
    public class CollideActorsAction : Byui.Games.Scripting.Action
    {
        private IKeyboardService _keyboardService;
        private Random rnd = new Random();

        public CollideActorsAction(IServiceFactory serviceFactory)
        {
            _keyboardService = serviceFactory.GetKeyboardService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
            {
                // get the actors from the cast
                List<Actor> bullets = scene.GetAllActors("bullets");
                List<Actor> meteors = scene.GetAllActors("meteors");
                List<Actor> newMeteors = new List<Actor>{};
                Actor player = scene.GetFirstActor("actors");
                
 
                foreach (Actor meteor in meteors){
                    foreach (Actor bullet in bullets){
                        // detect a collision between the actors.

                        
                        if (bullet.Overlaps(meteor))
                        {
                            scene.RemoveActor("bullets", bullet);
                            scene.RemoveActor("meteors", meteor);

                            float newScale = meteor.GetScale() * (float) .5;
                            if(meteor.GetScale() >= .3){
                                Actor newMeteor1 = new Actor(newVelocity(), meteor.GetPosition());
                                newMeteor1.ScaleTo(newScale);
                                newMeteor1.SizeTo(50, 50);
                                newMeteors.Add(newMeteor1);
                                Actor newMeteor2 = new Actor(newVelocity(), meteor.GetPosition());
                                newMeteor2.ScaleTo(newScale);
                                newMeteor2.SizeTo(50, 50);
                                newMeteors.Add(newMeteor2);
                            }  
                        }
                    }
                    
                    if (player.Overlaps(meteor))
                    {
                        player.SizeTo(0, 0);
                    }
                }

                foreach(Actor meteor in newMeteors)
                {
                    scene.AddActor("meteors", meteor);
                }
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't check if actors collide.", exception);
            }
        }

        private Vector2 newVelocity()
        {
            Vector2 velocity = new Vector2();
            while(velocity.X == 0 && velocity.Y == 0)
            {
                velocity.X = rnd.Next(-4,4);
                velocity.Y = rnd.Next(-4,4);
            }
            
                
            return velocity;
            }
    }
}