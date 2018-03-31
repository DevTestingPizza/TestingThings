using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using Newtonsoft.Json;

namespace TestingThings
{
    public class Spawns : BaseScript
    {
        public object GetSpawns()
        {
            return Exports["spawnmanager"].getSpawns();
        }
    }

    public class Class1 : BaseScript
    {
        

        public static int ScreenWidth = 1920;
        public static int ScreenHeight = 1080;
        public Class1()
        {
            Tick += OnTick;
            //Tick += Gfx.DrawLobbyOverlay;
            RequestScriptAudioBank("DLC_STUNT/STUNT_RACE_01", false);
            RequestScriptAudioBank("DLC_STUNT/STUNT_RACE_02", false);
            RequestScriptAudioBank("DLC_STUNT/STUNT_RACE_03", false);
            RequestAdditionalText("RACES", 0);
            StatSetInt((uint)GetHashKey("MP0_STAMINA"), 100, true);
            StatSetInt((uint)GetHashKey("MP0_STRENGTH"), 100, true);
            StatSetInt((uint)GetHashKey("MP0_LUNG_CAPACITY"), 100, true);
            StatSetInt((uint)GetHashKey("MP0_WHEELIE_ABILITY"), 100, true);
            StatSetInt((uint)GetHashKey("MP0_FLYING_ABILITY"), 100, true);
            StatSetInt((uint)GetHashKey("MP0_SHOOTING_ABILITY"), 100, true);
            StatSetInt((uint)GetHashKey("MP0_STEALTH_ABILITY"), 100, true);
            StopPlayerSwitch();
        }
        //20x40

        private async Task OnTick()
        {
            if (Game.IsControlJustPressed(0, (Control)166))
            {
                Gfx.ToggleLimbo();
            }
            ScreenWidth = CitizenFX.Core.UI.Screen.Resolution.Width;
            ScreenHeight = CitizenFX.Core.UI.Screen.Resolution.Height;
            Gfx.DrawStatBar("TIME", "00:00", 0);
            var i = 1;
            foreach (Player p in new PlayerList())
            {
                Gfx.DrawStatBar($"~HUD_COLOUR_NET_PLAYER{i}~{p.Name}", $"(player)", i);
            }
            if (ReturnTwo(2) != 2)
            {
                await Delay(0);
            }
            if (Gfx.LimboActive)
            {
                //DisableAllControlActions(0);
                Game.DisableAllControlsThisFrame(0);
                Game.EnableControlThisFrame(0, Control.MpTextChatAll);
                Game.EnableControlThisFrame(0, Control.FrontendPause);
            }
        }




    }


}
