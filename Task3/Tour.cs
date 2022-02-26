using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Task3
{
    public class Tour
    {
        private TourType _type;
        private List<Vehicle> _vehicls;
        private Int16 _meals;
        private Int16 _days;

        public Tour(TourType type, List<Vehicle> _vehicls, short meals, short days)
        {
            _type = type;
            _vehicls = vehicls;
            _meals = meals;
            _days = days;
        }

        public TourType Type => _type;

        public List<Vehicle> Vehicls => _vehicls;

        public short Meals => _meals;

        public short Days => _days;
    }
}