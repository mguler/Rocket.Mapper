namespace Rocket.Mapper.Abstract
{
    /// <summary>
    /// abstract definition for mapping configuration class
    /// </summary>
    public interface IMappingConfiguration
    {
        /// <summary>
        /// this method adds configuration to the given service provider instance 
        /// </summary>
        /// <param name="mappingServiceProvider">mapping service</param>
        void Configure(IMappingServiceProvider mappingServiceProvider);
    }
}
