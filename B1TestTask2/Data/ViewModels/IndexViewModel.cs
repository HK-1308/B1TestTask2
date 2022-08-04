using B1TestTask2.Data.Models;

namespace B1TestTask2.Data.ViewModels
{
    public class IndexViewModel
    {
        public IFormFile? File { get; set; }

        public List<FileXlsx> Files { get; set; }
    }
}
