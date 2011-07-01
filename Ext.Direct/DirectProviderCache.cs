using System;
using System.Collections.Generic;

namespace Ext.Direct
{

    /// <summary>
    /// Simple cache for Ext.Direct providers
    /// </summary>
    public class DirectProviderCache
    {
        #region Static members
        /// <summary>
        /// Our singleton instance.
        /// </summary>
        private static readonly DirectProviderCache _Instance = new DirectProviderCache();

        /// <summary>
        /// Our cache of providers.
        /// </summary>
        private static Dictionary<string, DirectProvider> _Providers = new Dictionary<string, DirectProvider>();

        /// <summary>
        /// Our locking object.
        /// </summary>
        private static object _SyncLock = new object();
        #endregion

        #region Static constructor
        /// <summary>
        /// Static constructor prevent class being marked with BeforeFieldInit.
        /// </summary>
        static DirectProviderCache()
        {
        }
        #endregion

        #region Static properties
        /// <summary>
        /// Gets the singleton instance of this object.
        /// </summary>
        /// <returns>The DirectProviderCache instance.</returns>
        public static DirectProviderCache GetInstance()
        {
            return _Instance;
        }
        #endregion

        #region Indexers
        /// <summary>
        /// Allows providers to be retrieved from the cache.
        /// </summary>
        /// <param name="providerName">The name of the provider to retrieve.</param>
        /// <returns>The DirectProvider, or null if not found.</returns>
        public DirectProvider this[string providerName]
        {
            get
            {
                if (this.ContainsProvider(providerName))
                {
                    return _Providers[providerName];
                }

                return null;
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Determines whether this cache contains the specified provider.
        /// </summary>
        /// <param name="providerName">The name of the provider to look for.</param>
        /// <returns><b>true</b> if our cache contains the provider, <b>false</b> otherwise.</returns>
        public bool ContainsProvider(string providerName)
        {
            return _Providers.ContainsKey(providerName);
        }

        /// <summary>
        /// Adds a provider to the cache.
        /// </summary>
        /// <param name="provider"></param>
        public void Add(DirectProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            // Double lock checking...
            if (!this.ContainsProvider(provider.Name))
            {
                lock (_SyncLock)
                {
                    if (!this.ContainsProvider(provider.Name))
                    {
                        _Providers.Add(provider.Name, provider);
                    }
                }
            }
        }
        #endregion
    }
}
