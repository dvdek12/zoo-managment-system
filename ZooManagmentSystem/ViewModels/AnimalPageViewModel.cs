using ZooManagmentSystem.Models.Animal;

namespace ZooManagmentSystem.ViewModels
{
    public class AnimalPageViewModel
    {
        public AnimalModel NewAnimal { get; set; }
        public IEnumerable<AnimalModel> Animals { get; set; }
    }
}
