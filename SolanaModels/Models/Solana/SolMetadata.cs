namespace Models.Models.Solana
{
    public class SolMetadata
    {
        public string Name { get; set; } = string.Empty;

        public string Ticker { get; set; } = string.Empty;

        public string ImageLink { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsMutable { get; set; }

        public bool MintAuthorityRevoked { get; set; }

        public bool FreezeAuthorityRevoked { get; set; }

        public List<Social> Socials { get; set; } = new List<Social>();

        public bool HasProfileOnDexScreener { get; set; }
    }

    public class Social
    {
        public string Label { get; set; } = string.Empty;

        public string Link { get; set; } = string.Empty;
    }
}