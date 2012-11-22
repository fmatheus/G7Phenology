using System;
using System.Collections.Generic;
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
    public class PhenologyDataContext
    {
        static PhenologyDataContext instance;

        public int Date = 0;
        public int mockId = 119;
        public Specimen[] Specimens;

        public PhenologyDataContext()
        {
            Specimens = new Specimen[mockId + 6];
        }
        public static PhenologyDataContext Singleton()
        {
            if (instance == null)
                instance = new PhenologyDataContext();
            return instance;
        }

        public Specimen selected()
        {
            if (mockId >= Specimens.Length || mockId < 0)
                finished();
            if (Specimens[mockId] == null)
                Specimens[mockId] = new Specimen { Id = mockId };
            return Specimens[mockId];
        }
        public Specimen next()
        {
            ++mockId;
            return selected();
        }
        public Specimen prev()
        {
            --mockId;
            return selected();
        }
        private void finished()
        {
            Date++;
            mockId = 120;
        }
    }
}
