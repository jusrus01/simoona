﻿namespace TemporaryDataLayer
{
    public class KudosShopItem : BaseModelWithOrg
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public string PictureId { get; set; }
    }
}