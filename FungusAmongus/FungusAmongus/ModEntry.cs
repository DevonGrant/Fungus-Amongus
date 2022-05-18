using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Objects;
using StardewValley.Quests;
using xTile;

namespace FungusAmongus
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        bool questComplete = false;
        Quest questRef = null;
        Boots speedShoes;

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.UpdateTicked += this.Update;
        }

        /*********
        ** Private methods
        *********/

        //Running an update function every tick (~60 ticks per second) to check for relevant information
        private void Update(object sender, UpdateTickedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            //Creating the new shoe asset and replacing the information from the id provided
            speedShoes = new Boots(515);
            speedShoes.DisplayName = "Speed Shoes";
            speedShoes.description = "Shoes that make you go fast!";

            //Looping through the player's quest log and seeing if a specific quest is present
            for (int i = 0; i < Game1.player.questLog.Count; i++)
            {
                if (Game1.player.questLog[i].GetDescription() == "The Chief just needs a couple battery packs and he'll be all set.")
                {
                    questRef = Game1.player.questLog[i];
                }
            }

            //Checking if the player has completed the specific quest and giving them their reward
            if (questRef != null)
            {
                if (questRef.completed && !questComplete)
                {
                    questComplete = true;
                    Game1.player.addItemToInventory(speedShoes);
                }
            }

            //Modifying the player's speed if they are wearing the speed shoes
            if (Game1.player.boots.Get() != null)
            {
                if (Game1.player.boots.Get().displayName == "Speed Shoes")
                {
                    Game1.player.speed = 7;
                }
            }
        }
    }
}
