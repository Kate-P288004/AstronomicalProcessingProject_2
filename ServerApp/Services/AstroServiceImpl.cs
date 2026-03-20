using AstroMathDLL;
using AstroProto;
using Grpc.Core;

namespace ServerApp.Services
{
    public class AstroServiceImpl : AstroService.AstroServiceBase
    {
        private readonly AstroCalculations _astro = new AstroCalculations();

        public override Task<VelocityReply> CalculateVelocity(VelocityRequest request, ServerCallContext context)
        {
            double result = _astro.CalculateStarVelocity(request.ObservedWavelength, request.RestWavelength);

            return Task.FromResult(new VelocityReply
            {
                Velocity = result
            });
        }

        public override Task<DistanceReply> CalculateDistance(DistanceRequest request, ServerCallContext context)
        {
            double parsecs = _astro.CalculateDistanceParsecs(request.Parallax);
            double lightYears = _astro.CalculateDistanceLightYears(request.Parallax);
            double km = _astro.CalculateDistanceKm(request.Parallax);

            return Task.FromResult(new DistanceReply
            {
                Parsecs = parsecs,
                LightYears = lightYears,
                Km = km
            });
        }

        public override Task<TemperatureReply> ConvertTemperature(TemperatureRequest request, ServerCallContext context)
        {
            double kelvin = _astro.ConvertCelsiusToKelvin(request.Celsius);

            return Task.FromResult(new TemperatureReply
            {
                Kelvin = kelvin
            });
        }

        public override Task<RadiusReply> CalculateRadius(RadiusRequest request, ServerCallContext context)
        {
            double radius = _astro.CalculateSchwarzschildRadius(request.Mass);

            return Task.FromResult(new RadiusReply
            {
                Radius = radius
            });
        }
    }
}