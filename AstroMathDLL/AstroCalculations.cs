using System;

namespace AstroMathDLL
{
    public class AstroCalculations
    {
        private const double SpeedOfLight = 3.0e8;
        private const double ParsecToLightYears = 3.26;
        private const double ParsecToKm = 3.086e13;
        private const double GravitationalConstant = 6.67e-11;

        public double CalculateStarVelocity(double observedWavelength, double restWavelength)
        {
            if (restWavelength == 0)
                throw new ArgumentException("Rest wavelength cannot be zero.");

            return SpeedOfLight * ((observedWavelength - restWavelength) / restWavelength);
        }

        public double CalculateDistanceParsecs(double parallaxArcsec)
        {
            if (parallaxArcsec <= 0)
                throw new ArgumentException("Parallax angle must be greater than zero.");

            return 1.0 / parallaxArcsec;
        }

        public double CalculateDistanceLightYears(double parallaxArcsec)
        {
            double parsecs = CalculateDistanceParsecs(parallaxArcsec);
            return parsecs * ParsecToLightYears;
        }

        public double CalculateDistanceKm(double parallaxArcsec)
        {
            double parsecs = CalculateDistanceParsecs(parallaxArcsec);
            return parsecs * ParsecToKm;
        }

        public double ConvertCelsiusToKelvin(double celsius)
        {
            return celsius + 273.15;
        }

        public double CalculateSchwarzschildRadius(double mass)
        {
            if (mass <= 0)
                throw new ArgumentException("Mass must be greater than zero.");

            return (2 * GravitationalConstant * mass) / (SpeedOfLight * SpeedOfLight);
        }
    }
}