
using System;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace DeepNestLib
{
    public class DeepNestRunner
    {
        public NestingContext Context = new NestingContext(1, 0);


        public delegate void IterationCompleteHandler(int iteration);

        public event IterationCompleteHandler IterationComplete;

        public void Run(int iterations)
        {
            Background.UseParallel = true;
            SvgNest.Config.placementType = PlacementTypeEnum.gravity;
           
            Context.StartNest();
            
            for (int i = 0; i < iterations; i++)
            {
                Context.NestIterate();

                if (IterationComplete != null)
                {
                    IterationComplete.Invoke(i);
                }
            }       
        }
    }
}
