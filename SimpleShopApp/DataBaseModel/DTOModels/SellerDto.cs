namespace DataBaseModel.DTOModels
{
    public class SellerDto : PropertyChangeNotifier
    {
        private int _id;
        private string _fullName;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }
    }
}
