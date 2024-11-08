namespace KooliProjekt.Data
{
    public class LogEntry
    {
        public int Id { get; set; }
        public DateTime LogDate { get; set; }
        public string LogEntryDescription { get; set; }
        public string User { get; set; }
    }
}
