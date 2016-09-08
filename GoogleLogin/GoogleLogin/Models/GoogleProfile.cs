namespace GoogleLogin.Models
{

    public class GoogleProfile
    {
        public string Kind { get; set; }
        public string Etag { get; set; }
        public string Occupation { get; set; }
        public string Gender { get; set; }
        public string ObjectType { get; set; }
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public Name Name { get; set; }
        public string Tagline { get; set; }
        public string Url { get; set; }
        public Image Image { get; set; }
        public Organization[] Organizations { get; set; }
        public Placeslived[] PlacesLived { get; set; }
        public bool IsPlusUser { get; set; }
        public int CircledByCount { get; set; }
        public bool Verified { get; set; }
        public Cover Cover { get; set; }
    }

    public class Name
    {
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
    }

    public class Image
    {
        public string Url { get; set; }
        public bool IsDefault { get; set; }
    }

    public class Cover
    {
        public string Layout { get; set; }
        public Coverphoto CoverPhoto { get; set; }
        public Coverinfo CoverInfo { get; set; }
    }

    public class Coverphoto
    {
        public string Url { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }

    public class Coverinfo
    {
        public int TopImageOffset { get; set; }
        public int LeftImageOffset { get; set; }
    }

    public class Organization
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool Primary { get; set; }
    }

    public class Placeslived
    {
        public string Value { get; set; }
        public bool Primary { get; set; }
    }
}