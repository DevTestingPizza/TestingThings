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
        private bool frontendActive = false;
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

            if (frontendActive)
            {
                if (Game.IsControlJustPressed(0, Control.FrontendAccept))
                {
                    var _return1 = BeginScaleformMovieMethodN("GET_COLUMN_SELECTION");
                    //var _return1 = BeginScaleformMovieMethodV("GET_COLUMN_SELECTION");
                    PushScaleformMovieMethodParameterInt(0);
                    Debug.Write(EndScaleformMovieMethodReturn().ToString() + "\n");
                    //var _return2 = BeginScaleformMovieMethodN("GET_COLUMN_SELECTION");
                    //PushScaleformMovieFunctionParameterInt(0);
                    ////PushScaleformMovieFunctionParameterInt(1);
                    ////Debug.Write($"Return 1: {_return1.ToString()}\n");
                    ////PushScaleformMovieFunctionN("GET_COLUMN_SELECTION");
                    ////PushScaleformMovieFunctionParameterInt(0);
                    //var _return = PopScaleformMovieFunction();
                    ////Debug.Write($"ret: {N_0x768ff8961ba904d6(_return)}\n");
                    //if (GetScaleformMovieFunctionReturnBool(_return))
                    //{
                    //    var t = GetScaleformMovieFunctionReturnInt(_return);

                    //    Debug.Write(t.ToString() + "\n");
                    //}
                    //else
                    //{
                    //    Debug.Write("No return value(?) :(\n");
                    //}


                }

            }
            if (Game.IsControlJustPressed(0, Control.FrontendSocialClubSecondary))
            {
                ///// RESET THE MENU IN CASE IT ALREADY EXISTS.
                RestartFrontendMenu((uint)GetHashKey("FE_MENU_VERSION_CORONA"), -1);
                //RestartFrontendMenu((uint)GetHashKey("FE_MENU_VERSION_CORONA_RACE"), -1);
                AddFrontendMenuContext(2010410515);
                ObjectDecalToggle(1037243298);
                AddFrontendMenuContext(696397158);

                // If the menu is active, deactivate it.
                if (IsPauseMenuActive())
                {
                    SetPauseMenuActive(false);
                    SetFrontendActive(false);
                    frontendActive = false;
                }
                // Otherwise, activate it.
                else
                {
                    frontendActive = true;
                    ActivateFrontendMenu((uint)GetHashKey("FE_MENU_VERSION_CORONA"), false, -1);
                    //ActivateFrontendMenu((uint)GetHashKey("FE_MENU_VERSION_CORONA_RACE"), false, -1);

                    // start a call
                    while (!IsPauseMenuActive() || IsPauseMenuRestarting())
                    {
                        await Delay(0);
                    }


                    ///// LOADING SCALEFORM AND ACTIVATING IT IN THE MENU?!
                    BeginScaleformMovieMethodV("PAUSE_MENU_PAGES_CORONA_RACE");
                    EndScaleformMovieMethod();


                    ///// PUSH THE DESCRIPTION AND TITLE UP, OTHERWISE IT'S BEHIND THE TOP HEADER BARS.
                    BeginScaleformMovieMethodV("SHIFT_CORONA_DESC"); // BeginScaleformMovieMethodV
                    PushScaleformMovieMethodParameterBool(true); // push up.
                    PushScaleformMovieMethodParameterBool(false); // show extra line
                    EndScaleformMovieMethod();


                    ///// SET THE MENU TITLE.
                    BeginScaleformMovieMethodN("SET_HEADER_TITLE"); // BeginScaleformMovieMethodV
                    BeginTextCommandScaleformString("STRING");
                    AddTextComponentSubstringPlayerName("Sumo");
                    EndTextCommandScaleformString();
                    PushScaleformMovieMethodParameterBool(true);
                    ///// SET THE MENU DESCRIPTION.
                    BeginTextCommandScaleformString("STRING");
                    AddTextComponentSubstringPlayerName("LOBBY - Waiting for players");
                    EndTextCommandScaleformString();
                    ///// UNKNOWN.
                    PushScaleformMovieMethodParameterBool(false);
                    ///// FINISH.
                    EndScaleformMovieMethod();


                    /////// HEADER HUD COLOR (OPTIONAL)
                    //BeginScaleformMovieMethodV("SET_ALL_HIGHLIGHTS"); // BeginScaleformMovieMethodV
                    //PushScaleformMovieMethodParameterBool(true); // if false, will be black/white (high contrast mode)
                    //PushScaleformMovieFunctionParameterInt(136); // HUD_COLOUR_PLATFORM_BLUE (136)
                    //EndScaleformMovieMethod();


                    await Delay(500);

                    ////////////////////////////////////////////////////////////////////////////////////////////
                    for (var x = 0; x < 5; x++)
                    {
                        ///// COLUMN 0 (LEFT) - ROW 0
                        PushScaleformMovieFunctionN("SET_DATA_SLOT");
                        PushScaleformMovieFunctionParameterInt(0); // column
                        PushScaleformMovieFunctionParameterInt(x); // index

                        // com.rockstargames.gtav.pauseMenu.pauseMenuItems.PauseMenuBaseItem::__set__data
                        PushScaleformMovieFunctionParameterInt(0); // menu ID 0
                        PushScaleformMovieFunctionParameterInt(0); // unique ID 0
                        PushScaleformMovieFunctionParameterInt(0); // type 0 (TYPE_MAIN_CHAR_SELECTOR)
                        PushScaleformMovieFunctionParameterInt(0); // initialIndex 1337 (rank)
                        PushScaleformMovieMethodParameterBool(true); // isSelectable true

                        ///// ITEM/ROW TITLE.
                        BeginTextCommandScaleformString("STRING");
                        AddTextComponentSubstringPlayerName(GetPlayerName(PlayerId()) + " " + x);
                        EndTextCommandScaleformString();

                        ///// UNSURE HOW THIS WORKS, BUT IF YOU UNCOMMENT THIS, IT'LL ADD AN ICON TO THE ROW.
                        ///// MAKING THE STRING "20" AND THE BOOL TRUE SEEMS TO DO SOMETHING WITH A ROCKSTAR LOGO INSTEAD.
                        BeginTextCommandScaleformString("STRING");
                        AddTextComponentSubstringPlayerName("");
                        EndTextCommandScaleformString();
                        PushScaleformMovieMethodParameterBool(false); // SOMETHING WITH ROCKSTAR/STAR LOGO SWITCHING.

                        ///// FINISH.
                        EndScaleformMovieMethod();
                    }

                    PushScaleformMovieFunctionN("DISPLAY_DATA_SLOT");
                    PushScaleformMovieFunctionParameterInt(0);
                    EndScaleformMovieMethod();

                    ////////////////////////////////////////////////////////////////////////////////////////////


                    ////////////////////////////////////////////////////////////////////////////////////////////
                    for (var x = 0; x < 5; x++)
                    {
                        ///// COLUMN 0 (LEFT) - ROW 0
                        PushScaleformMovieFunctionN("SET_DATA_SLOT");
                        PushScaleformMovieFunctionParameterInt(1); // column
                        PushScaleformMovieFunctionParameterInt(x); // index

                        PushScaleformMovieFunctionParameterInt(1); // menu ID 0
                        PushScaleformMovieFunctionParameterInt(1); // unique ID 0
                        PushScaleformMovieFunctionParameterInt(1); // type 0 (TYPE_MAIN_CHAR_SELECTOR)
                        PushScaleformMovieFunctionParameterInt(1); // initialIndex 1337 (rank)
                        PushScaleformMovieMethodParameterBool(false); // isSelectable true

                        ///// ITEM/ROW TITLE.
                        BeginTextCommandScaleformString("STRING");
                        AddTextComponentSubstringPlayerName("Item " + x);
                        EndTextCommandScaleformString();

                        ///// UNSURE HOW THIS WORKS, BUT IF YOU UNCOMMENT THIS, IT'LL ADD AN ICON TO THE ROW.
                        ///// MAKING THE STRING "20" AND THE BOOL TRUE SEEMS TO DO SOMETHING WITH A ROCKSTAR LOGO INSTEAD.
                        BeginTextCommandScaleformString("STRING");
                        AddTextComponentSubstringPlayerName("");
                        EndTextCommandScaleformString();
                        PushScaleformMovieMethodParameterBool(false); // SOMETHING WITH ROCKSTAR/STAR LOGO SWITCHING.

                        ///// FINISH.
                        EndScaleformMovieMethod();
                    }

                    PushScaleformMovieFunctionN("DISPLAY_DATA_SLOT");
                    PushScaleformMovieFunctionParameterInt(1);
                    EndScaleformMovieMethod();

                    ////////////////////////////////////////////////////////////////////////////////////////////

                    ////////////////////////////////////////////////////////////////////////////////////////////
                    for (var x = 0; x < 5; x++)
                    {
                        ///// COLUMN 0 (LEFT) - ROW 0
                        PushScaleformMovieFunctionN("SET_DATA_SLOT");
                        PushScaleformMovieFunctionParameterInt(3); // column
                        PushScaleformMovieFunctionParameterInt(x); // index

                        //// com.rockstargames.gtav.pauseMenu.pauseMenuItems.PauseMenuBaseItem::__set__data
                        //PushScaleformMovieFunctionParameterInt(0); // menu ID 0
                        //PushScaleformMovieFunctionParameterInt(0); // unique ID 0
                        //PushScaleformMovieFunctionParameterInt(0); // type 0 (TYPE_MAIN_CHAR_SELECTOR)
                        //PushScaleformMovieFunctionParameterInt(0); // initialIndex 1337 (rank)
                        //PushScaleformMovieMethodParameterBool(true); // isSelectable true

                        /////// ITEM/ROW TITLE.
                        //BeginTextCommandScaleformString("STRING");
                        //AddTextComponentSubstringPlayerName(GetPlayerName(PlayerId()) + " " + x);
                        //EndTextCommandScaleformString();

                        /////// UNSURE HOW THIS WORKS, BUT IF YOU UNCOMMENT THIS, IT'LL ADD AN ICON TO THE ROW.
                        /////// MAKING THE STRING "20" AND THE BOOL TRUE SEEMS TO DO SOMETHING WITH A ROCKSTAR LOGO INSTEAD.
                        //BeginTextCommandScaleformString("STRING");
                        //AddTextComponentSubstringPlayerName("");
                        //EndTextCommandScaleformString();
                        //PushScaleformMovieMethodParameterBool(true); // SOMETHING WITH ROCKSTAR/STAR LOGO SWITCHING.

                        /////// FINISH.
                        //EndScaleformMovieMethod();
                        // com.rockstargames.gtav.pauseMenu.pauseMenuItems.PauseMenuBaseItem::__set__data
                        PushScaleformMovieFunctionParameterInt(0); // menu ID 0
                        PushScaleformMovieFunctionParameterInt(0); // unique ID 0
                        PushScaleformMovieFunctionParameterInt(2); // type 2 (AS_ONLINE_IN_SESSION)
                        PushScaleformMovieFunctionParameterInt(1337); // initialIndex 1337 (rank)
                        PushScaleformMovieFunctionParameterBool(true); // isSelectable true

                        // data, see com.rockstargames.gtav.pauseMenu.pauseMenuItems.multiplayer.PauseMPMenuFriendsListItem
                        BeginTextCommandScaleformString("STRING");
                        AddTextComponentSubstringPlayerName("whoa");
                        EndTextCommandScaleformString();

                        PushScaleformMovieFunctionParameterInt(134); // color

                        PushScaleformMovieFunctionParameterBool(true);

                        PushScaleformMovieFunctionParameterInt(0); // unused

                        PushScaleformMovieFunctionParameterInt(65); // RANK_FREEMODE

                        PushScaleformMovieFunctionParameterInt(0); // unused

                        PushScaleformMovieFunctionParameterString("|*+ROFL");

                        PushScaleformMovieFunctionParameterBool(false); // 'kick'


                        BeginTextCommandScaleformString("STRING");
                        AddTextComponentSubstringPlayerName("cat food");
                        EndTextCommandScaleformString();

                        PushScaleformMovieFunctionParameterInt(9);

                        // statusStr
                        // statusColID

                        PopScaleformMovieFunctionVoid();
                    }

                    PushScaleformMovieFunctionN("DISPLAY_DATA_SLOT");
                    PushScaleformMovieFunctionParameterInt(3);
                    EndScaleformMovieMethod();

                    ////////////////////////////////////////////////////////////////////////////////////////////


                    ///// ACTIVATE THE FIRST COLUMN (FOCUS).
                    PushScaleformMovieFunctionN("SET_COLUMN_FOCUS");
                    PushScaleformMovieFunctionParameterInt(0); // column index // _loc7_
                    PushScaleformMovieFunctionParameterInt(1); // highlightIndex // _loc6_
                    PushScaleformMovieFunctionParameterInt(1); // scriptSetUniqID // _loc4_
                    PushScaleformMovieFunctionParameterInt(0); // scriptSetMenuState // _loc5_
                    EndScaleformMovieMethod();

                }
            }
        }




    }


}
