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
        private int shootTimer = 0;

        public ShootAction(IServiceFactory serviceFactory)
        {
            _keyboardService = serviceFactory.GetKeyboardService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            // creates a bullet actor with a slightly faster speed than the players
            // moving speed. creates the bullet in the middle of the player.
            
            try
            {
                Actor player = scene.GetFirstActor("actors");
                Vector2 playerSize = player.GetSize();

                //ensures the player cant just fire a stream of bullets
                shootTimer += 1;

                if(playerSize.X != 0 && playerSize.Y != 0)
                {
                    if (_keyboardService.IsKeyDown(KeyboardKey.Space) & shootTimer >= 20)
                    {
                        
                        shootTimer = 0;

                        float playerRotation = player.GetRotation() % 360;

                        if(playerRotation < 0){
                            playerRotation = 360 + playerRotation;
                        }

                        playerRotation = playerRotation *(float) (3.14159/180);

                        int directionX = (int)Math.Round(Math.Cos(playerRotation) * 7);
                        int directionY = (int)Math.Round(Math.Sin(playerRotation) * 7);

                        Vector2 playerVelocity = new Vector2(directionX, directionY);
                        Vector2 playerPosition = player.GetPosition();
                        //need to make X and Y based on rotation or put in center again
                        playerPosition.X = playerPosition.X + player.GetSize().X / 2;
                        playerPosition.Y = playerPosition.Y + player.GetSize().Y / 2;

                        Actor bullet = new Actor(playerVelocity, playerPosition);
                        bullet.SizeTo(10, 10);
                        scene.AddActor("bullets", bullet);
                        shootTimer = 0;
                    }
                }
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't shoot with actor.", exception);
            }
        }
    }
}