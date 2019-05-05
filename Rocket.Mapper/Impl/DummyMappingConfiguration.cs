using Rocket.Mapper.Abstract;

namespace Rocket.Mapper.Impl
{
    /// <summary>
    /// Dummy implementation of IMappingConfigurationService , this class also implements Visitor pattern
    /// </summary>
    public class DummyMappingConfiguration : IMappingConfiguration
    {
        /// <summary> 
        /// This method configures mappings
        /// </summary>
        /// <param name="mappingServiceProvider">instance of object</param>
        public void Configure(IMappingServiceProvider mappingServiceProvider)
        {
            mappingServiceProvider.Register<object, object>(source => source);
        }
    }
}
