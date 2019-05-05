using Rocket.Mapper.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rocket.Mapper.Impl
{
    /// <summary>
    /// Mapping service implementation
    /// </summary>
    public class MappingService : IMappingServiceProvider
    {
        /// <summary>
        /// Default mapper 
        /// </summary>
        public Func<object, object> DefaultMapper { get => throw new NotImplementedException("Not yet implemented"); }

        /// <summary>
        /// Contains registered mappings for specific types
        /// </summary>
        private readonly IList<Delegate> _mappingsCache = new List<Delegate>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public MappingService()
        {
                
        }

        /// <summary>
        /// contstructor for injecting predefined mapping configurations.  
        /// </summary>
        /// <param name="mappingConfigurations">An array of mapping configuration</param>
        public MappingService(IMappingConfiguration[] mappingConfigurations)
        {
            foreach (var mappingConfiguration in mappingConfigurations)
            {
                mappingConfiguration.Configure(this);
            }
        }

        /// <summary>
        /// This method creates an instance of the type given in the TTarget parameter and maps source object's properties to the target instance
        /// </summary>
        /// <typeparam name="TTarget">the target type</typeparam>
        /// <param name="source">object instance to be mapped to target type</param>
        /// <returns>an instance of target type</returns>
        public TTarget Map<TTarget>(object source)
        {
            var targetType = typeof(TTarget);
            var result = Map(source, targetType);
            return (TTarget)result;
        }
        /// <summary>
        /// This method creates an instance of the type given in the targetType parameter and maps source object's properties to the target instance
        /// </summary>
        /// <param name="source">source object</param>
        /// <param name="targetType">type of target instance</param>
        /// <returns>instance of the target type</returns>
        public object Map(object source, Type targetType)
        {
            if (source == null)
            {
                throw new ArgumentException($"Source object cannot be null");
            }

            var sourceType = source.GetType();
            var mapper = _mappingsCache.FirstOrDefault(mapping => mapping.Method.ReturnType == targetType &&
                                                                 mapping.Method.GetParameters().FirstOrDefault()
                                                                     ?.ParameterType == sourceType);

            if (mapper == null)
            {
                throw new Exception($"Mapping configuration for {sourceType} to {targetType.Name} does not exists");
            }

            var result = mapper.DynamicInvoke(source);
            return result;
        }

        /// <summary>
        /// Registers mapping configurations for specific types
        /// </summary>
        /// <typeparam name="TSource">source type</typeparam>
        /// <typeparam name="TTarget">target type</typeparam>
        /// <param name="func">the method which supplies mapping service between the types given in the type parameters</param>
        public void Register<TSource, TTarget>(Func<TSource, TTarget> func)
        {
            var alreadyExists = _mappingsCache.Any(mapping => mapping.GetType() == func.GetType());
            if (alreadyExists)
            {
                throw new Exception($"Mapping configuration for {typeof(TSource)} to {typeof(TTarget)} has already been defined before");
            }
            _mappingsCache.Add(func);
        }
    }
}
