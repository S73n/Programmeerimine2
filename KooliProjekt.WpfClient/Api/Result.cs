namespace KooliProjekt.WpfClient.Api
{
    public class Result
    {
        public string Error { get; set; }

        public bool HasError => !string.IsNullOrEmpty(Error);
    }
} 