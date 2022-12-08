using System;
using Byui.Games.Casting;
using Byui.Games.Directing;
using Byui.Games.Scripting;
using Byui.Games.Services;


namespace Example.Scaling
{
    /// <summary>
    /// The entry point for the program.
    /// </summary>
    /// <remarks>
    /// The purpose of this program is to demonstrate how Actors, Actions, Services and a Director 
    /// work together to move an actor on the screen.
    /// </remarks>
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Instantiate a service factory for other objects to use.
            IServiceFactory serviceFactory = new RaylibServiceFactory();

            Random rnd = new Random();

            // Instantiate the actors that are used in this example.
            Label label = new Label();
            
            Actor actor = new Actor();
            actor.SizeTo(50, 20);
            actor.MoveTo(0, 275);
            actor.Tint(Color.Blue());

            Actor meteor1 = new Actor();
            meteor1.SizeTo(50, 50);
            meteor1.MoveTo(rnd.Next(50, 1030), rnd.Next(0,4500));
            meteor1.Steer((float) rnd.Next(1,3), (float) rnd.Next(1,3));

            Actor meteor2 = new Actor();
            meteor2.SizeTo(50, 50);
            meteor2.MoveTo(rnd.Next(50, 1030), rnd.Next(0, 450));
            meteor2.Steer((float) -rnd.Next(1,3), (float) -rnd.Next(1,3));

            Actor meteor3 = new Actor();
            meteor3.SizeTo(50, 50);
            meteor3.MoveTo(rnd.Next(50, 1030), rnd.Next(0, 450));
            meteor3.Steer((float) rnd.Next(1,3), (float)  -rnd.Next(1,3));

            Actor screen = new Actor();
            screen.SizeTo(1080, 550);
            screen.MoveTo(0, 0);

            // Instantiate the actions that use the actors.
            SteerActorAction steerActorAction = new SteerActorAction(serviceFactory);
            RotateActorAction rotateActorAction = new RotateActorAction(serviceFactory);
            MoveActorAction moveActorAction = new MoveActorAction(serviceFactory);
            DrawActorAction drawActorAction = new DrawActorAction(serviceFactory);
            ShootAction shootAction = new ShootAction(serviceFactory);
            ResetAllAction resetAllAction = new ResetAllAction(serviceFactory);
            CollideActorsAction collideAction = new CollideActorsAction(serviceFactory);            
            // Instantiate a new scene, add the actors and actions.
            Scene scene = new Scene();
            scene.AddActor("actors", actor);
            scene.AddActor("labels", label);
            scene.AddActor("screen", screen);
            scene.AddActor("meteors", meteor1);
            scene.AddActor("meteors", meteor2);
            scene.AddActor("meteors", meteor3);

            scene.AddAction(Phase.Input, steerActorAction);
            scene.AddAction(Phase.Input, rotateActorAction);
            scene.AddAction(Phase.Input, shootAction);
            scene.AddAction(Phase.Input, resetAllAction);
            scene.AddAction(Phase.Update, moveActorAction);
            scene.AddAction(Phase.Output, drawActorAction);
            scene.AddAction(Phase.Output, collideAction);

            // Start the game.
            Director director = new Director(serviceFactory);
            director.Direct(scene);
        }
    }
}
