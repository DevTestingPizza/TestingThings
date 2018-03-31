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
    class Commands : BaseScript
    {
        public static bool _switch = true;
        public Commands()
        {
            RegisterCommand("tp", new Action(Transition), false);
            RegisterCommand("show", new Action(Intro), false);
            RegisterCommand("cam", new Action(Gfx.ToggleLimbo), false);
            RegisterCommand("spawn", new Action<int, object, string>(Spawn), false);
        }

        private void Spawn(int source, object args, string rawcommand)
        {
            var retval = Exports["spawnmanager"].getSpawns();
            //Debug.Write(retval[0].x);

            //var json = JsonConvert.SerializeObject(retval);
            //var t = JsonConvert.DeserializeObject<object>(json);
            //Debug.Write(json.ToString());
            //Debug.Write(t[1]["x"].ToString());
        }

        public async void Intro()
        {
            await Gfx.ShowIntroScaleform();
        }

        public void Transition()
        {
            if (_switch)
            {
                TransitionToCoords(new Vector4(736.34f, 1284.14f, 360.3f, 4f), (uint)GetHashKey("ZENTORNO"));
            }
            else
            {
                TransitionToCoords(new Vector4(1362.86f, 6513.25f, 19.53f, 91.1f), (uint)GetHashKey("ZENTORNO"));
            }

            _switch = !_switch;
        }


        public static async void TransitionToCoords(Vector4 targetPosition, uint vehicleModel = 0)
        {
            RequestCollisionAtCoord(targetPosition.X, targetPosition.Y, targetPosition.Z);
            var tempPedHash = (uint)GetHashKey("S_M_Y_Clown_01"); // Thanks, you're a genius Mraes!
            RequestModel(tempPedHash);
            while (!HasModelLoaded(tempPedHash))
            {
                await Delay(0);
            }
            var tempPed = CreatePed(4, tempPedHash, targetPosition.X, targetPosition.Y, targetPosition.Z, targetPosition.W, false, false);
            SetEntityVisible(tempPed, false, false);
            // Teleport into a new vehicle.
            if (vehicleModel != 0)
            {
                if (IsModelInCdimage(vehicleModel))
                {
                    RequestModel(vehicleModel);
                    while (!HasModelLoaded(vehicleModel))
                    {
                        await Delay(0);
                    }
                }
            }
            StartPlayerSwitch(PlayerPedId(), tempPed, 0, 1);
            await Delay(10);
            DeleteEntity(ref tempPed);
            SetModelAsNoLongerNeeded(tempPedHash);
            var veh = 0;
            if (vehicleModel != 0)
            {
                veh = CreateVehicle(vehicleModel, targetPosition.X, targetPosition.Y, targetPosition.Z, targetPosition.W, true, false);
                SetVehicleHasBeenOwnedByPlayer(veh, true);
                SetModelAsNoLongerNeeded(vehicleModel);
                SetVehicleNeedsToBeHotwired(veh, false);
            }

            while (GetPlayerSwitchState() != 10 && GetPlayerSwitchState() != 8)
            {
                DisplayRadar(false);
                await Delay(0);
            }
            SetEntityCoords(PlayerPedId(), targetPosition.X, targetPosition.Y, targetPosition.Z, false, false, false, false);
            SetEntityHeading(PlayerPedId(), targetPosition.W);
            if (vehicleModel != 0 && DoesEntityExist(veh) && IsEntityAVehicle(veh) && !IsEntityDead(veh))
            {
                SetPedIntoVehicle(PlayerPedId(), veh, -1);
            }
        }
    }
}
