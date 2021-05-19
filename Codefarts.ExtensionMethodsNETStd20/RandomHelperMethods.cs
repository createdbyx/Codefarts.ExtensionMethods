namespace System
{
    /// <summary>
    /// Provides various helper methods for the <see cref="Random"/> class.
    /// </summary>
    public static class RandomHelperMethods
    {
        ///// <summary>
        ///// Gets a random position inside a cylinder 1 unit high and 1 unit in radius.
        ///// </summary>
        ///// <remarks>The random point will be centered around 0.</remarks>
        // public static Vector3 GetInsideUnitCylinder(this Random rnd)
        // {
        //    return InsideUnitCylinder;
        // }

        ///// <summary>
        ///// Gets a random position inside a cylinder.
        ///// </summary>
        ///// <param name="rnd">The random.</param>
        ///// <param name="radius">The radius of the cylinder.</param>
        ///// <param name="height">The height of the cylinder.</param>
        ///// <returns>A new <see cref="Vector3"/>.</returns>
        ///// <remarks>
        ///// The random point will be centered around 0.
        ///// </remarks>
        // public static Vector3 GetInsideCylinder(this Random rnd, float radius, float height)
        // {
        //    var cylindar = InsideUnitCylinder;
        //    return new Vector3(cylindar.x * radius, cylindar.y * height, cylindar.z * radius);
        // }

        /// <summary>
        /// Gets a random position inside a 1x1x1 unit cube.
        /// </summary>
        /// <param name="rnd">The random.</param>
        /// <returns>A new <see cref="Vector3"/>.</returns>
        /// <remarks>The random point will be centered around 0.</remarks>
        public static void GetInsideUnitCube(this Random rnd, out float sizeX, out float sizeY, out float sizeZ)
        {
            sizeX = (float)(rnd.NextDouble() - 0.5f);
            sizeY = (float)(rnd.NextDouble() - 0.5f);
            sizeZ = (float)(rnd.NextDouble() - 0.5f);
        }

        /// <summary>
        /// Gets a random position inside a 1x1x1 unit cube.
        /// </summary>
        /// <param name="rnd">The random.</param>
        /// <param name="size">The size of the cube.</param>
        /// <returns>A new <see cref="Vector3"/>.</returns>
        /// <remarks>
        /// The random point will be centered around 0.
        /// </remarks>
        public static void GetInsideCube(this Random rnd, float sizeX, float sizeY, float sizeZ, out float positionX, out float positionY, out float positionZ)
        {
            var cubeX = (float)(rnd.NextDouble() - 0.5f);
            var cubeY = (float)(rnd.NextDouble() - 0.5f);
            var cubeZ = (float)(rnd.NextDouble() - 0.5f);

            positionX = cubeX * sizeX;
            positionY = cubeY * sizeY;
            positionZ = cubeZ * sizeZ;
        }
    }
}