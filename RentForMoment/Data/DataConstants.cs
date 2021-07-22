namespace RentForMoment.Data
{
     public class DataConstants
    {
        public class PersonProfileConstraint
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 20;
            public const int DescriptionMinLength = 15;
            public const int DescriptionMaxLength = 300;
            public const int MinYearsOld = 18;
            public const int MaxYearsOld = 88;
            public const int MinimumSkillsLength = 8;
            public const int MaximumSkillsLength = 88;
            public const int MinimumCityLength = 4;
            public const int MaximumCityLength = 100;
        }
        

        public class CategoryConstraint
        {
            public const int MaxNameLength = 20;
        }

        public class ChiefConstraint
        {
            public const int MaxLengthName = 25;
            public const int MaxPhoneLength = 30;
        }
    }
}
