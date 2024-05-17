using SoftwareSearch.Data;

namespace SoftwareSearch.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Software> Softwares {get;set;} = new List<Software>();
    }
}
