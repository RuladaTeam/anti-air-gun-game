using System.Collections.Generic;

namespace Core.Scripts
{
    public abstract class SceneNames
    {
        public const string MAIN_MENU_SCENE_NAME = "MainMenuScene";
        public const string CAR_SCENE_NAME = "CarScene";
        public const string BATTLEFIELD_PLANES_SCENE_NAME = "BattleFieldPlanesScene";
        public const string BATTLEFIELD_TANKS_SCENE_NAME = "BattleFieldTanksScene";
        public const string BATTLEFIELD_NIGHT_SCENE_NAME = "BattleFieldNightScene";
        public const string INFO_SCENE_NAME = "InfoScene";

        public static readonly Dictionary<Scenes, string> StringSceneNames = new ()
        {
            {Scenes.MainMenuScene, MAIN_MENU_SCENE_NAME},
            {Scenes.CarScene, CAR_SCENE_NAME},
            {Scenes.BattlefieldPlanesScene, BATTLEFIELD_PLANES_SCENE_NAME},
            {Scenes.BattlefieldTanksScene, BATTLEFIELD_TANKS_SCENE_NAME},
            {Scenes.BattlefieldNightScene, BATTLEFIELD_NIGHT_SCENE_NAME},
            {Scenes.InfoScene, INFO_SCENE_NAME},
        };
    }

    public enum Scenes
    {
        MainMenuScene,
        CarScene,
        BattlefieldPlanesScene,
        BattlefieldTanksScene,
        BattlefieldNightScene,
        InfoScene
    }
}
