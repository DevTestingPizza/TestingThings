using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace TestingThings
{
    static class VehicleSelector
    {
        public static async void AddPage(VehicleCard[] vehicles, Scaleform scaleformHandle, int startIndex)
        {
            var i = 0;
            foreach (VehicleCard vc in vehicles)
            {
                await AddVehicle(vc, scaleformHandle, startIndex + i);
                i++;
            }
        }

        public static async Task AddVehicle(VehicleCard vehicle, Scaleform scaleformHandle, int gridIndex)
        {
            if (vehicle.TextureDict != "" && vehicle.TextureName != "" && !HasStreamedTextureDictLoaded(vehicle.TextureDict))
            {
                Debug.WriteLine("Loading: " + vehicle.TextureDict + " : " + vehicle.TextureName);
                RequestStreamedTextureDict(vehicle.TextureDict, false);
                while (!HasStreamedTextureDictLoaded(vehicle.TextureDict))
                {
                    await BaseScript.Delay(0);
                }
                Debug.Write("Done!");
            }
            scaleformHandle.CallFunction("SET_GRID_ITEM", gridIndex, GetVehicleName(vehicle.ModelName), vehicle.TextureDict,
                vehicle.TextureName, 0, 0, -1, false, 0.0, 0.0, false, 0);
            Debug.WriteLine(GetVehicleName(vehicle.ModelName) + "has been loaded and added.");
        }

        public static List<VehicleCard> Vehicles = new List<VehicleCard>()
        {
            new VehicleCard(){ ModelName = "adder", TextureDict = "lgm_default" , TextureName = "adder"},
            new VehicleCard(){ ModelName = "banshee", TextureDict = "lgm_default" , TextureName = "banshee"},
            new VehicleCard(){ ModelName = "bullet", TextureDict = "lgm_default" , TextureName = "bullet"},
            new VehicleCard(){ ModelName = "carbonizzare", TextureDict = "lgm_default" , TextureName = "carboniz"},
            new VehicleCard(){ ModelName = "cheetah", TextureDict = "lgm_default" , TextureName = "cheetah"},
            new VehicleCard(){ ModelName = "coquette", TextureDict = "lgm_default" , TextureName = "coquette"},
            new VehicleCard(){ ModelName = "entityxf", TextureDict = "lgm_default" , TextureName = "entityxf"},
            new VehicleCard(){ ModelName = "feltzer2", TextureDict = "lgm_default" , TextureName = "feltzer"},
            new VehicleCard(){ ModelName = "hotknife", TextureDict = "lgm_default" , TextureName = "hotknife"},
            new VehicleCard(){ ModelName = "ztype", TextureDict = "lgm_default" , TextureName = "ztype"},

            new VehicleCard(){ ModelName = "verlier", TextureDict = "lgm_dlc_apartments" , TextureName = "verlier"},

            new VehicleCard(){ ModelName = "turismor", TextureDict = "lgm_dlc_business" , TextureName = "turismor"},

            new VehicleCard(){ ModelName = "zentorno", TextureDict = "lgm_dlc_business2" , TextureName = "zentorno"},
            new VehicleCard(){ ModelName = "brawler", TextureDict = "lgm_dlc_luxe" , TextureName = "brawler"},
            new VehicleCard(){ ModelName = "osiris", TextureDict = "lgm_dlc_luxe" , TextureName = "osiris"},
            new VehicleCard(){ ModelName = "t20", TextureDict = "lgm_dlc_luxe" , TextureName = "t20"},

            new VehicleCard(){ ModelName = "ruston", TextureDict = "lgm_dlc_specialraces" , TextureName = "ruston"},

            new VehicleCard(){ ModelName = "bifta", TextureDict = "sssa_default" , TextureName = "bifta"},
            new VehicleCard(){ ModelName = "dune", TextureDict = "sssa_default" , TextureName = "dune"},
            new VehicleCard(){ ModelName = "bodhi2", TextureDict = "sssa_default" , TextureName = "bodhi2"},
            new VehicleCard(){ ModelName = "issi2", TextureDict = "sssa_default" , TextureName = "issi2"},
            new VehicleCard(){ ModelName = "kalahari", TextureDict = "sssa_default" , TextureName = "kalahari"},
            new VehicleCard(){ ModelName = "rebel", TextureDict = "sssa_default" , TextureName = "rebel"},

            new VehicleCard(){ ModelName = "contender", TextureDict = "sssa_dlc_stunt" , TextureName = "contender"},
            new VehicleCard(){ ModelName = "rallytruck", TextureDict = "sssa_dlc_stunt" , TextureName = "rallytruck"},
            new VehicleCard(){ ModelName = "trophy2", TextureDict = "sssa_dlc_stunt" , TextureName = "trophy2"},

            new VehicleCard(){ ModelName = "kamacho", TextureDict = "sssa_dlc_xmas2017" , TextureName = "kamacho"},

            new VehicleCard(){ ModelName = "marshall", TextureDict = "candc_default" , TextureName = "marshall"},
            new VehicleCard(){ ModelName = "monster", TextureDict = "candc_default" , TextureName = "monster"},

            new VehicleCard(){ ModelName = "voltic2", TextureDict = "candc_importexport" , TextureName = "voltic2"},

            new VehicleCard(){ ModelName = "wastelander", TextureDict = "candc_importexport" , TextureName = "wastlndr"},

            new VehicleCard(){ ModelName = "caddy3", TextureDict = "foreclosure_bunker" , TextureName = "transportationb_2"},
        };

        public static string GetVehicleName(string modelName)
        {
            return GetLabelText(GetDisplayNameFromVehicleModel((uint)GetHashKey(modelName)));
        }
    }

    public struct VehicleCard
    {
        public string ModelName;
        public string TextureName;
        public string TextureDict;
    }


}
