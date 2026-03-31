using AstroMathDLL;
using AstroProto;
using Grpc.Core;
using System.Threading.Tasks;

namespace ServerApp.Services
{
    // ============================================================
    // AstroService Implementation
    // This class implements the gRPC service contract defined in
    // astro.proto and provides access to all astronomical methods.
    // ============================================================
    public class AstroServiceImpl : AstroService.AstroServiceBase
    {
        // Instance of third-party DLL class (AstroMath.DLL)
        private readonly AstroCalculations _astro = new AstroCalculations();

        // ============================================================
        // Star Velocity Service Method
        // Receives observed and rest wavelength from client,
        // calls DLL method, and returns velocity result.
        // ============================================================
        public override Task<VelocityReply> CalculateVelocity(VelocityRequest request, ServerCallContext context)
        {
            // Call DLL method
            double result = _astro.CalculateStarVelocity(
                request.ObservedWavelength,
                request.RestWavelength
            );

            // Return result to client
            return Task.FromResult(new VelocityReply
            {
                Velocity = result
            });
        }

        // ============================================================
        // Star Distance Service Method
        // Calculates distance in parsecs, light years and kilometres
        // using DLL methods and returns all values to client.
        // ============================================================
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

        // ============================================================
        // Temperature Conversion Service Method
        // Converts Celsius to Kelvin using DLL method and returns result.
        // ============================================================
        public override Task<TemperatureReply> ConvertTemperature(TemperatureRequest request, ServerCallContext context)
        {
            double kelvin = _astro.ConvertCelsiusToKelvin(request.Celsius);

            return Task.FromResult(new TemperatureReply
            {
                Kelvin = kelvin
            });
        }

        // ============================================================
        // Black Hole Event Horizon Service Method
        // Calculates Schwarzschild radius using DLL method and returns result.
        // ============================================================
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