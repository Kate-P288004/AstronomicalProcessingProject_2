using System;

namespace AstroMathDLL
{
    // ============================================================
    // AstroCalculations Class
    // Contains all required astronomical formulas:
    // 1. Star Velocity (Doppler Effect)
    // 2. Star Distance (Parallax)
    // 3. Temperature Conversion (Celsius to Kelvin)
    // 4. Black Hole Event Horizon (Schwarzschild Radius)
    // ============================================================
    public class AstroCalculations
    {
        // ============================================================
        // Constants used in calculations
        // ============================================================

        // Speed of light in m/s (assessment value)
        private const double SpeedOfLight = 299792458;

        // Conversion constants
        private const double ParsecToLightYears = 3.26156;
        private const double ParsecToKm = 3.0857e13;

        // Gravitational constant (assessment value)
        private const double GravitationalConstant = 6.674e-11;

        // ============================================================
        // Star Velocity Calculation (Doppler Effect)
        // ============================================================
        /// <summary>
        /// Calculates the velocity of a star using Doppler shift.
        /// </summary>
        /// <param name="observedWavelength">Observed wavelength (nm)</param>
        /// <param name="restWavelength">Rest wavelength (nm)</param>
        /// <returns>Velocity in metres per second</returns>
        public double CalculateStarVelocity(double observedWavelength, double restWavelength)
        {
            if (restWavelength == 0)
                throw new ArgumentException("Rest wavelength cannot be zero.");

            // V = ((observed - rest) / rest) * c
            return SpeedOfLight * ((observedWavelength - restWavelength) / restWavelength);
        }

        // ============================================================
        // Star Distance Calculation (Parallax)
        // ============================================================
        /// <summary>
        /// Calculates distance to a star in parsecs using parallax angle.
        /// </summary>
        /// <param name="parallaxArcsec">Parallax angle in arcseconds</param>
        /// <returns>Distance in parsecs</returns>
        public double CalculateDistanceParsecs(double parallaxArcsec)
        {
            if (parallaxArcsec <= 0)
                throw new ArgumentException("Parallax angle must be greater than zero.");

            // Distance = 1 / parallax
            return 1.0 / parallaxArcsec;
        }

        /// <summary>
        /// Converts star distance to light years.
        /// </summary>
        /// <param name="parallaxArcsec">Parallax angle</param>
        /// <returns>Distance in light years</returns>
        public double CalculateDistanceLightYears(double parallaxArcsec)
        {
            double parsecs = CalculateDistanceParsecs(parallaxArcsec);
            return parsecs * ParsecToLightYears;
        }

        /// <summary>
        /// Converts star distance to kilometres.
        /// </summary>
        /// <param name="parallaxArcsec">Parallax angle</param>
        /// <returns>Distance in kilometres</returns>
        public double CalculateDistanceKm(double parallaxArcsec)
        {
            double parsecs = CalculateDistanceParsecs(parallaxArcsec);
            return parsecs * ParsecToKm;
        }

        // ============================================================
        // Temperature Conversion
        // ============================================================
        /// <summary>
        /// Converts temperature from Celsius to Kelvin.
        /// </summary>
        /// <param name="celsius">Temperature in Celsius</param>
        /// <returns>Temperature in Kelvin</returns>
        public double ConvertCelsiusToKelvin(double celsius)
        {
            if (celsius < -273.15)
                throw new ArgumentException("Temperature cannot be below absolute zero.");

            // K = C + 273.15
            return celsius + 273.15;
        }

        // ============================================================
        // Black Hole Event Horizon (Schwarzschild Radius)
        // ============================================================
        /// <summary>
        /// Calculates the Schwarzschild radius of a black hole.
        /// </summary>
        /// <param name="mass">Mass of black hole (kg)</param>
        /// <returns>Radius in metres</returns>
        public double CalculateSchwarzschildRadius(double mass)
        {
            if (mass <= 0)
                throw new ArgumentException("Mass must be greater than zero.");

            // R = (2GM) / c^2
            return (2 * GravitationalConstant * mass) / (SpeedOfLight * SpeedOfLight);
        }
    }
}