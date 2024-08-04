using RimWorld;
using Verse;

namespace RoomDisinfector_v2
{
    public class CompProperties_PowerCleanliness : CompProperties
    {
        public float secondaryCleanliness;

        public CompProperties_PowerCleanliness()
        {
            this.compClass = typeof(CompPowerCleanliness);
        }
    }

    public class CompPowerCleanliness : ThingComp
    {
        public CompProperties_PowerCleanliness Props => (CompProperties_PowerCleanliness)props;
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
                    var cleanlinessComp = building.TryGetComp<CompPowerCleanliness>();
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
                    var cleanlinessComp = building.TryGetComp<CompPowerCleanliness>();
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
