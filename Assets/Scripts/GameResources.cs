using UnityEngine;
using System.Collections;

namespace StrategyGame {
    public enum TileTerrainType {
        Default, Crater, Border
    }
    public enum GoodsCategory {
        Solid, Liquid, Gas
    }

    public enum BuildingCategory
    {
        Housing, Services, Industry, Food
    }

    public enum ItemCategory
    {
        GoodService, Commodity, Ship
    }

	public static class GameResources {
        public static bool UIHovering { get; set;}
        public static int PlayTilesX { get { return 32; } }
        public static int PlayTilesZ { get { return 32; } }
        public static int NumTilesX { get { return numTilesX; } }
        public static int NumTilesZ { get { return numTilesZ; } }
        public static int TileResolution { get { return 32; } }
        public static float TileSide { get { return 10f; } }
        public static float TileDiag { get { return tileDiag; } }
        public static float TileRadius { get { return tileRadius; } }
        public static float MinDragDistance { get { return .1f; } }
        public static float MapDim { get { return mapDim; } }
        public static Vector3 MapBottomLeft { get { return mapBottomLeft; } }
        public static Vector3 MapTopRight { get { return mapTopRight; } }
        public static BuildingCategory[] BuildingCategories {get { return buildingCategories; } }

        private static int numTilesX = PlayTilesX * 2;
        private static int numTilesZ = PlayTilesZ;
        private static float tileDiag = Mathf.Sqrt(Mathf.Pow(TileSide,2) + Mathf.Pow(TileSide,2));
        private static float tileRadius = tileDiag / 2;
        private static float mapDim = tileDiag * numTilesZ;
        private static Vector3 mapBottomLeft = new Vector3 ( 0, 0, (numTilesZ * TileSide) / 2);
        private static Vector3 mapTopRight = new Vector3 ( (numTilesX * TileSide), 0, (numTilesZ * TileSide) / 2);
        private static BuildingCategory[] buildingCategories = new BuildingCategory[]{BuildingCategory.Housing, 
            BuildingCategory.Services, BuildingCategory.Industry, BuildingCategory.Food};
    }
}

