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
                if (Game.IsControlJustPressed(0, Control.FrontendAccept) || Game.IsControlJustPressed(0, Control.PhoneSelect))
                {
                    await Delay(100);
                    //var _return1 = BeginScaleformMovieMethodN("GET_COLUMN_SELECTION");
                    //var _return1 = BeginScaleformMovieMethodN("GET_COLUMN_SELECTION");
                    //var result1 = BeginScaleformMovieMethod(GetPauseMenuState(), "GET_COLUMN_SELECTION");
                    //PushScaleformMovieMethodParameterInt(0);
                    //Debug.Write(EndScaleformMovieMethodReturn().ToString() + "\n");
                    //var pop = PopScaleformMovieFunction();
                    //Debug.Write(GetScaleformMovieFunctionReturnBool(pop).ToString() + "\n");
                    //Debug.Write(GetScaleformMovieFunctionReturnInt(pop).ToString() + "\n");
                    //Debug.Write(pop.ToString() + "\n");
                    //Debug.Write(result1.ToString() + "\n");
                    Game.PlaySound("SELECT", "HUD_FRONTEND_DEFAULT_SOUNDSET");


                    //PlaySoundFrontend(-1, "SELECT", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);
                    PushScaleformMovieFunctionN("GET_COLUMN_SELECTION");
                    PushScaleformMovieMethodParameterInt(0);
                    var res = EndScaleformMovieMethodReturn();
                    var timer = GetGameTimer();
                    while (!GetScaleformMovieFunctionReturnBool(res))
                    {
                        await Delay(0);
                        if (GetGameTimer() - timer > 1000)
                        {
                            Debug.WriteLine("Took too long");
                            break;
                        }
                    }
                    var resInt = GetScaleformMovieFunctionReturnInt(res);
                    Debug.Write($"{resInt}\n");
                    //else
                    //{
                    //    var resInt = GetScaleformMovieFunctionReturnInt(res);
                    //    Debug.Write($"No Return value: {resInt}\n");
                    //}



                }

            }
            if (Game.IsControlJustPressed(0, Control.FrontendSocialClubSecondary))
            {
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
                    ///// RESET THE MENU IN CASE IT ALREADY EXISTS.
                    RestartFrontendMenu((uint)GetHashKey("FE_MENU_VERSION_CORONA"), -1);
                    //RestartFrontendMenu((uint)GetHashKey("FE_MENU_VERSION_CORONA"), -1);
                    //RestartFrontendMenu((uint)GetHashKey("FE_MENU_VERSION_CORONA_RACE"), -1);
                    AddFrontendMenuContext((uint)GetHashKey("FM_TUTORIAL"));
                    AddFrontendMenuContext((uint)GetHashKey("AUTOFILL_CORONA"));
                    AddFrontendMenuContext((uint)GetHashKey("CORONA_TOURNAMENT"));
                    AddFrontendMenuContext((uint)GetHashKey("AUTOFILL_CONTINUE"));

                    AddFrontendMenuContext(2010410515);
                    ObjectDecalToggle((uint)Int64.Parse("-228602367"));
                    //ObjectDecalToggle(1037243298);
                    //AddFrontendMenuContext(696397158);

                    ActivateFrontendMenu((uint)GetHashKey("FE_MENU_VERSION_CORONA"), false, -1);
                    //ActivateFrontendMenu((uint)GetHashKey("FE_MENU_VERSION_CORONA"), false, -1);
                    //ActivateFrontendMenu((uint)GetHashKey("FE_MENU_VERSION_CORONA_RACE"), false, -1);

                    // start a call
                    while (!IsPauseMenuActive() || IsPauseMenuRestarting())
                    {
                        await Delay(0);
                    }

                    AddFrontendMenuContext((uint)GetHashKey("FM_TUTORIAL"));
                    AddFrontendMenuContext((uint)GetHashKey("AUTOFILL_CORONA"));
                    AddFrontendMenuContext((uint)GetHashKey("CORONA_TOURNAMENT"));
                    AddFrontendMenuContext((uint)GetHashKey("AUTOFILL_CONTINUE"));

                    frontendActive = true;


                    ///// LOADING SCALEFORM AND ACTIVATING IT IN THE MENU?!
                    //BeginScaleformMovieMethodV("PAUSE_MENU_PAGES_CORONA_RACE");
                    //EndScaleformMovieMethod();


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
                    PushScaleformMovieMethodParameterBool(false); // unknown, (confirmed bool), but is 0 (false) in decompiled scripts
                    ///// SET THE MENU DESCRIPTION.
                    BeginTextCommandScaleformString("STRING");
                    AddTextComponentSubstringPlayerName("LOBBY - Waiting for players");
                    EndTextCommandScaleformString();
                    ///// UNKNOWN.
                    PushScaleformMovieMethodParameterBool(true); // uknown, (confirmed bool), but is 1 (true) in decompiled scripts
                    ///// FINISH.
                    EndScaleformMovieMethod(); // _POP_SCALEFORM_MOVIE_FUNCTION_VOID


                    /////// HEADER HUD COLOR (OPTIONAL)
                    //BeginScaleformMovieMethodV("SET_ALL_HIGHLIGHTS"); // BeginScaleformMovieMethodV
                    //PushScaleformMovieMethodParameterBool(true); // if false, will be black/white (high contrast mode)
                    //PushScaleformMovieMethodParameterInt(136); // HUD_COLOUR_PLATFORM_BLUE (136)
                    //EndScaleformMovieMethod();


                    await Delay(500);

                    ////////////////////////////////////////////////////////////////////////////////////////////
                    SetColumnSettingsRow(0, "Select a vehicle", "0", -1, true, HudColors.HudColor.HUD_COLOUR_ADVERSARY);
                    SetColumnSettingsRow(1, "Vote for a map", "0", -1, true, HudColors.HudColor.HUD_COLOUR_ADVERSARY);
                    SetColumnSettingsRow(2, "Quit to server list", "0", -1, true, HudColors.HudColor.HUD_COLOUR_ADVERSARY);
                    SetColumnSettingsRow(3, "Exit FiveM", "0", -1, true, HudColors.HudColor.HUD_COLOUR_ADVERSARY);

                    //BeginScaleformMovieMethodN("INIT_SCROLL_BAR");
                    //PushScaleformMovieMethodParameterBool(true);
                    //PushScaleformMovieMethodParameterInt(3);
                    //PushScaleformMovieMethodParameterInt(1);
                    //PushScaleformMovieMethodParameterInt(1);
                    //PushScaleformMovieMethodParameterInt(0);
                    //PushScaleformMovieMethodParameterInt(0);
                    //PopScaleformMovieFunctionVoid();

                    //BeginScaleformMovieMethodN("SET_SCROLL_BAR");
                    //PushScaleformMovieMethodParameterInt(2);
                    //PushScaleformMovieMethodParameterInt(5);
                    //PushScaleformMovieMethodParameterInt(1);
                    //PushScaleformMovieMethodParameterString("te");
                    //PopScaleformMovieFunctionVoid();
                    //for (var x = 0; x < 5; x++)
                    //{
                    //    ///// COLUMN 0 (LEFT) - ROW 0
                    //    PushScaleformMovieFunctionN("SET_DATA_SLOT");
                    //    PushScaleformMovieMethodParameterInt(0); // column
                    //    PushScaleformMovieMethodParameterInt(x); // index

                    //    // com.rockstargames.gtav.pauseMenu.pauseMenuItems.PauseMenuBaseItem::__set__data
                    //    PushScaleformMovieMethodParameterInt(0); // menu ID 0
                    //    PushScaleformMovieMethodParameterInt(0); // unique ID 0
                    //    PushScaleformMovieMethodParameterInt(0); // type 0 (TYPE_MAIN_CHAR_SELECTOR)
                    //    PushScaleformMovieMethodParameterInt(0); // initialIndex 1337 (rank)
                    //    PushScaleformMovieMethodParameterBool(true); // isSelectable true

                    //    ///// ITEM/ROW TITLE.
                    //    BeginTextCommandScaleformString("STRING");
                    //    AddTextComponentSubstringPlayerName("Setting " + x);
                    //    EndTextCommandScaleformString();

                    //    ///// UNSURE HOW THIS WORKS, BUT IF YOU UNCOMMENT THIS, IT'LL ADD AN ICON TO THE ROW.
                    //    ///// MAKING THE STRING "20" AND THE BOOL TRUE SEEMS TO DO SOMETHING WITH A ROCKSTAR LOGO INSTEAD.
                    //    BeginTextCommandScaleformString("STRING");
                    //    AddTextComponentSubstringPlayerName("");
                    //    EndTextCommandScaleformString();
                    //    PushScaleformMovieMethodParameterBool(false); // SOMETHING WITH ROCKSTAR/STAR LOGO SWITCHING.

                    //    ///// FINISH.
                    //    EndScaleformMovieMethod();
                    //}

                    PushScaleformMovieFunctionN("DISPLAY_DATA_SLOT");
                    PushScaleformMovieMethodParameterInt(0);
                    EndScaleformMovieMethod();

                    ////////////////////////////////////////////////////////////////////////////////////////////

                    #region Info Column Setup
                    ////////////////////////////////////////////////////////////////////////////////////////////
                    string[] leftText = new string[5] { "Rating", "Created by", "Snail?", "Max players?", "Type" };
                    string[] rightText = new string[5] { "100.00%", "Vespura", "~g~YES", "32", "Sumo~b~" };
                    /*
                    for (var x = 0; x < 5; x++)
                    {
                        
                        #region Old messy code
                        ///// COLUMN 0 (LEFT) - ROW 0
                        PushScaleformMovieFunctionN("SET_DATA_SLOT");
                        PushScaleformMovieMethodParameterInt(1); // column
                        PushScaleformMovieMethodParameterInt(x); // index

                        PushScaleformMovieMethodParameterInt(0); // menu ID 0
                        PushScaleformMovieMethodParameterInt(0); // unique ID 0

                        //if (x == 4)
                        //{
                        //    PushScaleformMovieMethodParameterInt(2); // right icon type 
                        //}
                        //else
                        //{
                        //    PushScaleformMovieMethodParameterInt(0); // right icon type 
                        //}
                        PushScaleformMovieMethodParameterInt(2); // right icon type
                        // 0 & 1 = right text, no icon.
                        // 2 = icon (variation 1) 
                        // 3 = crew label(?) (broken, always "undef" as crew text)
                        // 4 bordered rows, no icon.
                        // 5 no right text.
                        // >= 6 no icons, just left/right text.


                        PushScaleformMovieMethodParameterString("hello"); // right icon string or unused? 99% sure it's the latter.
                        PushScaleformMovieMethodParameterBool(false); // row isSelectable / false

                        PushScaleformMovieMethodParameterString(leftText[x]); // left text
                        PushScaleformMovieMethodParameterString(rightText[x]); // right text

                        if (x != 4)
                        {
                            PushScaleformMovieMethodParameterString("..+SUMO"); // (right icon variation) crew label text (if icon type is 3).
                                                                                // will switch to an int (see below) when icon type 2 is used.
                            PushScaleformMovieMethodParameterInt((int)HudColors.HudColor.HUD_COLOUR_BLUE); // color (hud color)
                            PushScaleformMovieMethodParameterBool(false); // checkmarked.
                        }
                        else
                        {
                            PushScaleformMovieMethodParameterString("0");
                            PushScaleformMovieMethodParameterInt((int)HudColors.HudColor.HUD_COLOUR_BLUE); // color (hud color)
                            PushScaleformMovieMethodParameterBool(false); // checkmarked.
                            // only uncomment this ↓ if the above string is commented out ↑
                            //PushScaleformMovieMethodParameterInt(0); // right icon variation?! (only used when int above (icon type) is 2.
                            // 0 = star / mission.
                            // 1 = skull / deathmatch / rampage.
                            // 2 = flag / race.
                            // 3 = shield / ?.
                            // 4 = multiple skulls / ?.
                            // >=5 = no icon.
                        }
                        //PushScaleformMovieMethodParameterString("voltic2"); // ?
                        //PushScaleformMovieMethodParameterString("candc_importexport"); // ?



                        ///// FINISH.
                        EndScaleformMovieMethod();
                        #endregion
                    }
                    */
                    SetColumnInfoRow(0, "Rating", "100.00%", false);
                    SetColumnInfoRow(1, "Created By", "Vespura", false);
                    SetColumnInfoRow(2, "Snail", "~g~YES", false);
                    SetColumnInfoRow(3, "Max Players", "32", false);
                    SetColumnInfoRow(4, "Type", "Sumo", 0, HudColors.HudColor.HUD_COLOUR_BLUE, false);


                    //PushScaleformMovieFunctionN("BUILD_MENU_GFX_FILES");
                    ////PushScaleformMovieMethodParameterString("");
                    ////PushScaleformMovieMethodParameterString("");
                    //PushScaleformMovieMethodParameterInt(1);
                    //PushScaleformMovieMethodParameterString("candc_importexport/voltic2");
                    //PushScaleformMovieMethodParameterString("candc_importexport");
                    //EndScaleformMovieMethod();

                    // Show the column.
                    PushScaleformMovieFunctionN("DISPLAY_DATA_SLOT");
                    PushScaleformMovieMethodParameterInt(1);
                    EndScaleformMovieMethod();

                    ////////////////////////////////////////////////////////////////////////////////////////////
                    #endregion

                    #region Players column setup
                    ////////////////////////////////////////////////////////////////////////////////////////////
                    //                          PLAYERS ROWS SETUP (COLUMN 3) HERE                            //
                    ////////////////////////////////////////////////////////////////////////////////////////////
                    var rowindex = 0;
                    foreach (Player p in new PlayerList())
                    {
                        // Add the player.
                        SetColumnPlayerRow(rowindex + 0, p.Name, 420, (HudColors.HudColor)p.Handle + 28, false, 115, "SUMO", false, "LOADING", HudColors.HudColor.HUD_COLOUR_RED);
                        SetColumnPlayerRow(rowindex + 1, p.Name, 420, (HudColors.HudColor)p.Handle + 28, false, 116, "SUMO", true, "KICKED", HudColors.HudColor.HUD_COLOUR_ORANGE);
                        SetColumnPlayerRow(rowindex + 2, p.Name, 420, (HudColors.HudColor)p.Handle + 28, false, 117, "SUMO", false, "JOINED", HudColors.HudColor.HUD_COLOUR_GREEN);
                        SetColumnPlayerRow(rowindex + 3, p.Name, 420, (HudColors.HudColor)p.Handle + 28, false, 118, "SUMO", false, "SNAILSOME", HudColors.HudColor.HUD_COLOUR_ADVERSARY);
                        SetColumnPlayerRow(rowindex + 4, p.Name, 420, (HudColors.HudColor)p.Handle + 28, false, 119, "SUMO", false, "", HudColors.HudColor.HUD_COLOUR_ADVERSARY);
                        SetColumnPlayerRow(rowindex + 5, p.Name, 420, (HudColors.HudColor)p.Handle + 28, false, 120, "SUMO", false, "SPECTATOR", HudColors.HudColor.HUD_COLOUR_GREY);
                        rowindex++;
                        #region old messy code
                        /*
                        ///// COLUMN 0 (LEFT) - ROW 0
                        PushScaleformMovieFunctionN("SET_DATA_SLOT");
                        PushScaleformMovieMethodParameterInt(3); // column
                        PushScaleformMovieMethodParameterInt(rowindex); // index

                        //// com.rockstargames.gtav.pauseMenu.pauseMenuItems.PauseMenuBaseItem::__set__data
                        //PushScaleformMovieMethodParameterInt(0); // menu ID 0
                        //PushScaleformMovieMethodParameterInt(0); // unique ID 0
                        //PushScaleformMovieMethodParameterInt(0); // type 0 (TYPE_MAIN_CHAR_SELECTOR)
                        //PushScaleformMovieMethodParameterInt(0); // initialIndex 1337 (rank)
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
                        PushScaleformMovieMethodParameterInt(0); // menu ID 0
                        PushScaleformMovieMethodParameterInt(0); // unique ID 0
                        PushScaleformMovieMethodParameterInt(2); // type 2 (AS_ONLINE_IN_SESSION)
                        PushScaleformMovieMethodParameterInt(120); // initialIndex 1337 (rank)
                        PushScaleformMovieMethodParameterBool(false); // isSelectable true

                        // data, see com.rockstargames.gtav.pauseMenu.pauseMenuItems.multiplayer.PauseMPMenuFriendsListItem
                        BeginTextCommandScaleformString("STRING");
                        AddTextComponentSubstringPlayerName(p.Name);
                        EndTextCommandScaleformString();

                        PushScaleformMovieMethodParameterInt(28 + p.Handle); // row color / 134

                        PushScaleformMovieMethodParameterBool(false); // if true, removes color from left bar & reduces color opacity on row itself.
                                                                        // False colors everything.

                        PushScaleformMovieMethodParameterString("0"); // unused / 0

                        PushScaleformMovieMethodParameterInt(65); // 66 EYE // 65 RANK_FREEMODE // 64 BOOT(KICK) // 63 GLOBE // <= 62 || >= 67 EMPTY 

                        PushScaleformMovieMethodParameterString("0"); // unused / 0

                        if (NetworkIsHost())
                        {
                            PushScaleformMovieMethodParameterString("..+SUMO"); // maybe show host?
                        }
                        else
                        {
                            PushScaleformMovieMethodParameterString("..+SUMO"); // show something else here?
                        }


                        PushScaleformMovieMethodParameterBool(false); // '(boot) kick blinking'

                        var state = "LOADING";
                        if (state == "JOINED")
                        {
                            BeginTextCommandScaleformString("STRING");
                            AddTextComponentSubstringPlayerName(state); // BADGE
                            EndTextCommandScaleformString();
                            PushScaleformMovieMethodParameterInt((int)HudColors.HudColor.HUD_COLOUR_HB_BLUE); // COLOR OF BADGE // HUD_COLORS // BLUE / JOINED
                        }
                        else if (state == "LOADING")
                        {
                            BeginTextCommandScaleformString("STRING");
                            AddTextComponentSubstringPlayerName(state); // BADGE
                            EndTextCommandScaleformString();
                            PushScaleformMovieMethodParameterInt((int)HudColors.HudColor.HUD_COLOUR_RED); // COLOR OF BADGE // HUD_COLORS // RED / LOADING
                        }
                        else if (state == "READY")
                        {
                            BeginTextCommandScaleformString("STRING");
                            AddTextComponentSubstringPlayerName(state); // BADGE
                            EndTextCommandScaleformString();
                            PushScaleformMovieMethodParameterInt((int)HudColors.HudColor.HUD_COLOUR_GREEN); // COLOR OF BADGE // HUD_COLORS // READY / GREEN
                        }

                        // statusStr
                        // statusColID

                        PopScaleformMovieFunctionVoid();
                        rowindex++;
                        */
                        #endregion
                    }
                    // Show the column.
                    PushScaleformMovieFunctionN("DISPLAY_DATA_SLOT");
                    PushScaleformMovieMethodParameterInt(3);
                    EndScaleformMovieMethod();

                    ////////////////////////////////////////////////////////////////////////////////////////////
                    #endregion

                    #region Menu focus setup
                    ///// ACTIVATE THE FIRST COLUMN (FOCUS).
                    PushScaleformMovieFunctionN("SET_COLUMN_FOCUS");
                    PushScaleformMovieMethodParameterInt(0); // column index // _loc7_
                    PushScaleformMovieMethodParameterInt(1); // highlightIndex // _loc6_
                    PushScaleformMovieMethodParameterInt(1); // scriptSetUniqID // _loc4_
                    PushScaleformMovieMethodParameterInt(0); // scriptSetMenuState // _loc5_
                    EndScaleformMovieMethod();
                    #endregion

                    ////////////////////////////////////////////////////////////////////////////////////////////

                }
            }
        }

        private void SetColumnSettingsRow(int rowindex, string leftText, string rightText, int rightSomething, bool rightDisabled, HudColors.HudColor rightThingColor)
        {
            ///// COLUMN 0 (LEFT) - ROW 0
            PushScaleformMovieFunctionN("SET_DATA_SLOT");
            PushScaleformMovieMethodParameterInt(0); // column
            PushScaleformMovieMethodParameterInt(rowindex); // index

            // com.rockstargames.gtav.pauseMenu.pauseMenuItems.PauseMenuBaseItem::__set__data
            PushScaleformMovieMethodParameterInt(0); // menu ID 0
            PushScaleformMovieMethodParameterInt(0); // unique ID 0
            PushScaleformMovieMethodParameterInt(0); // type 0
            PushScaleformMovieMethodParameterInt((int)rightThingColor); // initialIndex 0
            PushScaleformMovieMethodParameterBool(true); // isSelectable true

            PushScaleformMovieMethodParameterString(leftText);
            PushScaleformMovieMethodParameterString(rightText);

            ///// UNSURE HOW THIS WORKS, BUT IF YOU UNCOMMENT THIS, IT'LL ADD AN ICON TO THE ROW.
            ///// MAKING THE STRING "20" AND THE BOOL TRUE SEEMS TO DO SOMETHING WITH A ROCKSTAR LOGO INSTEAD.
            PushScaleformMovieMethodParameterInt(rightSomething);
            PushScaleformMovieMethodParameterString(rightText);
            PushScaleformMovieMethodParameterInt(rightSomething);

            PushScaleformMovieMethodParameterBool(rightDisabled); // SOMETHING WITH ROCKSTAR/STAR LOGO SWITCHING.

            ///// FINISH.
            EndScaleformMovieMethod();
        }

        #region Details Screen Row Functions
        /// <summary>
        /// Set the specified row for the details screen. No icons. Optionally add row borders.
        /// </summary>
        /// <param name="rowindex">The row index.</param>
        /// <param name="leftText">The left text.</param>
        /// <param name="rightText">The right text.</param>
        /// <param name="borderedRows">Adds borders to the row.</param>
        private void SetColumnInfoRow(int rowindex, string leftText, string rightText, bool borderedRows)
        {
            PushScaleformMovieFunctionN("SET_DATA_SLOT");
            PushScaleformMovieMethodParameterInt(1); // column
            PushScaleformMovieMethodParameterInt(rowindex); // index

            PushScaleformMovieMethodParameterInt(0); // menu ID 0
            PushScaleformMovieMethodParameterInt(0); // unique ID 0

            if (borderedRows)
            {
                PushScaleformMovieMethodParameterInt(4); // right icon type
            }
            else if (rightText == null || rightText == "")
            {
                PushScaleformMovieMethodParameterInt(5); // right icon type
            }
            else
            {
                PushScaleformMovieMethodParameterInt(0); // right icon type
            }
            // 0 & 1 = right text, no icon.
            // 2 = icon (variation 1) 
            // 3 = crew label(?) (broken, always "undef" as crew text)
            // 4 bordered rows, no icon.
            // 5 no right text.
            // >= 6 no icons, just left/right text.




            PushScaleformMovieMethodParameterInt(99); // values in decompiled scripts: 99, purpose unknown
            // below must be an int, not string according to decompiled scripts, see line above
            //PushScaleformMovieMethodParameterString("img://candc_importexport/voltic2");  // right icon string or unused? 99% sure it's the latter. real parameter type is also unknown

            PushScaleformMovieMethodParameterBool(false); // row isSelectable? used in decompiled scripts: 0(false)

            //PushScaleformMovieMethodParameterString(leftText); // left text     -   seems to be using _0xE83A3E3557A56640 (button name?!)
            PushScaleformMovieMethodParameterButtonName(leftText); // left text     -   seems to be using _0xE83A3E3557A56640 (button name?!)



            PushScaleformMovieMethodParameterString(rightText); // right text 
                                                                // needs to be an int according to decompiled scripts, but fuck that, because i want my own text
                                                                ////PushScaleformMovieMethodParameterInt(rightText)
                                                                //PushScaleformMovieMethodParameterInt(0);





            // according to decompiled scaleforms, this is used, but my own testing seems that the commented code below should work instead.
            BeginTextCommandScaleformString("MP_BET_CASH");
            AddTextComponentFormattedInteger(1, true);
            EndTextCommandScaleformString();
            /*
            // seems to be a string, but actually an int formatted to become a string (fmmc_launcer.c near line 252.572)
            PushScaleformMovieMethodParameterInt(5); // right icon variation?! (only used when int above (icon type) is 2.
                                                     // 0 = star / mission.
                                                     // 1 = skull / deathmatch / rampage.
                                                     // 2 = flag / race.
                                                     // 3 = shield / ?.
                                                     // 4 = multiple skulls / ?.
                                                     // >= 5 = no icon.

    */

            // from decompiled scripts:
            PushScaleformMovieMethodParameterString("unknown");
            PushScaleformMovieMethodParameterInt(65);
            PushScaleformMovieMethodParameterString("unknown");
            PushScaleformMovieMethodParameterString("unknown");
            PushScaleformMovieMethodParameterString("unknown");
            PushScaleformMovieMethodParameterInt(0);
            PushScaleformMovieMethodParameterInt(-1); // >0 or -1
            PushScaleformMovieMethodParameterBool(false);




            // seems BEGIN_TEXT_COMMAND_SCALEFORM_STRING is used in decompiled scripts here, for betting?

            // these two are incorrect?
            //PushScaleformMovieMethodParameterInt(0); // right icon color (hud color)
            //PushScaleformMovieMethodParameterBool(false); // right icon checkmarked.

            EndScaleformMovieMethod();
        }

        /// <summary>
        /// Set the specified row for the details screen. Crew Label as right icon, allowing for custom text there.
        /// </summary>
        /// <param name="rowindex">The row index.</param>
        /// <param name="leftText">The left text.</param>
        /// <param name="rightText">The right text.</param>
        /// <param name="crewLabelText">The text to show in the crew label. (max 4 characters)</param>
        private void SetColumnInfoRow(int rowindex, string leftText, string rightText, string crewLabelText)
        {
            PushScaleformMovieFunctionN("SET_DATA_SLOT");
            PushScaleformMovieMethodParameterInt(1); // column
            PushScaleformMovieMethodParameterInt(rowindex); // index

            PushScaleformMovieMethodParameterInt(0); // menu ID 0
            PushScaleformMovieMethodParameterInt(0); // unique ID 0

            PushScaleformMovieMethodParameterInt(3); // right icon type
                                                     // 0 & 1 = right text, no icon.
                                                     // 2 = icon (variation 1) 
                                                     // 3 = crew label(?) (broken, always "undef" as crew text)
                                                     // 4 bordered rows, no icon.
                                                     // 5 no right text.
                                                     // >= 6 no icons, just left/right text.

            PushScaleformMovieMethodParameterString("img://candc_importexport/voltic2");  // right icon string or unused? 99% sure it's the latter.
                                                                                          // real parameter type is also unknown

            PushScaleformMovieMethodParameterBool(false); // row isSelectable

            PushScaleformMovieMethodParameterString(leftText); // left text
            PushScaleformMovieMethodParameterString(rightText); // right text

            PushScaleformMovieMethodParameterString($"..+{crewLabelText}"); // (right icon variation) crew label text (if icon type is 3).
                                                                            // will switch to an int (see below) when icon type 2 is used.

            EndScaleformMovieMethod();
        }

        /// <summary>
        /// Set the specified row for the details screen. Right icon (type 1 / int version). Choose your own icon variation, color and checkmark on/off.
        /// </summary>
        /// <param name="rowindex">The row index.</param>
        /// <param name="leftText">The left text.</param>
        /// <param name="rightText">The right text.</param>
        /// <param name="rightIconTypeVariation">0 = star/mission. 1 = skull/deathmatch/rampage. 2 = flag/race. 3 = shield. 4 = multiple skulls.</param>
        /// <param name="iconColor">Hud color used for the icon.</param>
        /// <param name="iconCheckmark">Shows a (previously completed) checkmark on top of the icon.</param>
        private void SetColumnInfoRow(int rowindex, string leftText, string rightText, int rightIconTypeVariation, HudColors.HudColor iconColor,
            bool iconCheckmark)
        {
            PushScaleformMovieFunctionN("SET_DATA_SLOT");
            PushScaleformMovieMethodParameterInt(1); // column
            PushScaleformMovieMethodParameterInt(rowindex); // index

            PushScaleformMovieMethodParameterInt(0); // menu ID 0
            PushScaleformMovieMethodParameterInt(0); // unique ID 0

            if (rightIconTypeVariation < 0 || rightIconTypeVariation > 4)
            {
                PushScaleformMovieMethodParameterInt(0); // right icon type
            }
            else
            {
                PushScaleformMovieMethodParameterInt(2); // right icon type
            }
            // 0 & 1 = right text, no icon.
            // 2 = icon (variation 1) 
            // 3 = crew label(?) (broken, always "undef" as crew text)
            // 4 bordered rows, no icon.
            // 5 no right text.
            // >= 6 no icons, just left/right text.

            PushScaleformMovieMethodParameterString("img://candc_importexport/voltic2");  // right icon string or unused? 99% sure it's the latter.
                                                                                          // real parameter type is also unknown

            PushScaleformMovieMethodParameterBool(false); // row isSelectable

            PushScaleformMovieMethodParameterString(leftText); // left text
            PushScaleformMovieMethodParameterString(rightText); // right text

            PushScaleformMovieMethodParameterInt(rightIconTypeVariation); // right icon variation?! (only used when int above (icon type) is 2.
                                                                          // 0 = star / mission.
                                                                          // 1 = skull / deathmatch / rampage.
                                                                          // 2 = flag / race.
                                                                          // 3 = shield / ?.
                                                                          // 4 = multiple skulls / ?.
                                                                          // >= 5 = no icon.

            PushScaleformMovieMethodParameterInt((int)iconColor); // right icon color (hud color)
            PushScaleformMovieMethodParameterBool(iconCheckmark); // right icon checkmarked.

            EndScaleformMovieMethod();
        }


        /// <summary>
        /// Set the specified row for the details screen. Right icon (type 2 / string version). Choose your own icon variation string, color and checkmark on/off.
        /// </summary>
        /// <param name="rowindex">The row index.</param>
        /// <param name="leftText">The left text.</param>
        /// <param name="rightText">The right text.</param>
        /// <param name="rightIconTypeVariation">"0", "1", "2", "3" confirmed working. Actual purpose/usage is unkown.</param>
        /// <param name="iconColor">Hud color used for the icon.</param>
        /// <param name="iconCheckmark">Shows a (previously completed) checkmark on top of the icon.</param>
        private void SetColumnInfoRow(int rowindex, string leftText, string rightText, string rightIconTypeVariation, HudColors.HudColor iconColor,
            bool iconCheckmark)
        {
            PushScaleformMovieFunctionN("SET_DATA_SLOT");
            PushScaleformMovieMethodParameterInt(1); // column
            PushScaleformMovieMethodParameterInt(rowindex); // index

            PushScaleformMovieMethodParameterInt(0); // menu ID 0
            PushScaleformMovieMethodParameterInt(0); // unique ID 0

            PushScaleformMovieMethodParameterInt(2); // right icon type
                                                     // 0 & 1 = right text, no icon.
                                                     // 2 = icon (variation 1) 
                                                     // 3 = crew label(?) (broken, always "undef" as crew text)
                                                     // 4 bordered rows, no icon.
                                                     // 5 no right text.
                                                     // >= 6 no icons, just left/right text.

            PushScaleformMovieMethodParameterString("img://candc_importexport/voltic2");  // right icon string or unused? 99% sure it's the latter.
                                                                                          // real parameter type is also unknown

            PushScaleformMovieMethodParameterBool(false); // row isSelectable

            PushScaleformMovieMethodParameterString(leftText); // left text
            PushScaleformMovieMethodParameterString(rightText); // right text

            PushScaleformMovieMethodParameterString(rightIconTypeVariation); // "0", "1", "2", "3" confirmed working.

            PushScaleformMovieMethodParameterInt((int)iconColor); // right icon color (hud color)
            PushScaleformMovieMethodParameterBool(iconCheckmark); // right icon checkmarked.

            EndScaleformMovieMethod();
        }
        #endregion

        #region Players Screen Row Functions
        /// <summary>
        /// Sets up the specified row.
        /// </summary>
        /// <param name="rowindex">The index of the row.</param>
        /// <param name="name">The player name.</param>
        /// <param name="rank">The player's rank.</param>
        /// <param name="rowColor">The color for the row.</param>
        /// <param name="reduceRowColors">Reduces/removes color from the row.</param>
        /// <param name="rightIcon">66 = EYE -- 65 = RANK_FREEMODE -- 64 = BOOT(KICK) -- 63 = GLOBE -- 0 = no icon</param>
        /// <param name="crewLabelText">4 characters to place inside the crew logo.</param>
        /// <param name="blinkKickIcon">Makes the "rightIcon" switch for the kick/boot icon, then switching back repeatedly.</param>
        /// <param name="badgeText">The text for the status bar / badge on the right.</param>
        /// <param name="badgeColor">The color of the status bar / badge.</param>
        private void SetColumnPlayerRow(int rowindex, string name, int rank, HudColors.HudColor rowColor, bool reduceRowColors,
            int rightIcon, string crewLabelText, bool blinkKickIcon, string badgeText, HudColors.HudColor badgeColor)
        {
            PushScaleformMovieFunctionN("SET_DATA_SLOT");
            PushScaleformMovieMethodParameterInt(3); // column
            PushScaleformMovieMethodParameterInt(rowindex); // index

            PushScaleformMovieMethodParameterInt(0); // menu ID 0
            PushScaleformMovieMethodParameterInt(0); // unique ID 0
            PushScaleformMovieMethodParameterInt(2); // type 2 (AS_ONLINE_IN_SESSION)
            PushScaleformMovieMethodParameterInt(rank); // initialIndex 1337 (rank)
            PushScaleformMovieMethodParameterBool(false); // isSelectable true

            PushScaleformMovieMethodParameterString(name); // player name

            PushScaleformMovieMethodParameterInt((int)rowColor); // row color / 134

            PushScaleformMovieMethodParameterBool(reduceRowColors); // if true, removes color from left bar & reduces color opacity on row itself.
                                                                    // False colors everything.

            PushScaleformMovieMethodParameterString("0"); // unused / 0
            PushScaleformMovieMethodParameterInt(rightIcon); // 66 EYE // 65 RANK_FREEMODE // 64 BOOT(KICK) // 63 GLOBE // <= 62 || >= 67 EMPTY 
            PushScaleformMovieMethodParameterString("0"); // unused / 0

            PushScaleformMovieMethodParameterString($"..+{crewLabelText}"); // crew label text.

            PushScaleformMovieMethodParameterBool(blinkKickIcon); // '(boot) kick blinking'

            PushScaleformMovieMethodParameterString(badgeText); // badge text
            PushScaleformMovieMethodParameterInt((int)badgeColor); // badge color

            EndScaleformMovieMethod(); // finish
        }
        #endregion



    }


}
