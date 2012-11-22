using System;
using System.Collections;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace G7Phenology
{
    public class Specimen
    {
        private static readonly String[] mockSpecies = { "Phalaenopsis hieroglyphica", "Ophrys tenthredinifera", "Angraecum sesquipedale", "Paphiopedilum concolor" };
        private static readonly String[] mockNotes = { "Ao lado da ", "", "Atras da ", "Em baixo da " };

        public int Id;
        public String Sector
        {
            get { return "BS" + (Id / 4); }
        }
        public String Name
        {
            get { return mockSpecies[Id % 4]; }
        }
        public String[] Pictures
        {
            get { return new String[] {"Images/" + (Id % 4 + 1) + ".jpg"}; }
        }
        public String Comment
        {
            get { string note = mockNotes[Id % 4]; return String.IsNullOrEmpty(note) ? null : (note + (Id - 1)); }
        }

        public int[][] Phenologies =
        {
            new int[] {0,0,0,0,0,0},
            new int[] {0,0,0,0,0,0},
            new int[] {0,0,0,0,0,0},
            new int[] {0,0,0,0,0,0}
        };
        public void insertPhenology(int date, int[] phenology) {
            Phenologies[date] = phenology;
        }
        public void updatePhenology(int[] phenology) {
            Phenologies[PhenologyDataContext.Singleton().Date] = phenology;
        }
        public int[] getPhenology() {
            return Phenologies[PhenologyDataContext.Singleton().Date];
        }
    }
}
