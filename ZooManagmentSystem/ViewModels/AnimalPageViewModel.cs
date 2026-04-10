using ZooManagmentSystem.Models;

namespace ZooManagmentSystem.ViewModels
{
    public class AnimalPageViewModel
    {
        public AnimalModel NewAnimal { get; set; }
        public IEnumerable<AnimalModel> Animals { get; set; }
    }
}
