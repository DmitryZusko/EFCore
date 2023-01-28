namespace DataBaseModel.DTOModels
{
    public class CustomerDto : PropertyChangeNotifier
    {
        private int _id;
        private string _company;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Company
        {
            get => _company;
            set
            {
                _company = value;
                OnPropertyChanged(nameof(Company));
            }
        }
    }
}
