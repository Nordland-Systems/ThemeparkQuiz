namespace ThemeparkQuiz
{
    public enum WordTypes
    {
        Park,
        Coaster,
        Trackride,
        Waterride,
        Flatride,
        Walkthrough,
        Character,
        Event,
        Show,
        Area,
        ChangedName,
        Defunct,
        Restaurant,
        Other
    }

    public static class WordTypesExtension
    {
        public static string GetNameSingular(this WordTypes wordTypes)
        {
            switch (wordTypes)
            {
                default:
                    return "ERROR";
                case WordTypes.Coaster:
                    return "Achterbahn";
                case WordTypes.Trackride:
                    return "Schienenfahrt";
                case WordTypes.Waterride:
                    return "Wasserbahn";
                case WordTypes.Flatride:
                    return "Flatride";
                case WordTypes.Walkthrough:
                    return "Walkthrough";
                case WordTypes.Area:
                    return "Themenbereich";
                case WordTypes.Character:
                    return "Charakter";
                case WordTypes.Event:
                    return "Event";
                case WordTypes.Park:
                    return "Park";
                case WordTypes.ChangedName:
                    return "Geänderter Name";
                case WordTypes.Defunct:
                    return "Ehemalig";
                case WordTypes.Show:
                    return "Show";
                case WordTypes.Restaurant:
                    return "Restaurant";
                case WordTypes.Other:
                    return "Anderes";
            }
        }
        
        public static string GetNamePlural(this WordTypes wordTypes)
        {
            switch (wordTypes)
            {
                default:
                    return "ERROR";
                case WordTypes.Coaster:
                    return "Achterbahnen";
                case WordTypes.Trackride:
                    return "Schienenfahrten";
                case WordTypes.Waterride:
                    return "Wasserbahnen";
                case WordTypes.Flatride:
                    return "Flatrides";
                case WordTypes.Walkthrough:
                    return "Walkthroughs";
                case WordTypes.Area:
                    return "Themenbereiche";
                case WordTypes.Character:
                    return "Charaktere";
                case WordTypes.Event:
                    return "Events";
                case WordTypes.Park:
                    return "Parks";
                case WordTypes.ChangedName:
                    return "Geänderte Namen";
                case WordTypes.Defunct:
                    return "Ehemalige";
                case WordTypes.Show:
                    return "Shows";
                case WordTypes.Restaurant:
                    return "Restaurants";
                case WordTypes.Other:
                    return "Andere";
            }
        }
    }
}