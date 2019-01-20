using DPA_Musicsheets.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Factories
{
	class FileLoaderFactory
	{
		Dictionary<string, IFileLoader> fileloaders = new Dictionary<string, IFileLoader>();

		public void LoadFactory(string key, IFileLoader loader)
		{
			fileloaders.Add(key, loader);
		}

		public IFileLoader GetLoader(String type)
        {
            if (fileloaders.ContainsKey(type))
			{

				IFileLoader loader = fileloaders[type];

				return (IFileLoader)Activator.CreateInstance(loader.GetType());

			}
            return null;
        }

	}
}
