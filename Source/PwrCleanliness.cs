using RimWorld;
using Verse;

namespace RoomDisinfector_v2
{
    public class CompProperties_PoweredCleanliness : CompProperties
    {
        public float secondaryCleanliness;

        public CompProperties_PoweredCleanliness()
        {
            this.compClass = typeof(CompPoweredCleanliness);
        }
    }

    public class CompPoweredCleanliness : ThingComp
    {
        public CompProperties_PoweredCleanliness Props => (CompProperties_PoweredCleanliness)props;
    }

    public class StatPart_PwrCleanliness : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Building building)
            {
                var powerComp = building.TryGetComp<CompPowerTrader>();
                if (powerComp != null && powerComp.PowerOn)
                {
                    var cleanlinessComp = building.TryGetComp<CompPoweredCleanliness>();
                    if (cleanlinessComp != null)
                    {
                        val += cleanlinessComp.Props.secondaryCleanliness;
                    }
                }
            }
        }

        public override string ExplanationPart(StatRequest req)
        {
            if (req.HasThing && req.Thing is Building building)
            {
                var powerComp = building.TryGetComp<CompPowerTrader>();
                if (powerComp != null && powerComp.PowerOn)
                {
                    var cleanlinessComp = building.TryGetComp<CompPoweredCleanliness>();
                    if (cleanlinessComp != null)
                    {
                        return $"Powered cleanliness bonus: +{cleanlinessComp.Props.secondaryCleanliness}";
                    }
                }
            }
            return null;
        }
    }
}
