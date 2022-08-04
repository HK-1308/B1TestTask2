namespace B1TestTask2.Data.ViewModels
{
    public class ShowFileViewModel
    {
        public List<Record> Records { get; set; } = new List<Record>();

    }

    public class Record
    {
        public string Id { get; set; }

        public decimal IncomingActive { get; set; }

        public decimal IncomingPassive { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        public decimal OutgoingActive { get; set; }

        public decimal OutgoingPassive { get; set; }

        public string ClassName { get; set; }
    }
}
