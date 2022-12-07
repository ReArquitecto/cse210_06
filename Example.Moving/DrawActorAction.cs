using System;
using System.Collections.Generic;
using Byui.Games.Casting;
using Byui.Games.Scripting;
using Byui.Games.Services;


namespace Example.Scaling
{
    /// <summary>
    /// Draws the actors on the screen.
    /// </summary>
    public class DrawActorAction : Byui.Games.Scripting.Action
    {
        private IVideoService _videoService;

        public DrawActorAction(IServiceFactory serviceFactory)
        {
            _videoService = serviceFactory.GetVideoService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
            {
                // get the actors from the cast
                Label label = (Label) scene.GetFirstActor("labels");
                Actor actor = scene.GetFirstActor("actors");
                List<Actor> bullets = scene.GetAllActors("bullets");
                List<Actor> meteors = scene.GetAllActors("meteors");
                
                // draw the actors on the screen using the video service
                _videoService.ClearBuffer();
                _videoService.Draw(label);
                foreach (Actor bullet in bullets)
                {
                    _videoService.Draw(bullet);
                }
                foreach (Actor meteor in meteors)
                {
                    _videoService.Draw(meteor);
                }
                _videoService.Draw(actor);
                _videoService.FlushBuffer();
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't draw actors.", exception);
            }
        }
    }
}