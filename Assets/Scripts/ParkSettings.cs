namespace ThemeparkQuiz
{
    public class ParkSettings
    {
        private bool parkEnabled;
        private bool coastersEnabled;
        private bool flatridesEnabled;
        private bool charactersEnabled;
        private bool eventsEnabled;
        private bool areasEnabled;
        private bool defunctEnabled;

        public ParkSettings(bool parkEnabled, bool coastersEnabled, bool flatridesEnabled, bool charactersEnabled, bool eventsEnabled,
            bool areasEnabled, bool defunctEnabled)
        {
            this.parkEnabled = parkEnabled;
            this.coastersEnabled = coastersEnabled;
            this.flatridesEnabled = flatridesEnabled;
            this.charactersEnabled = charactersEnabled;
            this.eventsEnabled = eventsEnabled;
            this.areasEnabled = areasEnabled;
            this.defunctEnabled = defunctEnabled;
        }

        public bool ParkEnabled
        {
            get => parkEnabled;
            set => parkEnabled = value;
        }

        public bool CoastersEnabled
        {
            get => coastersEnabled;
            set => coastersEnabled = value;
        }

        public bool FlatridesEnabled
        {
            get => flatridesEnabled;
            set => flatridesEnabled = value;
        }

        public bool CharactersEnabled
        {
            get => charactersEnabled;
            set => charactersEnabled = value;
        }

        public bool EventsEnabled
        {
            get => eventsEnabled;
            set => eventsEnabled = value;
        }

        public bool AreasEnabled
        {
            get => areasEnabled;
            set => areasEnabled = value;
        }

        public bool DefunctEnabled
        {
            get => defunctEnabled;
            set => defunctEnabled = value;
        }
    }
}